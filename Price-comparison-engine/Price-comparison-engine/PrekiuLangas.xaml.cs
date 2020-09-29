using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for PrekiųLangas.xaml
    /// </summary>
    /// 

    public class Item
    {

        public BitmapImage nuotrauka { get; set; }
        public string Seller { get; set; }
        public double Pricea { get; set; }

        public string Price { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

    }

    public partial class PrekiuLangas : Window
    {
        public PrekiuLangas()
        {
            InitializeComponent();
        }

        private static async void getHtmlAssync(DataGrid dataGridas)
        {

            var prices = new List<Item>();
                if (MainWindow.zodis != "") 
                { 
                    var piguDaiktai = piguPaieska(await piguHtmlPaemimas());
                    surasymasIsPigu(piguDaiktai, prices);
                }
                else
                {
                    var Itemas = new Item { Seller = "Pigu", Name = "tokios prekės " + MainWindow.zodis + " nėra šioje parduotuvėje" };
                    prices.Add(Itemas);
                }
                var avitelosDaiktai = avitelosPaieska(await avitelosHtmlPaemimas());
                var elektromarktDaiktai = elektromarktPaieska(await elektromarktHtmlPaemimas());
                surasymasIsAvitelos(avitelosDaiktai, prices);
                surasymasIsElektromarkt(elektromarktDaiktai, prices);
                
                surikiavimasIrSurasymas(prices, dataGridas);
            
        }

        private static async Task<HtmlDocument> avitelosHtmlPaemimas()
        {
            var url = "https://avitela.lt/paieska/" + MainWindow.zodis;
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            return htmlDocument;
        }

        private static async Task<HtmlDocument> elektromarktHtmlPaemimas()
        {
            Regex regEx = new Regex(" ");
            var urlgalas = regEx.Replace(MainWindow.zodis, "+");
            var url2 = "https://www.elektromarkt.lt/lt/catalogsearch/result/?order=price&dir=desc&q=" + urlgalas;
            var httpClient2 = new HttpClient();
            var html2 = await httpClient2.GetStringAsync(url2);
            var htmlDocument2 = new HtmlDocument();
            htmlDocument2.LoadHtml(html2);
            return htmlDocument2;
        }
        
        private static async Task<HtmlDocument> piguHtmlPaemimas()
        {
            try
            {
                Regex regEx = new Regex(" ");
                var urlgalas = regEx.Replace(MainWindow.zodis, "+");
                var url2 = "https://pigu.lt/lt/search?q=" + urlgalas;
                var httpClient2 = new HttpClient();
                var html2 = await httpClient2.GetStringAsync(url2);
                var htmlDocument2 = new HtmlDocument();
                htmlDocument2.LoadHtml(html2);
                return htmlDocument2;
            }
            catch
            {
                return null;
            }
        }

        private static List<HtmlNode> avitelosPaieska(HtmlDocument htmlDocument)
        {
            try
            {
                var ProductsHtml = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("product-grid active")).ToList();

                var ProductListItems = ProductsHtml[0].Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Contains("col-6 col-md-4 col-lg-4")).ToList();

                return ProductListItems;
            }
            catch
            {
                return null;
            }
        }

        private static List<HtmlNode> elektromarktPaieska(HtmlDocument htmlDocument2)
        {

            try
            {
                var ProductsHtml2 = htmlDocument2.DocumentNode.Descendants("div")
               .Where(node => node.GetAttributeValue("class", "")
               .Equals("manafilters-category-products category-products")).ToList();

                var ProductListItems2 = ProductsHtml2[0].Descendants("li")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Contains("item js-ua-item")).ToList();

                return ProductListItems2;
            }
            catch
            {
                return null;
            }
        }

        private static List<HtmlNode> piguPaieska(HtmlDocument htmlDocument2)
        {

            try
            {
                var ProductsHtml2 = htmlDocument2.DocumentNode.Descendants("div")
               .Where(node => node.GetAttributeValue("class", "")
               .Equals("main-block fr")).ToList();

                var ProductListItems2 = ProductsHtml2[0].Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Contains("product-list-item")).ToList();

                return ProductListItems2;
            }
            catch
            {
                return null;
            }
        }
        private static void surasymasIsAvitelos(List<HtmlNode> ProductListItems, List<Item> prices)
        {
            if (ProductListItems != null)
            {
                foreach (var ProductListItem in ProductListItems)
                {
                    
                    var price = ProductListItem.Descendants("div")
                       .Where(node => node.GetAttributeValue("class", "")
                            .Equals("price")).FirstOrDefault().InnerText.Trim();

                    var name = ProductListItem.Descendants("div")
                       .Where(node => node.GetAttributeValue("class", "")
                             .Equals("name")).FirstOrDefault().InnerText.Trim();
                    
                    var link = ProductListItem.Descendants("a").FirstOrDefault().GetAttributeValue("href", "");

                    price = pasalinimasTrikdanciuSimboliu(price);
                    var priceAtsarg = price;
                    priceAtsarg = pasalinimasEuroSimbol(priceAtsarg);
                    double pricea = Convert.ToDouble(priceAtsarg);
                    var Itemas = new Item { Seller = "Avitela", Name = name, Pricea = pricea, Price = price, Link = link };
                    prices.Add(Itemas);
                }
            }
            else
            {
                var Itemas = new Item { Seller = "Avitela", Name = "tokios prekės " + MainWindow.zodis + " nėra šioje parduotuvėje" };
                prices.Add(Itemas);
            }
        }
        private static void surasymasIsPigu(List<HtmlNode> ProductListItems, List<Item> prices)
        {
            if (ProductListItems != null)
            {
                foreach (var ProductListItem in ProductListItems)
                {

                    var price = ProductListItem.Descendants("span")
                       .Where(node => node.GetAttributeValue("class", "")
                            .Equals("price notranslate")).FirstOrDefault().InnerText.Trim();

                    var name = ProductListItem.Descendants("p")
                       .Where(node => node.GetAttributeValue("class", "")
                             .Equals("product-name")).FirstOrDefault().InnerText.Trim();

                    var link = "https://pigu.lt/"+ProductListItem.Descendants("a").FirstOrDefault().GetAttributeValue("href", "");

                    price = pasalinimasTarpu(price);
                    var priceAtsarg = price;
                    price = pasalinimasEuroSimbol(price);
                    price = price + "€";
                    priceAtsarg = pasalinimasEuroSimbol(priceAtsarg);
                    
                    double pricea = Convert.ToDouble(priceAtsarg);
                    var Itemas = new Item { Seller = "Pigu", Name = name, Pricea = pricea, Price = price, Link = link };
                    prices.Add(Itemas);
                }
            }
            else
            {
                var Itemas = new Item { Seller = "Pigu", Name = "tokios prekės " + MainWindow.zodis + " nėra šioje parduotuvėje" };
                prices.Add(Itemas);
            }
        }
        private static void surasymasIsElektromarkt(List<HtmlNode> ProductListItems2, List<Item> prices)
        {
            if (ProductListItems2 != null)
            {
                foreach (var ProductListItem in ProductListItems2)
                {

                    var name = ProductListItem.Descendants("h2")
                       .Where(node => node.GetAttributeValue("class", "")
                             .Equals("product-name")).FirstOrDefault().InnerText.Trim();

                    var price = ProductListItem.Descendants("span")
                       .Where(node => node.GetAttributeValue("class", "")
                             .Equals("price")).FirstOrDefault().InnerText.Trim();

                    var link = ProductListItem.Descendants("a").FirstOrDefault().GetAttributeValue("href", "");

                    string imgLink = ProductListItem.Descendants("img").FirstOrDefault().GetAttributeValue("src", "");

                    /*
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();

                    bitmap.UriSource = new Uri(imgLink, UriKind.Absolute);
                    bitmap.EndInit();
                    Image a = new Image();
                    a.Source = bitmap;
                    */

                    var imgUrl = new Uri(imgLink);
                    var imageData = new System.Net.WebClient().DownloadData(imgUrl);

                    var bitmapImage = new BitmapImage { CacheOption = BitmapCacheOption.OnLoad };
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = new System.IO.MemoryStream(imageData);
                    bitmapImage.EndInit();

                    price = pasalinimasTarpu(price);
                    var priceAtsarg = price;
                    priceAtsarg = pasalinimasEuroSimbol(priceAtsarg);

                    double pricea = Double.Parse(priceAtsarg);
                    var Itemas = new Item { nuotrauka = bitmapImage, Seller = "Elektromarkt", Name = name, Pricea = pricea, Price = price, Link = link };
                    prices.Add(Itemas);

                }
            }
            else
            {
                var Itemas = new Item { Seller = "Elektromarkt", Name = "tokios prekės " + MainWindow.zodis + " nėra šioje parduotuvėje" };
                prices.Add(Itemas);
            }
        }
        private static string pasalinimasEuroSimbol(string priceAtsarg)
        {
            var charsToRemove = new string[] { "€" };
            foreach (var c in charsToRemove)
            {
                priceAtsarg = priceAtsarg.Replace(c, string.Empty);
            }

            return priceAtsarg;
        }

        private static string pasalinimasTrikdanciuSimboliu(string price)
        {
            int index = price.IndexOf("\n");
            if (index > 0)
            {
                price = price.Substring(0, index);
            }

            var charsToChange = new string[] { "." };
            foreach (var c in charsToChange)
            {
                price = price.Replace(c, ",");
            }
            return price;
        }

        private static string pasalinimasTarpu(string price)
        {
            var charsToRemove = new string[] { " " };
            foreach (var c in charsToRemove)
            {
                price = price.Replace(c, string.Empty);
            }
            return price;
        }

        private static void surikiavimasIrSurasymas(List<Item> prices, DataGrid dataGridas)
        {
            List<Item> SortedPricesList = prices.OrderBy(o => o.Pricea).ToList();
            foreach (Item item in SortedPricesList)
            {
                dataGridas.Items.Add(item);
            }
            prices.Clear();
        }


        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RectangleIstempimasAukstis(rectangle1);
            RectangleIstempimasPlotis(rectangle2);
        }

        private void RectangleIstempimasAukstis(Rectangle plotelis)
        {
            plotelis.Height = this.ActualHeight;
        }

        private void RectangleIstempimasPlotis(Rectangle plotelis)
        {
            plotelis.Width = this.ActualWidth;
        }
        private void DataGridTest_Initialized(object sender, EventArgs e)
        {
            getHtmlAssync(DataGridas);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VertinimoLangas vertinimoLangoAtidarymas = new VertinimoLangas();
            vertinimoLangoAtidarymas.Show();
        }

        private void linkButton_Click(object sender, RoutedEventArgs e)
        {
            string link = (((Button)sender).DataContext as Item).Link;
            if (link != null)
            {
                System.Diagnostics.Process.Start(link);
            }
        }
    }
}
