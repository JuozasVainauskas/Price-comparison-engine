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

namespace Price_comparison_engine
{
    /// <summary>
    /// Interaction logic for VertinimoLangas.xaml
    /// </summary>
    public partial class VertinimoLangas : Window
    {
        public VertinimoLangas()
        {
            InitializeComponent();
            elektromarktIv.Text = "Įvertinimo dar nėra";
            avitelaIv.Text = "Įvertinimo dar nėra";
        }

        private static double avitela = 0;
        private static int avitelaBalsavusiuSk = 1;

        private static double elektromarkt = 0;
        private static int elektromarktBalsavusiuSk = 1;
        private void Vertinti_avitela(object sender, RoutedEventArgs e)
        {
            var calc = avitela / (3 * avitelaBalsavusiuSk);

            avitelaIv.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
            avitelaBalsavusiuSk++;
            avitelaApt.SelectedIndex = -1;
            avitelaKok.SelectedIndex = -1;
            avitelaPris.SelectedIndex = -1;
        }
        private void Vertinti_elektromarkt(object sender, RoutedEventArgs e)
        {
            var calc = elektromarkt / (3 * elektromarktBalsavusiuSk);
            elektromarktIv.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
            elektromarktBalsavusiuSk++;
            elektroApt.SelectedIndex = -1;
            elektroKok.SelectedIndex = -1;
            elektroPris.SelectedIndex = -1;
        }

        private void Avitela_Aptarnavimas(object sender, SelectionChangedEventArgs e)
        {
            avitela += avitelaApt.SelectedIndex +1;
        }

        private void Avitela_Kokybe(object sender, SelectionChangedEventArgs e)
        {
            avitela += avitelaApt.SelectedIndex + 1;
        }

        private void Avitela_Pristatymas(object sender, SelectionChangedEventArgs e)
        {
            avitela += avitelaApt.SelectedIndex + 1;
        }

        private void Elektromarkt_Aptarnavimas(object sender, SelectionChangedEventArgs e)
        {
            elektromarkt += avitelaApt.SelectedIndex + 1;
        }

        private void Elektromarkt_Kokybe(object sender, SelectionChangedEventArgs e)
        {
            elektromarkt += avitelaApt.SelectedIndex + 1;
        }

        private void Elektromarkt_Pristatymas(object sender, SelectionChangedEventArgs e)
        {
            elektromarkt += avitelaApt.SelectedIndex + 1;
        }

        private void Rasyti()
        {
            //Prisijungimas prie duomenu bazes
            var sqlPrisijungti = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\PCEDatabase.mdf;Integrated Security=SSPI;Connect Timeout=30");
            //Tikrinam ar svaru
            try
            {
                //Jei sql busena buvo uzdaryta, tai prisijungiam
                if (sqlPrisijungti.State == ConnectionState.Closed)
                {
                    sqlPrisijungti.Open();
                }
                //Query
                var eile = "SELECT BalsuSkaicius, BalsavusiuSkaicius FROM ParduotuviuDuomenys WHERE ParduotuvesPavadinimas=@ParduotuvesPavadinimas";
                var sqlKomanda = new SqlCommand(eile, sqlPrisijungti);
                sqlKomanda.CommandType = CommandType.Text;
                sqlKomanda.Parameters.AddWithValue("@ParduotuvesPavadinimas", "Avitela");
                using (SqlDataReader skaityti = sqlKomanda.ExecuteReader())
                {
                    if (skaityti.Read())
                    {
                        kažkokiareiksme1 = skaityti["BalsuSuma"].ToString();
                        kažkokiareiksme = skaityti["BalsavusiuSkaicius"].ToString();
                    }
                }
                sqlKomanda.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Jei klaida, ismesti
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Atsijungti
                sqlPrisijungti.Close();
            }
        }
    }
}
