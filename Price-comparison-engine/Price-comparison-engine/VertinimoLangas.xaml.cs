using Price_comparison_engine.Klases;
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
        private static string balsuIndex = "";

        private void Parduotuve(object sender, SelectionChangedEventArgs e)
        {
            if(parduotuve.SelectedIndex == 0)
            {
                aptarnavimas.IsEnabled = true;
                pristatymas.IsEnabled = true;
                kokybe.IsEnabled = true;
                Skaityti("Avitela", PrisijungimoLangas.email, ref balsuIndex, ref balsai, ref balsavusiuSk);
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
                Skaityti("Elektromarkt", PrisijungimoLangas.email, ref balsuIndex, ref balsai, ref balsavusiuSk);
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
            if(parduotuve.SelectedIndex == 0 && !balsuIndex.Contains("0"))
            {
                MessageBox.Show(balsuIndex);
                balsavusiuSk++;
                var calc = balsai / (3 * balsavusiuSk);
                balsuIndex += "0";
                Rasyti("Avitela",balsuIndex,PrisijungimoLangas.email, balsai, balsavusiuSk);
                Atstatyti(calc);
            }
            else if (parduotuve.SelectedIndex == 1 && !balsuIndex.Contains("1"))
            {
                balsavusiuSk++;
                var calc = balsai / (3 * balsavusiuSk);
                balsuIndex += "1";
               Rasyti("Elektromarkt", balsuIndex, PrisijungimoLangas.email, balsai, balsavusiuSk);
                Atstatyti(calc);
            }
            else
            {
                MessageBox.Show("Jau balsavote už šią parduotuvę!" + balsuIndex);
                aptarnavimas.IsEnabled = true;
                pristatymas.IsEnabled = true;
                kokybe.IsEnabled = true;
                parduotuve.IsEnabled = true;
                aptarnavimas.SelectedIndex = -1;
                kokybe.SelectedIndex = -1;
                pristatymas.SelectedIndex = -1;
                parduotuve.SelectedIndex = -1;
                return;
            }
        }

        //Funkcija parasyta su ref, tai jei nori grazinti values, rasyti - Skaityti(pavadinimas, ref balsuSuma, ref balsavusiuSkaicius);
        private void Skaityti(string parduotuvesPavadinimas, string email, ref string balsuIndex , ref double balsuSuma, ref int balsavusiuSkaicius)
        {
            using (var kontekstas = new DuomenuBazesKontekstas())
            {
                var rezultatas = kontekstas.ParduotuviuDuomenys.SingleOrDefault(c => c.ParduotuvesPavadinimas == parduotuvesPavadinimas);

                if (rezultatas != null)
                {
                    balsuSuma = rezultatas.BalsuSuma;
                    balsavusiuSkaicius = Convert.ToInt32(rezultatas.BalsavusiuSkaicius);
                }
            }

            using (var kontekstas = new DuomenuBazesKontekstas())
            {
                var rezultatas = kontekstas.NaudotojoDuomenys.SingleOrDefault(c => c.Email == email);

                if (rezultatas != null)
                {
                    balsuIndex = rezultatas.ArBalsavo;
                }
            }
        }

        private void Rasyti(string parduotuvesPavadinimas,string balsuIndex,string email, double balsuSuma, int balsavusiuSkaicius)
        {
            using (var kontekstas = new DuomenuBazesKontekstas())
            {
                var rezultatas = kontekstas.ParduotuviuDuomenys.SingleOrDefault(b => b.ParduotuvesPavadinimas== parduotuvesPavadinimas);
                if (rezultatas != null)
                {
                    rezultatas.BalsavusiuSkaicius = balsavusiuSkaicius;
                    rezultatas.BalsuSuma = balsuSuma;
                    kontekstas.SaveChanges();
                }
            }

            using (var kontekstas = new DuomenuBazesKontekstas())
            {
                var rezultatas = kontekstas.NaudotojoDuomenys.SingleOrDefault(b => b.Email == email);
                if (rezultatas != null)
                {
                    rezultatas.ArBalsavo = balsuIndex;
                    kontekstas.SaveChanges();
                }
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
        private void Atstatyti(double calc)
        {
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
    }
}