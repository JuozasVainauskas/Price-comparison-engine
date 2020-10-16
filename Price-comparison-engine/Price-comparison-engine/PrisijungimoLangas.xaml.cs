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
    /// Interaction logic for PrisijungimoLangas.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        readonly MainWindow pagrindinisLangas;
        public static Enum userRole { get; set; }
        public static string email { get; set; }

        public LoginWindow(MainWindow pagrindinisLangas)
        {
            InitializeComponent();
            this.pagrindinisLangas = pagrindinisLangas;
            userRole = Role.User;
            email = "";
        }
        private void Prisijungti_mygtukas(object sender, RoutedEventArgs e)
        {
            email = Email.Text;

            using (var context = new DatabaseContext())
            {
                var result = context.UserData.SingleOrDefault(c => c.Email == Email.Text);
                
                if (result != null)
                {
                    var passwordSalt = result.PasswordSalt;
                    var passworHash = result.PasswordHash;
                    if(result.Role == "0")
                    {
                        userRole = Role.User;
                    }
                    else if (result.Role == "1")
                    {
                        userRole = Role.Admin;
                    }

                    var naudotojoIvestasSlaptazodis = GenerateHash.GenerateSHA256Hash(Slaptazodis.Password, passwordSalt);

                    if (passworHash.Equals(naudotojoIvestasSlaptazodis))
                    {
                        var mainWindowLoggedIn = new MainWindowLoggedIn();
                        mainWindowLoggedIn.Show();

                        this.Close();
                        pagrindinisLangas.Close();
                    }
                    else
                    {
                        MessageBox.Show("Blogai įvestas slaptažodis!");
                    }
                }
                else
                {
                    MessageBox.Show("Toks email nerastas arba įvestas blogai!");
                }
            }
        }

        private void Sukurti_nauja_slaptazodi_mygtukas(object sender, RoutedEventArgs e)
        {
            var patvirtLangasSlaptKeitimui = new PatvirtLangasSlaptKeitimui();
            patvirtLangasSlaptKeitimui.Show();
        }
    }
}
