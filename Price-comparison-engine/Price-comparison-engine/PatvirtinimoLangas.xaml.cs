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
        readonly DuomenuBazesKontekstas kontekstas;
        private string kodas;
        public PatvirtinimoLangas(DuomenuBazesKontekstas kontekstas, MainWindow pagrindinisLangas, RegistracijosLangas registracijosLangas, string kodas, string email)
        {
            InitializeComponent();
            new SiustiEmail(kodas, email);
            this.pagrindinisLangas = pagrindinisLangas;
            this.registracijosLangas = registracijosLangas;
            this.kontekstas = kontekstas;
            this.kodas = kodas;
        }

        private void PatvirtintiMygtukas(object sender, RoutedEventArgs e)
        {
            if (kodas.Equals(PatvirtinimoLangelis.Text))
            {
                kontekstas.SaveChanges();

                pagrindinisLangas.Close();
                registracijosLangas.Close();
                MessageBox.Show("Sėkmingai prisiregistravote.");

                var mainWindowLoggedIn = new MainWindowLoggedIn();
                mainWindowLoggedIn.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Blogai įvestas kodas.");
            }
        }
    }
}
