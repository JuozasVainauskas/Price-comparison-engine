﻿using System;
using System.Collections.Generic;
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
    /// Interaction logic for PrisijungimoLangas.xaml
    /// </summary>
    public partial class PrisijungimoLangas : Window
    {
        public PrisijungimoLangas()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindowLogedIn mainwindowlogedin = new MainWindowLogedIn();
            mainwindowlogedin.Show();
            this.Close();
        }

        private void Sukurti_nauja_slaptazodi_mygtukas(object sender, RoutedEventArgs e)
        {

        }
    }
}
