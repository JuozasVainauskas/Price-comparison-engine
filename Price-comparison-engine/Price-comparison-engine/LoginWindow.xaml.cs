using Price_comparison_engine.Classes;
using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        readonly MainWindow mainWindow;
        public static Enum UserRole { get; set; }
        public static string Email { get; set; }

        public LoginWindow(MainWindow pagrindinisLangas)
        {
            InitializeComponent();
            this.mainWindow = pagrindinisLangas;
            UserRole = Role.User;
            Email = "";
        }
        private void LoginClick(object sender, RoutedEventArgs e)
        {
            Email = EmailBox.Text;

            using (var context = new DatabaseContext())
            {
                var result = context.UserData.SingleOrDefault(c => c.Email == EmailBox.Text);
                
                if (result != null)
                {
                    var passwordSalt = result.PasswordSalt;
                    var passworHash = result.PasswordHash;
                    if(result.Role == "0")
                    {
                        UserRole = Role.User;
                    }
                    else if (result.Role == "1")
                    {
                        UserRole = Role.Admin;
                    }

                    var userEnteredPassword = GenerateHash.GenerateSHA256Hash(passwordBox.Password, passwordSalt);

                    if (passworHash.Equals(userEnteredPassword))
                    {
                        var mainWindowLoggedIn = new MainWindowLoggedIn();
                        mainWindowLoggedIn.Show();

                        this.Close();
                        mainWindow.Close();
                    }
                    else
                    {
                        MessageBox.Show("Blogai įvestas slaptažodis!");
                    }
                }
                else
                {
                    MessageBox.Show("Toks email nerastas arba įvestas blogai!");
                }
            }
        }

        private void ResetPasswordClick(object sender, RoutedEventArgs e)
        {
            var confirmNewPasswordWindow = new PatvirtLangasSlaptKeitimui();
            confirmNewPasswordWindow.Show();
        }
    }
}
