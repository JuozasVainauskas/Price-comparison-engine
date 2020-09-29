using Price_comparison_engine.Klases;
using System;
using System.Collections.Generic;
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

namespace Price_comparison_engine
{
    /// <summary>
    /// Interaction logic for PatvirtinimoLangas.xaml
    /// </summary>
    public partial class PatvirtinimoLangas : Window
    {
        readonly MainWindow pagrindinisLangas;
        readonly RegistracijosLangas registracijosLangas;
        private SqlConnection sqlRegistruotis;
        private SqlCommand sqlKomanda;
        private string kodas;
        public PatvirtinimoLangas(SqlConnection sqlRegistruotis, MainWindow pagrindinisLangas, RegistracijosLangas registracijosLangas,  SqlCommand sqlKomanda, string kodas, string email)
        {
            InitializeComponent();
            new SiustiEmail(kodas, email);
            this.pagrindinisLangas = pagrindinisLangas;
            this.registracijosLangas = registracijosLangas;
            this.sqlRegistruotis = sqlRegistruotis;
            this.sqlKomanda = sqlKomanda;
            this.kodas = kodas;
        }

        protected override void OnClosed(EventArgs e)
        {
            //MessageBox.Show("Nepavyko priregistruoti/prijungti (naujo) vartotojo.");
        }

        private void patvirtintiMygtukas(object sender, RoutedEventArgs e)
        {
            try
            {
                if (kodas == PatvirtinimoLangelis.Text)
                {
                    //sqlKomanda.ExecuteNonQuery();

                    pagrindinisLangas.Close();
                    registracijosLangas.Close();
                    MessageBox.Show("Sėkmingai prisiregistravote.");
                    this.Close();

                    MainWindowLoggedIn mainwindowloggedin = new MainWindowLoggedIn();
                    mainwindowloggedin.Show();

                }
                else
                {
                    MessageBox.Show("Blogai įvestas kodas.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlRegistruotis.Close();
            }
        }
    }
}
