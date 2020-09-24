﻿using System;
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

        public PrisijungimoLangas(MainWindow pagrindinisLangas)
        {
            InitializeComponent();
            this.pagrindinisLangas = pagrindinisLangas;
        }

        private void Prisijungti_mygtukas(object sender, RoutedEventArgs e)
        {

            var sqlPrisijungti = new SqlConnection(@"Data Source=localhost\sqlexpress; Initial Catalog=DuomenuBaze; Integrated Security=True;");
            try
            {
                if (sqlPrisijungti.State == ConnectionState.Closed)
                {
                    sqlPrisijungti.Open();
                }
                var eile = "SELECT COUNT(1) FROM NaudotojoLentele WHERE Email=@Email AND Slaptazodis=@Slaptazodis";
                var sqlKomanda = new SqlCommand(eile, sqlPrisijungti);
                sqlKomanda.CommandType = CommandType.Text;
                sqlKomanda.Parameters.AddWithValue("@Email", Email.Text);
                sqlKomanda.Parameters.AddWithValue("@Slaptazodis", Slaptazodis.Password);
                int kiekis = Convert.ToInt32(sqlKomanda.ExecuteScalar());
                if (kiekis == 1)
                {
                    var mainwindowlogedin = new MainWindowLogedIn();
                    mainwindowlogedin.Show();
                    this.Close();
                    pagrindinisLangas.Close();
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
                sqlPrisijungti.Close();
            }
        }

        private void Sukurti_nauja_slaptazodi_mygtukas(object sender, RoutedEventArgs e)
        {

        }
    }
}
