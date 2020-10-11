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
        }

        class Komentaras
        {
            public string Tekstas { get; set; }
        }

        private static double balsai = 0;
        private static int balsavusiuSk = 0;
        private static string balsuIndex = "";
        private static List<string> komentarai = new List<string>();
        private static List<string> email = new List<string>();

        private static Dictionary<string, int> rating = new Dictionary<string, int>()
        {
            {"Aptarnavimas", 0},
            {"PrekiuKokybe", 0},
            {"Pristatymas", 0}
        };

        private static Dictionary<string, Dictionary<string, int>> vertinimai = new Dictionary<string, Dictionary<string, int>>()
        {
            {"Avitela", new Dictionary<string, int>()
                {
                    {"Aptarnavimas", 0},
                    {"PrekiuKokybe", 0},
                    {"Pristatymas", 0}
                }
            },
            {"Elektromarkt", new Dictionary<string, int>()
                {
                    {"Aptarnavimas", 0},
                    {"PrekiuKokybe", 0},
                    {"Pristatymas", 0}
                }
            },
            {"Pigu.lt", new Dictionary<string, int>()
                {
                    {"Aptarnavimas", 0},
                    {"PrekiuKokybe", 0},
                    {"Pristatymas", 0}
                }
            }
        };

        private void Parduotuve(object sender, SelectionChangedEventArgs e)
        {
            if(parduotuve.SelectedIndex == 0)
            {
                tvarkytiDuomenis(0, listViewas, email, komentarai);
                Skaityti("Avitela", PrisijungimoLangas.email, ref balsuIndex, ref balsai, ref balsavusiuSk);
                ParduotuvesImg.Source = new BitmapImage(new Uri("Nuotraukos/avitela.png", UriKind.RelativeOrAbsolute));
                var calc = balsai / (3 * balsavusiuSk);
                ivertinimas.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                parduotuve.IsEnabled = false;
            }
            if (parduotuve.SelectedIndex == 1)
            {
                tvarkytiDuomenis(1, listViewas, email, komentarai);
                Skaityti("Elektromarkt", PrisijungimoLangas.email, ref balsuIndex, ref balsai, ref balsavusiuSk);
                ParduotuvesImg.Source = new BitmapImage(new Uri("Nuotraukos/elektromarkt.png", UriKind.RelativeOrAbsolute));
                var calc = balsai / (3 * balsavusiuSk);
                ivertinimas.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                parduotuve.IsEnabled = false;
            }
            if (parduotuve.SelectedIndex == 2)
            {
                tvarkytiDuomenis(2, listViewas, email, komentarai);
                Skaityti("Pigu.lt", PrisijungimoLangas.email, ref balsuIndex, ref balsai, ref balsavusiuSk);
                ParduotuvesImg.Source = new BitmapImage(new Uri("Nuotraukos/avitela.png", UriKind.RelativeOrAbsolute));
                var calc = balsai / (3 * balsavusiuSk);
                ivertinimas.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                parduotuve.IsEnabled = false;
            }
        }

        private void Vertinti(object sender, RoutedEventArgs e)
        {
            if (rating["Aptarnavimas"] == 0 || rating["PrekiuKokybe"] == 0 || rating["Pristatymas"] == 0)
            {
                MessageBox.Show("Turite pažymėti vertinimą prie visų kriterijų.");
            }
            else
            {
                if (parduotuve.SelectedIndex == 0 && !balsuIndex.Contains("_0"))
                {
                    balsavusiuSk++;
                    balsai += rating["Aptarnavimas"] + rating["PrekiuKokybe"] + rating["Pristatymas"];
                    vertinimai["Avitela"]["Aptarnavimas"] = rating["Aptarnavimas"];
                    vertinimai["Avitela"]["PrekiuKokybe"] = rating["PrekiuKokybe"];
                    vertinimai["Avitela"]["Pristatymas"] = rating["Pristatymas"];
                    var calc = balsai / (3 * balsavusiuSk);
                    balsuIndex += "_0";
                    Rasyti("Avitela", balsuIndex, PrisijungimoLangas.email, balsai, balsavusiuSk);
                    Atstatyti(calc);
                }
                else if (parduotuve.SelectedIndex == 1 && !balsuIndex.Contains("_1"))
                {
                    balsavusiuSk++;
                    balsai += rating["Aptarnavimas"] + rating["PrekiuKokybe"] + rating["Pristatymas"];
                    vertinimai["Elektromarkt"]["Aptarnavimas"] = rating["Aptarnavimas"];
                    vertinimai["Elektromarkt"]["PrekiuKokybe"] = rating["PrekiuKokybe"];
                    vertinimai["Elektromarkt"]["Pristatymas"] = rating["Pristatymas"];
                    var calc = balsai / (3 * balsavusiuSk);
                    balsuIndex += "_1";
                    Rasyti("Elektromarkt", balsuIndex, PrisijungimoLangas.email, balsai, balsavusiuSk);
                    Atstatyti(calc);
                }
                else if (parduotuve.SelectedIndex == 2 && !balsuIndex.Contains("_2"))
                {
                    balsavusiuSk++;
                    balsai += rating["Aptarnavimas"] + rating["PrekiuKokybe"] + rating["Pristatymas"];
                    vertinimai["Pigu.lt"]["Aptarnavimas"] = rating["Aptarnavimas"];
                    vertinimai["Pigu.lt"]["PrekiuKokybe"] = rating["PrekiuKokybe"];
                    vertinimai["Pigu.lt"]["Pristatymas"] = rating["Pristatymas"];
                    var calc = balsai / (3 * balsavusiuSk);
                    balsuIndex += "_2";
                    Rasyti("Pigu.lt", balsuIndex, PrisijungimoLangas.email, balsai, balsavusiuSk);
                    Atstatyti(calc);
                }
                else
                {
                    MessageBox.Show("Jau balsavote už šią parduotuvę!");
                    parduotuve.IsEnabled = true;
                    parduotuve.SelectedIndex = -1;
                }
                ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Nuotraukos/Star_0.png");
                ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Nuotraukos/Star_0.png");
                ChangeImgSource(Aptarnavimas3, "AptarnavimasImg3", "Nuotraukos/Star_0.png");
                ChangeImgSource(Aptarnavimas4, "AptarnavimasImg4", "Nuotraukos/Star_0.png");
                ChangeImgSource(Aptarnavimas5, "AptarnavimasImg5", "Nuotraukos/Star_0.png");
                ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Nuotraukos/Star_0.png");
                ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Nuotraukos/Star_0.png");
                ChangeImgSource(PrekiuKokybe3, "PrekiuKokybeImg3", "Nuotraukos/Star_0.png");
                ChangeImgSource(PrekiuKokybe4, "PrekiuKokybeImg4", "Nuotraukos/Star_0.png");
                ChangeImgSource(PrekiuKokybe5, "PrekiuKokybeImg5", "Nuotraukos/Star_0.png");
                ChangeImgSource(Pristatymas1, "PristatymasImg1", "Nuotraukos/Star_0.png");
                ChangeImgSource(Pristatymas2, "PristatymasImg2", "Nuotraukos/Star_0.png");
                ChangeImgSource(Pristatymas3, "PristatymasImg3", "Nuotraukos/Star_0.png");
                ChangeImgSource(Pristatymas4, "PristatymasImg4", "Nuotraukos/Star_0.png");
                ChangeImgSource(Pristatymas5, "PristatymasImg5", "Nuotraukos/Star_0.png");
                rating["Aptarnavimas"] = 0;
                rating["PrekiuKokybe"] = 0;
                rating["Pristatymas"] = 0;
            }
        }

        private void Siusti(object sender, RoutedEventArgs e)
        {
            if(parduotuve.SelectedIndex == -1)
            {
                MessageBox.Show("Nepasirinkote parduotuvės!");
                KomentaruLangelis.Clear();
            }
            else if (parduotuve.SelectedIndex == 0)
            {
                if (balsuIndex.Contains("_0"))
                {
                    RasytiKomentarus(PrisijungimoLangas.email, 0, vertinimai["Avitela"]["Aptarnavimas"], vertinimai["Avitela"]["PrekiuKokybe"], vertinimai["Avitela"]["Pristatymas"], KomentaruLangelis.Text);
                    KomentaruLangelis.Clear();
                    SkaitytiKomentaruDuomenis(ref email, ref komentarai);
                    sujungtiList(email, komentarai);
                    tvarkytiDuomenis(0, listViewas, email, komentarai);


                }
                else
                {
                    MessageBox.Show("Pirmiausiai, turite įvertinti parduotuvę.");
                }
            }
            else if (parduotuve.SelectedIndex == 1)
            {
                if (balsuIndex.Contains("_1"))
                {
                    RasytiKomentarus(PrisijungimoLangas.email, 1, vertinimai["Elektromarkt"]["Aptarnavimas"], vertinimai["Elektromarkt"]["PrekiuKokybe"], vertinimai["Elektromarkt"]["Pristatymas"], KomentaruLangelis.Text);
                    KomentaruLangelis.Clear();
                    SkaitytiKomentaruDuomenis(ref email, ref komentarai);
                    sujungtiList(email, komentarai);
                    tvarkytiDuomenis(1, listViewas, email, komentarai);

                }
                else
                {
                    MessageBox.Show("Pirmiausiai, turite įvertinti parduotuvę.");
                }
            }
            else if (parduotuve.SelectedIndex == 2)
            {
                if (balsuIndex.Contains("_2"))
                {
                    RasytiKomentarus(PrisijungimoLangas.email, 2, vertinimai["Pigu.lt"]["Aptarnavimas"], vertinimai["Pigu.lt"]["PrekiuKokybe"], vertinimai["Pigu.lt"]["Pristatymas"], KomentaruLangelis.Text);
                    KomentaruLangelis.Clear();
                    SkaitytiKomentaruDuomenis(ref email, ref komentarai);
                    sujungtiList(email, komentarai);
                    tvarkytiDuomenis(2, listViewas, email, komentarai);

                }
                else
                {
                    MessageBox.Show("Pirmiausiai, turite įvertinti parduotuvę.");
                }
            }
            else
            {
                MessageBox.Show("Jau esate palikęs atsiliepimą už šią parduotuvę!");
                parduotuve.IsEnabled = true;
                parduotuve.SelectedIndex = -1;
                return;
            }
        }
        private static void SkaitytiKomentaruDuomenis(ref List<string> email, ref List<string> komentarai)
        {
            email.Clear();
            komentarai.Clear();
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

        private static void tvarkytiDuomenis(int index, ListView lv ,List<string> email, List<string>komentarai)
        {
            lv.Items.Clear();
            foreach(var element in komentarai)
            {
                if(element.Contains("_" + index + "_"))
                {
                    string[] tempString;
                    tempString = element.Split(';');
                    string emailas = tempString[0];

                    foreach(var stringElement in tempString)
                    {
                        if(stringElement.Contains("_" + index + "_"))
                        {
                            string[] anotherTempString;
                            anotherTempString = stringElement.Split('_');
                            var text = anotherTempString[1];
                            lv.Items.Add(new Komentaras() { Tekstas = emailas + " " + anotherTempString[2] + " Apt.:" + anotherTempString[3] + " Pr. Kok.:" + anotherTempString[4] + " Prist.:" + anotherTempString[5]});
                            lv.Items.Add(new Komentaras() { Tekstas = anotherTempString[6]});

                        }
                    }
                }

            }
        }

        private static void sujungtiList(List<string> a, List<string> b)
        {
            for(int i = 0; i < a.Count; i++)
            {
                a[i] = a[i] + ";" + b[i];
                b[i] = a[i];
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

                if (rezultatas != null)
                {
                    balsuIndex = rezultatas.ArBalsavo;
                }
            }
        }

        private static void RasytiKomentarus(string email, int parduotuvesId, double aptarnavimas, double prekiuKokybe, double pristatymas, string komentaras)
        {
            using (var kontekstas = new DuomenuBazesKontekstas())
            {
                var rezultatas = kontekstas.NaudotojoDuomenys.SingleOrDefault(b => b.Email == email);

                if (rezultatas != null)
                {
                    var temp = rezultatas.Komentaras;

                    if (rezultatas.ArBalsavo.Contains("_" + parduotuvesId) && !temp.Contains("_" + parduotuvesId + "_"))
                    {
                        temp += string.Concat("_", parduotuvesId, "_", DateTime.Now.ToString("yyyy-MM-dd HH:mm"), "_", string.Format("{0:F2}", aptarnavimas), "_", string.Format("{0:F2}", prekiuKokybe), "_", string.Format("{0:F2}", pristatymas), "_", komentaras, ";");
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

        private void Atstatyti(double calc)
        {
            ivertinimas.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
            parduotuve.IsEnabled = true;
            parduotuve.SelectedIndex = -1;
        }

        private void listViewas_Loaded(object sender, RoutedEventArgs e)
        {
            SkaitytiKomentaruDuomenis(ref email, ref komentarai);
            sujungtiList(email, komentarai);
            tvarkytiDuomenis(0, listViewas ,email, komentarai);
        }

        private void KeistiKomentarus(object sender, SelectionChangedEventArgs e)
        {
            if(PasirinktiKomentara.SelectedIndex == 0)
            {
                tvarkytiDuomenis(0, listViewas, email, komentarai);
            }
            else if(PasirinktiKomentara.SelectedIndex == 1)
            {
                tvarkytiDuomenis(1, listViewas, email, komentarai);
            }
            else if(PasirinktiKomentara.SelectedIndex == 2)
            {
                tvarkytiDuomenis(2, listViewas, email, komentarai);
            }
        }

        private void ChangeImgSource(Button button, string imgSrc, string imgToChangeSrc)
        {
            var ct = button.Template;
            var btnImage = (Image)ct.FindName(imgSrc, button);
            btnImage.Source = new BitmapImage(new Uri(imgToChangeSrc, UriKind.RelativeOrAbsolute));
        }

        private bool CheckImgSource(Button button, string imgSrc)
        {
            var imgBool = false;
            var imgToChangeSrc = "Nuotraukos/Star_1.png";

            var ct = button.Template;
            var btnImage = (Image)ct.FindName(imgSrc, button);
            var btnImageSrc = btnImage.Source.ToString();

            btnImageSrc =  btnImageSrc.Substring(Math.Max(0, btnImageSrc.Length-imgToChangeSrc.Length));

            if (btnImageSrc.Equals(imgToChangeSrc))
            {
                imgBool = true;
            }
            return imgBool;
        }

        private void AptarnavimasStar1(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Nuotraukos/Star_0.png");
            ChangeImgSource(Aptarnavimas3, "AptarnavimasImg3", "Nuotraukos/Star_0.png");
            ChangeImgSource(Aptarnavimas4, "AptarnavimasImg4", "Nuotraukos/Star_0.png");
            ChangeImgSource(Aptarnavimas5, "AptarnavimasImg5", "Nuotraukos/Star_0.png");
            rating["Aptarnavimas"] = 1;
        }

        private void AptarnavimasStar2(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(Aptarnavimas3, "AptarnavimasImg3", "Nuotraukos/Star_0.png");
            ChangeImgSource(Aptarnavimas4, "AptarnavimasImg4", "Nuotraukos/Star_0.png");
            ChangeImgSource(Aptarnavimas5, "AptarnavimasImg5", "Nuotraukos/Star_0.png");
            rating["Aptarnavimas"] = 2;
        }

        private void AptarnavimasStar3(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(Aptarnavimas3, "AptarnavimasImg3", "Nuotraukos/Star_1.png");
            ChangeImgSource(Aptarnavimas4, "AptarnavimasImg4", "Nuotraukos/Star_0.png");
            ChangeImgSource(Aptarnavimas5, "AptarnavimasImg5", "Nuotraukos/Star_0.png");
            rating["Aptarnavimas"] = 3;
        }

        private void AptarnavimasStar4(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(Aptarnavimas3, "AptarnavimasImg3", "Nuotraukos/Star_1.png");
            ChangeImgSource(Aptarnavimas4, "AptarnavimasImg4", "Nuotraukos/Star_1.png");
            ChangeImgSource(Aptarnavimas5, "AptarnavimasImg5", "Nuotraukos/Star_0.png");
            rating["Aptarnavimas"] = 4;
        }

        private void AptarnavimasStar5(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(Aptarnavimas3, "AptarnavimasImg3", "Nuotraukos/Star_1.png");
            ChangeImgSource(Aptarnavimas4, "AptarnavimasImg4", "Nuotraukos/Star_1.png");
            ChangeImgSource(Aptarnavimas5, "AptarnavimasImg5", "Nuotraukos/Star_1.png");
            rating["Aptarnavimas"] = 5;
        }

        private void PrekiuKokybeStar1(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Nuotraukos/Star_0.png");
            ChangeImgSource(PrekiuKokybe3, "PrekiuKokybeImg3", "Nuotraukos/Star_0.png");
            ChangeImgSource(PrekiuKokybe4, "PrekiuKokybeImg4", "Nuotraukos/Star_0.png");
            ChangeImgSource(PrekiuKokybe5, "PrekiuKokybeImg5", "Nuotraukos/Star_0.png");
            rating["PrekiuKokybe"] = 1;
        }

        private void PrekiuKokybeStar2(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(PrekiuKokybe3, "PrekiuKokybeImg3", "Nuotraukos/Star_0.png");
            ChangeImgSource(PrekiuKokybe4, "PrekiuKokybeImg4", "Nuotraukos/Star_0.png");
            ChangeImgSource(PrekiuKokybe5, "PrekiuKokybeImg5", "Nuotraukos/Star_0.png");
            rating["PrekiuKokybe"] = 2;
        }

        private void PrekiuKokybeStar3(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(PrekiuKokybe3, "PrekiuKokybeImg3", "Nuotraukos/Star_1.png");
            ChangeImgSource(PrekiuKokybe4, "PrekiuKokybeImg4", "Nuotraukos/Star_0.png");
            ChangeImgSource(PrekiuKokybe5, "PrekiuKokybeImg5", "Nuotraukos/Star_0.png");
            rating["PrekiuKokybe"] = 3;
        }

        private void PrekiuKokybeStar4(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(PrekiuKokybe3, "PrekiuKokybeImg3", "Nuotraukos/Star_1.png");
            ChangeImgSource(PrekiuKokybe4, "PrekiuKokybeImg4", "Nuotraukos/Star_1.png");
            ChangeImgSource(PrekiuKokybe5, "PrekiuKokybeImg5", "Nuotraukos/Star_0.png");
            rating["PrekiuKokybe"] = 4;
        }

        private void PrekiuKokybeStar5(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(PrekiuKokybe3, "PrekiuKokybeImg3", "Nuotraukos/Star_1.png");
            ChangeImgSource(PrekiuKokybe4, "PrekiuKokybeImg4", "Nuotraukos/Star_1.png");
            ChangeImgSource(PrekiuKokybe5, "PrekiuKokybeImg5", "Nuotraukos/Star_1.png");
            rating["PrekiuKokybe"] = 5;
        }

        private void PristatymasStar1(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Pristatymas1, "PristatymasImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(Pristatymas2, "PristatymasImg2", "Nuotraukos/Star_0.png");
            ChangeImgSource(Pristatymas3, "PristatymasImg3", "Nuotraukos/Star_0.png");
            ChangeImgSource(Pristatymas4, "PristatymasImg4", "Nuotraukos/Star_0.png");
            ChangeImgSource(Pristatymas5, "PristatymasImg5", "Nuotraukos/Star_0.png");
            rating["Pristatymas"] = 1;
        }

        private void PristatymasStar2(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Pristatymas1, "PristatymasImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(Pristatymas2, "PristatymasImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(Pristatymas3, "PristatymasImg3", "Nuotraukos/Star_0.png");
            ChangeImgSource(Pristatymas4, "PristatymasImg4", "Nuotraukos/Star_0.png");
            ChangeImgSource(Pristatymas5, "PristatymasImg5", "Nuotraukos/Star_0.png");
            rating["Pristatymas"] = 2;
        }

        private void PristatymasStar3(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Pristatymas1, "PristatymasImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(Pristatymas2, "PristatymasImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(Pristatymas3, "PristatymasImg3", "Nuotraukos/Star_1.png");
            ChangeImgSource(Pristatymas4, "PristatymasImg4", "Nuotraukos/Star_0.png");
            ChangeImgSource(Pristatymas5, "PristatymasImg5", "Nuotraukos/Star_0.png");
            rating["Pristatymas"] = 3;
        }

        private void PristatymasStar4(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Pristatymas1, "PristatymasImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(Pristatymas2, "PristatymasImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(Pristatymas3, "PristatymasImg3", "Nuotraukos/Star_1.png");
            ChangeImgSource(Pristatymas4, "PristatymasImg4", "Nuotraukos/Star_1.png");
            ChangeImgSource(Pristatymas5, "PristatymasImg5", "Nuotraukos/Star_0.png");
            rating["Pristatymas"] = 4;
        }

        private void PristatymasStar5(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Pristatymas1, "PristatymasImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(Pristatymas2, "PristatymasImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(Pristatymas3, "PristatymasImg3", "Nuotraukos/Star_1.png");
            ChangeImgSource(Pristatymas4, "PristatymasImg4", "Nuotraukos/Star_1.png");
            ChangeImgSource(Pristatymas5, "PristatymasImg5", "Nuotraukos/Star_1.png");
            rating["Pristatymas"] = 5;
        }

        private void AptarnavimasImg1_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Aptarnavimas1, "AptarnavimasImg1"))
            {
                ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Nuotraukos/Star_2.png");
            }
        }

        private void AptarnavimasImg1_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Aptarnavimas1, "AptarnavimasImg1"))
            {
                ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Nuotraukos/Star_0.png");
            }
        }

        private void AptarnavimasImg2_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Aptarnavimas2, "AptarnavimasImg2") && !CheckImgSource(Aptarnavimas1, "AptarnavimasImg1"))
            {
                ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Nuotraukos/Star_2.png");
                ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Nuotraukos/Star_2.png");
            }
        }

        private void AptarnavimasImg2_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Aptarnavimas2, "AptarnavimasImg2") && !CheckImgSource(Aptarnavimas1, "AptarnavimasImg1"))
            {
                ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Nuotraukos/Star_0.png");
                ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Nuotraukos/Star_0.png");
            }
        }

        private void AptarnavimasImg3_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Aptarnavimas3, "AptarnavimasImg3") && !CheckImgSource(Aptarnavimas2, "AptarnavimasImg2") && !CheckImgSource(Aptarnavimas1, "AptarnavimasImg1"))
            {
                ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Nuotraukos/Star_2.png");
                ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Nuotraukos/Star_2.png");
                ChangeImgSource(Aptarnavimas3, "AptarnavimasImg3", "Nuotraukos/Star_2.png");
            }
        }

        private void AptarnavimasImg3_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Aptarnavimas3, "AptarnavimasImg3") && !CheckImgSource(Aptarnavimas2, "AptarnavimasImg2") && !CheckImgSource(Aptarnavimas1, "AptarnavimasImg1"))
            {
                ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Nuotraukos/Star_0.png");
                ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Nuotraukos/Star_0.png");
                ChangeImgSource(Aptarnavimas3, "AptarnavimasImg3", "Nuotraukos/Star_0.png");
            }
        }

        private void AptarnavimasImg4_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Aptarnavimas4, "AptarnavimasImg4") && !CheckImgSource(Aptarnavimas3, "AptarnavimasImg3") && !CheckImgSource(Aptarnavimas2, "AptarnavimasImg2") && !CheckImgSource(Aptarnavimas1, "AptarnavimasImg1"))
            {
                ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Nuotraukos/Star_2.png");
                ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Nuotraukos/Star_2.png");
                ChangeImgSource(Aptarnavimas3, "AptarnavimasImg3", "Nuotraukos/Star_2.png");
                ChangeImgSource(Aptarnavimas4, "AptarnavimasImg4", "Nuotraukos/Star_2.png");
            }
        }

        private void AptarnavimasImg4_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Aptarnavimas4, "AptarnavimasImg4") && !CheckImgSource(Aptarnavimas3, "AptarnavimasImg3") && !CheckImgSource(Aptarnavimas2, "AptarnavimasImg2") && !CheckImgSource(Aptarnavimas1, "AptarnavimasImg1"))
            {
                ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Nuotraukos/Star_0.png");
                ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Nuotraukos/Star_0.png");
                ChangeImgSource(Aptarnavimas3, "AptarnavimasImg3", "Nuotraukos/Star_0.png");
                ChangeImgSource(Aptarnavimas4, "AptarnavimasImg4", "Nuotraukos/Star_0.png");
            }
        }

        private void AptarnavimasImg5_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Aptarnavimas5, "AptarnavimasImg5") && !CheckImgSource(Aptarnavimas4, "AptarnavimasImg4") && !CheckImgSource(Aptarnavimas3, "AptarnavimasImg3") && !CheckImgSource(Aptarnavimas2, "AptarnavimasImg2") && !CheckImgSource(Aptarnavimas1, "AptarnavimasImg1"))
            {
                ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Nuotraukos/Star_2.png");
                ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Nuotraukos/Star_2.png");
                ChangeImgSource(Aptarnavimas3, "AptarnavimasImg3", "Nuotraukos/Star_2.png");
                ChangeImgSource(Aptarnavimas4, "AptarnavimasImg4", "Nuotraukos/Star_2.png");
                ChangeImgSource(Aptarnavimas5, "AptarnavimasImg5", "Nuotraukos/Star_2.png");
            }
        }

        private void AptarnavimasImg5_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Aptarnavimas5, "AptarnavimasImg5") && !CheckImgSource(Aptarnavimas4, "AptarnavimasImg4") && !CheckImgSource(Aptarnavimas3, "AptarnavimasImg3") && !CheckImgSource(Aptarnavimas2, "AptarnavimasImg2") && !CheckImgSource(Aptarnavimas1, "AptarnavimasImg1"))
            {
                ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Nuotraukos/Star_0.png");
                ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Nuotraukos/Star_0.png");
                ChangeImgSource(Aptarnavimas3, "AptarnavimasImg3", "Nuotraukos/Star_0.png");
                ChangeImgSource(Aptarnavimas4, "AptarnavimasImg4", "Nuotraukos/Star_0.png");
                ChangeImgSource(Aptarnavimas5, "AptarnavimasImg5", "Nuotraukos/Star_0.png");
            }
        }

        private void PrekiuKokybeImg1_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(PrekiuKokybe1, "PrekiuKokybeImg1"))
            {
                ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Nuotraukos/Star_2.png");
            }
        }

        private void PrekiuKokybeImg1_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(PrekiuKokybe1, "PrekiuKokybeImg1"))
            {
                ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Nuotraukos/Star_0.png");
            }
        }

        private void PrekiuKokybeImg2_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(PrekiuKokybe2, "PrekiuKokybeImg2") && !CheckImgSource(PrekiuKokybe1, "PrekiuKokybeImg1"))
            {
                ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Nuotraukos/Star_2.png");
                ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Nuotraukos/Star_2.png");
            }
        }

        private void PrekiuKokybeImg2_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(PrekiuKokybe2, "PrekiuKokybeImg2") && !CheckImgSource(PrekiuKokybe1, "PrekiuKokybeImg1"))
            {
                ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Nuotraukos/Star_0.png");
                ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Nuotraukos/Star_0.png");
            }
        }

        private void PrekiuKokybeImg3_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(PrekiuKokybe3, "PrekiuKokybeImg3") && !CheckImgSource(PrekiuKokybe2, "PrekiuKokybeImg2") && !CheckImgSource(PrekiuKokybe1, "PrekiuKokybeImg1"))
            {
                ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Nuotraukos/Star_2.png");
                ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Nuotraukos/Star_2.png");
                ChangeImgSource(PrekiuKokybe3, "PrekiuKokybeImg3", "Nuotraukos/Star_2.png");
            }
        }

        private void PrekiuKokybeImg3_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(PrekiuKokybe3, "PrekiuKokybeImg3") && !CheckImgSource(PrekiuKokybe2, "PrekiuKokybeImg2") && !CheckImgSource(PrekiuKokybe1, "PrekiuKokybeImg1"))
            {
                ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Nuotraukos/Star_0.png");
                ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Nuotraukos/Star_0.png");
                ChangeImgSource(PrekiuKokybe3, "PrekiuKokybeImg3", "Nuotraukos/Star_0.png");
            }
        }

        private void PrekiuKokybeImg4_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(PrekiuKokybe4, "PrekiuKokybeImg4") && !CheckImgSource(PrekiuKokybe3, "PrekiuKokybeImg3") && !CheckImgSource(PrekiuKokybe2, "PrekiuKokybeImg2") && !CheckImgSource(PrekiuKokybe1, "PrekiuKokybeImg1"))
            {
                ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Nuotraukos/Star_2.png");
                ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Nuotraukos/Star_2.png");
                ChangeImgSource(PrekiuKokybe3, "PrekiuKokybeImg3", "Nuotraukos/Star_2.png");
                ChangeImgSource(PrekiuKokybe4, "PrekiuKokybeImg4", "Nuotraukos/Star_2.png");
            }
        }

        private void PrekiuKokybeImg4_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(PrekiuKokybe4, "PrekiuKokybeImg4") && !CheckImgSource(PrekiuKokybe3, "PrekiuKokybeImg3") && !CheckImgSource(PrekiuKokybe2, "PrekiuKokybeImg2") && !CheckImgSource(PrekiuKokybe1, "PrekiuKokybeImg1"))
            {
                ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Nuotraukos/Star_0.png");
                ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Nuotraukos/Star_0.png");
                ChangeImgSource(PrekiuKokybe3, "PrekiuKokybeImg3", "Nuotraukos/Star_0.png");
                ChangeImgSource(PrekiuKokybe4, "PrekiuKokybeImg4", "Nuotraukos/Star_0.png");
            }
        }

        private void PrekiuKokybeImg5_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(PrekiuKokybe5, "PrekiuKokybeImg5") && !CheckImgSource(PrekiuKokybe4, "PrekiuKokybeImg4") && !CheckImgSource(PrekiuKokybe3, "PrekiuKokybeImg3") && !CheckImgSource(PrekiuKokybe2, "PrekiuKokybeImg2") && !CheckImgSource(PrekiuKokybe1, "PrekiuKokybeImg1"))
            {
                ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Nuotraukos/Star_2.png");
                ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Nuotraukos/Star_2.png");
                ChangeImgSource(PrekiuKokybe3, "PrekiuKokybeImg3", "Nuotraukos/Star_2.png");
                ChangeImgSource(PrekiuKokybe4, "PrekiuKokybeImg4", "Nuotraukos/Star_2.png");
                ChangeImgSource(PrekiuKokybe5, "PrekiuKokybeImg5", "Nuotraukos/Star_2.png");
            }
        }

        private void PrekiuKokybeImg5_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(PrekiuKokybe5, "PrekiuKokybeImg5") && !CheckImgSource(PrekiuKokybe4, "PrekiuKokybeImg4") && !CheckImgSource(PrekiuKokybe3, "PrekiuKokybeImg3") && !CheckImgSource(PrekiuKokybe2, "PrekiuKokybeImg2") && !CheckImgSource(PrekiuKokybe1, "PrekiuKokybeImg1"))
            {
                ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Nuotraukos/Star_0.png");
                ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Nuotraukos/Star_0.png");
                ChangeImgSource(PrekiuKokybe3, "PrekiuKokybeImg3", "Nuotraukos/Star_0.png");
                ChangeImgSource(PrekiuKokybe4, "PrekiuKokybeImg4", "Nuotraukos/Star_0.png");
                ChangeImgSource(PrekiuKokybe5, "PrekiuKokybeImg5", "Nuotraukos/Star_0.png");
            }
        }

        private void PristatymasImg1_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Pristatymas1, "PristatymasImg1"))
            {
                ChangeImgSource(Pristatymas1, "PristatymasImg1", "Nuotraukos/Star_2.png");
            }
        }

        private void PristatymasImg1_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Pristatymas1, "PristatymasImg1"))
            {
                ChangeImgSource(Pristatymas1, "PristatymasImg1", "Nuotraukos/Star_0.png");
            }
        }
        
        private void PristatymasImg2_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Pristatymas2, "PristatymasImg2") && !CheckImgSource(Pristatymas1, "PristatymasImg1"))
            {
                ChangeImgSource(Pristatymas1, "PristatymasImg1", "Nuotraukos/Star_2.png");
                ChangeImgSource(Pristatymas2, "PristatymasImg2", "Nuotraukos/Star_2.png");
            }
        }

        private void PristatymasImg2_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Pristatymas2, "PristatymasImg2") && !CheckImgSource(Pristatymas1, "PristatymasImg1"))
            {
                ChangeImgSource(Pristatymas1, "PristatymasImg1", "Nuotraukos/Star_0.png");
                ChangeImgSource(Pristatymas2, "PristatymasImg2", "Nuotraukos/Star_0.png");
            }
        }

        private void PristatymasImg3_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Pristatymas3, "PristatymasImg3") && !CheckImgSource(Pristatymas2, "PristatymasImg2") && !CheckImgSource(Pristatymas1, "PristatymasImg1"))
            {
                ChangeImgSource(Pristatymas1, "PristatymasImg1", "Nuotraukos/Star_2.png");
                ChangeImgSource(Pristatymas2, "PristatymasImg2", "Nuotraukos/Star_2.png");
                ChangeImgSource(Pristatymas3, "PristatymasImg3", "Nuotraukos/Star_2.png");
            }
        }

        private void PristatymasImg3_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Pristatymas3, "PristatymasImg3") && !CheckImgSource(Pristatymas2, "PristatymasImg2") && !CheckImgSource(Pristatymas1, "PristatymasImg1"))
            {
                ChangeImgSource(Pristatymas1, "PristatymasImg1", "Nuotraukos/Star_0.png");
                ChangeImgSource(Pristatymas2, "PristatymasImg2", "Nuotraukos/Star_0.png");
                ChangeImgSource(Pristatymas3, "PristatymasImg3", "Nuotraukos/Star_0.png");
            }
        }

        private void PristatymasImg4_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Pristatymas4, "PristatymasImg4") && !CheckImgSource(Pristatymas3, "PristatymasImg3") && !CheckImgSource(Pristatymas2, "PristatymasImg2") && !CheckImgSource(Pristatymas1, "PristatymasImg1"))
            {
                ChangeImgSource(Pristatymas1, "PristatymasImg1", "Nuotraukos/Star_2.png");
                ChangeImgSource(Pristatymas2, "PristatymasImg2", "Nuotraukos/Star_2.png");
                ChangeImgSource(Pristatymas3, "PristatymasImg3", "Nuotraukos/Star_2.png");
                ChangeImgSource(Pristatymas4, "PristatymasImg4", "Nuotraukos/Star_2.png");
            }
        }

        private void PristatymasImg4_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Pristatymas4, "PristatymasImg4") && !CheckImgSource(Pristatymas3, "PristatymasImg3") && !CheckImgSource(Pristatymas2, "PristatymasImg2") && !CheckImgSource(Pristatymas1, "PristatymasImg1"))
            {
                ChangeImgSource(Pristatymas1, "PristatymasImg1", "Nuotraukos/Star_0.png");
                ChangeImgSource(Pristatymas2, "PristatymasImg2", "Nuotraukos/Star_0.png");
                ChangeImgSource(Pristatymas3, "PristatymasImg3", "Nuotraukos/Star_0.png");
                ChangeImgSource(Pristatymas4, "PristatymasImg4", "Nuotraukos/Star_0.png");
            }
        }

        private void PristatymasImg5_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Pristatymas5, "PristatymasImg5") && !CheckImgSource(Pristatymas4, "PristatymasImg4") && !CheckImgSource(Pristatymas3, "PristatymasImg3") && !CheckImgSource(Pristatymas2, "PristatymasImg2") && !CheckImgSource(Pristatymas1, "PristatymasImg1"))
            {
                ChangeImgSource(Pristatymas1, "PristatymasImg1", "Nuotraukos/Star_2.png");
                ChangeImgSource(Pristatymas2, "PristatymasImg2", "Nuotraukos/Star_2.png");
                ChangeImgSource(Pristatymas3, "PristatymasImg3", "Nuotraukos/Star_2.png");
                ChangeImgSource(Pristatymas4, "PristatymasImg4", "Nuotraukos/Star_2.png");
                ChangeImgSource(Pristatymas5, "PristatymasImg5", "Nuotraukos/Star_2.png");
            }
        }

        private void PristatymasImg5_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Pristatymas5, "PristatymasImg5") && !CheckImgSource(Pristatymas4, "PristatymasImg4") && !CheckImgSource(Pristatymas3, "PristatymasImg3") && !CheckImgSource(Pristatymas2, "PristatymasImg2") && !CheckImgSource(Pristatymas1, "PristatymasImg1"))
            {
                ChangeImgSource(Pristatymas1, "PristatymasImg1", "Nuotraukos/Star_0.png");
                ChangeImgSource(Pristatymas2, "PristatymasImg2", "Nuotraukos/Star_0.png");
                ChangeImgSource(Pristatymas3, "PristatymasImg3", "Nuotraukos/Star_0.png");
                ChangeImgSource(Pristatymas4, "PristatymasImg4", "Nuotraukos/Star_0.png");
                ChangeImgSource(Pristatymas5, "PristatymasImg5", "Nuotraukos/Star_0.png");
            }
        }
    }
}