using Price_comparison_engine.Classes;
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
        readonly RegistrationWindow registracijosLangas;
        readonly DatabaseContext context;
        private string kodas;
        public PatvirtinimoLangas(DatabaseContext context, MainWindow pagrindinisLangas, RegistrationWindow registracijosLangas, string kodas, string email)
        {
            InitializeComponent();
            new SiustiEmail(kodas, email);
            this.pagrindinisLangas = pagrindinisLangas;
            this.registracijosLangas = registracijosLangas;
            this.context = context;
            this.kodas = kodas;
        }

        private void PatvirtintiMygtukas(object sender, RoutedEventArgs e)
        {
            if (kodas.Equals(PatvirtinimoLangelis.Text))
            {
                context.SaveChanges();

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
