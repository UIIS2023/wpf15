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
    /// Interaction logic for FrmParking.xaml
    /// </summary>
    public partial class FrmParking : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        private bool azuriraj;
        private DataRowView red;
        public FrmParking()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajucuListu();
            txtBrojMesta.Focus();
        }
        public FrmParking(bool azuriraj, DataRowView red)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajucuListu();
            txtBrojMesta.Focus();
            this.azuriraj = azuriraj;
            this.red = red;

        }

        private void PopuniPadajucuListu()
        {
            try
            {
                
                konekcija.Open();
                string vratiRezervaciju = @"select RezervacijaID, BrojDana as 'Zauzetost' from tblRezervacija";
         
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
                cmd.Parameters.Add("BrojMesta", SqlDbType.Int).Value = txtBrojMesta.Text;
                cmd.Parameters.Add("@RezervacijaID", SqlDbType.Int).Value = cbRezervacija.SelectedValue;
                if (ParkingExists(Convert.ToInt32(txtBrojMesta.Text), azuriraj ? Convert.ToInt32(red["ID"]) : 0))
                {
                    MessageBox.Show("Već postoji parking sa istim brojem mesta. Molimo unesite jedinstven broj mesta.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (azuriraj)
                {
                  
                    
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                        cmd.CommandText = @"update tblParking
                                        set BrojMesta=@BrojMesta, RezervacijaID=@RezervacijaID
                                        where ParkingID=@id";
                    red = null;
                    
                }
                else
                {
                    cmd.CommandText = @"insert into tblParking(BrojMesta, RezervacijaID)
                                        values(@BrojMesta, @RezervacijaID)";
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

        private bool ParkingExists(int brojMesta, int currentParkingID)
        {
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM tblParking WHERE BrojMesta = @BrojMesta AND ParkingID <> @CurrentParkingID", konekcija);
            cmd.Parameters.AddWithValue("@BrojMesta", brojMesta);

            // Dodajte trenutni ID parkinga ako je u režimu ažuriranja
            if (azuriraj)
            {
                cmd.Parameters.AddWithValue("@CurrentParkingID", currentParkingID);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CurrentParkingID", DBNull.Value);
            }

            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }
    }
}
