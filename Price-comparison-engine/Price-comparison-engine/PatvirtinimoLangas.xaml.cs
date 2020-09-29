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
        private SqlCommand sqlKomanda;
        public PatvirtinimoLangas(MainWindow pagrindinisLangas, RegistracijosLangas registracijosLangas,  SqlCommand sqlKomanda, String kodas)
        {
            InitializeComponent();
            this.pagrindinisLangas = pagrindinisLangas;
            this.registracijosLangas = registracijosLangas;
            this.sqlKomanda = sqlKomanda;
        }

        protected override void OnClosed(EventArgs e)
        {
            MessageBox.Show("Nepavyko priregistruoti/prijungti (naujo) vartotojo.");
        }

        private void patvirtintiMygtukas(object sender, RoutedEventArgs e)
        {
            sqlKomanda.ExecuteNonQuery();
            pagrindinisLangas.Close();
            registracijosLangas.Close();
            this.Close();
            var mainwindowlogedin = new MainWindowLogedIn();
            mainwindowlogedin.Show();
        }
    }
}
