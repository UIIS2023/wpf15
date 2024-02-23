using System;
using System.Collections.Generic;
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
    /// Interaction logic for FrmPrijava.xaml
    /// </summary>
    public partial class FrmPrijava : Window
    {
        public FrmPrijava()
        {
            InitializeComponent();
            txtUsername.Focus();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Uneseno korisničko ime i lozinka
                string unesenoKorisnickoIme = txtUsername.Text;
                string unesenaLozinka = txtPassword.Password; ;

                if (ValidateCredentials(unesenoKorisnickoIme, unesenaLozinka))
                {
                    this.DialogResult = true;
                    // Zatvaranje trenutnog prozora za logovanje
                    this.Close();

                    // Otvaranje glavnog prozora (MainWindow) će se sada izvršiti u konstruktoru MainWindow
                }
                else
                {
                    // Prikaz poruke o grešci ukoliko su uneti neispravni podaci
                    MessageBox.Show("Netacno korisnicko ime ili lozinka. Pokusajte ponovo.", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Došlo je do greške: {ex.Message}", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private bool ValidateCredentials(string username, string password)
        {
            return username == "andrijana" && password == "andrijana";

        }

    }
}
