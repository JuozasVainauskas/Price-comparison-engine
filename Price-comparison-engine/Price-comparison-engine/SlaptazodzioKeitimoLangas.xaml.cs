using Price_comparison_engine.Classes;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace Price_comparison_engine
{
    /// <summary>
    /// Interaction logic for SlaptazodzioKeitimoLangas.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {

        readonly string email;

        public ChangePasswordWindow(string email)
        {
            InitializeComponent();
            this.email = email;
        }

        private void ChangePassword(object sender, RoutedEventArgs e)
        {
            var pattern = new Regex(@"(\.*\d+\.*[a-zA-Z]\.*[a-zA-Z]\.*[a-zA-Z]\.*)|(\.*[a-zA-Z]\.*\d+\.*[a-zA-Z]\.*[a-zA-Z]\.*)|(\.*[a-zA-Z]\.*[a-zA-Z]\.*\d+\.*[a-zA-Z]\.*)|(\.*[a-zA-Z]\.*[a-zA-Z]\.*[a-zA-Z]\.*\d+\.*)", RegexOptions.Compiled);

            if (passwordBox.Password == "" || confirmPasswordBox.Password == "")
            {
                MessageBox.Show("Prašome užpildyti visus laukus.");
            }
            else if (!pattern.IsMatch(passwordBox.Password))
            {
                MessageBox.Show("Slaptažodyje turi būti bent trys raidės ir vienas skaičius!!!");
            }
            else if (passwordBox.Password != confirmPasswordBox.Password)
            {
                MessageBox.Show("Slaptažodžiai nesutampa.");
            }
            else
            {
                var passwordSalt = GenerateHash.CreateSalt(10);
                var passwordHash = GenerateHash.GenerateSHA256Hash(passwordBox.Password, passwordSalt);

                using (var context = new DatabaseContext())
                {
                    var result = context.UserData.SingleOrDefault(b => b.Email == email);
                    if (result != null)
                    {
                        result.PasswordHash = passwordHash;
                        result.PasswordSalt = passwordSalt;
                        context.SaveChanges();

                        MessageBox.Show("Slaptažodis pakeistas sėkmingai.");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Vartotojas tokiu emailu neegzistuoja arba nebuvo rastas.");
                    }
                }
            }
        }
    }
}
