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
            AtnaujintiStatistika();
        }

        private void AtnaujintiStatistika()
        {
            List<int> ls = Skaiciuoti();
            RegisteredUsers.Text = ls[0].ToString();
            Admins.Text = ls[1].ToString();
            Users.Text = ls[2].ToString();
            Goods.Text = ls[3].ToString();
        }

        public partial class Vartotojas
        {
            public int ID { get; set; }
            public string Email { get; set; }   
            public string Role { get; set; }
        }

        private static string role = "0";
        private void SkirtiRole(string email, string role)
        {
            using (var kontekstas = new DuomenuBazesKontekstas())
            {
                var rezultatas = kontekstas.NaudotojoDuomenys.SingleOrDefault(b => b.Email == email);
                if (rezultatas != null)
                {
                    rezultatas.Role = role;
                    kontekstas.SaveChanges();
                    AtnaujintiStatistika();
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
                role = RolesPriskirimas.SelectedIndex.ToString();
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

        private void Istrinti(string email)
        {
            using (var kontekstas = new DuomenuBazesKontekstas())
            {
                var savedItems = kontekstas.SavedItems.Where(c => c.Email == email).ToList();
                foreach(var savedItem in savedItems)
                {
                    kontekstas.SavedItems.Remove(savedItem);
                }
                kontekstas.SaveChangesAsync();

                var result = kontekstas.NaudotojoDuomenys.SingleOrDefault(b => b.Email == email);

                if (result != null)
                {
                    kontekstas.NaudotojoDuomenys.Remove(result);
                    kontekstas.SaveChanges();
                    AtnaujintiStatistika();
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
                        Role = "0"
                    };
                    kontekstas.NaudotojoDuomenys.Add(naudotojoDuomenys);
                    kontekstas.SaveChanges();
                    AtnaujintiStatistika();
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
        private static List<string> Role = new List<string>();
        private void RodytiVartotojus(object sender, EventArgs e)
        {
          Skaityti(ref Email, ref Role);
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

        private static void Skaityti(ref List<string> Email, ref List<string> Role)
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
        private static List<int> Skaiciuoti()
        {
            using (var kontekstas = new DuomenuBazesKontekstas())
            {
                List<int> StatistikosListas = new List<int>();

                var VisiNariai = kontekstas.NaudotojoDuomenys
               .Where(o => o.NaudotojoID >= 0)
               .Count();
                StatistikosListas.Add(VisiNariai);

                var Administratoriai = kontekstas.NaudotojoDuomenys
               .Where(o => o.Role == "1")
               .Count();
                StatistikosListas.Add(Administratoriai);

                var PaprastiNariai = kontekstas.NaudotojoDuomenys
               .Where(o => o.Role == "0")
               .Count();
                StatistikosListas.Add(PaprastiNariai);

                var Prekes = kontekstas.PrekiuDuomenys
               .Where(o => o.PrekiuID >= 0)
               .Count();
                StatistikosListas.Add(Prekes);

                return StatistikosListas;
            }
        }
    }
}
