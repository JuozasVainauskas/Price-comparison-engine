using Price_comparison_engine.Classes;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ConfirmationWindow.xaml
    /// </summary>
    public partial class ConfirmationWindow : Window
    {
        readonly MainWindow mainWindow;
        readonly RegisteringWindow registeringWindow;
        readonly DatabaseContext context;
        private string code;
        public ConfirmationWindow(DatabaseContext context, MainWindow mainWindow, RegisteringWindow registeringWindow, string code, string email)
        {
            InitializeComponent();
            new SendEmail(code, email);
            this.mainWindow = mainWindow;
            this.registeringWindow = registeringWindow;
            this.context = context;
            this.code = code;
        }

        private void ConfirmClick(object sender, RoutedEventArgs e)
        {
            if (code.Equals(ConfirmBox.Text))
            {
                context.SaveChanges();

                mainWindow.Close();
                registeringWindow.Close();
                MessageBox.Show("Sėkmingai prisiregistravote.");

                var mainWindowLoggedIn = new MainWindowLoggedIn();
                mainWindowLoggedIn.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Blogai įvestas kodas.");
            }
        }
    }
}
