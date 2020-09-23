﻿using System;
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
        public static String zodis;
        private void Ieškoti_Click(object sender, RoutedEventArgs e)
        {
            zodis = ieskojimoLaukas.Text;
            PrekiuLangas prekiųLangoAtidarymas = new PrekiuLangas();
            prekiųLangoAtidarymas.Show();
            this.Close();
        }

        private void RegistruotisMygtukas_Click(object sender, RoutedEventArgs e)
        {
            RegistracijosLangas registracijosLangoAtidarymas = new RegistracijosLangas(this);
            registracijosLangoAtidarymas.Show();
        }

        private void PrisijungtiMygtukas_Click(object sender, RoutedEventArgs e)
        {
            PrisijungimoLangas prisijungimoLangoAtidarymas = new PrisijungimoLangas(this);
            prisijungimoLangoAtidarymas.Show();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double skirtumasPlocio = this.ActualWidth / 1.2;
            double skirtumasIlgio = this.ActualHeight / 1.1;
            double skirtumasPlocioBlokeliui = this.ActualHeight / 1.7;
            double skirtumasPlocioNuotraukai = this.ActualWidth / 1.4;
            double skirtumasIlgioNuotraukai = this.ActualHeight / 1.4;
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

        public static int slideCounter = 1;
        public static int slideCounter2 = 3;

        public static String link1 = "http://www.google.com";
        public static String link2 = "http://www.facebook.com";
        public static String link3 = "http://www.gmail.com";

        private void Slider_Back(object sender, MouseButtonEventArgs e)
        {
            if (slideCounter > 3)
            {
                slideCounter = 1;
            }
            string temporary;
            temporary = link1;

            img1.Source = img2.Source;
            link1 = link2;
            img2.Source = img3.Source;
            link2 = link3;
            img3.Source = new BitmapImage(new Uri("Nuotraukos/" + slideCounter + ".jpg", UriKind.RelativeOrAbsolute));
            link3 = temporary;
            slideCounter2 = slideCounter;
            slideCounter++;
        }

        private void Slider_Front(object sender, MouseButtonEventArgs e)
        {
            if (slideCounter2 <= 0)
            {
                slideCounter2 = 3;
            }
            string temporary;
            temporary = link3;

            img3.Source = img2.Source;
            link3 = link2;
            img2.Source = img1.Source;
            link2 = link1;
            img1.Source = new BitmapImage(new Uri("Nuotraukos/" + slideCounter2 + ".jpg", UriKind.RelativeOrAbsolute));
            link1 = temporary;
            slideCounter = slideCounter2;
            slideCounter2--;
        }

        private void Img1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(link1);
        }

        private void Img2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(link2);
        }

        private void Img3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(link3);
        }
    }
}