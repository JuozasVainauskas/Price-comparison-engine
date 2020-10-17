using Price_comparison_engine.Classes;
using System.Windows;


namespace Price_comparison_engine
{
    /// <summary>
    /// Interaction logic for PatvirtinimoLangas.xaml
    /// </summary>
    public partial class ConfirmationWindow : Window
    {
        readonly MainWindow mainWindow;
        readonly RegistrationWindow registrationWindow;
        readonly DatabaseContext context;
        private string code;
        public ConfirmationWindow(DatabaseContext context, MainWindow mainWindow, RegistrationWindow registrationWindow, string code, string email)
        {
            InitializeComponent();
            new SendEmail(code, email);
            this.mainWindow = mainWindow;
            this.registrationWindow = registrationWindow;
            this.context = context;
            this.code = code;
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            if (code.Equals(textboxConfirm.Text))
            {
                context.SaveChanges();

                mainWindow.Close();
                registrationWindow.Close();
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
