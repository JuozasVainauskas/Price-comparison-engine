using Price_comparison_engine.Klases;
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
                var sqlPrisijungti = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\PCEDatabase.mdf;Integrated Security=SSPI;Connect Timeout=30");
                try
                {
                    if (sqlPrisijungti.State == ConnectionState.Closed)
                    {
                        sqlPrisijungti.Open();
                    }

                    String salt = GeneruotiHash.SukurtiSalt(10);
                    String slaptazodzioHash = GeneruotiHash.GenerateSHA256Hash(slaptazodis.Password, salt);

                    var eile = "UPDATE NaudotojoDuomenys SET SlaptazodzioHash=@SlaptazodzioHash, SlaptazodzioSalt=@SlaptazodzioSalt WHERE Email=@Email";
                    var sqlKomanda = new SqlCommand(eile, sqlPrisijungti);
                    sqlKomanda.CommandType = CommandType.Text;
                    sqlKomanda.Parameters.AddWithValue("@Email", email);
                    sqlKomanda.Parameters.AddWithValue("@SlaptazodzioHash", slaptazodzioHash);
                    sqlKomanda.Parameters.AddWithValue("@SlaptazodzioSalt", salt);
                    sqlKomanda.ExecuteNonQuery();

                    MessageBox.Show("Slaptažodis pakeistas sėkmingai.");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sqlPrisijungti.Close();
                }
            }
        }
    }
}
