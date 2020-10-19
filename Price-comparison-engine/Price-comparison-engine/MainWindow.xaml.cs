using Price_comparison_engine.Classes;
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
            var environment = System.Environment.CurrentDirectory;
            var directoryInfo = Directory.GetParent(environment).Parent;
            if (directoryInfo != null)
            {
                var projectDirectory = directoryInfo.FullName;
                AppDomain.CurrentDomain.SetData("DataDirectory", projectDirectory);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataDirectoryInitialize();
            ReadFromDatabase(ref pageUrl, ref imgUrl);
            if (pageUrl.Count >= 3 && imgUrl.Count >= 3)
            {
                Img1.Source = new BitmapImage(new Uri(imgUrl[0], UriKind.Absolute));
                Img2.Source = new BitmapImage(new Uri(imgUrl[1], UriKind.Absolute));
                Img3.Source = new BitmapImage(new Uri(imgUrl[2], UriKind.Absolute));
            }
        }

        private void QaClick(object sender, RoutedEventArgs e)
        {
            var qaWindow = new QaWindow();
            qaWindow.Show();
        }

        private void ContactClick(object sender, RoutedEventArgs e)
        {
            var contactWindow = new ContactWindow();
            contactWindow.Show();
        }

        public static string word;

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            word = SearchBox.Text;
            var itemsWindow = new ItemsWindow(null);
            itemsWindow.Show();
        }

        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            var registeringWindow = new RegisteringWindow(this);
            registeringWindow.Show();
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow(this);
            loginWindow.Show();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var differenceOfWidth = this.ActualWidth / 1.2;
            var differenceOfLength = this.ActualHeight / 1.1;
            var differenceOfBlockWidth = this.ActualHeight / 1.7;
            var differenceOfImageWidth = this.ActualWidth / 1.4;
            var differenceOfImageLength = this.ActualHeight / 1.4;
            MygtukoResize(LoginButton, differenceOfWidth, differenceOfLength);
            MygtukoResize(RegisteringButton, differenceOfWidth, differenceOfLength);
            MygtukoResize(QaButton, differenceOfWidth, differenceOfLength);
            MygtukoResize(ContactButton, differenceOfWidth, differenceOfLength);
            MygtukoResize(SearchButton, differenceOfWidth, differenceOfLength);
            TextBoxResize(SearchBox, differenceOfWidth / 3, differenceOfLength);
            RectangleResize(MiddleLine, differenceOfBlockWidth);
            RectangleWidening(UpperLine);
            RectangleWidening(MiddleLine);
            RectangleWidening(BottomLine);
            SlideShowResize(Img1, differenceOfImageWidth, differenceOfImageLength);
            SlideShowResize(Img2, differenceOfImageWidth, differenceOfImageLength);
            SlideShowResize(Img3, differenceOfImageWidth, differenceOfImageLength);
            SlideShowResize(ButtonRight, differenceOfLength, differenceOfLength);
            SlideShowResize(ButtonLeft, differenceOfLength, differenceOfLength);
        }

        private void MygtukoResize(Button button, double width=0, double length=0)
        {
            button.Width = this.ActualWidth - width;
            button.Height = this.ActualHeight - length;
        }

        private void TextBlockResize(TextBlock textBlock, double width=0, double length=0)
        {
            textBlock.Width = this.ActualWidth - width;
            textBlock.Height = this.ActualHeight - length;
        }

        private void TextBoxResize(TextBox textBox, double width=0, double length=0)
        {
            textBox.Width = this.ActualWidth - width;
            textBox.Height = this.ActualHeight - length;
        }
        private void RectangleResize(Rectangle rectangle, double length=0)
        {
            rectangle.Height = this.ActualHeight - length;
        }
        private void RectangleWidening(Rectangle rectangle,double differenceOfWidth=0)
        {
            rectangle.Width = this.ActualWidth-differenceOfWidth;
        }
        private void SlideShowResize(Image image, double width=0, double length=0)
        {
            image.Width = this.ActualWidth-width;
            image.Height = this.ActualHeight-length;
        }

        private static List<string> pageUrl = new List<string>();
        private static List<string> imgUrl = new List<string>();

        public static int indexFront = 3;
        public static int indexBack = 0;
        public static int urlIndex = 0;

        private void Slider_Back(object sender, MouseButtonEventArgs e)
        {
            if (indexBack > 0)
            {
                urlIndex--;
                indexBack--;
                indexFront--;
                Img1.Source = new BitmapImage(new Uri(imgUrl[indexBack], UriKind.Absolute));
                Img2.Source = new BitmapImage(new Uri(imgUrl[indexBack + 1], UriKind.Absolute));
                Img3.Source = new BitmapImage(new Uri(imgUrl[indexBack + 2], UriKind.Absolute));
            }
        }

        private void Slider_Front(object sender, MouseButtonEventArgs e)
        {
            if (indexFront < pageUrl.Count - 1)
            {
                urlIndex++;
                Img1.Source = new BitmapImage(new Uri(imgUrl[indexFront - 2], UriKind.Absolute));
                Img2.Source = new BitmapImage(new Uri(imgUrl[indexFront - 1], UriKind.Absolute));
                Img3.Source = new BitmapImage(new Uri(imgUrl[indexFront], UriKind.Absolute));
                indexFront++;
                indexBack++;
            }
        }

        private void Img1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (pageUrl.Count >= 3)
            {
                System.Diagnostics.Process.Start(pageUrl[urlIndex]);
            }
        }

        private void Img2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (pageUrl.Count >= 3)
            {
                System.Diagnostics.Process.Start(pageUrl[urlIndex + 1]);
            }
        }

        private void Img3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (pageUrl.Count >= 3)
            {
                System.Diagnostics.Process.Start(pageUrl[urlIndex + 2]);
            }
        }

        private static void ReadFromDatabase(ref List<string> pageUrl, ref List<string> imgUrl)
        {
            using (var context = new DatabaseContext())
            {
                var tempPageUrl = context.ItemsTable.Select(column => column.PageUrl).ToList();
                var tempImgUrl = context.ItemsTable.Select(column => column.ImgUrl).ToList();

                for (int i = 0; i < tempPageUrl.Count; i++)
                {
                    if (tempPageUrl.ElementAt(i) != null && tempImgUrl.ElementAt(i) != null)
                    {
                        pageUrl.Add(tempPageUrl.ElementAt(i));
                        imgUrl.Add(tempImgUrl.ElementAt(i));
                    }
                }
            }
        }
    }
}