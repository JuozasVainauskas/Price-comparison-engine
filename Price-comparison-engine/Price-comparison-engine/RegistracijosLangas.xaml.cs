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
    public partial class RegistracijosLangas : Window
    {
        readonly MainWindow pagrindinisLangas;

        public RegistracijosLangas(MainWindow pagrindinisLangas)
        {
            InitializeComponent();
            this.pagrindinisLangas = pagrindinisLangas;
        }

        private void Registruotis_Mygtukas(object sender, RoutedEventArgs e)
        {
            var passwordSalt = GenerateHash.SukurtiSalt(10);
            var passwordHash = GenerateHash.GenerateSHA256Hash(Slaptazodis.Password, passwordSalt);
            
            var pattern1 = new Regex(@"(\.*\d+\.*[a-zA-Z]\.*[a-zA-Z]\.*[a-zA-Z]\.*)|(\.*[a-zA-Z]\.*\d+\.*[a-zA-Z]\.*[a-zA-Z]\.*)|(\.*[a-zA-Z]\.*[a-zA-Z]\.*\d+\.*[a-zA-Z]\.*)|(\.*[a-zA-Z]\.*[a-zA-Z]\.*[a-zA-Z]\.*\d+\.*)", RegexOptions.Compiled);
            var pattern2 = new Regex(@"([a-zA-Z0-9._-]*[a-zA-Z0-9][a-zA-Z0-9._-]*)(@gmail.com)$", RegexOptions.Compiled);
            
            if (string.IsNullOrWhiteSpace(Email.Text) || string.IsNullOrWhiteSpace(Slaptazodis.Password) || string.IsNullOrWhiteSpace(SlaptazodisPatvirtinti.Password))
            {
                MessageBox.Show("Prašome užpildyti visus laukus.");
            }
            else if (!pattern2.IsMatch(Email.Text))
            {
                MessageBox.Show("Email turi būti rašomas tokia tvarka:\nTuri sutapti su jūsų naudojamu gmail,\nkitaip negalėsite patvirtinti registracijos,\nTuri būti naudojamos raidės arba skaičiai,\nTuri būti nors vienas skaičius arba raidė,\nEmail'o pabaiga turi baigtis: @gmail.com, pvz.: kazkas@gmail.com");
            }
            else if (!pattern1.IsMatch(Slaptazodis.Password))
            {
                MessageBox.Show("Slaptažodyje turi būti bent trys raidės ir vienas skaičius!!!");
            }
            else if (!Slaptazodis.Password.Equals(SlaptazodisPatvirtinti.Password))
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

                    var kodas = GenerateHash.SukurtiSalt(16);
                    kodas = kodas.Remove(kodas.Length - 2);
                    var patvirtinimoLangas = new PatvirtinimoLangas(context, pagrindinisLangas, this, kodas, Email.Text.Trim());
                    patvirtinimoLangas.Show();
                }
            }
        }
    }
}
