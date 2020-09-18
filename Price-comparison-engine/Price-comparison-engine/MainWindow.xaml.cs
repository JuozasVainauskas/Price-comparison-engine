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
        public static int slideCounter = 1;
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
            mygtukoResize(prisijungimosMygtukas, skirtumasPlocio, skirtumasIlgio);
            mygtukoResize(registracijosMygtukas, skirtumasPlocio, skirtumasIlgio);
            mygtukoResize(DUKMygtukas, skirtumasPlocio, skirtumasIlgio);
            mygtukoResize(kontaktuMygtukas, skirtumasPlocio, skirtumasIlgio);
            mygtukoResize(Ieškoti, skirtumasPlocio, skirtumasIlgio);
            textBoxResize(ieškojimoLaukas, skirtumasPlocio/3, skirtumasIlgio);
            rectangleResize(vidurinėLinija, skirtumasPlocioBlokeliui);
            rectangleIštempimas(viršutinėLinija);
            rectangleIštempimas(vidurinėLinija);
            rectangleIštempimas(apatinėLinija);
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
        private void rectangleResize(Rectangle plotelis, double ilgis)
        {
            plotelis.Height = this.ActualHeight - ilgis;
        }
        private void rectangleIštempimas(Rectangle plotelis)
        {
            plotelis.Width = this.ActualWidth;
        }

        private void Slider_Back(object sender, MouseButtonEventArgs e)
        {
            if(slideCounter>3)
            {
                slideCounter = 1;
            }
            img1.Source = img2.Source;
            img2.Source = img3.Source;
            img3.Source = new BitmapImage(new Uri("Nuotraukos/" + slideCounter + ".jpg", UriKind.RelativeOrAbsolute));
            slideCounter++;
            

        }
        private void Slider_Front(object sender, MouseButtonEventArgs e)
        {
            if (slideCounter > 3)
            {
                slideCounter = 1;
            }
            img3.Source = img2.Source;
            img2.Source = img1.Source;
            img1.Source = new BitmapImage(new Uri("Nuotraukos/" + slideCounter + ".jpg", UriKind.RelativeOrAbsolute));
            slideCounter++;
        }

    }
}
