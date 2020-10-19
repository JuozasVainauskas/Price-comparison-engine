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
        private readonly MainWindow _mainWindow;
        private readonly RegisteringWindow _registeringWindow;
        private readonly DatabaseContext _context;
        private readonly string _code;
        public ConfirmationWindow(DatabaseContext context, MainWindow mainWindow, RegisteringWindow registeringWindow, string code, string email)
        {
            InitializeComponent();
            new SendEmail(code, email);
            _mainWindow = mainWindow;
            _registeringWindow = registeringWindow;
            _context = context;
            _code = code;
        }

        private void ConfirmClick(object sender, RoutedEventArgs e)
        {
            if (_code.Equals(ConfirmBox.Text))
            {
                _context.SaveChanges();

                _mainWindow.Close();
                _registeringWindow.Close();
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
