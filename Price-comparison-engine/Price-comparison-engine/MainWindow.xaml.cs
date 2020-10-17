using Price_comparison_engine.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
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
            var projectDirectory = Directory.GetParent(enviroment).Parent.FullName;
            AppDomain.CurrentDomain.SetData("DataDirectory", projectDirectory);
        }

        public MainWindow()
        {
            InitializeComponent();
            DataDirectoryInitialize();
            Read(ref pageUrl, ref imgUrl);
            if (pageUrl.Count >= 3 && imgUrl.Count >= 3)
            {
                img1.Source = new BitmapImage(new Uri(imgUrl[0], UriKind.Absolute));
                img2.Source = new BitmapImage(new Uri(imgUrl[1], UriKind.Absolute));
                img3.Source = new BitmapImage(new Uri(imgUrl[2], UriKind.Absolute));
            }
        }

        private void Faq(object sender, RoutedEventArgs e)
        {
            var dukLangoAtidarymas = new FaqWindow();
            dukLangoAtidarymas.Show();
        }

        private void Contacts(object sender, RoutedEventArgs e)
        {
            var openContactsWindow = new ContactsWindow();
            openContactsWindow.Show();
        }

        public  static string word;

        private void Search(object sender, RoutedEventArgs e)
        {
            word = searchField.Text;
            var openGoodsWindow = new GoodsWindow(null);
            openGoodsWindow.Show();
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            var openRegistrationWindow = new RegistrationWindow(this);
            openRegistrationWindow.Show();
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            var openLoginWindow = new LoginWindow(this);
            openLoginWindow.Show();
        }

        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var widhDifference = this.ActualWidth / 1.2;
            var heightDifference = this.ActualHeight / 1.1;
            var widthDifferenceForBlocks = this.ActualHeight / 1.7;
            var widthDifferenceForPhotos = this.ActualWidth / 1.4;
            var heightDifferenceForPhotos = this.ActualHeight / 1.4;
            ButtonResize(loginButton, widhDifference, heightDifference);
            ButtonResize(registrationButton, widhDifference, heightDifference);
            ButtonResize(faqButton, widhDifference, heightDifference);
            ButtonResize(contactsButton, widhDifference, heightDifference);
            ButtonResize(search, widhDifference, heightDifference);
            TextBoxResize(searchField, widhDifference / 3, heightDifference);
            RectangleResize(middleLine, widthDifferenceForBlocks);
            RectangleResize(upperLine);
            RectangleResize(middleLine);
            RectangleResize(bottomLine);
            SlideShowResize(img1, widthDifferenceForPhotos, heightDifferenceForPhotos);
            SlideShowResize(img2, widthDifferenceForPhotos, heightDifferenceForPhotos);
            SlideShowResize(img3, widthDifferenceForPhotos, heightDifferenceForPhotos);
            SlideShowResize(toLeft, heightDifference, heightDifference);
            SlideShowResize(toRight, heightDifference, heightDifference);
        }

        private void ButtonResize(Button button, double width, double height)
        {
            button.Width = this.ActualWidth - width;
            button.Height = this.ActualHeight - height;
        }

        private void TextBlockResize(TextBlock textblock, double width, double height)
        {
            textblock.Width = this.ActualWidth - width;
            textblock.Height = this.ActualHeight - height;
        }

        private void TextBoxResize(TextBox textbox, double width, double height)
        {
            textbox.Width = this.ActualWidth - width;
            textbox.Height = this.ActualHeight - height;
        }
        private void RectangleResize(Rectangle area, double height)
        {
            area.Height = this.ActualHeight - height;
        }
        private void RectangleResize(Rectangle area)
        {
            area.Width = this.ActualWidth;
        }
        private void SlideShowResize(Image image,double width, double height)
        {
            image.Width = this.ActualWidth-width;
            image.Height = this.ActualHeight-height;
        }

        private static List<string> pageUrl = new List<string>();
        private static List<string> imgUrl = new List<string>();

        public static int IndexFront = 3;
        public static int IndexBack = 0;
        public static int UrlIndex = 0;

        private void SliderBack(object sender, MouseButtonEventArgs e)
        {
            if (IndexBack > 0)
            {
                UrlIndex--;
                IndexBack--;
                IndexFront--;
                img1.Source = new BitmapImage(new Uri(imgUrl[IndexBack], UriKind.Absolute));
                img2.Source = new BitmapImage(new Uri(imgUrl[IndexBack + 1], UriKind.Absolute));
                img3.Source = new BitmapImage(new Uri(imgUrl[IndexBack + 2], UriKind.Absolute));
            }
        }

        private void SliderFront(object sender, MouseButtonEventArgs e)
        {
            if (IndexFront < pageUrl.Count - 1)
            {
                UrlIndex++;
                img1.Source = new BitmapImage(new Uri(imgUrl[IndexFront - 2], UriKind.Absolute));
                img2.Source = new BitmapImage(new Uri(imgUrl[IndexFront - 1], UriKind.Absolute));
                img3.Source = new BitmapImage(new Uri(imgUrl[IndexFront], UriKind.Absolute));
                IndexFront++;
                IndexBack++;
            }
        }

        private void Img1MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (pageUrl.Count >= 3)
            {
                System.Diagnostics.Process.Start(pageUrl[UrlIndex]);
            }
        }

        private void Img2MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (pageUrl.Count >= 3)
            {
                System.Diagnostics.Process.Start(pageUrl[UrlIndex + 1]);
            }
        }

        private void Img3MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (pageUrl.Count >= 3)
            {
                System.Diagnostics.Process.Start(pageUrl[UrlIndex + 2]);
            }
        }

        private static void Read(ref List<string> pageUrl, ref List<string> imgUrl)
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