using Price_comparison_engine.Klases;
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
                //var sqlRegistruotis = new SqlConnection(@"Data Source=localhost\sqlexpress; Initial Catalog=PCEDatabase; Integrated Security=True;");
                var sqlRegistruotis = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ernes\Documents\GitHub\Price-comparison-engine\Price-comparison-engine\Price-comparison-engine\PCEDatabase.mdf;Integrated Security=True;Connect Timeout=30");
                var duomenuAdapteris = new SqlDataAdapter("SELECT NaudotojoEmail FROM DuomenuStrukturos WHERE NaudotojoEmail='" + Email.Text.Trim() + "'", sqlRegistruotis);
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
                        if (sqlRegistruotis.State == ConnectionState.Closed)
                        {
                            sqlRegistruotis.Open();
                        }
                        var eile = "INSERT INTO DuomenuStrukturos(NaudotojoEmail, NaudotojoSlaptazodis) VALUES (@Email, @Slaptazodis)";
                        var sqlKomanda = new SqlCommand(eile, sqlRegistruotis);
                        sqlKomanda.CommandType = CommandType.Text;
                        sqlKomanda.Parameters.AddWithValue("@Email", Email.Text.Trim());
                        sqlKomanda.Parameters.AddWithValue("@Slaptazodis", Slaptazodis.Password.Trim());
                        sqlKomanda.ExecuteNonQuery();

                        var mainwindowlogedin = new MainWindowLogedIn();
                        mainwindowlogedin.Show();
                        this.Close();
                        pagrindinisLangas.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        sqlRegistruotis.Close();
                    }
                }
            }
        }
    }
}
