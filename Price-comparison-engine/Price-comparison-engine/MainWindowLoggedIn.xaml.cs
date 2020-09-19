using System;
using System.Collections.Generic;
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
    public partial class MainWindowLogedIn : Window
    {
        public MainWindowLogedIn()
        {
            InitializeComponent();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double skirtumasPlocio = this.ActualWidth / 1.2;
            double skirtumasIlgio = this.ActualHeight / 1.1;
            double skirtumasPlocioBlokeliui = this.ActualHeight / 1.05;
            mygtukoResize(Ieškoti, skirtumasPlocio, skirtumasIlgio);
            textBoxResize(ieškojimoLaukas, skirtumasPlocio / 3, skirtumasIlgio);
            rectangleIštempimas(viršutinėlinija);
            rectangleIštempimas(vidurinėLinija);
            rectangleResize(vidurinėLinija, skirtumasPlocioBlokeliui);
            rectangleIštempimas(vidurinėLinija2);
            rectangleResize(vidurinėLinija2, skirtumasPlocioBlokeliui);
            rectangleIštempimas(apatinėLinija);
        }

        private void rectangleResize(Rectangle plotelis, double ilgis)
        {
            plotelis.Height = this.ActualHeight - ilgis;
        }

        private void mygtukoResize(Button mygtukas, double plotis, double ilgis)
        {
            mygtukas.Width = this.ActualWidth - plotis;
            mygtukas.Height = this.ActualHeight - ilgis;
        }

        private void textBoxResize(TextBox tekstBoksas, double plotis, double ilgis)
        {
            tekstBoksas.Width = this.ActualWidth - plotis;
            tekstBoksas.Height = this.ActualHeight - ilgis;
        }

        private void rectangleIštempimas(Rectangle plotelis)
        {
            plotelis.Width = this.ActualWidth;
        }
        public static int slideCounter = 1;
        public static int slideCounter2 = 3;
        public static int slideCounter_2 = 1;
        public static int slideCounter2_2 = 3;
        private void Slider_Back(object sender, MouseButtonEventArgs e)
        {
            if (slideCounter > 3)
            {
                slideCounter = 1;
            }
            img1.Source = img2.Source;
            img2.Source = img3.Source;
            img3.Source = new BitmapImage(new Uri("Nuotraukos/" + slideCounter + ".jpg", UriKind.RelativeOrAbsolute));
            slideCounter2 = slideCounter;
            slideCounter++;
        }

        private void Slider_Front(object sender, MouseButtonEventArgs e)
        {
            if (slideCounter2 <= 0)
            {
                slideCounter2 = 3;
            }
            img3.Source = img2.Source;
            img2.Source = img1.Source;
            img1.Source = new BitmapImage(new Uri("Nuotraukos/" + slideCounter2 + ".jpg", UriKind.RelativeOrAbsolute));
            slideCounter = slideCounter2;
            slideCounter2--;
        }
        

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

        private void atsijungimoMygtukas_Click(object sender, RoutedEventArgs e)
        {
            MainWindow pagrindinisLangas = new MainWindow();
            pagrindinisLangas.Show();
            this.Close();
        }
    }
}
    
