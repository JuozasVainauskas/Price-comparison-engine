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
        readonly RegisterWindow registracijosLangas;
        private SqlConnection sqlRegister;
        private SqlCommand sqlCommand;
        private string code;
        public PatvirtinimoLangas(SqlConnection sqlRegister, SqlCommand sqlCommand, MainWindow pagrindinisLangas, RegisterWindow registracijosLangas, string code, string email)
        {
            InitializeComponent();
            new SendEmail(code, email);
            this.pagrindinisLangas = pagrindinisLangas;
            this.registracijosLangas = registracijosLangas;
            this.sqlRegister = sqlRegister;
            this.sqlCommand = sqlCommand;
            this.code = code;
        }

        private void PatvirtintiMygtukas(object sender, RoutedEventArgs e)
        {
            try
            {
                if (code == PatvirtinimoLangelis.Text)
                {
                    sqlCommand.ExecuteNonQuery();

                    pagrindinisLangas.Close();
                    registracijosLangas.Close();
                    MessageBox.Show("Sėkmingai prisiregistravote.");

                    var mainWindowLoggedIn = new MainWindowLoggedIn();
                    mainWindowLoggedIn.Show();
                    sqlRegister.Close();
                    this.Close();
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
        }
    }
}
