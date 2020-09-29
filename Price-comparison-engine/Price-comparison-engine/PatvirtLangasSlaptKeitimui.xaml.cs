using Price_comparison_engine.Klases;
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
    /// Interaction logic for PatvirtLangasSlaptKeitimui.xaml
    /// </summary>
    public partial class PatvirtLangasSlaptKeitimui : Window
    {
        private string kodas;
        private string email;

        public PatvirtLangasSlaptKeitimui()
        {
            InitializeComponent();
        }

        private void SiustiMygtukas(object sender, RoutedEventArgs e)
        {
            email = emailLangelis.Text;
            kodas = GeneruotiHash.SukurtiSalt(16);
            kodas = kodas.Remove(kodas.Length - 2);

            new SiustiEmail(kodas, email);

            emailLangelis.Visibility = Visibility.Collapsed;
            pranesimas1.Visibility = Visibility.Collapsed;
            siustiKodaMygtukas.Visibility = Visibility.Collapsed;

            patvirtinimoLangelis.Visibility = Visibility.Visible;
            pranesimas2.Visibility = Visibility.Visible;
            patvirtintiMygtukas.Visibility = Visibility.Visible;
        }

        private void PatvirtintiMygtukas(object sender, RoutedEventArgs e)
        {
            if (kodas == patvirtinimoLangelis.Text)
            {
                var slaptazodzioKeitimoLangas = new SlaptazodzioKeitimoLangas();
                slaptazodzioKeitimoLangas.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Blogai įvestas kodas.");
            }
        }
    }
}
