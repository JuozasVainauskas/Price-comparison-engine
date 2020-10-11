using Price_comparison_engine.Klases;
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
    public partial class PrisijungimoLangas : Window
    {
        readonly MainWindow pagrindinisLangas;
        public static string Role { get; set; }
        public static string email { get; set; }

        public PrisijungimoLangas(MainWindow pagrindinisLangas)
        {
            InitializeComponent();
            this.pagrindinisLangas = pagrindinisLangas;
            Role = "0";
            email = "";
        }
        private void Prisijungti_mygtukas(object sender, RoutedEventArgs e)
        {
            email = Email.Text;

            using (var kontekstas = new DuomenuBazesKontekstas())
            {
                var rezultatas = kontekstas.NaudotojoDuomenys.SingleOrDefault(c => c.Email == Email.Text);
                
                if (rezultatas != null)
                {
                    var salt = rezultatas.SlaptazodzioSalt;
                    var slaptazodzioHash = rezultatas.SlaptazodzioHash;
                    Role = rezultatas.Role;

                    var naudotojoIvestasSlaptazodis = GeneruotiHash.GenerateSHA256Hash(Slaptazodis.Password, salt);

                    if (slaptazodzioHash.Equals(naudotojoIvestasSlaptazodis))
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
