using Price_comparison_engine.Klases;
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
            String salt = GeneruotiHash.SukurtiSalt(10);
            String slaptazodzioHash = GeneruotiHash.GenerateSHA256Hash(Slaptazodis.Password, salt);
            
            var pattern1 = new Regex(@"(\.*\d+\.*[a-zA-Z]\.*[a-zA-Z]\.*[a-zA-Z]\.*)|(\.*[a-zA-Z]\.*\d+\.*[a-zA-Z]\.*[a-zA-Z]\.*)|(\.*[a-zA-Z]\.*[a-zA-Z]\.*\d+\.*[a-zA-Z]\.*)|(\.*[a-zA-Z]\.*[a-zA-Z]\.*[a-zA-Z]\.*\d+\.*)", RegexOptions.Compiled);
            var pattern2 = new Regex(@"([a-zA-Z0-9._-]*[a-zA-Z0-9][a-zA-Z0-9._-]*)(@gmail.com)$", RegexOptions.Compiled);
            
            if (Email.Text == "" || Slaptazodis.Password == "" || SlaptazodisPatvirtinti.Password == "")
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
            else if (Slaptazodis.Password != SlaptazodisPatvirtinti.Password)
            {
                MessageBox.Show("Slaptažodžiai nesutampa.");
            }
            else
            {
                var kontekstas = new DuomenuBazesKontekstas();
                var rezultatas = kontekstas.NaudotojoDuomenys.SingleOrDefault(c => c.Email == Email.Text);
                if (rezultatas != null)
                { 
                    MessageBox.Show("Toks email jau panaudotas. Pabandykite kitą.");
                }
                else
                {
                    /*new DuomenuBazesKontekstas().ExecuteStoreCommand(@"UPDATE Users SET lname = @lname WHERE Id = @id", new SqlParameter("lname", lname), new SqlParameter("id", id));*/

                    //using (var kontekstas = new DuomenuBazesKontekstas())
                    //{
                    //    using (var dbKontekstoPervedimas = kontekstas.Database.BeginTransaction())
                    //    {
                    //        var naudotojoDuomenys = new NaudotojoDuomenys()
                    //        {
                    //            Email = Email.Text,
                    //            SlaptazodzioHash = slaptazodzioHash,
                    //            SlaptazodzioSalt = salt,
                    //            ArBalsavo = "0",
                    //            Role = 0
                    //        };
                    //        kontekstas.NaudotojoDuomenys.Add(naudotojoDuomenys);
                    //        kontekstas.SaveChanges();

                    //        dbKontekstoPervedimas.Commit();
                    //    }
                    //}

                    var naudotojoDuomenys = new NaudotojoDuomenys()
                    {
                        Email = Email.Text,
                        SlaptazodzioHash = slaptazodzioHash,
                        SlaptazodzioSalt = salt,
                        ArBalsavo = "0",
                        Role = 0
                    };
                    kontekstas.NaudotojoDuomenys.Add(naudotojoDuomenys);

                    string kodas = GeneruotiHash.SukurtiSalt(16);
                    kodas = kodas.Remove(kodas.Length - 2);
                    var patvirtinimoLangas = new PatvirtinimoLangas(kontekstas, pagrindinisLangas, this, kodas, Email.Text.Trim());
                    patvirtinimoLangas.Show();
                }
            }
        }
    }
}
