using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
    /// Interaction logic for FrmSoba.xaml
    /// </summary>
    public partial class FrmSoba : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        private bool azuriraj;
        private DataRowView red;


        public FrmSoba()
        {
            InitializeComponent();
            PopuniPadajuceListe();
            txtBrojSobe.Focus();
        }
        public FrmSoba(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            PopuniPadajuceListe();
            txtBrojSobe.Focus();
            this.azuriraj = azuriraj;
            this.red = red;
            
        }
        private void PopuniPadajuceListe()
        {
            try
            {
                konekcija = kon.KreirajKonekciju();
                konekcija.Open();

                string vratiVrstuSobe = @"select VrstaSobeID, Naziv 
                                            From tblVrstaSobe";
                DataTable dtVrstaSobe = new DataTable();
                SqlDataAdapter daVrstaSobe = new SqlDataAdapter(vratiVrstuSobe, konekcija);
                daVrstaSobe.Fill(dtVrstaSobe);
                cbVrstaSobe.ItemsSource = dtVrstaSobe.DefaultView;
                dtVrstaSobe.Dispose();
                daVrstaSobe.Dispose();


                string vratiRezervaciju = @"select RezervacijaID, BrojDana as Dani
                                            From tblRezervacija";
                DataTable dtRezervacija = new DataTable();
                SqlDataAdapter daRezervacija = new SqlDataAdapter(vratiRezervaciju, konekcija);
                daRezervacija.Fill(dtRezervacija);
                cbRezervacija.ItemsSource = dtRezervacija.DefaultView;
                dtRezervacija.Dispose();
                daRezervacija.Dispose();

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
                cmd.Parameters.Add("@BrojSobe", SqlDbType.NChar).Value = txtBrojSobe.Text;
                cmd.Parameters.Add("@Sprat", SqlDbType.Int).Value = txtSprat.Text;
                cmd.Parameters.Add("@VrstaSobeID", SqlDbType.Int).Value = cbVrstaSobe.SelectedValue;
                cmd.Parameters.Add("@StatusSobe", SqlDbType.Bit).Value = Convert.ToInt32(cbStatusSobe.IsChecked);
                cmd.Parameters.Add("@RezervacijaID", SqlDbType.Int).Value = cbRezervacija.SelectedValue;
                if (BrojSobeAlreadyExists(txtBrojSobe.Text))
                {
                    MessageBox.Show("Već postoji soba sa istim brojem sobe. Molimo unesite jedinstven broj sobe.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (this.azuriraj)
                {
                    
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"update tblSoba
                                        set BrojSobe=@BrojSobe, Sprat=@Sprat, VrstaSobeID=@VrstaSobeID,
                                        StatusSobe=@StatusSobe, RezervacijaID=@RezervacijaID
                                        where SobaID=@id";
                     this.red = null;
                }
                else
                {
                    cmd.CommandText = @"insert into tblSoba(BrojSobe, Sprat, VrstaSobeID,
                                        StatusSobe, RezervacijaID
                                        values(@BrojSobe, @Sprat, @VrstaSobeID, 
                                        @StatusSobe, @RezervacijaID)";
                }
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Unos podataka nije validan!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
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
        private bool BrojSobeAlreadyExists(string brojSobe)
        {
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM tblSoba WHERE BrojSobe = @BrojSobe AND SobaID <> @CurrentSobaID", konekcija);
            cmd.Parameters.AddWithValue("@BrojSobe", brojSobe);

            // Dodajte trenutni ID sobe ako je u režimu ažuriranja
            if (azuriraj)
            {
                cmd.Parameters.AddWithValue("@CurrentSobaID", Convert.ToInt32(red["ID"]));
            }
            else
            {
                cmd.Parameters.AddWithValue("@CurrentSobaID", DBNull.Value);
            }

            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }
    }
}
