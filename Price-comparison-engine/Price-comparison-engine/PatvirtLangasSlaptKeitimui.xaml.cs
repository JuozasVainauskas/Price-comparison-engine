using Price_comparison_engine.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private string code;
        private string email;

        public PatvirtLangasSlaptKeitimui()
        {
            InitializeComponent();
        }

        private void SiustiMygtukas(object sender, RoutedEventArgs e)
        {
            var pattern = new Regex(@"([a-zA-Z0-9]+)(@gmail.com)$", RegexOptions.Compiled);
            if (emailLangelis.Text == "")
            {
                MessageBox.Show("Prašome užpildyti laukelį.");
            }
            else if (!pattern.IsMatch(emailLangelis.Text))
            {
                MessageBox.Show("Email turi būti rašomas tokia tvarka:\nTuri sutapti su jūsų naudojamu gmail,\nkitaip negalėsite gauti patvirtinimo kodo,\nTuri būti naudojamos raidės arba skaičiai,\nTuri būti nors vienas skaičius arba raidė,\nEmail'o pabaiga turi baigtis: @gmail.com, pvz.: kazkas@gmail.com");
            }
            else
            {
                email = emailLangelis.Text;
                code = GenerateHash.CreateSalt(16);
                code = code.Remove(code.Length - 2);

                new SendEmail(code, email);

                emailLangelis.Visibility = Visibility.Collapsed;
                pranesimas1.Visibility = Visibility.Collapsed;
                siustiKodaMygtukas.Visibility = Visibility.Collapsed;

                patvirtinimoLangelis.Visibility = Visibility.Visible;
                pranesimas2.Visibility = Visibility.Visible;
                patvirtintiMygtukas.Visibility = Visibility.Visible;

            }
        }

        private void PatvirtintiMygtukas(object sender, RoutedEventArgs e)
        {
            if (code == patvirtinimoLangelis.Text)
            {
                var slaptazodzioKeitimoLangas = new SlaptazodzioKeitimoLangas(email);
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
