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

namespace Price_comparison_engine
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        readonly MainWindow mainWindow;

        public RegisterWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void Register_Button(object sender, RoutedEventArgs e)
        {
            String salt = GenerateHash.CreateSalt(10);
            String passwordHash = GenerateHash.GenerateSHA256Hash(PasswordField.Password, salt);
            
            var pattern1 = new Regex(@"(\.*\d+\.*[a-zA-Z]\.*[a-zA-Z]\.*[a-zA-Z]\.*)|(\.*[a-zA-Z]\.*\d+\.*[a-zA-Z]\.*[a-zA-Z]\.*)|(\.*[a-zA-Z]\.*[a-zA-Z]\.*\d+\.*[a-zA-Z]\.*)|(\.*[a-zA-Z]\.*[a-zA-Z]\.*[a-zA-Z]\.*\d+\.*)", RegexOptions.Compiled);
            var pattern2 = new Regex(@"([a-zA-Z0-9]+)(@gmail.com)$", RegexOptions.Compiled);
            
            if (Email.Text == "" || PasswordField.Password == "" || PasswordConfirmField.Password == "")
            {
                MessageBox.Show("Prašome užpildyti visus laukus.");
            }
            else if (!pattern2.IsMatch(Email.Text))
            {
                MessageBox.Show("Email turi būti rašomas tokia tvarka:\nTuri sutapti su jūsų naudojamu gmail,\nkitaip negalėsite patvirtinti registracijos,\nTuri būti naudojamos raidės arba skaičiai,\nTuri būti nors vienas skaičius arba raidė,\nEmail'o pabaiga turi baigtis: @gmail.com, pvz.: kazkas@gmail.com");
            }
            else if (!pattern1.IsMatch(PasswordField.Password))
            {
                MessageBox.Show("Slaptažodyje turi būti bent trys raidės ir vienas skaičius!!!");
            }
            else if (PasswordField.Password != PasswordConfirmField.Password)
            {
                MessageBox.Show("Slaptažodžiai nesutampa.");
            }
            else
            {
                var sqlRegister = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\PCEDatabase.mdf;Integrated Security=SSPI;Connect Timeout=30");
                var duomenuAdapteris = new SqlDataAdapter("SELECT Email FROM UserData WHERE Email='" + Email.Text.Trim() + "'", sqlRegister);
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
                        if (sqlRegister.State == ConnectionState.Closed)
                        {
                            sqlRegister.Open();
                        }

                        var queue = "INSERT INTO UserData(Email, PasswordHash, PasswordSalt) VALUES (@Email, @PasswordHash, @PasswordSalt)";
                        var sqlCommand = new SqlCommand(queue, sqlRegister);
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.Parameters.AddWithValue("@Email", Email.Text.Trim());
                        sqlCommand.Parameters.AddWithValue("@PasswordHash", passwordHash);
                        sqlCommand.Parameters.AddWithValue("@PasswordSalt", salt);

                        string code = GenerateHash.CreateSalt(16);
                        code = code.Remove(code.Length - 2);
                        var patvirtinimoLangas = new PatvirtinimoLangas(sqlRegister, sqlCommand, mainWindow, this, code, Email.Text.Trim());
                        patvirtinimoLangas.Show();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
