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

        private static Dictionary<string, double> vertinimai = new Dictionary<string, double>()
        {
            {"Avitela", 0},
            {"Elektromarkt", 0},
            {"Pigu.lt", 0}
        };

        private void Parduotuve(object sender, SelectionChangedEventArgs e)
        {
            if(parduotuve.SelectedIndex == 0)
            {
                tvarkytiDuomenis(0, listViewas, email, komentarai);
                Skaityti("Avitela", PrisijungimoLangas.email, ref balsuIndex, ref balsai, ref balsavusiuSk);
                ParduotuvesImg.Source = new BitmapImage(new Uri("Nuotraukos/avitela.png", UriKind.RelativeOrAbsolute));
                var calc = balsai / (3 * balsavusiuSk);
                KeistiImg(calc);
                ivertinimas.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";

                parduotuve.IsEnabled = false;
            }
            if (parduotuve.SelectedIndex == 1)
            {
                tvarkytiDuomenis(1, listViewas, email, komentarai);
                Skaityti("Elektromarkt", PrisijungimoLangas.email, ref balsuIndex, ref balsai, ref balsavusiuSk);
                ParduotuvesImg.Source = new BitmapImage(new Uri("Nuotraukos/elektromarkt.png", UriKind.RelativeOrAbsolute));
                var calc = balsai / (3 * balsavusiuSk);
                KeistiImg(calc);
                ivertinimas.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                parduotuve.IsEnabled = false;
            }
            if (parduotuve.SelectedIndex == 2)
            {
                tvarkytiDuomenis(2, listViewas, email, komentarai);
                Skaityti("Pigu.lt", PrisijungimoLangas.email, ref balsuIndex, ref balsai, ref balsavusiuSk);
                ParduotuvesImg.Source = new BitmapImage(new Uri("Nuotraukos/avitela.png", UriKind.RelativeOrAbsolute));
                var calc = balsai / (3 * balsavusiuSk);
                KeistiImg(calc);
                ivertinimas.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                parduotuve.IsEnabled = false;
            }
        }
        private void Aptarnavimas(object sender, SelectionChangedEventArgs e)
        {
            //vertinimai["Avitela"] += aptarnavimas.SelectedIndex + 1;
            //vertinimai["Elektromarkt"] += aptarnavimas.SelectedIndex + 1;
            //vertinimai["Pigu.lt"] += aptarnavimas.SelectedIndex + 1;
            //balsai += aptarnavimas.SelectedIndex + 1;
            //aptarnavimas.IsEnabled = false;
        }

        private void Kokybe(object sender, SelectionChangedEventArgs e)
        {
            //vertinimai["Avitela"] += kokybe.SelectedIndex + 1;
            //vertinimai["Elektromarkt"] += kokybe.SelectedIndex + 1;
            //vertinimai["Pigu.lt"] += kokybe.SelectedIndex + 1;
            //balsai += kokybe.SelectedIndex + 1;
            //kokybe.IsEnabled = false;
        }

        private void Pristatymas(object sender, SelectionChangedEventArgs e)
        {
            //vertinimai["Avitela"] += pristatymas.SelectedIndex + 1;
            //vertinimai["Elektromarkt"] += pristatymas.SelectedIndex + 1;
            //vertinimai["Pigu.lt"] += pristatymas.SelectedIndex + 1;
            //balsai += pristatymas.SelectedIndex + 1;
            //pristatymas.IsEnabled = false;
        }
        private void Vertinti(object sender, RoutedEventArgs e)
        {
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
                    RasytiKomentarus(PrisijungimoLangas.email, 0, vertinimai["Avitela"]/3, KomentaruLangelis.Text);
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
                    RasytiKomentarus(PrisijungimoLangas.email, 1, vertinimai["Elektromarkt"]/3, KomentaruLangelis.Text);
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
                    RasytiKomentarus(PrisijungimoLangas.email, 2, vertinimai["Pigu.lt"]/3, KomentaruLangelis.Text);
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
                            lv.Items.Add(new Komentaras() { Tekstas = emailas + " " + anotherTempString[2] + " " + anotherTempString[3]});
                            lv.Items.Add(new Komentaras() { Tekstas = anotherTempString[4]});

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
            //if (calc < 0.5)
            //{
            //    ivertinimoImg.Source = new BitmapImage(new Uri("Nuotraukos/0.png", UriKind.Relative));
            //}
            //else if (calc < 1.5)
            //{
            //    ivertinimoImg.Source = new BitmapImage(new Uri("Nuotraukos/11.png", UriKind.Relative));
            //}
            //else if (calc < 2.5)
            //{
            //    ivertinimoImg.Source = new BitmapImage(new Uri("Nuotraukos/22.png", UriKind.Relative));
            //}
            //else if (calc < 3.5)
            //{
            //    ivertinimoImg.Source = new BitmapImage(new Uri("Nuotraukos/33.png", UriKind.Relative));
            //}
            //else if (calc < 4.5)
            //{
            //    ivertinimoImg.Source = new BitmapImage(new Uri("Nuotraukos/4.png", UriKind.Relative));
            //}
            //else
            //{
            //    ivertinimoImg.Source = new BitmapImage(new Uri("Nuotraukos/5.png", UriKind.RelativeOrAbsolute));
            //}
        }
        private void Atstatyti(double calc)
        {
            ivertinimas.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
            KeistiImg(calc);
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
        private bool CheckImgSource(Button button, string imgSrc, string imgToChangeSrc)
        {
            var imgBool = false;
            var ct = button.Template;
            var btnImageName = ct.FindName(imgSrc, button).ToString();
            if (btnImageName.Equals(imgToChangeSrc))
            {
                imgBool = true;
            }
            return imgBool;

        }
        private void AptarnavimasStar1(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Nuotraukos/Star_1.png");
        }

        private void AptarnavimasStar2(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Nuotraukos/Star_1.png");
        }

        private void AptarnavimasStar3(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(Aptarnavimas3, "AptarnavimasImg3", "Nuotraukos/Star_1.png");
        }

        private void AptarnavimasStar4(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(Aptarnavimas3, "AptarnavimasImg3", "Nuotraukos/Star_1.png");
            ChangeImgSource(Aptarnavimas4, "AptarnavimasImg4", "Nuotraukos/Star_1.png");
        }

        private void AptarnavimasStar5(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(Aptarnavimas3, "AptarnavimasImg3", "Nuotraukos/Star_1.png");
            ChangeImgSource(Aptarnavimas4, "AptarnavimasImg4", "Nuotraukos/Star_1.png");
            ChangeImgSource(Aptarnavimas5, "AptarnavimasImg5", "Nuotraukos/Star_1.png");
        }

        private void PrekiuKokybeStar1(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Nuotraukos/Star_1.png");
        }

        private void PrekiuKokybeStar2(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Nuotraukos/Star_1.png");
        }

        private void PrekiuKokybeStar3(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(PrekiuKokybe3, "PrekiuKokybeImg3", "Nuotraukos/Star_1.png");
        }

        private void PrekiuKokybeStar4(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(PrekiuKokybe3, "PrekiuKokybeImg3", "Nuotraukos/Star_1.png");
            ChangeImgSource(PrekiuKokybe4, "PrekiuKokybeImg4", "Nuotraukos/Star_1.png");
        }

        private void PrekiuKokybeStar5(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(PrekiuKokybe3, "PrekiuKokybeImg3", "Nuotraukos/Star_1.png");
            ChangeImgSource(PrekiuKokybe4, "PrekiuKokybeImg4", "Nuotraukos/Star_1.png");
            ChangeImgSource(PrekiuKokybe5, "PrekiuKokybeImg5", "Nuotraukos/Star_1.png");
        }

        private void PristatymasStar1(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Pristatymas1, "PristatymasImg1", "Nuotraukos/Star_1.png");
        }

        private void PristatymasStar2(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Pristatymas1, "PristatymasImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(Pristatymas2, "PristatymasImg2", "Nuotraukos/Star_1.png");
        }

        private void PristatymasStar3(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Pristatymas1, "PristatymasImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(Pristatymas2, "PristatymasImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(Pristatymas3, "PristatymasImg3", "Nuotraukos/Star_1.png");
        }

        private void PristatymasStar4(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Pristatymas1, "PristatymasImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(Pristatymas2, "PristatymasImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(Pristatymas3, "PristatymasImg3", "Nuotraukos/Star_1.png");
            ChangeImgSource(Pristatymas4, "PristatymasImg4", "Nuotraukos/Star_1.png");
        }

        private void PristatymasStar5(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Pristatymas1, "PristatymasImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(Pristatymas2, "PristatymasImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(Pristatymas3, "PristatymasImg3", "Nuotraukos/Star_1.png");
            ChangeImgSource(Pristatymas4, "PristatymasImg4", "Nuotraukos/Star_1.png");
            ChangeImgSource(Pristatymas5, "PristatymasImg5", "Nuotraukos/Star_1.png");
        }

        private void AptarnavimasImg1_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Aptarnavimas1, "AptarnavimasImg1", "Nuotraukos/Star_1.png"))
            {
                ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Nuotraukos/Star_2.png");
            }
        }

        private void AptarnavimasImg1_OnMouseLeave(object sender, MouseEventArgs e)
        {
            ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Nuotraukos/Star_0.png");
        }
    }
}