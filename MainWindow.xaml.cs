using Hotel.Forme;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hotel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string ucitanaTabela;
        bool azuriraj;
        DataRowView red;
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();

        #region Select upiti
        string vrsteSobeSelect = @"select VrstaSobeID as ID, Naziv as 'Naziv sobe'
                                          from tblVrstaSobe";
        string rezervacijaSelect = @"select RezervacijaID as ID, tblGost.Ime  + ' ' + tblGost.Prezime as 'Gost', tblRadnik.Kontakt as 'Kontakt za pomoc',
                                            tblRestoran.RestoranUsluga as 'Restoran usluga', tblSpa.SpaUsluga as 'Spa usluga',
                                            CenaRezervacije as Cena, BrojDana as 'Dani'
                                            from tblRezervacija join tblGost on tblRezervacija.GostID= tblGost.GostID
		                                    join tblRadnik on tblRezervacija.RadnikID= tblRadnik.RadnikID
		                                    join tblRestoran on tblRezervacija.RestoranID= tblRestoran.RestoranID	
		                                    join tblSpa on tblRezervacija.SpaID= tblSpa.SpaID";
        string gostSelect = @"select GostID as ID, Ime, Prezime, Jmbg, Kontakt
                                     from tblGost";
        string parkingSelect = @"select ParkingID as ID, BrojMesta as 'Broj mesta', tblRezervacija.BrojDana as 'Dani zauzetosti'
                                        from tblParking join tblRezervacija on tblParking.RezervacijaID = tblRezervacija.RezervacijaID";
        string radnikSelect = @"select RadnikID as ID, Ime, Prezime, Zaduzenje, Jmbg, Kontakt 
                                       from tblRadnik";
        string restoranSelect = @"select RestoranID as ID, RestoranUsluga as 'Restoran usluga'
                                         from tblRestoran";
        string sobaSelect = @"select SobaID as ID, BrojSobe as 'Broj sobe', Sprat, tblVrstaSobe.Naziv as 'Naziv sobe', StatusSobe as 'Status sobe', tblRezervacija.BrojDana as 'Dani zauzetosti' 
                                    from tblSoba join tblVrstaSobe on tblSoba.VrstaSobeID=tblVrstaSobe.VrstaSobeID
                                    join tblRezervacija on tblSoba.RezervacijaID= tblRezervacija.RezervacijaID";
        string spaSelect = @"select SpaID as ID, SpaUsluga as 'Spa usluga' 
                                    from tblSpa";
        #endregion
        #region Select sa uslovom
        string selectUslovVrstaSobe = @"select *
                                        from tblVrstaSobe
                                        where VrstaSobeID=";
        string selectUslovRezervacija = @"select *
                                        from tblRezervacija
                                        where RezervacijaID=";
        string selectUslovGost = @"select *
                                    from tblGost
                                    where GostID=";
        string selectUslovParking = @"select *
                                     from tblParking
                                     where ParkingID=";
        string selectUslovRadnik = @"select *
                                        from tblRadnik
                                        where RadnikID=";
        string selectUslovRestoran = @"select *
                                        from tblRestoran
                                        where RestoranID=";
        string selectUslovSoba = @"select *
                                    from tblSoba
                                    where SobaID=";
        string selectUslovSpa = @"select *
                                  from tblSpa
                                  where SpaID=";
        #endregion
        #region delete upiti
         string deleteVrstaSobe = @"delete from tblVrstaSobe
                                          where VrstaSobeID=";
         string deleteRezervacija = @"delete from tblRezervacija
                                            where RezervacijaID=";
         string deleteGost = @"delete from tblGost
                                     where GostID=";
         string deleteParking = @"delete from tblParking
                                      where ParkingID=";
         string deleteRadnik = @"delete from tblRadnik
                                        where RadnikID=";
         string deleteRestoran = @"delete from tblRestoran
                                            where RestoranID=";
         string deleteSoba = @"delete from tblSoba
                                        where SobaID=";
         string deleteSpa = @"delete from tblSpa
                                    where SpaID=";
        #endregion
        public MainWindow()
        {
           InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            //UcitajPodatke(DataGridCentralni, rezervacijaSelect);
            
            Forme.FrmPrijava prijava = new Forme.FrmPrijava();

            prijava.ShowDialog(); // Blokirajući poziv, čeka dok se prozor za prijavljivanje ne zatvori

            if (prijava.DialogResult == true)
            {
                // Ako je rezultat prijavljivanja tačan, otvori MainWindow
                UcitajPodatke(DataGridCentralni, rezervacijaSelect);
            }
            else
            {
                // Ako korisnik otkaže prijavljivanje, zatvori aplikaciju
                Application.Current.Shutdown();
            }
        }
        private void UcitajPodatke(DataGrid grid, string selectUpit)
        {
            try
            {
                konekcija.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectUpit, konekcija);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                if (grid != null)
                {
                    grid.ItemsSource = dt.DefaultView;
                }
                ucitanaTabela = selectUpit;
                dt.Dispose();
                dataAdapter.Dispose();
            }
            catch (SqlException)
            {
                MessageBox.Show("Neuspešno učitani podaci.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                konekcija?.Close();
            }
        }



        private void btnGost_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(DataGridCentralni, gostSelect);
        }

        private void btnRadnik_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(DataGridCentralni, radnikSelect);
        }

        private void btnRestoran_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(DataGridCentralni, restoranSelect);
        }

        private void btnSpa_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(DataGridCentralni, spaSelect);
        }

        private void btnParking_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(DataGridCentralni, parkingSelect);
        }

        private void btnSoba_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(DataGridCentralni, sobaSelect);
        }

        private void btnVrstaSobe_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(DataGridCentralni, vrsteSobeSelect);
        }

        private void btnRezervacija_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(DataGridCentralni, rezervacijaSelect);
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Window prozor;

            if (ucitanaTabela == vrsteSobeSelect)
            {
                prozor = new FrmVrstaSobe();
                prozor.ShowDialog();
                UcitajPodatke(DataGridCentralni, vrsteSobeSelect);
            }
            else if (ucitanaTabela == restoranSelect)
            {
                prozor = new FrmRestoran();
                prozor.ShowDialog();
                UcitajPodatke(DataGridCentralni, restoranSelect);
            }
            else if (ucitanaTabela == rezervacijaSelect)
            {
                prozor = new FrmRezervacija();
                prozor.ShowDialog();
                UcitajPodatke(DataGridCentralni, rezervacijaSelect);
            }
            else if (ucitanaTabela == sobaSelect)
            {
                prozor = new FrmSoba();
                prozor.ShowDialog();
                UcitajPodatke(DataGridCentralni, sobaSelect);
            }
            else if (ucitanaTabela == gostSelect)
            {
                prozor = new FrmGost();
                prozor.ShowDialog();
                UcitajPodatke(DataGridCentralni, gostSelect);
            }
            else if (ucitanaTabela == radnikSelect)
            {
                prozor = new FrmRadnik();
                prozor.ShowDialog();
                UcitajPodatke(DataGridCentralni, radnikSelect);
            }
            else if (ucitanaTabela == spaSelect)
            {
                prozor = new FrmSpa();
                prozor.ShowDialog();
                UcitajPodatke(DataGridCentralni, spaSelect);
            }
            else if (ucitanaTabela == parkingSelect)
            {
                prozor = new FrmParking();
                prozor.ShowDialog();
                UcitajPodatke(DataGridCentralni, parkingSelect);
            }
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            if (ucitanaTabela == vrsteSobeSelect)
            {
                PopuniFormu(DataGridCentralni, selectUslovVrstaSobe);
                UcitajPodatke(DataGridCentralni, vrsteSobeSelect);
            }
            else if (ucitanaTabela == sobaSelect)
            {
                PopuniFormu(DataGridCentralni, selectUslovSoba);
                UcitajPodatke(DataGridCentralni, sobaSelect);
            }
            else if (ucitanaTabela == spaSelect)
            {
                PopuniFormu(DataGridCentralni, selectUslovSpa);
                UcitajPodatke(DataGridCentralni, spaSelect);
            }
            else if (ucitanaTabela == restoranSelect)
            {
                PopuniFormu(DataGridCentralni, selectUslovRestoran);
                UcitajPodatke(DataGridCentralni, restoranSelect);
            }
            else if (ucitanaTabela == rezervacijaSelect)
            {
                PopuniFormu(DataGridCentralni, selectUslovRezervacija);
                UcitajPodatke(DataGridCentralni, rezervacijaSelect);
            }
            else if (ucitanaTabela == radnikSelect)
            {
                PopuniFormu(DataGridCentralni, selectUslovRadnik);
                UcitajPodatke(DataGridCentralni, radnikSelect);
            }
            else if (ucitanaTabela == parkingSelect)
            {
                PopuniFormu(DataGridCentralni, selectUslovParking);
                UcitajPodatke(DataGridCentralni, parkingSelect);
            }
            else if (ucitanaTabela == gostSelect)
            {
                PopuniFormu(DataGridCentralni, selectUslovGost);
                UcitajPodatke(DataGridCentralni, gostSelect);
            }
        }
        void PopuniFormu(DataGrid grid, string selectUslov)
        {
            try
            {
                konekcija.Open();
                azuriraj = true;
                red = (DataRowView)DataGridCentralni.SelectedItems[0];
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                cmd.CommandText = selectUslov + "@id";
                SqlDataReader citac = cmd.ExecuteReader();
                cmd.Dispose();
                if (citac.Read())
                {
                    if (ucitanaTabela == vrsteSobeSelect)
                    {
                        FrmVrstaSobe prozorVrstaSobe = new FrmVrstaSobe(azuriraj, red);
                        prozorVrstaSobe.txtNazivSobe.Text = citac["Naziv"].ToString();
                        prozorVrstaSobe.ShowDialog();
                    }
                    else if (ucitanaTabela == sobaSelect)
                    {
                        FrmSoba prozorSoba = new FrmSoba(azuriraj, red);
                        prozorSoba.txtBrojSobe.Text = citac["BrojSobe"].ToString();
                        prozorSoba.txtSprat.Text = citac["Sprat"].ToString();
                        prozorSoba.cbVrstaSobe.SelectedValue = citac["VrstaSobeID"].ToString();
                        prozorSoba.cbStatusSobe.IsChecked = (bool)citac["StatusSobe"];
                        prozorSoba.cbRezervacija.SelectedValue = citac["RezervacijaID"].ToString();
                        prozorSoba.ShowDialog();
                    }
                    else if (ucitanaTabela == rezervacijaSelect)
                    {
                        FrmRezervacija prozorRezervacija = new FrmRezervacija(azuriraj, red);
                        prozorRezervacija.cbGost.SelectedValue = citac["GostID"].ToString();
                        prozorRezervacija.cbRadnik.SelectedValue = citac["RadnikID"].ToString();
                        prozorRezervacija.cbRestoran.SelectedValue = citac["RestoranID"].ToString();
                        prozorRezervacija.cbSpa.SelectedValue = citac["SpaID"].ToString();
                        prozorRezervacija.txtCenaRezervacije.Text = citac["CenaRezervacije"].ToString();
                        prozorRezervacija.txtBrojDana.Text = citac["BrojDana"].ToString();
                        prozorRezervacija.ShowDialog();
                    }
                    else if (ucitanaTabela == restoranSelect)
                    {
                        FrmRestoran prozorRestoran = new FrmRestoran(azuriraj, red);
                        prozorRestoran.txtRestoranUsluga.Text = citac["RestoranUsluga"].ToString();
                        prozorRestoran.ShowDialog();
                    }
                    else if (ucitanaTabela == spaSelect)
                    {
                        FrmSpa prozorSpa = new FrmSpa(azuriraj,red);
                        prozorSpa.txtSpaUsluga.Text = citac["SpaUsluga"].ToString();
                        prozorSpa.ShowDialog();
                    }
                    else if (ucitanaTabela == gostSelect)
                    {
                        FrmGost prozorGost = new FrmGost(azuriraj, red);
                        prozorGost.txtIme.Text = citac["Ime"].ToString();
                        prozorGost.txtPrezime.Text = citac["Prezime"].ToString();
                        prozorGost.txtJmbg.Text = citac["Jmbg"].ToString();
                        prozorGost.txtKontakt.Text = citac["Kontakt"].ToString();
                        prozorGost.ShowDialog();
                    }
                    else if (ucitanaTabela == radnikSelect)
                    {
                        FrmRadnik prozorRadnik = new FrmRadnik(azuriraj, red);
                        prozorRadnik.txtIme.Text = citac["Ime"].ToString();
                        prozorRadnik.txtPrezime.Text = citac["Prezime"].ToString();
                        prozorRadnik.txtZaduzenje.Text = citac["Zaduzenje"].ToString();
                        prozorRadnik.txtJmbg.Text = citac["Jmbg"].ToString();
                        prozorRadnik.txtKontakt.Text = citac["Kontakt"].ToString();
                        prozorRadnik.ShowDialog();
                    }
                    else if (ucitanaTabela == parkingSelect)
                    {
                        FrmParking prozorParking = new FrmParking(azuriraj, red);
                        prozorParking.txtBrojMesta.Text = citac["BrojMesta"].ToString();
                        prozorParking.cbRezervacija.SelectedValue = citac["RezervacijaID"].ToString();
                        prozorParking.ShowDialog();
                    }

                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Niste selektovali red!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
                azuriraj = false;
            }
        }
        private void ObrisiZapis(DataGrid grid, string deleteUpit)
        {
            try
            {
                konekcija.Open();
                DataRowView red = (DataRowView)grid.SelectedItems[0];

                MessageBoxResult rezultat = MessageBox.Show("Da li ste sigurni da želite da obrišete?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (rezultat == MessageBoxResult.Yes)
                {
                    SqlCommand komanda = new SqlCommand
                    {
                        Connection = konekcija
                    };
                    komanda.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    komanda.CommandText = deleteUpit + "@id";
                    komanda.ExecuteNonQuery();
                    komanda.Dispose();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Niste selektovali red!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SqlException)
            {
                MessageBox.Show("Postoje povezani podaci u drugim tabelama!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (ucitanaTabela == vrsteSobeSelect)
            {
                ObrisiZapis(DataGridCentralni, deleteVrstaSobe);
                UcitajPodatke(DataGridCentralni, vrsteSobeSelect);
            }
            else if (ucitanaTabela == rezervacijaSelect)
            {
                ObrisiZapis(DataGridCentralni, deleteRezervacija);
                UcitajPodatke(DataGridCentralni, rezervacijaSelect);
            }
            else if (ucitanaTabela == sobaSelect)
            {
                ObrisiZapis(DataGridCentralni, deleteSoba);
                UcitajPodatke(DataGridCentralni, sobaSelect);
            }
            else if (ucitanaTabela == spaSelect)
            {
                ObrisiZapis(DataGridCentralni, deleteSpa);
                UcitajPodatke(DataGridCentralni, spaSelect);
            }
            else if (ucitanaTabela == restoranSelect)
            {
                ObrisiZapis(DataGridCentralni, deleteRestoran);
                UcitajPodatke(DataGridCentralni, restoranSelect);
            }
            else if (ucitanaTabela == radnikSelect)
            {
                ObrisiZapis(DataGridCentralni, deleteRadnik);
                UcitajPodatke(DataGridCentralni, radnikSelect);
            }
            else if (ucitanaTabela == parkingSelect)
            {
                ObrisiZapis(DataGridCentralni, deleteParking);
                UcitajPodatke(DataGridCentralni, parkingSelect);
            }
            else if (ucitanaTabela == gostSelect)
            {
                ObrisiZapis(DataGridCentralni, deleteGost);
                UcitajPodatke(DataGridCentralni, gostSelect);
            }
        }
    }
}
