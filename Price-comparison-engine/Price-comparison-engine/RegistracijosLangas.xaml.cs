using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
    /// Interaction logic for RegistracijosLangas.xaml
    /// </summary>
    public partial class RegistracijosLangas : Window
    {
        readonly MainWindow pagrindinisLangas;

        public RegistracijosLangas(MainWindow pagrindinisLangas)
        {
            InitializeComponent();
            this.pagrindinisLangas = pagrindinisLangas;
        }

        private void Registruotis_Mygtukas(object sender, RoutedEventArgs e)
        {
            if (Email.Text == "" || Slaptazodis.Password == "" || SlaptazodisPatvirtinti.Password == "")
            {
                MessageBox.Show("Prašome užpildyti visus laukus.");
            }
            else if (Slaptazodis.Password != SlaptazodisPatvirtinti.Password)
            {
                MessageBox.Show("Slaptažodžiai nesutampa.");
            }
            else
            {
                using (SqlConnection sqlRegistruotis = new SqlConnection(@"Data Source=localhost\sqlexpress; Initial Catalog=DuomenuBaze; Integrated Security=True;"))
                {
                    SqlDataAdapter duomenuAdapteris = new SqlDataAdapter("SELECT Email FROM NaudotojoLentele WHERE Email='"+Email.Text.Trim()+"'", sqlRegistruotis);
                    DataTable duomenuLentele = new DataTable();
                    duomenuAdapteris.Fill(duomenuLentele);
                    if (duomenuLentele.Rows.Count >= 1)
                    {
                        MessageBox.Show("Toks email jau panaudotas. Pabandykite kitą.");
                    }
                    else
                    {
                        //SELECT ISNULL(MAX(CAST(NaudotojoID AS int)), 0) + 1 FROM NaudotojoLentele

                        sqlRegistruotis.Open();
                        SqlCommand sqlKomanda = new SqlCommand("PridetiNaudotoja", sqlRegistruotis);
                        sqlKomanda.CommandType = CommandType.StoredProcedure;
                        sqlKomanda.Parameters.AddWithValue("@Email", Email.Text.Trim());
                        sqlKomanda.Parameters.AddWithValue("@Slaptazodis", Slaptazodis.Password.Trim());
                        sqlKomanda.ExecuteNonQuery();

                        MainWindowLogedIn mainwindowlogedin = new MainWindowLogedIn();
                        mainwindowlogedin.Show();
                        this.Close();
                        pagrindinisLangas.Close();
                    }
                }
            }
        }
    }
}
