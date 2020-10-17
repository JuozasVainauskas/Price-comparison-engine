using Price_comparison_engine.Classes;
using System;
using System.Net;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
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
using System.Threading;

namespace Price_comparison_engine
{
    /// <summary>
    /// Interaction logic for RegistracijosLangas.xaml
    /// </summary>
    public partial class RegisteringWindow : Window
    {
        readonly MainWindow mainWindow;

        public RegisteringWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            var passwordSalt = GenerateHash.CreateSalt(10);
            var passwordHash = GenerateHash.GenerateSHA256Hash(PasswordBox.Password, passwordSalt);
            
            var pattern1 = new Regex(@"(\.*\d+\.*[a-zA-Z]\.*[a-zA-Z]\.*[a-zA-Z]\.*)|(\.*[a-zA-Z]\.*\d+\.*[a-zA-Z]\.*[a-zA-Z]\.*)|(\.*[a-zA-Z]\.*[a-zA-Z]\.*\d+\.*[a-zA-Z]\.*)|(\.*[a-zA-Z]\.*[a-zA-Z]\.*[a-zA-Z]\.*\d+\.*)", RegexOptions.Compiled);
            var pattern2 = new Regex(@"([a-zA-Z0-9._-]*[a-zA-Z0-9][a-zA-Z0-9._-]*)(@gmail.com)$", RegexOptions.Compiled);
            
            if (string.IsNullOrWhiteSpace(EmailBox.Text) || string.IsNullOrWhiteSpace(PasswordBox.Password) || string.IsNullOrWhiteSpace(PasswordConfirmBox.Password))
            {
                MessageBox.Show("Prašome užpildyti visus laukus.");
            }
            else if (!pattern2.IsMatch(EmailBox.Text))
            {
                MessageBox.Show("Email turi būti rašomas tokia tvarka:\nTuri sutapti su jūsų naudojamu gmail,\nkitaip negalėsite patvirtinti registracijos,\nTuri būti naudojamos raidės arba skaičiai,\nTuri būti nors vienas skaičius arba raidė,\nEmail'o pabaiga turi baigtis: @gmail.com, pvz.: kazkas@gmail.com");
            }
            else if (!pattern1.IsMatch(PasswordBox.Password))
            {
                MessageBox.Show("Slaptažodyje turi būti bent trys raidės ir vienas skaičius!!!");
            }
            else if (!PasswordBox.Password.Equals(PasswordConfirmBox.Password))
            {
                MessageBox.Show("Slaptažodžiai nesutampa.");
            }
            else
            {
                var context = new DatabaseContext();
                var result = context.UserData.SingleOrDefault(c => c.Email == EmailBox.Text);
                if (result != null)
                { 
                    MessageBox.Show("Toks email jau panaudotas. Pabandykite kitą.");
                }
                else
                {
                    var userData = new UserData()
                    {
                        Email = EmailBox.Text,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        Role = "0"
                    };
                    context.UserData.Add(userData);

                    var code = GenerateHash.CreateSalt(16);
                    code = code.Remove(code.Length - 2);
                    var confirmationWindow = new ConfirmationWindow(context, mainWindow, this, code, EmailBox.Text.Trim());
                    confirmationWindow.Show();
                }
            }
        }
    }
}
