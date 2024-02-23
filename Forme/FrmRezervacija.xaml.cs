using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hotel.Forme
{
    /// <summary>
    /// Interaction logic for FrmRezervacija.xaml
    /// </summary>
    public partial class FrmRezervacija : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        private bool azuriraj;
        private DataRowView red;
        public FrmRezervacija()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
            txtCenaRezervacije.Focus();
            
        }
        public FrmRezervacija(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
            txtCenaRezervacije.Focus();
            this.azuriraj = azuriraj;
            this.red = red;
            
        }
        private void PopuniPadajuceListe()
        {
            try
            {
                ;
                konekcija.Open();

                string vratiGosta = @"select GostID, Ime +' '+Prezime as 'Gost'
                                        From tblGost";
               
                SqlDataAdapter daGost = new SqlDataAdapter(vratiGosta, konekcija);
                DataTable dtGost = new DataTable();
                daGost.Fill(dtGost);
                cbGost.ItemsSource = dtGost.DefaultView;
                dtGost.Dispose();
                daGost.Dispose();

                string vratiRadnika =@"select RadnikID, Kontakt as 'Kontakt'
                                       From tblRadnik";
                DataTable dtRadnik = new DataTable();
                SqlDataAdapter daRadnik = new SqlDataAdapter(vratiRadnika, konekcija);
                daRadnik.Fill(dtRadnik);
                cbRadnik.ItemsSource = dtRadnik.DefaultView;
                dtRadnik.Dispose();
                daRadnik.Dispose();

                string vratiRestoran = @"select RestoranID, RestoranUsluga as 'Restoran'
                                         From tblRestoran";
                DataTable dtRestoran = new DataTable();
                SqlDataAdapter daRestoran = new SqlDataAdapter(vratiRestoran, konekcija);
                daRestoran.Fill(dtRestoran);
                cbRestoran.ItemsSource = dtRestoran.DefaultView;
                dtRestoran.Dispose();
                daRestoran.Dispose();

                string vratiSpa = @"select SpaID, SpaUsluga as 'Spa' 
                                    From tblSpa";
                DataTable dtSpa = new DataTable();
                SqlDataAdapter daSpa = new SqlDataAdapter(vratiSpa, konekcija);
                daSpa.Fill(dtSpa);
                cbSpa.ItemsSource = dtSpa.DefaultView;
                dtSpa.Dispose();
                daSpa.Dispose();
            }
            catch (SqlException)
            {
                MessageBox.Show("Padajuće liste nisu popunjene.",
                    "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }
        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                konekcija.Open();
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                
                cmd.Parameters.Add("@GostID", SqlDbType.Int).Value = cbGost.SelectedValue ; 
                cmd.Parameters.Add("@RadnikID", SqlDbType.Int).Value= cbRadnik.SelectedValue ;
                cmd.Parameters.Add("@RestoranID", SqlDbType.Int).Value = cbRestoran.SelectedValue;
                cmd.Parameters.Add("@SpaID", SqlDbType.Int).Value = cbSpa.SelectedValue;
                cmd.Parameters.Add("@CenaRezervacije", SqlDbType.Money).Value = txtCenaRezervacije.Text;
                cmd.Parameters.Add("@BrojDana", SqlDbType.Int).Value= txtBrojDana.Text;
                if (azuriraj)
                {
                    
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"update tblRezervacija
                                        set GostID=@GostID, RadnikID=@RadnikID, RestoranID=@RestoranID,
                                        SpaID=@SpaID, CenaRezervacije=@CenaRezervacije, BrojDana=@BrojDana
                                        where RezervacijaID=@id";
                    red = null;
                }
                else
                {
                    cmd.CommandText = @"insert into tblRezervacija(GostID, RadnikID, RestoranID, 
                                        SpaID, CenaRezervacije, BrojDana)
                                        values(@GostID, @RadnikID, @RestoranID, 
                                        @SpaID, @CenaRezervacije, @BrojDana)";
                }
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                Close();
            }
            catch (SqlException)
            {

                MessageBox.Show("Unos nije validan", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
