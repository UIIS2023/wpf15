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
    /// Interaction logic for FrmRestoran.xaml
    /// </summary>
    public partial class FrmRestoran : Window
    {
        SqlConnection konekcija = new SqlConnection();
        Konekcija kon = new Konekcija();
        bool azuriraj;
        DataRowView pomocniRed;

        public FrmRestoran()
        {
            InitializeComponent();
            txtRestoranUsluga.Focus();
            konekcija = kon.KreirajKonekciju();
        }
        public FrmRestoran(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            txtRestoranUsluga.Focus();
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
                cmd.Parameters.Add("@RestoranUsluga", SqlDbType.NVarChar).Value = txtRestoranUsluga.Text;
                if (azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"update tblRestoran
                                        set RestoranUsluga=@RestoranUsluga
                                        where RestoranID=@id";
                    pomocniRed = null;
                }
                else
                {
                    cmd.CommandText = @"insert into tblRestoran(RestoranUsluga)
                                        values(@RestoranUsluga)";
                }
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Unos podataka nije validan!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                konekcija.Close();
            }
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
