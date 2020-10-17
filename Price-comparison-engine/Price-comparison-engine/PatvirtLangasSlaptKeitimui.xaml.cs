using Price_comparison_engine.Classes;
using System.Text.RegularExpressions;
using System.Windows;


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

        private void Send(object sender, RoutedEventArgs e)
        {
            var pattern = new Regex(@"([a-zA-Z0-9]+)(@gmail.com)$", RegexOptions.Compiled);
            if (string.IsNullOrWhiteSpace(emailBox.Text))
            {
                MessageBox.Show("Prašome užpildyti laukelį.");
            }
            else if (!pattern.IsMatch(emailBox.Text))
            {
                MessageBox.Show("Email turi būti rašomas tokia tvarka:\nTuri sutapti su jūsų naudojamu gmail,\nkitaip negalėsite gauti patvirtinimo kodo,\nTuri būti naudojamos raidės arba skaičiai,\nTuri būti nors vienas skaičius arba raidė,\nEmail'o pabaiga turi baigtis: @gmail.com, pvz.: kazkas@gmail.com");
            }
            else
            {
                email = emailBox.Text;
                code = GenerateHash.CreateSalt(16);
                code = code.Remove(code.Length - 2);

                new SendEmail(code, email);

                emailBox.Visibility = Visibility.Collapsed;
                message1.Visibility = Visibility.Collapsed;
                sendCodeBtn.Visibility = Visibility.Collapsed;

                confirmationBox.Visibility = Visibility.Visible;
                message2.Visibility = Visibility.Visible;
                confirmBtn.Visibility = Visibility.Visible;

            }
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            if (code.Equals(confirmationBox.Text))
            {
                var changePasswordWindow = new ChangePasswordWindow(email);
                changePasswordWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Blogai įvestas kodas.");
            }
        }
    }
}
