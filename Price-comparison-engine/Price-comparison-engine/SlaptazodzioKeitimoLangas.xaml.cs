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
                var sqlLogin = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\PCEDatabase.mdf;Integrated Security=SSPI;Connect Timeout=30");
                try
                {
                    if (sqlLogin.State == ConnectionState.Closed)
                    {
                        sqlLogin.Open();
                    }

                    String salt = GenerateHash.CreateSalt(10);
                    String slaptazodzioHash = GenerateHash.GenerateSHA256Hash(slaptazodis.Password, salt);

                    var queue = "UPDATE UserData SET PasswordHash=@PasswordHash, PasswordSalt=@PasswordSalt WHERE Email=@Email";
                    var sqlCommand = new SqlCommand(queue, sqlLogin);
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Parameters.AddWithValue("@Email", email);
                    sqlCommand.Parameters.AddWithValue("@PasswordHash", slaptazodzioHash);
                    sqlCommand.Parameters.AddWithValue("@PasswordSalt", salt);
                    sqlCommand.ExecuteNonQuery();

                    MessageBox.Show("Slaptažodis pakeistas sėkmingai.");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sqlLogin.Close();
                }
            }
        }
    }
}
