using System;
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
    /// Interaction logic for ContactWindow.xaml
    /// </summary>
    public partial class ContactWindow : Window
    {
        public ContactWindow()
        {
            InitializeComponent();
        }
        private void TbReferAFriend_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                LaunchEmailClientByShellExecute();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        [System.Runtime.InteropServices.DllImport("shell32.dll")]
        public static extern IntPtr ShellExecute(IntPtr hwnd, string lpOperation,
                    string lpFile, string lpParameters, string lpDirectory, int nShowCmd);

        private static void LaunchEmailClientByShellExecute()
        {
            ShellExecute(IntPtr.Zero, "open", "mailto:vitkauskas.j@gmail.com?subject=Read%20This&body=message%20contents", "", "", 4/* sw_shownoactivate */);
        }
    }

}
