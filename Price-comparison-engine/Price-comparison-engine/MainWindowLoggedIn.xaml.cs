using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Price_comparison_engine.Classes;

namespace Price_comparison_engine
{
    public partial class MainWindowLoggedIn : Window
    {
        public MainWindowLoggedIn()
        {
            InitializeComponent();
            if (LoginWindow.UserRole.Equals(Role.Admin))
            {
                AdminButton.IsEnabled = true;
                AdminButton.Visibility = Visibility.Visible;
            }
            ReadImages(ref _pageUrl, ref _imgUrl);
            if (_pageUrl.Count >= 3 && _imgUrl.Count >= 3)
            {
                Img1.Source = new BitmapImage(new Uri(_imgUrl[0], UriKind.Absolute));
                Img2.Source = new BitmapImage(new Uri(_imgUrl[1], UriKind.Absolute));
                Img3.Source = new BitmapImage(new Uri(_imgUrl[2], UriKind.Absolute));
            }
        }
        public static async void GetHtmlAssync(DataGrid dataGridLoggedIn)
        {
            if (ReadSavedItems(LoginWindow.Email).Any())
            {
                foreach (var item in ReadSavedItems(LoginWindow.Email))
                {
                    dataGridLoggedIn.Items.Add(item);
                }
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var imageWidthDifference = this.ActualWidth / 1.1;
            var imageLengthDifference = this.ActualHeight / 1.1;
            var widthDifference = this.ActualWidth / 1.2;
            TextBoxResize(SearchBox, widthDifference / 3);
            UpperLine.Width = this.ActualWidth;
            RectangleIštempimas(BottomLine);
            SlideShowResize(Img1, imageWidthDifference, imageLengthDifference);
            SlideShowResize(Img2, imageWidthDifference, imageLengthDifference);
            SlideShowResize(Img3, imageWidthDifference, imageLengthDifference);
        }
        private static List<string> _pageUrl = new List<string>();
        private static List<string> _imgUrl = new List<string>();

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
                Img1.Source = new BitmapImage(new Uri(_imgUrl[indexBack], UriKind.Absolute));
                Img2.Source = new BitmapImage(new Uri(_imgUrl[indexBack + 1], UriKind.Absolute));
                Img3.Source = new BitmapImage(new Uri(_imgUrl[indexBack + 2], UriKind.Absolute));
            }
        }

        private void Slider_Front(object sender, MouseButtonEventArgs e)
        {
            if (indexFront < _pageUrl.Count - 1)
            {
                urlIndex++;
                Img1.Source = new BitmapImage(new Uri(_imgUrl[indexFront - 2], UriKind.Absolute));
                Img2.Source = new BitmapImage(new Uri(_imgUrl[indexFront - 1], UriKind.Absolute));
                Img3.Source = new BitmapImage(new Uri(_imgUrl[indexFront], UriKind.Absolute));
                indexFront++;
                indexBack++;
            }
        }

        private void Img1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_pageUrl.Count >= 3)
            {
                System.Diagnostics.Process.Start(_pageUrl[urlIndex]);
            }
        }

        private void Img2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_pageUrl.Count >= 3)
            {
                System.Diagnostics.Process.Start(_pageUrl[urlIndex + 1]);
            }
        }

        private void Img3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_pageUrl.Count >= 3)
            {
                System.Diagnostics.Process.Start(_pageUrl[urlIndex + 2]);
            }
        }
        private static void ReadImages(ref List<string> pageUrl, ref List<string> imgUrl)
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
  
        private void RectangleResize(Rectangle rectangle, double length)
        {
            rectangle.Height = this.ActualHeight - length;
        }

        private void MygtukoResize(Button button, double width, double length)
        {
            button.Width = this.ActualWidth - width;
            button.Height = this.ActualHeight - length;
        }

        private void TextBoxResize(TextBox textBlock, double width)
        {
            textBlock.Width = this.ActualWidth - width;
        }

        private void RectangleIštempimas(Rectangle rectangle)
        {
            rectangle.Width = this.ActualWidth;
        }

        private void SlideShowResize(Image image, double width, double length)
        {
            image.Width = this.ActualWidth - width;
            image.Height = this.ActualHeight - length;
        }

        public static int slideCounter = 1;
        public static int slideCounter2 = 3;
        public static int slideCounter_2 = 1;
        public static int slideCounter2_2 = 3;

        private void LogOffButtonClick(object sender, RoutedEventArgs e)
        {
            LoginWindow.Email = "";
            LoginWindow.UserRole = Role.User;
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            MainWindow.word = SearchBox.Text;
            ItemsWindow itemsWindow = new ItemsWindow(this);
            itemsWindow.Show();
        }

        private void AdminLoginClick(object sender, RoutedEventArgs e)
        {
                var adminWindow = new Admin(this);
                adminWindow.Show();
        }

        private void ImageClick_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var name = (((Image)sender).DataContext as Item)?.Name;
            var window = new ParticularItemWindow(name);
            window.Show();
        }
        private void RateClick(object sender, RoutedEventArgs e)
        {
            var ratingWindow = new RatingWindow();
            ratingWindow.Show();
        }

        private void LinkButtonClick(object sender, RoutedEventArgs e)
        {
            var link = (((Button)sender).DataContext as Item)?.Link;
            if (link != null) Process.Start(link);
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            var currentRowIndex = DataGridLoggedIn.Items.IndexOf(DataGridLoggedIn.CurrentItem);

            using (var context = new DatabaseContext())
            {
                var tempItem = (Item)DataGridLoggedIn.Items.GetItemAt(currentRowIndex);

                var result = context.SavedItems.SingleOrDefault(b => b.Email == LoginWindow.Email && b.PageUrl == tempItem.Link && b.ImgUrl == tempItem.Picture && b.ShopName == tempItem.Seller && b.ItemName == tempItem.Name && b.Price == tempItem.Price);

                if (result != null)
                {
                    context.SavedItems.Remove(result);
                    context.SaveChanges();
                }
            }

            DataGridLoggedIn.Items.RemoveAt(currentRowIndex);
        }

        private void DataGridLoggedIn_Initialized(object sender, EventArgs e)
        {
            GetHtmlAssync(DataGridLoggedIn);
        }

        private static List<Item> ReadSavedItems(string email)
        {
            var item = new List<Item>();

            using (var context = new DatabaseContext())
            {
                var result = context.SavedItems.Where(x => x.Email == email).Select(x => new Item { Link = x.PageUrl, Picture = x.ImgUrl, Seller = x.ShopName, Name = x.ItemName, Price = x.Price }).ToList();

                foreach (var singleItem in result)
                {
                    item.Add(singleItem);
                }
            }
            return item;
        }
    }
}
    
