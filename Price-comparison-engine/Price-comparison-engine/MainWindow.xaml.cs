using Price_comparison_engine.Klases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Price_comparison_engine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public void DataDirectoryInitialize()
        {
            var enviroment = System.Environment.CurrentDirectory;
            var projectDirectory = Directory.GetParent(enviroment).Parent.FullName;
            AppDomain.CurrentDomain.SetData("DataDirectory", projectDirectory);
        }

        public MainWindow()
        {
            InitializeComponent();
            DataDirectoryInitialize();
            Skaityti(ref puslapioUrl, ref imgUrl);
            if (puslapioUrl.Count >= 3 && imgUrl.Count >= 3)
            {
                img1.Source = new BitmapImage(new Uri(imgUrl[0], UriKind.Absolute));
                img2.Source = new BitmapImage(new Uri(imgUrl[1], UriKind.Absolute));
                img3.Source = new BitmapImage(new Uri(imgUrl[2], UriKind.Absolute));
            }
        }

        private void DUKMygtukas_Click(object sender, RoutedEventArgs e)
        {
            var dukLangoAtidarymas = new DUK_Langas();
            dukLangoAtidarymas.Show();
        }

        private void KontaktaiMygtukas_Click(object sender, RoutedEventArgs e)
        {
            var kontaktuLangoAtidarymas = new KontaktuLangas();
            kontaktuLangoAtidarymas.Show();
        }

        public  static string zodis;

        private void Ieškoti_Click(object sender, RoutedEventArgs e)
        {
            zodis = ieskojimoLaukas.Text;
            var prekiųLangoAtidarymas = new PrekiuLangas();
            prekiųLangoAtidarymas.Show();
        }

        private void RegistruotisMygtukas_Click(object sender, RoutedEventArgs e)
        {
            var registracijosLangoAtidarymas = new RegistracijosLangas(this);
            registracijosLangoAtidarymas.Show();
        }

        private void PrisijungtiMygtukas_Click(object sender, RoutedEventArgs e)
        {
            var prisijungimoLangoAtidarymas = new PrisijungimoLangas(this);
            prisijungimoLangoAtidarymas.Show();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var skirtumasPlocio = this.ActualWidth / 1.2;
            var skirtumasIlgio = this.ActualHeight / 1.1;
            var skirtumasPlocioBlokeliui = this.ActualHeight / 1.7;
            var skirtumasPlocioNuotraukai = this.ActualWidth / 1.4;
            var skirtumasIlgioNuotraukai = this.ActualHeight / 1.4;
            MygtukoResize(prisijungimosMygtukas, skirtumasPlocio, skirtumasIlgio);
            MygtukoResize(registracijosMygtukas, skirtumasPlocio, skirtumasIlgio);
            MygtukoResize(DUKMygtukas, skirtumasPlocio, skirtumasIlgio);
            MygtukoResize(kontaktuMygtukas, skirtumasPlocio, skirtumasIlgio);
            MygtukoResize(Ieškoti, skirtumasPlocio, skirtumasIlgio);
            TextBoxResize(ieskojimoLaukas, skirtumasPlocio / 3, skirtumasIlgio);
            RectangleResize(vidurinėLinija, skirtumasPlocioBlokeliui);
            RectangleIštempimas(viršutinėLinija);
            RectangleIštempimas(vidurinėLinija);
            RectangleIštempimas(apatinėLinija);
            SlideShowResize(img1, skirtumasPlocioNuotraukai, skirtumasIlgioNuotraukai);
            SlideShowResize(img2, skirtumasPlocioNuotraukai, skirtumasIlgioNuotraukai);
            SlideShowResize(img3, skirtumasPlocioNuotraukai, skirtumasIlgioNuotraukai);
            SlideShowResize(iKairePuse, skirtumasIlgio, skirtumasIlgio);
            SlideShowResize(iDesinePuse, skirtumasIlgio, skirtumasIlgio);
        }

        private void MygtukoResize(Button mygtukas, double plotis, double ilgis)
        {
            mygtukas.Width = this.ActualWidth - plotis;
            mygtukas.Height = this.ActualHeight - ilgis;
        }

        private void TextBlockResize(TextBlock tekstoBlokas, double plotis, double ilgis)
        {
            tekstoBlokas.Width = this.ActualWidth - plotis;
            tekstoBlokas.Height = this.ActualHeight - ilgis;
        }

        private void TextBoxResize(TextBox tekstBoksas, double plotis, double ilgis)
        {
            tekstBoksas.Width = this.ActualWidth - plotis;
            tekstBoksas.Height = this.ActualHeight - ilgis;
        }
        private void RectangleResize(Rectangle plotelis, double ilgis)
        {
            plotelis.Height = this.ActualHeight - ilgis;
        }
        private void RectangleIštempimas(Rectangle plotelis)
        {
            plotelis.Width = this.ActualWidth;
        }
        private void SlideShowResize(Image nuotrauka,double plotis, double ilgis)
        {
            nuotrauka.Width = this.ActualWidth-plotis;
            nuotrauka.Height = this.ActualHeight-ilgis;
        }

        private static List<string> puslapioUrl = new List<string>();
        private static List<string> imgUrl = new List<string>();

        public static int indexFront = 3;
        public static int indexBack = 0;
        public static int urlIndex = 0;

        private void Slider_Back(object sender, MouseButtonEventArgs e)
        {
            if (indexBack > 0)
            {
                urlIndex--;
                indexBack--;
                indexFront--;
                img1.Source = new BitmapImage(new Uri(imgUrl[indexBack], UriKind.Absolute));
                img2.Source = new BitmapImage(new Uri(imgUrl[indexBack + 1], UriKind.Absolute));
                img3.Source = new BitmapImage(new Uri(imgUrl[indexBack + 2], UriKind.Absolute));
            }
        }

        private void Slider_Front(object sender, MouseButtonEventArgs e)
        {
            if (indexFront < puslapioUrl.Count - 1)
            {
                urlIndex++;
                img1.Source = new BitmapImage(new Uri(imgUrl[indexFront - 2], UriKind.Absolute));
                img2.Source = new BitmapImage(new Uri(imgUrl[indexFront - 1], UriKind.Absolute));
                img3.Source = new BitmapImage(new Uri(imgUrl[indexFront], UriKind.Absolute));
                indexFront++;
                indexBack++;
            }
        }

        private void Img1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (puslapioUrl.Count >= 3)
            {
                System.Diagnostics.Process.Start(puslapioUrl[urlIndex]);
            }
        }

        private void Img2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (puslapioUrl.Count >= 3)
            {
                System.Diagnostics.Process.Start(puslapioUrl[urlIndex + 1]);
            }
        }

        private void Img3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (puslapioUrl.Count >= 3)
            {
                System.Diagnostics.Process.Start(puslapioUrl[urlIndex + 2]);
            }
        }

        private static void Skaityti(ref List<string> puslapioUrl, ref List<string> imgUrl)
        {
            using (var kontekstas = new DuomenuBazesKontekstas())
            {
                var tempPuslapioUrl = kontekstas.PuslapiuDuomenys.Select(column => column.PuslapioURL).ToList();
                var tempImgUrl = kontekstas.PuslapiuDuomenys.Select(column => column.ImgURL).ToList();

                if (tempPuslapioUrl != null && tempImgUrl != null)
                {
                    puslapioUrl = tempPuslapioUrl;
                    imgUrl = tempImgUrl;
                }
            }
        }
    }
}