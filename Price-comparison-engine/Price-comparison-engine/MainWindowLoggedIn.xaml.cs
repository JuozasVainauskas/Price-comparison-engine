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
    }
}
    
