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
    /// Interaction logic for VertinimoLangas.xaml
    /// </summary>
    public partial class VertinimoLangas : Window
    {
        public VertinimoLangas()
        {
            InitializeComponent();
        }

        private void Vertinti_mygtukas(object sender, RoutedEventArgs e)
        {

        }

        private void Avitela_Aptarnavimas(object sender, SelectionChangedEventArgs e)
        {
            var item = avitelaApt.SelectedValue;
            Console.WriteLine(item.ToString());
        }

        private void Avitela_Kokybe(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Avitela_Pristatymas(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Elektromarkt_Aptarnavimas(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Elektromarkt_Kokybe(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Elektromarkt_Pristatymas(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
