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
        private static List<string> komentarai = new List<string>();
        private static List<string> email;

        private static Dictionary<string, double> vertinimai = new Dictionary<string, double>()
        {
            {"Elektromarkt", 0},
            {"Avitela", 0},
            {"Pigu.lt", 0}
        };

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
                KeistiImg(calc);
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
                KeistiImg(calc);
                ivertinimas.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                parduotuve.IsEnabled = false;
            }
            if (parduotuve.SelectedIndex == 2)
            {
                aptarnavimas.IsEnabled = true;
                pristatymas.IsEnabled = true;
                kokybe.IsEnabled = true;
                Skaityti("Pigu.lt", PrisijungimoLangas.email, ref balsuIndex, ref balsai, ref balsavusiuSk);
                ParduotuvesImg.Source = new BitmapImage(new Uri("Nuotraukos/elektromarkt.png", UriKind.RelativeOrAbsolute));
                var calc = balsai / (3 * balsavusiuSk);
                KeistiImg(calc);
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

            komentarai.Add("_0_Data_vertinimas_Komentaras;_0_Data_vertinimas_Komentaras;");
            tvarkytiDuomenis(0, email, komentarai);

            if (parduotuve.SelectedIndex == 0 && !balsuIndex.Contains("_0"))
            {
                balsavusiuSk++;
                var calc = balsai / (3 * balsavusiuSk);
                balsuIndex += "_0";
                Rasyti("Avitela",balsuIndex,PrisijungimoLangas.email, balsai, balsavusiuSk);
                Atstatyti(calc);
            }
            else if (parduotuve.SelectedIndex == 1 && !balsuIndex.Contains("_1"))
            {
                balsavusiuSk++;
                var calc = balsai / (3 * balsavusiuSk);
                balsuIndex += "_1";
               Rasyti("Elektromarkt", balsuIndex, PrisijungimoLangas.email, balsai, balsavusiuSk);
                Atstatyti(calc);
            }
            else if (parduotuve.SelectedIndex == 2 && !balsuIndex.Contains("_2"))
            {
                balsavusiuSk++;
                var calc = balsai / (3 * balsavusiuSk);
                balsuIndex += "_2";
                Rasyti("Pigu.lt", balsuIndex, PrisijungimoLangas.email, balsai, balsavusiuSk);
                Atstatyti(calc);
            }
            else
            {
                MessageBox.Show("Jau balsavote už šią parduotuvę!");
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

        private void Siusti(object sender, RoutedEventArgs e)
        {
            if (parduotuve.SelectedIndex == 0 && balsuIndex.Contains("_0"))
            {
                RasytiKomentarus(PrisijungimoLangas.email, 0, 0, KomentaruLangelis.Text);
            }
            else if (parduotuve.SelectedIndex == 1 && balsuIndex.Contains("_1"))
            {
                RasytiKomentarus(PrisijungimoLangas.email, 1, 0, KomentaruLangelis.Text);
            }
            else if (parduotuve.SelectedIndex == 2 && balsuIndex.Contains("_2"))
            {
                RasytiKomentarus(PrisijungimoLangas.email, 2, 0, KomentaruLangelis.Text);
            }
            else
            {
                MessageBox.Show("Jau esate palikęs atsiliepimą už šią parduotuvę!");
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
        private static void SkaitytiKomentaruDuomenis(ref List<string> email, ref List<string> komentarai)
        {
            using (var kontekstas = new DuomenuBazesKontekstas())
            {
                var tempEmail = kontekstas.NaudotojoDuomenys.Select(column => column.Email).ToList();
                var tempKomentarai = kontekstas.NaudotojoDuomenys.Select(column => column.Komentaras).ToList();

                if (tempEmail != null && tempKomentarai != null)
                {
                    email = tempEmail;
                    komentarai = tempKomentarai;
                }
            }
        }

        private static void tvarkytiDuomenis(int index, List<string>email, List<string>komentarai)
        {
            foreach(var element in komentarai)
            {
                if(element.Contains("_" + index + "_"))
                {
                    Console.WriteLine("#1:" + element);
                    string[] tempString;
                    tempString = element.Split(';');

                    foreach(var stringElement in tempString)
                    {
                        Console.WriteLine(stringElement);
                        if(stringElement.Contains("_" + index + "_"))
                        {
                            Console.WriteLine("#2:" + stringElement);
                            string[] anotherTempString;
                            anotherTempString = stringElement.Split('_');
                            Console.WriteLine(anotherTempString[1]); // INDEKSAS
                            Console.WriteLine(anotherTempString[2]); // DATA
                            Console.WriteLine(anotherTempString[3]); // VERTINIMAS
                            Console.WriteLine(anotherTempString[4]); // KOMENTARAS
                        }
                    }
                }

            }
        }

        //Funkcija parasyta su ref, tai jei nori grazinti values, rasyti - Skaityti(pavadinimas, ref balsuSuma, ref balsavusiuSkaicius);
        private static void Skaityti(string parduotuvesPavadinimas, string email, ref string balsuIndex, ref double balsuSuma, ref int balsavusiuSkaicius)
        {
            using (var kontekstas = new DuomenuBazesKontekstas())
            {
                var rezultatas = kontekstas.ParduotuviuDuomenys.SingleOrDefault(c => c.ParduotuvesPavadinimas == parduotuvesPavadinimas);

                if (rezultatas != null)
                {
                    balsuSuma = rezultatas.BalsuSuma;
                    balsavusiuSkaicius = rezultatas.BalsavusiuSkaicius;
                }
            }

            using (var kontekstas = new DuomenuBazesKontekstas())
            {
                var rezultatas = kontekstas.NaudotojoDuomenys.SingleOrDefault(c => c.Email == email);
                var tempKomentaras = kontekstas.NaudotojoDuomenys.Select(column => column.Komentaras).ToList();

                if (rezultatas != null)
                {
                    balsuIndex = rezultatas.ArBalsavo;
                }
            }
        }

        private static void RasytiKomentarus(string email, int parduotuvesId, double vertinimas, string komentaras)
        {
            using (var kontekstas = new DuomenuBazesKontekstas())
            {
                var rezultatas = kontekstas.NaudotojoDuomenys.SingleOrDefault(b => b.Email == email);

                if (rezultatas != null)
                {
                    var temp = rezultatas.Komentaras;

                    if (rezultatas.ArBalsavo.Contains("_" + parduotuvesId) && !temp.Contains("_" + parduotuvesId + "_"))
                    {
                        temp += string.Concat("_", parduotuvesId, "_", DateTime.Now.ToString("yyyy-MM-dd HH:mm"), "_", string.Format("{0:F2}", vertinimas), "_", komentaras, ";");
                    }

                    rezultatas.Komentaras = temp;
                    kontekstas.SaveChanges();
                }
            }
        }

        private static void Rasyti(string parduotuvesPavadinimas, string balsuIndex, string email, double balsuSuma, int balsavusiuSkaicius)
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

        private void KeistiImg(double calc)
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
            KeistiImg(calc);
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