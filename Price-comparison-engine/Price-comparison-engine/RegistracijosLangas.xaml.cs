using Price_comparison_engine.Classes;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace Price_comparison_engine
{
    /// <summary>
    /// Interaction logic for RegistracijosLangas.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        readonly MainWindow MainWindow;

        public RegistrationWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.MainWindow = mainWindow;
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            var passwordSalt = GenerateHash.CreateSalt(10);
            var passwordHash = GenerateHash.GenerateSHA256Hash(passwordBox.Password, passwordSalt);
            
            var pattern1 = new Regex(@"(\.*\d+\.*[a-zA-Z]\.*[a-zA-Z]\.*[a-zA-Z]\.*)|(\.*[a-zA-Z]\.*\d+\.*[a-zA-Z]\.*[a-zA-Z]\.*)|(\.*[a-zA-Z]\.*[a-zA-Z]\.*\d+\.*[a-zA-Z]\.*)|(\.*[a-zA-Z]\.*[a-zA-Z]\.*[a-zA-Z]\.*\d+\.*)", RegexOptions.Compiled);
            var pattern2 = new Regex(@"([a-zA-Z0-9._-]*[a-zA-Z0-9][a-zA-Z0-9._-]*)(@gmail.com)$", RegexOptions.Compiled);
            
            if (string.IsNullOrWhiteSpace(Email.Text) || string.IsNullOrWhiteSpace(passwordBox.Password) || string.IsNullOrWhiteSpace(passwordConfirmBox.Password))
            {
                MessageBox.Show("Prašome užpildyti visus laukus.");
            }
            else if (!pattern2.IsMatch(Email.Text))
            {
                MessageBox.Show("Email turi būti rašomas tokia tvarka:\nTuri sutapti su jūsų naudojamu gmail,\nkitaip negalėsite patvirtinti registracijos,\nTuri būti naudojamos raidės arba skaičiai,\nTuri būti nors vienas skaičius arba raidė,\nEmail'o pabaiga turi baigtis: @gmail.com, pvz.: kazkas@gmail.com");
            }
            else if (!pattern1.IsMatch(passwordBox.Password))
            {
                MessageBox.Show("Slaptažodyje turi būti bent trys raidės ir vienas skaičius!!!");
            }
            else if (!passwordBox.Password.Equals(passwordConfirmBox.Password))
            {
                MessageBox.Show("Slaptažodžiai nesutampa.");
            }
            else
            {
                var context = new DatabaseContext();
                var result = context.UserData.SingleOrDefault(c => c.Email == Email.Text);
                if (result != null)
                { 
                    MessageBox.Show("Toks email jau panaudotas. Pabandykite kitą.");
                }
                else
                {
                    var userData = new UserData()
                    {
                        Email = Email.Text,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        Role = "0"
                    };
                    context.UserData.Add(userData);

                    var code = GenerateHash.CreateSalt(16);
                    code = code.Remove(code.Length - 2);
                    var confirmationWindow = new ConfirmationWindow(context, MainWindow, this, code, Email.Text.Trim());
                    confirmationWindow.Show();
                }
            }
        }
    }
}
