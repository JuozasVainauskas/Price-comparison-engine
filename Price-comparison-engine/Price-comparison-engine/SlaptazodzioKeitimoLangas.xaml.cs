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
    /// Interaction logic for SlaptazodzioKeitimoLangas.xaml
    /// </summary>
    public partial class SlaptazodzioKeitimoLangas : Window
    {

        readonly string email;

        public SlaptazodzioKeitimoLangas(string email)
        {
            InitializeComponent();
            this.email = email;
        }

        private void PakeistiSlaptazodi(object sender, RoutedEventArgs e)
        {
            var pattern = new Regex(@"(\.*\d+\.*[a-zA-Z]\.*[a-zA-Z]\.*[a-zA-Z]\.*)|(\.*[a-zA-Z]\.*\d+\.*[a-zA-Z]\.*[a-zA-Z]\.*)|(\.*[a-zA-Z]\.*[a-zA-Z]\.*\d+\.*[a-zA-Z]\.*)|(\.*[a-zA-Z]\.*[a-zA-Z]\.*[a-zA-Z]\.*\d+\.*)", RegexOptions.Compiled);

            if (slaptazodis.Password == "" || slaptazodisPatvirtinti.Password == "")
            {
                MessageBox.Show("Prašome užpildyti visus laukus.");
            }
            else if (!pattern.IsMatch(slaptazodis.Password))
            {
                MessageBox.Show("Slaptažodyje turi būti bent trys raidės ir vienas skaičius!!!");
            }
            else if (slaptazodis.Password != slaptazodisPatvirtinti.Password)
            {
                MessageBox.Show("Slaptažodžiai nesutampa.");
            }
            else
            {
                var passwordSalt = GenerateHash.SukurtiSalt(10);
                var passwordHash = GenerateHash.GenerateSHA256Hash(slaptazodis.Password, passwordSalt);

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
