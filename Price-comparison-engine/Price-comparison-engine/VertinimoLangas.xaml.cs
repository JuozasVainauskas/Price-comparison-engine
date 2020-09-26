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
            elektromarktIv.Text = "Įvertinimo dar nėra";
            avitelaIv.Text = "Įvertinimo dar nėra";
        }

        private static double avitela = 0;
        private static int avitelaBalsavusiuSk = 1;

        private static double elektromarkt = 0;
        private static int elektromarktBalsavusiuSk = 1;
        private void Vertinti_avitela(object sender, RoutedEventArgs e)
        {
            var calc = avitela / (3 * avitelaBalsavusiuSk);

            avitelaIv.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
            avitelaBalsavusiuSk++;
            avitelaApt.SelectedIndex = -1;
            avitelaKok.SelectedIndex = -1;
            avitelaPris.SelectedIndex = -1;
        }
        private void Vertinti_elektromarkt(object sender, RoutedEventArgs e)
        {
            var calc = elektromarkt / (3 * elektromarktBalsavusiuSk);
            elektromarktIv.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
            elektromarktBalsavusiuSk++;
            elektroApt.SelectedIndex = -1;
            elektroKok.SelectedIndex = -1;
            elektroPris.SelectedIndex = -1;
        }

        private void Avitela_Aptarnavimas(object sender, SelectionChangedEventArgs e)
        {
            avitela += avitelaApt.SelectedIndex +1;
        }

        private void Avitela_Kokybe(object sender, SelectionChangedEventArgs e)
        {
            avitela += avitelaApt.SelectedIndex + 1;
        }

        private void Avitela_Pristatymas(object sender, SelectionChangedEventArgs e)
        {
            avitela += avitelaApt.SelectedIndex + 1;
        }

        private void Elektromarkt_Aptarnavimas(object sender, SelectionChangedEventArgs e)
        {
            elektromarkt += avitelaApt.SelectedIndex + 1;
        }

        private void Elektromarkt_Kokybe(object sender, SelectionChangedEventArgs e)
        {
            elektromarkt += avitelaApt.SelectedIndex + 1;
        }

        private void Elektromarkt_Pristatymas(object sender, SelectionChangedEventArgs e)
        {
            elektromarkt += avitelaApt.SelectedIndex + 1;
        }
    }
}
