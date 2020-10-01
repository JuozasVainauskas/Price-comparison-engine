using Price_comparison_engine.Classes;
using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        readonly MainWindow mainWindow;

        public LoginWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void Login_button(object sender, RoutedEventArgs e)
        {
            //var sqlLogin = new SqlConnection(@"Data Source=localhost\sqlexpress; Initial Catalog=PCEDatabase; Integrated Security=True;");
            var sqlLogin = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\PCEDatabase.mdf;Integrated Security=SSPI;Connect Timeout=30");
            try
            {
                if (sqlLogin.State == ConnectionState.Closed)
                {
                    sqlLogin.Open();
                }
                var queue = "SELECT PasswordSalt FROM UserData WHERE Email=@Email";
                var sqlCommand = new SqlCommand(queue, sqlLogin);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@Email", Email.Text);

                String salt = "";
                String passwordHash;

                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.Read())
                    {
                        salt = sqlDataReader["PasswordSalt"].ToString();
                    }
                }

                passwordHash = GenerateHash.GenerateSHA256Hash(PasswordField.Password, salt);
                
                queue = "SELECT COUNT(1) FROM UserData WHERE Email=@Email AND PasswordHash=@PasswordHash";
                sqlCommand = new SqlCommand(queue, sqlLogin);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddWithValue("@Email", Email.Text);
                sqlCommand.Parameters.AddWithValue("@PasswordHash", passwordHash); //PasswordHash with salt
                int count = Convert.ToInt32(sqlCommand.ExecuteScalar());
                if (count == 1)
                {
                    var mainWindowLoggedIn = new MainWindowLoggedIn();
                    mainWindowLoggedIn.Show();
                    this.Close();
                    mainWindow.Close();
                }
                else
                {
                    MessageBox.Show("Blogai įvestas email arba slaptažodis!");
                }
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

        private void Create_new_password_button(object sender, RoutedEventArgs e)
        {
            var patvirtLangasSlaptKeitimui = new PatvirtLangasSlaptKeitimui();
            patvirtLangasSlaptKeitimui.Show();
        }
    }
}
