using Price_comparison_engine.Classes;
using System;
using System.Linq;
using System.Windows;

namespace Price_comparison_engine
{
    /// <summary>
    /// Interaction logic for PrisijungimoLangas.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        readonly MainWindow MainWindow;
        public static Enum userRole { get; set; }
        public static string email { get; set; }

        public LoginWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.MainWindow = mainWindow;
            userRole = Role.User;
            email = "";
        }
        private void Login(object sender, RoutedEventArgs e)
        {
            email = Email.Text;

            using (var context = new DatabaseContext())
            {
                var result = context.UserData.SingleOrDefault(c => c.Email == Email.Text);
                
                if (result != null)
                {
                    var passwordSalt = result.PasswordSalt;
                    var passworHash = result.PasswordHash;
                    if(result.Role == "0")
                    {
                        userRole = Role.User;
                    }
                    else if (result.Role == "1")
                    {
                        userRole = Role.Admin;
                    }

                    var usersPasswordInput = GenerateHash.GenerateSHA256Hash(password.Password, passwordSalt);

                    if (passworHash.Equals(usersPasswordInput))
                    {
                        var mainWindowLoggedIn = new MainWindowLoggedIn();
                        mainWindowLoggedIn.Show();

                        this.Close();
                        MainWindow.Close();
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

        private void CreateNewPassword(object sender, RoutedEventArgs e)
        {
            var confirmNewPasswordWindow = new ConfirmNewPasswordWindow();
            confirmNewPasswordWindow.Show();
        }
    }
}
