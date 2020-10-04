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
            var sqlPrisijungti = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\PCEDatabase.mdf;Integrated Security=SSPI;Connect Timeout=30");
            try
            {
                if (sqlPrisijungti.State == ConnectionState.Closed)
                {
                    sqlPrisijungti.Open();
                }

                var eile = "UPDATE NaudotojoDuomenys SET Role = @Role WHERE Email=@Email";
                var sqlKomanda = new SqlCommand(eile, sqlPrisijungti);
                sqlKomanda.CommandType = CommandType.Text;
                sqlKomanda.Parameters.AddWithValue("@Role", role);
                sqlKomanda.Parameters.AddWithValue("@Email", email);
                sqlKomanda.ExecuteNonQuery();
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
