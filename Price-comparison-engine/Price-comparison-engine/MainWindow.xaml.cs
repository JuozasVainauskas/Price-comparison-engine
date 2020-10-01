using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Price_comparison_engine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public void DataDirectoryInitialize()
        {
            var enviroment = System.Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(enviroment).Parent.FullName;
            AppDomain.CurrentDomain.SetData("DataDirectory", projectDirectory);
        }

        public MainWindow()
        {
            InitializeComponent();
            DataDirectoryInitialize();
            Read(ref PuslapioURL, ref ImgURL);
            img1.Source = new BitmapImage(new Uri(ImgURL[0], UriKind.Absolute));
            img2.Source = new BitmapImage(new Uri(ImgURL[1], UriKind.Absolute));
            img3.Source = new BitmapImage(new Uri(ImgURL[2], UriKind.Absolute));

        }

        private void DUKButton_Click(object sender, RoutedEventArgs e)
        {
            DUK_Window dukWindowOpener = new DUK_Window();
            dukWindowOpener.Show();
        }

        private void ContactsButton_Click(object sender, RoutedEventArgs e)
        {
            ContactsWindow contactsWindowOpener = new ContactsWindow();
            contactsWindowOpener.Show();
        }

        public  static String zodis;

        private void Search_Button(object sender, RoutedEventArgs e)
        {
            zodis = searchField.Text;
            PrekiuLangas prekiųLangoAtidarymas = new PrekiuLangas();
            prekiųLangoAtidarymas.Show();
        }

        private void Register_Button(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindowOpener = new RegisterWindow(this);
            registerWindowOpener.Show();
        }

        private void Login_Button(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindowOpener = new LoginWindow(this);
            loginWindowOpener.Show();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double skirtumasPlocio = this.ActualWidth / 1.2;
            double skirtumasIlgio = this.ActualHeight / 1.1;
            double skirtumasPlocioBlokeliui = this.ActualHeight / 1.7;
            double skirtumasPlocioNuotraukai = this.ActualWidth / 1.4;
            double skirtumasIlgioNuotraukai = this.ActualHeight / 1.4;
            ButtonResize(loginButton, skirtumasPlocio, skirtumasIlgio);
            ButtonResize(registerButton, skirtumasPlocio, skirtumasIlgio);
            ButtonResize(DUKButton, skirtumasPlocio, skirtumasIlgio);
            ButtonResize(contactsButton, skirtumasPlocio, skirtumasIlgio);
            ButtonResize(searchButton, skirtumasPlocio, skirtumasIlgio);
            TextBoxResize(searchField, skirtumasPlocio / 3, skirtumasIlgio);
            RectangleResize(vidurinėLinija, skirtumasPlocioBlokeliui);
            RectangleIštempimas(viršutinėLinija);
            RectangleIštempimas(vidurinėLinija);
            RectangleIštempimas(apatinėLinija);
            SlideShowResize(img1, skirtumasPlocioNuotraukai, skirtumasIlgioNuotraukai);
            SlideShowResize(img2, skirtumasPlocioNuotraukai, skirtumasIlgioNuotraukai);
            SlideShowResize(img3, skirtumasPlocioNuotraukai, skirtumasIlgioNuotraukai);
            SlideShowResize(iKairePuse, skirtumasIlgio, skirtumasIlgio);
            SlideShowResize(iDesinePuse, skirtumasIlgio, skirtumasIlgio);
        }

        private void ButtonResize(Button button, double width, double length)
        {
            button.Width = this.ActualWidth - width;
            button.Height = this.ActualHeight - length;
        }

        private void TextBlockResize(TextBlock textBlock, double width, double length)
        {
            textBlock.Width = this.ActualWidth - width;
            textBlock.Height = this.ActualHeight - length;
        }

        private void TextBoxResize(TextBox textBox, double width, double length)
        {
            textBox.Width = this.ActualWidth - width;
            textBox.Height = this.ActualHeight - length;
        }
        private void RectangleResize(Rectangle plotelis, double length)
        {
            plotelis.Height = this.ActualHeight - length;
        }
        private void RectangleIštempimas(Rectangle plotelis)
        {
            plotelis.Width = this.ActualWidth;
        }
        private void SlideShowResize(Image nuotrauka,double width, double length)
        {
            nuotrauka.Width = this.ActualWidth-width;
            nuotrauka.Height = this.ActualHeight-length;
        }

        private static List<String> PuslapioURL = new List<string>();
        private static List<String> ImgURL = new List<string>();

        public static int indexFront = 3;
        public static int indexBack = 0;

        public static String link1 = "http://www.google.com";
        public static String link2 = "http://www.facebook.com";
        public static String link3 = "http://www.gmail.com";

        private void Slider_Back(object sender, MouseButtonEventArgs e)
        {
            img1.Source = new BitmapImage(new Uri(ImgURL[indexBack], UriKind.Absolute));
            img2.Source = new BitmapImage(new Uri(ImgURL[indexBack + 1], UriKind.Absolute));
            img3.Source = new BitmapImage(new Uri(ImgURL[indexBack + 2], UriKind.Absolute));
            indexBack--;
            indexFront--;
        }

        private void Slider_Front(object sender, MouseButtonEventArgs e)
        {
            img1.Source = new BitmapImage(new Uri(ImgURL[indexFront-2], UriKind.Absolute));
            img2.Source = new BitmapImage(new Uri(ImgURL[indexFront-1], UriKind.Absolute));
            img3.Source = new BitmapImage(new Uri(ImgURL[indexFront], UriKind.Absolute));
            indexFront++;
            indexBack++;
        }

        private void Img1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(link1);
        }

        private void Img2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(link2);
        }

        private void Img3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(link3);
        }

        private static void Read(ref List<String> PuslapioURL, ref List<String> ImgURL)
        {
            var sqlLogin = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\PCEDatabase.mdf;Integrated Security=SSPI;Connect Timeout=30");
            try
            {
                if (sqlLogin.State == ConnectionState.Closed)
                {
                    sqlLogin.Open();
                }

                var queue = "SELECT PageURL, ImgURL FROM PageData";
                var sqlCommand = new SqlCommand(queue, sqlLogin);
                sqlCommand.CommandType = CommandType.Text;
                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        PuslapioURL.Add(sqlDataReader["PageURL"].ToString());
                        ImgURL.Add(sqlDataReader["ImgURL"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlLogin.Close();
            }
        }
    }
}