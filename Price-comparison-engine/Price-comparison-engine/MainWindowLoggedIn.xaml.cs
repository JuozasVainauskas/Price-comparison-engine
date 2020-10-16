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
            if (LoginWindow.NarioRole.Equals(Role.Admin))
            {
                administravimas.IsEnabled = true;
                administravimas.Visibility = Visibility.Visible;
            }
            Skaityti(ref pageUrl, ref imgUrl);
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

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var skirtumasPlocioNuotraukai = this.ActualWidth / 1.1;
            var skirtumasIlgioNuotraukai = this.ActualHeight / 1.1;
            var skirtumasPlocio = this.ActualWidth / 1.2;
            TextBoxResize(ieškojimoLaukas, skirtumasPlocio / 3);
            viršutinėlinija.Width = this.ActualWidth;
            RectangleIštempimas(apatinėLinija);
            SlideShowResize(img1, skirtumasPlocioNuotraukai, skirtumasIlgioNuotraukai);
            SlideShowResize(img2, skirtumasPlocioNuotraukai, skirtumasIlgioNuotraukai);
            SlideShowResize(img3, skirtumasPlocioNuotraukai, skirtumasIlgioNuotraukai);
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
                img1.Source = new BitmapImage(new Uri(imgUrl[indexBack], UriKind.Absolute));
                img2.Source = new BitmapImage(new Uri(imgUrl[indexBack + 1], UriKind.Absolute));
                img3.Source = new BitmapImage(new Uri(imgUrl[indexBack + 2], UriKind.Absolute));
            }
        }

        private void Slider_Front(object sender, MouseButtonEventArgs e)
        {
            if (indexFront < pageUrl.Count - 1)
            {
                urlIndex++;
                img1.Source = new BitmapImage(new Uri(imgUrl[indexFront - 2], UriKind.Absolute));
                img2.Source = new BitmapImage(new Uri(imgUrl[indexFront - 1], UriKind.Absolute));
                img3.Source = new BitmapImage(new Uri(imgUrl[indexFront], UriKind.Absolute));
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
        private static void Skaityti(ref List<string> pageUrl, ref List<string> imgUrl)
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
  
        private void RectangleResize(Rectangle plotelis, double ilgis)
        {
            plotelis.Height = this.ActualHeight - ilgis;
        }

        private void MygtukoResize(Button mygtukas, double plotis, double ilgis)
        {
            mygtukas.Width = this.ActualWidth - plotis;
            mygtukas.Height = this.ActualHeight - ilgis;
        }

        private void TextBoxResize(TextBox tekstBoksas, double plotis)
        {
            tekstBoksas.Width = this.ActualWidth - plotis;
        }

        private void RectangleIštempimas(Rectangle plotelis)
        {
            plotelis.Width = this.ActualWidth;
        }

        private void SlideShowResize(Image nuotrauka, double plotis, double ilgis)
        {
            nuotrauka.Width = this.ActualWidth - plotis;
            nuotrauka.Height = this.ActualHeight - ilgis;
        }

        public static int slideCounter = 1;
        public static int slideCounter2 = 3;
        public static int slideCounter_2 = 1;
        public static int slideCounter2_2 = 3;

        private void AtsijungimoMygtukas_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow.email = "";
            LoginWindow.NarioRole = Role.User;
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Ieškoti_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.word = ieškojimoLaukas.Text;
            GoodsWindow prekiuLangas = new GoodsWindow(this);
            prekiuLangas.Show();
        }

        private void AdminPrisijungimas(object sender, RoutedEventArgs e)
        {
                var adminLangoAtidarymas = new Admin(this);
                adminLangoAtidarymas.Show();
        }

        private void ImageClick_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var name = (((Image)sender).DataContext as Item)?.Name;
            var langas = new KonkretiPreke(name);
            langas.Show();
        }
        private void VertintiClick(object sender, RoutedEventArgs e)
        {
            var vertinimoLangoAtidarymas = new VertinimoLangas();
            vertinimoLangoAtidarymas.Show();
        }

        private void LinkButton_Click(object sender, RoutedEventArgs e)
        {
            var link = (((Button)sender).DataContext as Item)?.Link;
            if (link != null) Process.Start(link);
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var currentRowIndex = DataGridLoggedIn.Items.IndexOf(DataGridLoggedIn.CurrentItem);

            using (var context = new DatabaseContext())
            {
                var tempItem = (Item)DataGridLoggedIn.Items.GetItemAt(currentRowIndex);

                var result = context.SavedItems.SingleOrDefault(b => b.Email == LoginWindow.email && b.PageUrl == tempItem.Link && b.ImgUrl == tempItem.Nuotrauka && b.ShopName == tempItem.Seller && b.ItemName == tempItem.Name && b.Price == tempItem.Price);

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
                var result = context.SavedItems.Where(x => x.Email == email).Select(x => new Item { Link = x.PageUrl, Nuotrauka = x.ImgUrl, Seller = x.ShopName, Name = x.ItemName, Price = x.Price }).ToList();

                foreach (var singleItem in result)
                {
                    item.Add(singleItem);
                }
            }
            return item;
        }
    }
}
    
