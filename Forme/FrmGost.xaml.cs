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
    /// Interaction logic for FrmGost.xaml
    /// </summary>
    public partial class FrmGost : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        public bool azuriraj;
        DataRowView pomocniRed;
        public FrmGost()
        {
            InitializeComponent();
            txtIme.Focus();
            konekcija = kon.KreirajKonekciju();
        }
        public FrmGost(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            txtIme.Focus();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
            konekcija = kon.KreirajKonekciju();
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
                cmd.Parameters.Add("@Ime", SqlDbType.NVarChar).Value = txtIme.Text;
                cmd.Parameters.Add("@Prezime", SqlDbType.NVarChar).Value = txtPrezime.Text;
                cmd.Parameters.Add("@Jmbg", SqlDbType.NVarChar).Value = txtJmbg.Text;
                cmd.Parameters.Add("@Kontakt", SqlDbType.NVarChar).Value = txtKontakt.Text;

                if (txtJmbg.Text.Length != 13 || !txtJmbg.Text.All(char.IsDigit))
                {
                    MessageBox.Show("JMBG mora sadržati tačno 13 cifara i biti numerički!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (Jmbg(txtJmbg.Text))
                {
                    MessageBox.Show("JMBG već postoji u bazi podataka. Molimo unesite jedinstven JMBG.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!txtKontakt.Text.StartsWith("+381"))
                {
                    MessageBox.Show("Kontakt mora početi sa +381!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


                if (azuriraj)
                {
                    DataRowView red = pomocniRed;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"update tblGost
                                        set Ime=@Ime, Prezime=@Prezime, Jmbg= @Jmbg, Kontakt=@Kontakt 
                                        where GostID = @id";
                    pomocniRed = null;
                }
                else {
                cmd.CommandText = @"insert into tblGost(Ime, Prezime, Jmbg, Kontakt)
                                    values(@Ime, @Prezime, @Jmbg, @Kontakt)";
                
                }
                
                
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Unos odredjenih vrednosti nije validan", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            { 
                if(konekcija != null)
                {
                    konekcija.Close();
                }
            }
            
        }
        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private bool Jmbg(string jmbg)
        {
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM tblRadnik WHERE Jmbg = @Jmbg AND RadnikID <> @CurrentRadnikID", konekcija);
            cmd.Parameters.AddWithValue("@Jmbg", jmbg);

            if (azuriraj)
            {
                DataRowView red = pomocniRed;
                cmd.Parameters.AddWithValue("@CurrentRadnikID", red["ID"]);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CurrentRadnikID", DBNull.Value);
            }

            int count = (int)cmd.ExecuteScalar();
            return count > 0;
        }
    }
}
