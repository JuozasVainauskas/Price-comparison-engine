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
            Skaityti("Avitela", ref avitela, ref avitelaBalsavusiuSk);
            Skaityti("Elektromarkt", ref elektromarkt, ref elektromarktBalsavusiuSk);
            if (avitelaBalsavusiuSk != 0)
            {
                var calc = avitela / (3 * avitelaBalsavusiuSk);
                avitelaIv.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
            }
            else
            {
                avitelaIv.Text = "Parduotuvė neįvertinta";
            }
            if (elektromarktBalsavusiuSk != 0)
            {
                var calc = elektromarkt / (3 * elektromarktBalsavusiuSk);
                elektromarktIv.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
            }
            else
            {
                elektromarktIv.Text = "Parduotuvė neįvertinta";
            }
        }

        private static double avitela = 0;
        private static int avitelaBalsavusiuSk = 0;

        private static double elektromarkt = 0;
        private static int elektromarktBalsavusiuSk = 0;
        private void Vertinti_avitela(object sender, RoutedEventArgs e)
        {
            avitelaBalsavusiuSk++;
            var calc = avitela / (3 * avitelaBalsavusiuSk);
            Rasyti("avitela", avitela, avitelaBalsavusiuSk);

            avitelaIv.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
            avitelaApt.SelectedIndex = -1;
            avitelaKok.SelectedIndex = -1;
            avitelaPris.SelectedIndex = -1;
            
        }
        private void Vertinti_elektromarkt(object sender, RoutedEventArgs e)
        {
            elektromarktBalsavusiuSk++;
            var calc = elektromarkt / (3 * elektromarktBalsavusiuSk);
            Rasyti("Elektromarkt", elektromarkt, elektromarktBalsavusiuSk);


            elektromarktIv.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
            elektroApt.SelectedIndex = -1;
            elektroKok.SelectedIndex = -1;
            elektroPris.SelectedIndex = -1;
        }

        private void Avitela_Aptarnavimas(object sender, SelectionChangedEventArgs e)
        {
            avitela += avitelaApt.SelectedIndex + 1;
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
        private void Skaityti(string parduotuvesPavadinimas, ref double balsuSuma, ref int balsavusiuSkaicius)
        {
            var sqlPrisijungti = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\PCEDatabase.mdf;Integrated Security=SSPI;Connect Timeout=30");
            try
            {
                if (sqlPrisijungti.State == ConnectionState.Closed)
                {
                    sqlPrisijungti.Open();
                }

                var eile = "SELECT BalsuSuma, BalsavusiuSkaicius FROM ParduotuviuDuomenys WHERE ParduotuvesPavadinimas=@ParduotuvesPavadinimas";
                var sqlKomanda = new SqlCommand(eile, sqlPrisijungti);
                sqlKomanda.CommandType = CommandType.Text;
                sqlKomanda.Parameters.AddWithValue("@ParduotuvesPavadinimas", parduotuvesPavadinimas);
                using (SqlDataReader skaityti = sqlKomanda.ExecuteReader())
                {
                    if (skaityti.Read())
                    {
                        balsuSuma = Convert.ToDouble(skaityti["BalsuSuma"].ToString());
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

        private void Rasyti(string parduotuvesPavadinimas, double balsuSuma, int balsavusiuSkaicius)
        {
            var sqlPrisijungti = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\PCEDatabase.mdf;Integrated Security=SSPI;Connect Timeout=30");
            try
            {
                if (sqlPrisijungti.State == ConnectionState.Closed)
                {
                    sqlPrisijungti.Open();
                }

                var eile = "UPDATE ParduotuviuDuomenys SET BalsuSuma=@BalsuSuma, BalsavusiuSkaicius=@BalsavusiuSkaicius FROM ParduotuviuDuomenys WHERE ParduotuvesPavadinimas=@ParduotuvesPavadinimas";
                var sqlKomanda = new SqlCommand(eile, sqlPrisijungti);
                sqlKomanda.CommandType = CommandType.Text;
                sqlKomanda.Parameters.AddWithValue("@BalsuSuma", balsuSuma);
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