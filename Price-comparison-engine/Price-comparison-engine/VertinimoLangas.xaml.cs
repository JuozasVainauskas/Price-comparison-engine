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
            aptarnavimas.IsEnabled = false;
            pristatymas.IsEnabled = false;
            kokybe.IsEnabled = false;
        }

        private static double balsai = 0;
        private static int balsavusiuSk = 0;

        private void Parduotuve(object sender, SelectionChangedEventArgs e)
        {
            if(parduotuve.SelectedIndex == 0)
            {
                aptarnavimas.IsEnabled = true;
                pristatymas.IsEnabled = true;
                kokybe.IsEnabled = true;
                Skaityti("Avitela", ref balsai, ref balsavusiuSk);
                ParduotuvesImg.Source = new BitmapImage(new Uri("Nuotraukos/avitela.png", UriKind.RelativeOrAbsolute));
                var calc = balsai / (3 * balsavusiuSk);
                keistiImg(calc);
                ivertinimas.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";

                parduotuve.IsEnabled = false;
            }
            if (parduotuve.SelectedIndex == 1)
            {
                aptarnavimas.IsEnabled = true;
                pristatymas.IsEnabled = true;
                kokybe.IsEnabled = true;
                Skaityti("Elektromarkt", ref balsai, ref balsavusiuSk);
                ParduotuvesImg.Source = new BitmapImage(new Uri("Nuotraukos/elektromarkt.png", UriKind.RelativeOrAbsolute));
                var calc = balsai / (3 * balsavusiuSk);
                keistiImg(calc);
                ivertinimas.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                parduotuve.IsEnabled = false;
            }
        }
        private void Aptarnavimas(object sender, SelectionChangedEventArgs e)
        {
           balsai += aptarnavimas.SelectedIndex + 1;
            aptarnavimas.IsEnabled = false;
        }

        private void Kokybe(object sender, SelectionChangedEventArgs e)
        {
            balsai += kokybe.SelectedIndex + 1;
            kokybe.IsEnabled = false;
        }

        private void Pristatymas(object sender, SelectionChangedEventArgs e)
        {
            balsai += pristatymas.SelectedIndex + 1;
            pristatymas.IsEnabled = false;
        }
        private void Vertinti(object sender, RoutedEventArgs e)
        {
            balsavusiuSk++;
            var calc = balsai / (3 * balsavusiuSk);
            if(parduotuve.SelectedIndex == 0)
            {
                Rasyti("Avitela", balsai, balsavusiuSk);
            }
            if (parduotuve.SelectedIndex == 1)
            {
                Rasyti("Elektromarkt", balsai, balsavusiuSk);
            }

            ivertinimas.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
            keistiImg(calc);
            aptarnavimas.IsEnabled = true;
            pristatymas.IsEnabled = true;
            kokybe.IsEnabled = true;
            parduotuve.IsEnabled = true;
            aptarnavimas.SelectedIndex = -1;
            kokybe.SelectedIndex = -1;
            pristatymas.SelectedIndex = -1;
            parduotuve.SelectedIndex = -1;
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

                var eile = "UPDATE ParduotuviuDuomenys SET BalsuSuma=@BalsuSuma, BalsavusiuSkaicius=@BalsavusiuSkaicius WHERE ParduotuvesPavadinimas=@ParduotuvesPavadinimas";
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

        private void keistiImg(double calc)
        {
            if (calc < 0.5)
            {
                ivertinimoImg.Source = new BitmapImage(new Uri("Nuotraukos/0.png", UriKind.Relative));
            }
            else if (calc < 1.5)
            {
                ivertinimoImg.Source = new BitmapImage(new Uri("Nuotraukos/11.png", UriKind.Relative));
            }
            else if (calc < 2.5)
            {
                ivertinimoImg.Source = new BitmapImage(new Uri("Nuotraukos/22.png", UriKind.Relative));
            }
            else if (calc < 3.5)
            {
                ivertinimoImg.Source = new BitmapImage(new Uri("Nuotraukos/33.png", UriKind.Relative));
            }
            else if (calc < 4.5)
            {
                ivertinimoImg.Source = new BitmapImage(new Uri("Nuotraukos/4.png", UriKind.Relative));
            }
            else
            {
                ivertinimoImg.Source = new BitmapImage(new Uri("Nuotraukos/5.png", UriKind.RelativeOrAbsolute));
            }
        }
    }
}