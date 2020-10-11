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
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
        }

        public partial class Vartotojas
        {
            public int ID { get; set; }
            public string Email { get; set; }   
            public int Role { get; set; }


        }

        private static int role = 0;
        private static void SkirtiRole(string email,int role)
        {
            using (var kontekstas = new DuomenuBazesKontekstas())
            {
                var rezultatas = kontekstas.NaudotojoDuomenys.SingleOrDefault(b => b.Email == email);
                if (rezultatas != null)
                {
                    rezultatas.Role = role;
                    kontekstas.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Vartotojas tokiu emailu neegzistuoja arba nebuvo rastas.");
                }
            }
        }

        private void Priskirti(object sender, RoutedEventArgs e)
        {
            if (RolesPriskirimas.SelectedIndex != -1 && ArTinkamasPastas(email.Text))
            {
                role = RolesPriskirimas.SelectedIndex;
                SkirtiRole(email.Text, role);
                MessageBox.Show(email.Text + " priskirta nauja rolė!");
                email.Text = "";
                UsersTable.Items.Clear();
                Email.Clear();
                Role.Clear();
                Skaityti(ref Email, ref Role);
                iLentele();
            }
        }

        private static void Istrinti(string email)
        {
            using (var kontekstas = new DuomenuBazesKontekstas())
            {
                var rezultatas = kontekstas.NaudotojoDuomenys.SingleOrDefault(b => b.Email == email);
                if (rezultatas != null)
                {
                    kontekstas.NaudotojoDuomenys.Remove(rezultatas);
                    kontekstas.SaveChanges();
                    MessageBox.Show("Vartotojas " + email + " buvo ištrintas iš duomenų bazės!");
                }
                else
                {
                    MessageBox.Show("Vartotojas tokiu emailu neegzistuoja arba nebuvo rastas.");
                }
            }
        }

        private void IstrintiVartotoja(object sender, RoutedEventArgs e)
        {
            if(ArTinkamasPastas(emailToDelete.Text))
            {
                Istrinti(emailToDelete.Text);
                emailToDelete.Clear();
                UsersTable.Items.Clear();
                Email.Clear();
                Role.Clear();
                Skaityti(ref Email, ref Role);
                iLentele();
            }
        }

        private void Sukurti(string email, string slaptazodis)
        {
            var salt = GeneruotiHash.SukurtiSalt(10);
            var slaptazodzioHash = GeneruotiHash.GenerateSHA256Hash(PasswordToCreate.Password, salt);

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(slaptazodis))
            {
                MessageBox.Show("Prašome užpildyti visus laukus.");
            }
            else if (!ArTinkamasPastas(email))
            {
                MessageBox.Show("Neteisingai suformatuotas el. paštas!");
            }
            else
            {
                var kontekstas = new DuomenuBazesKontekstas();
                var rezultatas = kontekstas.NaudotojoDuomenys.SingleOrDefault(c => c.Email == email);
                if (rezultatas != null)
                {
                    MessageBox.Show("Toks email jau panaudotas. Pabandykite kitą.");
                }
                else
                {
                    var naudotojoDuomenys = new NaudotojoDuomenys()
                    {
                        Email = email,
                        SlaptazodzioHash = slaptazodzioHash,
                        SlaptazodzioSalt = salt,
                        ArBalsavo = "",
                        Komentaras = "",
                        Role = 0
                    };
                    kontekstas.NaudotojoDuomenys.Add(naudotojoDuomenys);
                    kontekstas.SaveChanges();
                    MessageBox.Show("Vartotojas sekmingai sukurtas!");
                }
            }
        }

        private void SukurtiVartotoja(object sender, RoutedEventArgs e)
        {
            Sukurti(EmailToCreate.Text,PasswordToCreate.Password);
            EmailToCreate.Clear();
            PasswordToCreate.Clear();
            UsersTable.Items.Clear();
            Email.Clear();
            Role.Clear();
            Skaityti(ref Email, ref Role);
            iLentele();
        }

        private bool ArTinkamasPastas(string email)
        {
            var pattern2 = new Regex(@"([a-zA-Z0-9]+)(@gmail.com)$", RegexOptions.Compiled);
            if (email == "")
            {
                return false;
            }
            else if (!pattern2.IsMatch(email))
            {
                return false;
            }
            else return true;

        }

        private static List<string> Email = new List<string>();
        private static List<int> Role = new List<int>();
        private void RodytiVartotojus(object sender, EventArgs e)
        {
          Skaityti(ref Email,ref Role);
          iLentele();
        }

        private void iLentele()
        {
            for (int i = 0; i < Email.Count; i++)
            {
                var vartotojas = new Vartotojas { ID = i, Email = Email[i], Role = Role[i] };
                UsersTable.Items.Add(vartotojas);
            }
        }

        private static void Skaityti(ref List<string> Email, ref List<int> Role)
        {
            using (var kontekstas = new DuomenuBazesKontekstas())
            {
                var tempEmail = kontekstas.NaudotojoDuomenys.Select(column => column.Email).ToList();
                var tempRole = kontekstas.NaudotojoDuomenys.Select(column => column.Role).ToList();

                if (tempEmail != null && tempRole != null)
                {
                    Email = tempEmail;
                    Role = tempRole;
                }
            }
        }
    }
}
