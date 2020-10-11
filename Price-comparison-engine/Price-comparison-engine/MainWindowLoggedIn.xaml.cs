using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    /// DataGridLoggedIn_Initialized
    public partial class MainWindowLoggedIn : Window
    {
        public MainWindowLoggedIn()
        {
            InitializeComponent();
            if(vartotojoRole.Equals("1"))
            {
                administravimas.Visibility = Visibility.Visible;
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


        private void Slider_Back2(object sender, MouseButtonEventArgs e)
        {
            if (slideCounter_2 > 3)
            {
                slideCounter_2 = 1;
            }
            img1_2.Source = img2_2.Source;
            img2_2.Source = img3_2.Source;
            img3_2.Source = new BitmapImage(new Uri("Nuotraukos/" + slideCounter_2 + ".jpg", UriKind.RelativeOrAbsolute));
            slideCounter2_2 = slideCounter_2;
            slideCounter_2++;
        }


        private void Slider_Front2(object sender, MouseButtonEventArgs e)
        {
            if (slideCounter2_2 <= 0)
            {
                slideCounter2_2 = 3;
            }
            img3_2.Source = img2_2.Source;
            img2_2.Source = img1_2.Source;
            img1_2.Source = new BitmapImage(new Uri("Nuotraukos/" + slideCounter2_2 + ".jpg", UriKind.RelativeOrAbsolute));
            slideCounter_2 = slideCounter2_2;
            slideCounter2_2--;
        }

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
        private void Button_Click(object sender, RoutedEventArgs e)
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
    
