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

        private static int role = 0;
        private void SkirtiRole(string email,int role)
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
            var pattern2 = new Regex(@"([a-zA-Z0-9]+)(@gmail.com)$", RegexOptions.Compiled);

            if (email.Text == "")
            {
                MessageBox.Show("Įveskite vartotojo el.paštą, kuriam norite priskirti naują rolę!");
            }
            else if (!pattern2.IsMatch(email.Text))
            {
                MessageBox.Show("Email turi būti rašomas tokia tvarka:\nTuri būti naudojamos raidės arba skaičiai,\nTuri būti nors vienas skaičius arba raidė,\nEmail'o pabaiga turi baigtis: @gmail.com, pvz.: kazkas@gmail.com");
            }
            else if (RolesPriskirimas.SelectedIndex!= -1)
            {
                role = RolesPriskirimas.SelectedIndex;
                SkirtiRole(email.Text, role);
                MessageBox.Show(email.Text + " priskirta nauja rolė!");
                email.Text = "";
            }
        }
    }
}
