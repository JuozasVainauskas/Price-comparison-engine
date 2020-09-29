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

        //Funkcija parasyta su ref, tai jei nori grazinti values, rasyti - Skaityti(pavadinimas, ref balsuSuma, ref balsavusiuSkaicius);
        private void Skaityti(string parduotuvesPavadinimas, ref int balsuSuma, ref int balsavusiuSkaicius)
        {
            var sqlPrisijungti = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\PCEDatabase.mdf;Integrated Security=SSPI;Connect Timeout=30");
            try
            {
                if (sqlPrisijungti.State == ConnectionState.Closed)
                {
                    sqlPrisijungti.Open();
                }

                var eile = "SELECT BalsuSkaicius, BalsavusiuSkaicius FROM ParduotuviuDuomenys WHERE ParduotuvesPavadinimas=@ParduotuvesPavadinimas";
                var sqlKomanda = new SqlCommand(eile, sqlPrisijungti);
                sqlKomanda.CommandType = CommandType.Text;
                sqlKomanda.Parameters.AddWithValue("@ParduotuvesPavadinimas", parduotuvesPavadinimas);
                using (SqlDataReader skaityti = sqlKomanda.ExecuteReader())
                {
                    if (skaityti.Read())
                    {
                        balsuSuma = Convert.ToInt32(skaityti["BalsuSuma"].ToString());
                        balsavusiuSkaicius = Convert.ToInt32(skaityti["BalsavusiuSkaicius"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlPrisijungti.Close();
            }
        }

        private void Rasyti(string parduotuvesPavadinimas, int balsuSuma, int balsavusiuSkaicius)
        {
            var sqlPrisijungti = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\PCEDatabase.mdf;Integrated Security=SSPI;Connect Timeout=30");
            try
            {
                if (sqlPrisijungti.State == ConnectionState.Closed)
                {
                    sqlPrisijungti.Open();
                }

                var eile = "UPDATE ParduotuviuDuomenys SET BalsuSkaicius=@BalsuSkaicius, BalsavusiuSkaicius=@BalsavusiuSkaicius FROM ParduotuviuDuomenys WHERE ParduotuvesPavadinimas=@ParduotuvesPavadinimas";
                var sqlKomanda = new SqlCommand(eile, sqlPrisijungti);
                sqlKomanda.CommandType = CommandType.Text;
                sqlKomanda.Parameters.AddWithValue("@BalsuSkaicius", balsuSuma);
                sqlKomanda.Parameters.AddWithValue("@BalsavusiuSkaicius", balsavusiuSkaicius);
                sqlKomanda.Parameters.AddWithValue("@ParduotuvesPavadinimas", parduotuvesPavadinimas);
                sqlKomanda.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlPrisijungti.Close();
            }
        }
    }
}
