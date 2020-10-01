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
    /// Interaction logic for RatingWindow.xaml
    /// </summary>
    public partial class RatingWindow : Window
    {
        public RatingWindow()
        {
            InitializeComponent();
            Read("Avitela", ref avitela, ref avitelaVotersNumber);
            Read("Elektromarkt", ref elektromarkt, ref elektromarktVotersNumber);
            if (avitelaVotersNumber != 0)
            {
                var calc = avitela / (3 * avitelaVotersNumber);
                avitelaRating.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
            }
            else
            {
                avitelaRating.Text = "Parduotuvė neįvertinta";
            }
            if (elektromarktVotersNumber != 0)
            {
                var calc = elektromarkt / (3 * elektromarktVotersNumber);
                avitelaRating.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
            }
            else
            {
                avitelaRating.Text = "Parduotuvė neįvertinta";
            }
        }

        private static double avitela = 0;
        private static int avitelaVotersNumber = 0;

        private static double elektromarkt = 0;
        private static int elektromarktVotersNumber = 0;
        private void Vertinti_avitela(object sender, RoutedEventArgs e)
        {
            avitelaVotersNumber++;
            var calc = avitela / (3 * avitelaVotersNumber);
            Write("avitela", avitela, avitelaVotersNumber);

            avitelaRating.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
            avitelaApt.SelectedIndex = -1;
            avitelaKok.SelectedIndex = -1;
            avitelaPris.SelectedIndex = -1;
            
        }
        private void Vertinti_elektromarkt(object sender, RoutedEventArgs e)
        {
            elektromarktVotersNumber++;
            var calc = elektromarkt / (3 * elektromarktVotersNumber);
            Write("Elektromarkt", elektromarkt, elektromarktVotersNumber);


            avitelaRating.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
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
            avitela += avitelaKok.SelectedIndex + 1;
        }

        private void Avitela_Pristatymas(object sender, SelectionChangedEventArgs e)
        {
            avitela += avitelaPris.SelectedIndex + 1;
        }

        private void Elektromarkt_Aptarnavimas(object sender, SelectionChangedEventArgs e)
        {
            elektromarkt += elektroApt.SelectedIndex + 1;
        }

        private void Elektromarkt_Kokybe(object sender, SelectionChangedEventArgs e)
        {
            elektromarkt += elektroKok.SelectedIndex + 1;
        }

        private void Elektromarkt_Pristatymas(object sender, SelectionChangedEventArgs e)
        {
            elektromarkt += elektroPris.SelectedIndex + 1;
        }

        //Funkcija parasyta su ref, tai jei nori grazinti values, rasyti - Read(pavadinimas, ref votesCount, ref votersNumber);
        private void Read(string shopName, ref double votesCount, ref int votersNumber)
        {
            var sqlLogin = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\PCEDatabase.mdf;Integrated Security=SSPI;Connect Timeout=30");
            try
            {
                if (sqlLogin.State == ConnectionState.Closed)
                {
                    sqlLogin.Open();
                }

                var queue = "SELECT VotesCount, VotersNumber FROM ShopRatingData WHERE ShopName=@ShopName";
                var sqlCommand = new SqlCommand(queue, sqlLogin);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@ShopName", shopName);
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.Read())
                    {
                        votesCount = Convert.ToDouble(sqlDataReader["VotesCount"].ToString());
                        votersNumber = Convert.ToInt32(sqlDataReader["VotersNumber"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlLogin.Close();
            }
        }

        private void Write(string shopName, double votesCount, int votersNumber)
        {
            var sqlLogin = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\PCEDatabase.mdf;Integrated Security=SSPI;Connect Timeout=30");
            try
            {
                if (sqlLogin.State == ConnectionState.Closed)
                {
                    sqlLogin.Open();
                }

                var queue = "UPDATE ShopRatingData SET VotesCount=@VotesCount, VotersNumber=@VotersNumber WHERE ShopName=@ShopName";
                var sqlCommand = new SqlCommand(queue, sqlLogin);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@VotesCount", votesCount);
                sqlCommand.Parameters.AddWithValue("@VotersNumber", votersNumber);
                sqlCommand.Parameters.AddWithValue("@ShopName", shopName);
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlLogin.Close();
            }
        }
    }
}