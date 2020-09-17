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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Price_comparison_engine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DUKMygtukas_Click(object sender, RoutedEventArgs e)
        {
            DUK_Langas dukLangoAtidarymas = new DUK_Langas();
            dukLangoAtidarymas.Show();
        }

        private void KontaktaiMygtukas_Click(object sender, RoutedEventArgs e)
        {
            KontaktuLangas kontaktuLangoAtidarymas = new KontaktuLangas();
            kontaktuLangoAtidarymas.Show();
        }

        private void Ieškoti_Click(object sender, RoutedEventArgs e)
        {
            PrekiųLangas prekiųLangoAtidarymas = new PrekiųLangas();
            prekiųLangoAtidarymas.Show();
        }

        private void RegistruotisMygtukas_Click(object sender, RoutedEventArgs e)
        {
            RegistracijosLangas registracijosLangoAtidarymas = new RegistracijosLangas();
            registracijosLangoAtidarymas.Show();
        }

        private void PrisijungtiMygtukas_Click(object sender, RoutedEventArgs e)
        {
            PrisijungimoLangas prisijungimoLangoAtidarymas = new PrisijungimoLangas();
            prisijungimoLangoAtidarymas.Show();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            viršutinėLinija.Width = this.ActualWidth;
            vidurinėLinija.Width = this.ActualWidth;
            //pasiūlymaiLinija.Width = this.ActualWidth;
            apatinėLinija.Width = this.ActualWidth;

            /*
            viršutinėLinija.Height = this.ActualHeight/11;
            vidurinėLinija.Height = this.ActualHeight/4;
            pasiūlymaiLinija.Height = this.ActualHeight/4;
            apatinėLinija.Height = this.ActualHeight/4;
            */

            double skirtumasPlocioqqq = this.ActualHeight / 1.7;

            rectangleResize(vidurinėLinija, skirtumasPlocioqqq);

            double skirtumasPlocio = this.ActualWidth / 1.2;
            double skirtumasIlgio = this.ActualHeight / 1.1;
            mygtukoResize(prisijungimosMygtukas, skirtumasPlocio, skirtumasIlgio);
            mygtukoResize(registracijosMygtukas, skirtumasPlocio, skirtumasIlgio);
            mygtukoResize(DUKMygtukas, skirtumasPlocio, skirtumasIlgio);
            mygtukoResize(kontaktuMygtukas, skirtumasPlocio, skirtumasIlgio);
            mygtukoResize(Ieškoti, skirtumasPlocio, skirtumasIlgio);
            textBoxResize(ieškojimoLaukas, skirtumasPlocio/3, skirtumasIlgio);
        }

        private void mygtukoResize(Button mygtukas, double plotis, double ilgis)
        {
            mygtukas.Width = this.ActualWidth - plotis;
            mygtukas.Height = this.ActualHeight - ilgis;
        }

        private void textBlockResize(TextBlock tekstoBlokas, double plotis, double ilgis)
        {
            tekstoBlokas.Width = this.ActualWidth - plotis;
            tekstoBlokas.Height = this.ActualHeight - ilgis;
        }

        private void textBoxResize(TextBox tekstBoksas, double plotis, double ilgis)
        {
            tekstBoksas.Width = this.ActualWidth - plotis;
            tekstBoksas.Height = this.ActualHeight - ilgis;
        }
        private void rectangleResize(Rectangle a, double plotis)
        {
            a.Height = this.ActualHeight - plotis;
        }
    }
}
