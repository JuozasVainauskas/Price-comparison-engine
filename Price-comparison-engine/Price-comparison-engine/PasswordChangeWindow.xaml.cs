using Price_comparison_engine.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for PasswordChangeWindow.xaml
    /// </summary>
    public partial class PasswordChangeWindow : Window
    {

        readonly string email;

        public PasswordChangeWindow(string email)
        {
            InitializeComponent();
            this.email = email;
        }

        private void ChangePasswordClick(object sender, RoutedEventArgs e)
        {
            var pattern = new Regex(@"(\.*\d+\.*[a-zA-Z]\.*[a-zA-Z]\.*[a-zA-Z]\.*)|(\.*[a-zA-Z]\.*\d+\.*[a-zA-Z]\.*[a-zA-Z]\.*)|(\.*[a-zA-Z]\.*[a-zA-Z]\.*\d+\.*[a-zA-Z]\.*)|(\.*[a-zA-Z]\.*[a-zA-Z]\.*[a-zA-Z]\.*\d+\.*)", RegexOptions.Compiled);

            if (PasswordBox.Password == "" || PasswordConfirmBox.Password == "")
            {
                MessageBox.Show("Prašome užpildyti visus laukus.");
            }
            else if (!pattern.IsMatch(PasswordBox.Password))
            {
                MessageBox.Show("Slaptažodyje turi būti bent trys raidės ir vienas skaičius!!!");
            }
            else if (PasswordBox.Password != PasswordConfirmBox.Password)
            {
                MessageBox.Show("Slaptažodžiai nesutampa.");
            }
            else
            {
                var passwordSalt = GenerateHash.CreateSalt(10);
                var passwordHash = GenerateHash.GenerateSHA256Hash(PasswordBox.Password, passwordSalt);

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
