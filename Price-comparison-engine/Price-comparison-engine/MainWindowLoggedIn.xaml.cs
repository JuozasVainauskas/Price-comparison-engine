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
            if (LoginWindow.userRole.Equals(Role.Admin))
            {
                administration.IsEnabled = true;
                administration.Visibility = Visibility.Visible;
            }
            Read(ref pageUrl, ref imgUrl);
            if (pageUrl.Count >= 3 && imgUrl.Count >= 3)
            {
                img1.Source = new BitmapImage(new Uri(imgUrl[0], UriKind.Absolute));
                img2.Source = new BitmapImage(new Uri(imgUrl[1], UriKind.Absolute));
                img3.Source = new BitmapImage(new Uri(imgUrl[2], UriKind.Absolute));
            }
        }
        public static async void GetHtmlAssync(DataGrid DataGridLoggedIn)
        {
            if (ReadSavedItems(LoginWindow.email).Any())
            {
                foreach (var item in ReadSavedItems(LoginWindow.email))
                {
                    DataGridLoggedIn.Items.Add(item);
                }
            }
        }

        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var widthDifferenceForPhotos = this.ActualWidth / 1.1;
            var heightDifferenceForPhotos = this.ActualHeight / 1.1;
            var widthDifference = this.ActualWidth / 1.2;
            TextBoxResize(searchField, widthDifference / 3);
            upperLine.Width = this.ActualWidth;
            RectangleResize(bottomLine);
            SlideShowResize(img1, widthDifferenceForPhotos, heightDifferenceForPhotos);
            SlideShowResize(img2, widthDifferenceForPhotos, heightDifferenceForPhotos);
            SlideShowResize(img3, widthDifferenceForPhotos, heightDifferenceForPhotos);
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
  
        private void RectangleResize(Rectangle area, double height)
        {
            area.Height = this.ActualHeight - height;
        }

        private void MygtukoResize(Button button, double width, double height)
        {
            button.Width = this.ActualWidth - width;
            button.Height = this.ActualHeight - height;
        }

        private void TextBoxResize(TextBox textbox, double width)
        {
            textbox.Width = this.ActualWidth - width;
        }

        private void RectangleResize(Rectangle area)
        {
            area.Width = this.ActualWidth;
        }

        private void SlideShowResize(Image image, double width, double height)
        {
            image.Width = this.ActualWidth - width;
            image.Height = this.ActualHeight - height;
        }

       // public static int slideCounter = 1;
       // public static int slideCounter2 = 3;
       // public static int slideCounter_2 = 1;
       // public static int slideCounter2_2 = 3;

        private void LogOut(object sender, RoutedEventArgs e)
        {
            LoginWindow.email = "";
            LoginWindow.userRole = Role.User;
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            MainWindow.word = searchField.Text;
            GoodsWindow goodsWindow = new GoodsWindow(this);
            goodsWindow.Show();
        }

        private void AdminLogin(object sender, RoutedEventArgs e)
        {
                var adminLangoAtidarymas = new Admin(this);
                adminLangoAtidarymas.Show();
        }

        private void CurrentGoods(object sender, MouseButtonEventArgs e)
        {
            var name = (((Image)sender).DataContext as Item)?.Name;
            var langas = new CurrentGoods(name);
            langas.Show();
        }
        private void Evaluate(object sender, RoutedEventArgs e)
        {
            var evaluationWindow = new EvaluationWindow();
            evaluationWindow.Show();
        }

        private void Link(object sender, RoutedEventArgs e)
        {
            var link = (((Button)sender).DataContext as Item)?.Link;
            if (link != null) Process.Start(link);
        }

        private void delete(object sender, RoutedEventArgs e)
        {
            var currentRowIndex = DataGridLoggedIn.Items.IndexOf(DataGridLoggedIn.CurrentItem);

            using (var context = new DatabaseContext())
            {
                var tempItem = (Item)DataGridLoggedIn.Items.GetItemAt(currentRowIndex);

                var result = context.SavedItems.SingleOrDefault(b => b.Email == LoginWindow.email && b.PageUrl == tempItem.Link && b.ImgUrl == tempItem.Photo && b.ShopName == tempItem.Seller && b.ItemName == tempItem.Name && b.Price == tempItem.Price);

                if (result != null)
                {
                    context.SavedItems.Remove(result);
                    context.SaveChanges();
                }
            }

            DataGridLoggedIn.Items.RemoveAt(currentRowIndex);
        }

        private void InitialiseDatagrid(object sender, EventArgs e)
        {
            GetHtmlAssync(DataGridLoggedIn);
        }

        private static List<Item> ReadSavedItems(string email)
        {
            var item = new List<Item>();

            using (var context = new DatabaseContext())
            {
                var result = context.SavedItems.Where(x => x.Email == email).Select(x => new Item { Link = x.PageUrl, Photo = x.ImgUrl, Seller = x.ShopName, Name = x.ItemName, Price = x.Price }).ToList();

                foreach (var singleItem in result)
                {
                    item.Add(singleItem);
                }
            }
            return item;
        }
    }
}
    
