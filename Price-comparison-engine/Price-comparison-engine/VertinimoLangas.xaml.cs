using Price_comparison_engine.Klases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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

        private static int balsai = 0;
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
            {"Pigu", new Dictionary<string, int>()
                {
                    {"Aptarnavimas", 0},
                    {"PrekiuKokybe", 0},
                    {"Pristatymas", 0}
                }
            },
            {"Barbora", new Dictionary<string, int>()
                {
                    {"Aptarnavimas", 0},
                    {"PrekiuKokybe", 0},
                    {"Pristatymas", 0}
                }
            },
            {"Bigbox", new Dictionary<string, int>()
                {
                    {"Aptarnavimas", 0},
                    {"PrekiuKokybe", 0},
                    {"Pristatymas", 0}
                }
            },
            {"Rde", new Dictionary<string, int>()
                {
                    {"Aptarnavimas", 0},
                    {"PrekiuKokybe", 0},
                    {"Pristatymas", 0}
                }
            },
            {"GintarineVaistine", new Dictionary<string, int>()
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
                var calc = (double)balsai / (3 * balsavusiuSk);
                ivertinimas.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                parduotuve.IsEnabled = false;
            }
            if (parduotuve.SelectedIndex == 1)
            {
                tvarkytiDuomenis(1, listViewas, email, komentarai);
                Skaityti("Elektromarkt", PrisijungimoLangas.email, ref balsuIndex, ref balsai, ref balsavusiuSk);
                ParduotuvesImg.Source = new BitmapImage(new Uri("Nuotraukos/elektromarkt.png", UriKind.RelativeOrAbsolute));
                var calc = (double)balsai / (3 * balsavusiuSk);
                ivertinimas.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                parduotuve.IsEnabled = false;
            }
            if (parduotuve.SelectedIndex == 2)
            {
                tvarkytiDuomenis(2, listViewas, email, komentarai);
                Skaityti("Pigu", PrisijungimoLangas.email, ref balsuIndex, ref balsai, ref balsavusiuSk);
                ParduotuvesImg.Source = new BitmapImage(new Uri("Nuotraukos/avitela.png", UriKind.RelativeOrAbsolute));
                var calc = (double)balsai / (3 * balsavusiuSk);
                ivertinimas.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                parduotuve.IsEnabled = false;
            }
            if (parduotuve.SelectedIndex == 3)
            {
                tvarkytiDuomenis(3, listViewas, email, komentarai);
                Skaityti("Barbora", PrisijungimoLangas.email, ref balsuIndex, ref balsai, ref balsavusiuSk);
                ParduotuvesImg.Source = new BitmapImage(new Uri("Nuotraukos/avitela.png", UriKind.RelativeOrAbsolute));
                var calc = (double)balsai / (3 * balsavusiuSk);
                ivertinimas.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                parduotuve.IsEnabled = false;
            }
            if (parduotuve.SelectedIndex == 4)
            {
                tvarkytiDuomenis(4, listViewas, email, komentarai);
                Skaityti("Bigbox", PrisijungimoLangas.email, ref balsuIndex, ref balsai, ref balsavusiuSk);
                ParduotuvesImg.Source = new BitmapImage(new Uri("Nuotraukos/avitela.png", UriKind.RelativeOrAbsolute));
                var calc = (double)balsai / (3 * balsavusiuSk);
                ivertinimas.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                parduotuve.IsEnabled = false;
            }
            if (parduotuve.SelectedIndex == 5)
            {
                tvarkytiDuomenis(5, listViewas, email, komentarai);
                Skaityti("Rde", PrisijungimoLangas.email, ref balsuIndex, ref balsai, ref balsavusiuSk);
                ParduotuvesImg.Source = new BitmapImage(new Uri("Nuotraukos/avitela.png", UriKind.RelativeOrAbsolute));
                var calc = (double)balsai / (3 * balsavusiuSk);
                ivertinimas.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                parduotuve.IsEnabled = false;
            }
            if (parduotuve.SelectedIndex == 6)
            {
                tvarkytiDuomenis(6, listViewas, email, komentarai);
                Skaityti("GintarineVaistine", PrisijungimoLangas.email, ref balsuIndex, ref balsai, ref balsavusiuSk);
                ParduotuvesImg.Source = new BitmapImage(new Uri("Nuotraukos/avitela.png", UriKind.RelativeOrAbsolute));
                var calc = (double)balsai / (3 * balsavusiuSk);
                ivertinimas.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                parduotuve.IsEnabled = false;
            }
        }

        private void VertintiClick(object sender, RoutedEventArgs e)
        {
            if (parduotuve.SelectedIndex == -1)
            {
                MessageBox.Show("Turite pasirinkti parduotuvę.");
            }
            else if (rating["Aptarnavimas"] == 0 || rating["PrekiuKokybe"] == 0 || rating["Pristatymas"] == 0)
            {
                MessageBox.Show("Turite pažymėti vertinimą prie visų kriterijų.");
            }
            else
            {
                if (parduotuve.SelectedIndex == 0 && !balsuIndex.Contains("_0"))
                {
                    //Rating
                    balsavusiuSk++;
                    balsai += rating["Aptarnavimas"] + rating["PrekiuKokybe"] + rating["Pristatymas"];
                    vertinimai["Avitela"]["Aptarnavimas"] = rating["Aptarnavimas"];
                    vertinimai["Avitela"]["PrekiuKokybe"] = rating["PrekiuKokybe"];
                    vertinimai["Avitela"]["Pristatymas"] = rating["Pristatymas"];
                    var calc = (double)balsai / (3 * balsavusiuSk);
                    balsuIndex += "_0";
                    Rasyti("Avitela", balsuIndex, PrisijungimoLangas.email, balsai, balsavusiuSk);
                    Atstatyti(calc);
                    //Comment
                    RasytiKomentarus(PrisijungimoLangas.email, 0, vertinimai["Avitela"]["Aptarnavimas"], vertinimai["Avitela"]["PrekiuKokybe"], vertinimai["Avitela"]["Pristatymas"], KomentaruLangelis.Text);
                    KomentaruLangelis.Clear();
                    SkaitytiKomentaruDuomenis(ref email, ref komentarai);
                    sujungtiList(email, komentarai);
                    tvarkytiDuomenis(0, listViewas, email, komentarai);

                }
                else if (parduotuve.SelectedIndex == 1 && !balsuIndex.Contains("_1"))
                {
                    //Rating
                    balsavusiuSk++;
                    balsai += rating["Aptarnavimas"] + rating["PrekiuKokybe"] + rating["Pristatymas"];
                    vertinimai["Elektromarkt"]["Aptarnavimas"] = rating["Aptarnavimas"];
                    vertinimai["Elektromarkt"]["PrekiuKokybe"] = rating["PrekiuKokybe"];
                    vertinimai["Elektromarkt"]["Pristatymas"] = rating["Pristatymas"];
                    var calc = (double)balsai / (3 * balsavusiuSk);
                    balsuIndex += "_1";
                    Rasyti("Elektromarkt", balsuIndex, PrisijungimoLangas.email, balsai, balsavusiuSk);
                    Atstatyti(calc);
                    //Comment
                    RasytiKomentarus(PrisijungimoLangas.email, 1, vertinimai["Elektromarkt"]["Aptarnavimas"], vertinimai["Elektromarkt"]["PrekiuKokybe"], vertinimai["Elektromarkt"]["Pristatymas"], KomentaruLangelis.Text);
                    KomentaruLangelis.Clear();
                    SkaitytiKomentaruDuomenis(ref email, ref komentarai);
                    sujungtiList(email, komentarai);
                    tvarkytiDuomenis(1, listViewas, email, komentarai);
                }
                else if (parduotuve.SelectedIndex == 2 && !balsuIndex.Contains("_2"))
                {
                    //Rating
                    balsavusiuSk++;
                    balsai += rating["Aptarnavimas"] + rating["PrekiuKokybe"] + rating["Pristatymas"];
                    vertinimai["Pigu"]["Aptarnavimas"] = rating["Aptarnavimas"];
                    vertinimai["Pigu"]["PrekiuKokybe"] = rating["PrekiuKokybe"];
                    vertinimai["Pigu"]["Pristatymas"] = rating["Pristatymas"];
                    var calc = (double)balsai / (3 * balsavusiuSk);
                    balsuIndex += "_2";
                    Rasyti("Pigu", balsuIndex, PrisijungimoLangas.email, balsai, balsavusiuSk);
                    Atstatyti(calc);
                    //Comment
                    RasytiKomentarus(PrisijungimoLangas.email, 2, vertinimai["Pigu"]["Aptarnavimas"], vertinimai["Pigu"]["PrekiuKokybe"], vertinimai["Pigu"]["Pristatymas"], KomentaruLangelis.Text);
                    KomentaruLangelis.Clear();
                    SkaitytiKomentaruDuomenis(ref email, ref komentarai);
                    sujungtiList(email, komentarai);
                    tvarkytiDuomenis(2, listViewas, email, komentarai);
                }
                else if (parduotuve.SelectedIndex == 3 && !balsuIndex.Contains("_3"))
                {
                    //Rating
                    balsavusiuSk++;
                    balsai += rating["Aptarnavimas"] + rating["PrekiuKokybe"] + rating["Pristatymas"];
                    vertinimai["Barbora"]["Aptarnavimas"] = rating["Aptarnavimas"];
                    vertinimai["Barbora"]["PrekiuKokybe"] = rating["PrekiuKokybe"];
                    vertinimai["Barbora"]["Pristatymas"] = rating["Pristatymas"];
                    var calc = (double)balsai / (3 * balsavusiuSk);
                    balsuIndex += "_3";
                    Rasyti("Barbora", balsuIndex, PrisijungimoLangas.email, balsai, balsavusiuSk);
                    Atstatyti(calc);
                    //Comment
                    RasytiKomentarus(PrisijungimoLangas.email, 3, vertinimai["Barbora"]["Aptarnavimas"], vertinimai["Barbora"]["PrekiuKokybe"], vertinimai["Barbora"]["Pristatymas"], KomentaruLangelis.Text);
                    KomentaruLangelis.Clear();
                    SkaitytiKomentaruDuomenis(ref email, ref komentarai);
                    sujungtiList(email, komentarai);
                    tvarkytiDuomenis(3, listViewas, email, komentarai);
                }
                else if (parduotuve.SelectedIndex == 4 && !balsuIndex.Contains("_4"))
                {
                    //Rating
                    balsavusiuSk++;
                    balsai += rating["Aptarnavimas"] + rating["PrekiuKokybe"] + rating["Pristatymas"];
                    vertinimai["Bigbox"]["Aptarnavimas"] = rating["Aptarnavimas"];
                    vertinimai["Bigbox"]["PrekiuKokybe"] = rating["PrekiuKokybe"];
                    vertinimai["Bigbox"]["Pristatymas"] = rating["Pristatymas"];
                    var calc = (double)balsai / (3 * balsavusiuSk);
                    balsuIndex += "_4";
                    Rasyti("Bigbox", balsuIndex, PrisijungimoLangas.email, balsai, balsavusiuSk);
                    Atstatyti(calc);
                    //Comment
                    RasytiKomentarus(PrisijungimoLangas.email, 4, vertinimai["Bigbox"]["Aptarnavimas"], vertinimai["Bigbox"]["PrekiuKokybe"], vertinimai["Bigbox"]["Pristatymas"], KomentaruLangelis.Text);
                    KomentaruLangelis.Clear();
                    SkaitytiKomentaruDuomenis(ref email, ref komentarai);
                    sujungtiList(email, komentarai);
                    tvarkytiDuomenis(4, listViewas, email, komentarai);
                }
                else if (parduotuve.SelectedIndex == 5 && !balsuIndex.Contains("_5"))
                {
                    //Rating
                    balsavusiuSk++;
                    balsai += rating["Aptarnavimas"] + rating["PrekiuKokybe"] + rating["Pristatymas"];
                    vertinimai["Rde"]["Aptarnavimas"] = rating["Aptarnavimas"];
                    vertinimai["Rde"]["PrekiuKokybe"] = rating["PrekiuKokybe"];
                    vertinimai["Rde"]["Pristatymas"] = rating["Pristatymas"];
                    var calc = (double)balsai / (3 * balsavusiuSk);
                    balsuIndex += "_5";
                    Rasyti("Rde", balsuIndex, PrisijungimoLangas.email, balsai, balsavusiuSk);
                    Atstatyti(calc);
                    //Comment
                    RasytiKomentarus(PrisijungimoLangas.email, 5, vertinimai["Rde"]["Aptarnavimas"], vertinimai["Rde"]["PrekiuKokybe"], vertinimai["Rde"]["Pristatymas"], KomentaruLangelis.Text);
                    KomentaruLangelis.Clear();
                    SkaitytiKomentaruDuomenis(ref email, ref komentarai);
                    sujungtiList(email, komentarai);
                    tvarkytiDuomenis(5, listViewas, email, komentarai);
                }
                else if (parduotuve.SelectedIndex == 6 && !balsuIndex.Contains("_6"))
                {
                    //Rating
                    balsavusiuSk++;
                    balsai += rating["Aptarnavimas"] + rating["PrekiuKokybe"] + rating["Pristatymas"];
                    vertinimai["GintarineVaistine"]["Aptarnavimas"] = rating["Aptarnavimas"];
                    vertinimai["GintarineVaistine"]["PrekiuKokybe"] = rating["PrekiuKokybe"];
                    vertinimai["GintarineVaistine"]["Pristatymas"] = rating["Pristatymas"];
                    var calc = (double)balsai / (3 * balsavusiuSk);
                    balsuIndex += "_6";
                    Rasyti("GintarineVaistine", balsuIndex, PrisijungimoLangas.email, balsai, balsavusiuSk);
                    Atstatyti(calc);
                    //Comment
                    RasytiKomentarus(PrisijungimoLangas.email, 6, vertinimai["GintarineVaistine"]["Aptarnavimas"], vertinimai["GintarineVaistine"]["PrekiuKokybe"], vertinimai["GintarineVaistine"]["Pristatymas"], KomentaruLangelis.Text);
                    KomentaruLangelis.Clear();
                    SkaitytiKomentaruDuomenis(ref email, ref komentarai);
                    sujungtiList(email, komentarai);
                    tvarkytiDuomenis(6, listViewas, email, komentarai);
                }
                else
                {
                    MessageBox.Show("Jau esate palikęs atsiliepimą už šią parduotuvę!");
                    parduotuve.IsEnabled = true;
                    parduotuve.SelectedIndex = -1;
                    KomentaruLangelis.Clear();
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
                            lv.Items.Add(new Komentaras() { Tekstas = emailas + " " + anotherTempString[2] + " Apt.: " + anotherTempString[3] + " Pr. Kok.: " + anotherTempString[4] + " Prist.: " + anotherTempString[5]});
                            if (!string.IsNullOrWhiteSpace(anotherTempString[6]))
                            {
                                lv.Items.Add(new Komentaras() { Tekstas = anotherTempString[6] });
                            }

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
        private static void Skaityti(string shopName, string email, ref string haveVoted, ref int votesNumber, ref int votersNumber)
        {
            using (var context = new DuomenuBazesKontekstas())
            {
                var result = context.ShopRatingTable.SingleOrDefault(c => c.ShopName == shopName);

                if (result != null)
                {
                    votesNumber = result.VotesNumber;
                    votersNumber = result.VotersNumber;
                }
            }

            using (var context = new DuomenuBazesKontekstas())
            {
                var result = context.NaudotojoDuomenys.SingleOrDefault(c => c.Email == email);

                if (result != null)
                {
                    haveVoted = result.ArBalsavo;
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

        private static void Rasyti(string shopName, string haveVoted, string email, int votesNumber, int votersNumber)
        {
            using (var context = new DuomenuBazesKontekstas())
            {
                var result = context.ShopRatingTable.SingleOrDefault(b => b.ShopName == shopName);
                if (result != null)
                {
                    result.VotersNumber = votersNumber;
                    result.VotesNumber = votesNumber;
                    context.SaveChanges();
                }
            }

            using (var context = new DuomenuBazesKontekstas())
            {
                var result = context.NaudotojoDuomenys.SingleOrDefault(b => b.Email == email);
                if (result != null)
                {
                    result.ArBalsavo = haveVoted;
                    context.SaveChanges();
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