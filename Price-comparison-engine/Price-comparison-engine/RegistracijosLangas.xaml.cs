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
            var pattern2 = new Regex(@"([a-zA-Z0-9]+)(@gmail.com)$", RegexOptions.Compiled);
            
            if (Email.Text == "" || Slaptazodis.Password == "" || SlaptazodisPatvirtinti.Password == "")
            {
                MessageBox.Show("Prašome užpildyti visus laukus.");
            }
            else if (!pattern2.IsMatch(Email.Text))
            {
                MessageBox.Show("Email turi būti rašomas tokia tvarka:\nTuri būti naudojamos raidės arba skaičiai,\nTuri būti nors vienas skaičius arba raidė,\nEmail'o pabaiga turi baigtis: @gmail.com, pvz.: kazkas@gmail.com");
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
                //var sqlRegistruotis = new SqlConnection(@"Data Source=localhost\sqlexpress; Initial Catalog=PCEDatabase; Integrated Security=True;");
                var sqlRegistruotis = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\PCEDatabase.mdf;Integrated Security=SSPI;Connect Timeout=30");
                var duomenuAdapteris = new SqlDataAdapter("SELECT Email FROM NaudotojoDuomenys WHERE Email='" + Email.Text.Trim() + "'", sqlRegistruotis);
                var duomenuLentele = new DataTable();
                duomenuAdapteris.Fill(duomenuLentele);
                if (duomenuLentele.Rows.Count >= 1)
                {
                    MessageBox.Show("Toks email jau panaudotas. Pabandykite kitą.");
                }
                else
                {
                    try
                    {
                        if (sqlRegistruotis.State == ConnectionState.Closed)
                        {
                            sqlRegistruotis.Open();
                        }

                        var eile = "INSERT INTO NaudotojoDuomenys(Email, SlaptazodzioHash, SlaptazodzioSalt) VALUES (@Email, @SlaptazodzioHash, @SlaptazodzioSalt)";
                        var sqlKomanda = new SqlCommand(eile, sqlRegistruotis);
                        sqlKomanda.CommandType = CommandType.Text;
                        sqlKomanda.Parameters.AddWithValue("@Email", Email.Text.Trim());
                        sqlKomanda.Parameters.AddWithValue("@SlaptazodzioHash", slaptazodzioHash);
                        sqlKomanda.Parameters.AddWithValue("@SlaptazodzioSalt", salt);

                        string kodas = GeneruotiHash.SukurtiSalt(16);
                        kodas = kodas.Remove(kodas.Length - 2);
                        var patvirtinimoLangas = new PatvirtinimoLangas(sqlRegistruotis, pagrindinisLangas, this, sqlKomanda, kodas, Email.Text.Trim());
                        patvirtinimoLangas.Show();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                    }
                }
            }
        }
    }
}
