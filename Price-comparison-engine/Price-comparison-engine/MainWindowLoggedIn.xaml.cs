using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;
using Price_comparison_engine.Klases;

namespace Price_comparison_engine
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    /// DataGridLoggedIn_Initialized
    public partial class MainWindowLoggedIn : Window
    {
        public void DataDirectoryInitialize()
        {
            var enviroment = System.Environment.CurrentDirectory;
            var projectDirectory = Directory.GetParent(enviroment).Parent.FullName;
            AppDomain.CurrentDomain.SetData("DataDirectory", projectDirectory);
        }
        public MainWindowLoggedIn()
        {
            InitializeComponent();
            if (string.IsNullOrWhiteSpace(PrisijungimoLangas.email))
            {
                VertinimoMygtukas.IsEnabled = false;
                VertinimoMygtukas.Visibility = Visibility.Collapsed;
            }
            else
            {
                VertinimoMygtukas.IsEnabled = true;
                VertinimoMygtukas.Visibility = Visibility.Visible;
            }
            if (vartotojoRole.Equals("1"))
            {
                administravimas.Visibility = Visibility.Visible;
            }
            DataDirectoryInitialize();
            Skaityti(ref puslapioUrl, ref imgUrl);
            if (puslapioUrl.Count >= 3 && imgUrl.Count >= 3)
            {
                img1.Source = new BitmapImage(new Uri(imgUrl[0], UriKind.Absolute));
                img2.Source = new BitmapImage(new Uri(imgUrl[1], UriKind.Absolute));
                img3.Source = new BitmapImage(new Uri(imgUrl[2], UriKind.Absolute));
            }
        }

        private static readonly string vartotojoRole = PrisijungimoLangas.Role;

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
           /* var skirtumasPlocio = this.ActualWidth / 1.2;
            var skirtumasIlgio = this.ActualHeight / 1.1;
            var skirtumasPlocioBlokeliui = this.ActualHeight / 1.05;
            var skirtumasPlocioNuotraukai = this.ActualWidth / 1.2;
            var skirtumasIlgioNuotraukai = this.ActualHeight / 1.2;
            MygtukoResize(Ieškoti, skirtumasPlocio, skirtumasIlgio);
            TextBoxResize(ieškojimoLaukas, skirtumasPlocio / 3, skirtumasIlgio);
            RectangleIštempimas(viršutinėlinija);
            RectangleIštempimas(vidurinėLinija);
            //RectangleResize(vidurinėLinija, skirtumasPlocioBlokeliui);
            RectangleIštempimas(vidurinėLinija2);
            //RectangleResize(vidurinėLinija2, skirtumasPlocioBlokeliui);
            RectangleIštempimas(apatinėLinija);
            SlideShowResize(img1_2, skirtumasPlocioNuotraukai, skirtumasIlgioNuotraukai);
            SlideShowResize(img2_2, skirtumasPlocioNuotraukai, skirtumasIlgioNuotraukai);
            SlideShowResize(img3_2, skirtumasPlocioNuotraukai, skirtumasIlgioNuotraukai);
            SlideShowResize(iKairePuse1, skirtumasIlgio, skirtumasIlgio);
            SlideShowResize(iDesinePuse1, skirtumasIlgio, skirtumasIlgio);*/
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
        private static async void GetHtmlAssync(DataGrid DataGridLoggedIn)
        {
        }

        private void RectangleResize(Rectangle plotelis, double ilgis)
        {
            plotelis.Height = this.ActualHeight - ilgis;
        }

        private void MygtukoResize(Button mygtukas, double plotis, double ilgis)
        {
            mygtukas.Width = this.ActualWidth - plotis;
            mygtukas.Height = this.ActualHeight - ilgis;
        }

        private void TextBoxResize(TextBox tekstBoksas, double plotis, double ilgis)
        {
            tekstBoksas.Width = this.ActualWidth - plotis;
            tekstBoksas.Height = this.ActualHeight - ilgis;
        }

        private void RectangleIštempimas(Rectangle plotelis)
        {
            plotelis.Width = this.ActualWidth;
        }

        private void SlideShowResize(Image nuotrauka, double plotis, double ilgis)
        {
            nuotrauka.Width = this.ActualWidth - plotis;
            nuotrauka.Height = this.ActualHeight - ilgis;
        }


        public static int slideCounter = 1;
        public static int slideCounter2 = 3;
        public static int slideCounter_2 = 1;
        public static int slideCounter2_2 = 3;


        private void AtsijungimoMygtukas_Click(object sender, RoutedEventArgs e)
        {
            PrisijungimoLangas.email = "";
            var pagrindinisLangas = new MainWindow();
            pagrindinisLangas.Show();
            this.Close();
        }

        private void Ieškoti_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.zodis = ieškojimoLaukas.Text;
            var prekiųLangoAtidarymas = new PrekiuLangas();
            prekiųLangoAtidarymas.Show();
        }

        private void AdminPrisijungimas(object sender, RoutedEventArgs e)
        {
                var adminLangoAtidarymas = new Admin();
                adminLangoAtidarymas.Show();
        }

        private void DataGridas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ImageClick_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var name = (((Image)sender).DataContext as Item)?.Name;
            var langas = new KonkretiPreke(name);
            langas.Show();
        }
        private void VertintiClick(object sender, RoutedEventArgs e)
        {
            var vertinimoLangoAtidarymas = new VertinimoLangas();
            vertinimoLangoAtidarymas.Show();
        }

        private void LinkButton_Click(object sender, RoutedEventArgs e)
        {
            var link = (((Button)sender).DataContext as Item)?.Link;
            if (link != null) Process.Start(link);
        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void DataGridLoggedIn_Initialized(object sender, EventArgs e)
        {
            GetHtmlAssync(DataGridLoggedIn);
        }
    }
}
    
