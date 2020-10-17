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
    public partial class ConfirmNewPasswordWindow : Window
    {
        private string code;
        private string email;

        public ConfirmNewPasswordWindow()
        {
            InitializeComponent();
        }

        private void SendCodeClick(object sender, RoutedEventArgs e)
        {
            var pattern = new Regex(@"([a-zA-Z0-9]+)(@gmail.com)$", RegexOptions.Compiled);
            if (string.IsNullOrWhiteSpace(EmailBox.Text))
            {
                MessageBox.Show("Prašome užpildyti laukelį.");
            }
            else if (!pattern.IsMatch(EmailBox.Text))
            {
                MessageBox.Show("Email turi būti rašomas tokia tvarka:\nTuri sutapti su jūsų naudojamu gmail,\nkitaip negalėsite gauti patvirtinimo kodo,\nTuri būti naudojamos raidės arba skaičiai,\nTuri būti nors vienas skaičius arba raidė,\nEmail'o pabaiga turi baigtis: @gmail.com, pvz.: kazkas@gmail.com");
            }
            else
            {
                email = EmailBox.Text;
                code = GenerateHash.CreateSalt(16);
                code = code.Remove(code.Length - 2);

                new SendEmail(code, email);

                EmailBox.Visibility = Visibility.Collapsed;
                Message1.Visibility = Visibility.Collapsed;
                SendCodeButton.Visibility = Visibility.Collapsed;

                ConfirmBox.Visibility = Visibility.Visible;
                Message2.Visibility = Visibility.Visible;
                ConfirmButton.Visibility = Visibility.Visible;

            }
        }

        private void ConfirmClick(object sender, RoutedEventArgs e)
        {
            if (code.Equals(ConfirmBox.Text))
            {
                var passwordChangeWindow = new PasswordChangeWindow(email);
                passwordChangeWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Blogai įvestas kodas.");
            }
        }
    }
}
