using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using HtmlAgilityPack;

namespace Price_comparison_engine
{
    /// <summary>
    /// Interaction logic for KonkretiPreke.xaml
    /// </summary>
    partial class Item
    {

        public String nuotraukaa { get; set; }
        public string Sellerr { get; set; }
        public double Priceaa { get; set; }

        public string Pricee { get; set; }

        public string Namee { get; set; }

        public string Linkk { get; set; }

    }
    public partial class KonkretiPreke : Window
    {
        public static string pav;
        public KonkretiPreke(string pavadinimas)
        {
            pav = pavadinimas;
            Console.WriteLine(pav);
            InitializeComponent();
        }

        private static async void GetHtmlAssync(DataGrid dataGridas2)
        {
            var prices = new List<Item>();
            var piguDaiktai = PiguPaieska(await PiguHtmlPaemimas());
            var avitelosDaiktai = AvitelosPaieska(await AvitelosHtmlPaemimas());
            var elektromarktDaiktai = ElektromarktPaieska(await ElektromarktHtmlPaemimas());
            SurasymasIsAvitelos(avitelosDaiktai, prices);
            SurasymasIsElektromarkt(elektromarktDaiktai, prices);
            SurasymasIsPigu(piguDaiktai, prices);
            SurikiavimasIrSurasymas(prices, dataGridas2);
        }

        private static async Task<HtmlDocument> AvitelosHtmlPaemimas()
        {
            var url = "https://avitela.lt/paieska/" + pav;
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            return htmlDocument;
        }

        private static async Task<HtmlDocument> ElektromarktHtmlPaemimas()
        {
            Regex regEx = new Regex(" ");
            var urlgalas = regEx.Replace(pav, "+");
            var url2 = "https://www.elektromarkt.lt/lt/catalogsearch/result/?order=price&dir=desc&q=" + urlgalas;
            var httpClient2 = new HttpClient();
            var html2 = await httpClient2.GetStringAsync(url2);
            var htmlDocument2 = new HtmlDocument();
            htmlDocument2.LoadHtml(html2);
            return htmlDocument2;
        }

        private static async Task<HtmlDocument> PiguHtmlPaemimas()
        {
            try
            {
                Regex regEx = new Regex(" ");
                var urlgalas = regEx.Replace(pav, "+");
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

        private static List<HtmlNode> AvitelosPaieska(HtmlDocument htmlDocument)
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

        private static List<HtmlNode> ElektromarktPaieska(HtmlDocument htmlDocument2)
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

        private static List<HtmlNode> PiguPaieska(HtmlDocument htmlDocument2)
        {


            if (htmlDocument2 != null)
            {
                var ProductsHtml2 = htmlDocument2.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("main-block fr")).ToList();

                var ProductListItems2 = ProductsHtml2[0].Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Contains("product-list-item")).ToList();

                return ProductListItems2;
            }
            else
                return null;

        }
        private static void SurasymasIsAvitelos(List<HtmlNode> ProductListItems, List<Item> prices)
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
                    if (price != "")
                    {
                        price = PasalinimasTrikdanciuSimboliu(price);
                        var priceAtsarg = price;
                        priceAtsarg = PasalinimasEuroSimbol(priceAtsarg);
                        double pricea = Convert.ToDouble(priceAtsarg);
                        var Itemas = new Item { Sellerr = "Avitela", Namee = name, Priceaa = pricea, Pricee = price, Linkk = link };
                        prices.Add(Itemas);
                    }
                }
            }
            else
            {
                var Itemas = new Item { Seller = "Avitela", Name = "tokios prekės " + MainWindow.zodis + " nėra šioje parduotuvėje" };
                prices.Add(Itemas);
            }
        }
        private static void SurasymasIsPigu(List<HtmlNode> ProductListItems, List<Item> prices)
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

                    var link = "https://pigu.lt/" + ProductListItem.Descendants("a").FirstOrDefault().GetAttributeValue("href", "");

                    string imgLink = ProductListItem.Descendants("img")
                       .Where(node => node.GetAttributeValue("src", "")
                             .Contains("jpg")).FirstOrDefault().GetAttributeValue("src", "");

                    Console.WriteLine(imgLink);
                    price = PasalinimasTarpuPigu(price);
                    var priceAtsarg = price;
                    price = PasalinimasEuroSimbol(price);
                    price = price + "€";
                    priceAtsarg = PasalinimasEuroSimbol(priceAtsarg);

                    double pricea = Convert.ToDouble(priceAtsarg);
                    var Itemas = new Item { nuotraukaa = imgLink, Sellerr = "Pigu", Namee = name, Priceaa = pricea, Pricee = price, Linkk = link };
                    prices.Add(Itemas);
                }
            }
            else
            {
                var Itemas = new Item { Seller = "Pigu", Name = "tokios prekės " + MainWindow.zodis + " nėra šioje parduotuvėje" };
                prices.Add(Itemas);
            }
        }
        private static void SurasymasIsElektromarkt(List<HtmlNode> ProductListItems2, List<Item> prices)
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

                    price = PasalinimasTarpu(price);
                    var priceAtsarg = price;
                    priceAtsarg = PasalinimasEuroSimbol(priceAtsarg);

                    double pricea = Double.Parse(priceAtsarg);
                    var Itemas = new Item { nuotraukaa = imgLink, Sellerr = "Elektromarkt", Namee = name, Priceaa = pricea, Pricee = price, Linkk = link };
                    prices.Add(Itemas);

                }
            }
            else
            {
                var Itemas = new Item { Seller = "Elektromarkt", Name = "tokios prekės " + MainWindow.zodis + " nėra šioje parduotuvėje" };
                prices.Add(Itemas);
            }
        }
        private static string PasalinimasEuroSimbol(string priceAtsarg)
        {
            var charsToRemove = new string[] { "€" };
            foreach (var c in charsToRemove)
            {
                priceAtsarg = priceAtsarg.Replace(c, string.Empty);
            }

            return priceAtsarg;
        }

        private static string PasalinimasTrikdanciuSimboliu(string price)
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

        private static string PasalinimasTarpu(string price)
        {
            var charsToRemove = new string[] { " " };
            foreach (var c in charsToRemove)
            {
                price = price.Replace(c, string.Empty);
            }
            return price;
        }

        private static string PasalinimasTarpuPigu(string price)
        {
            var charsToRemove = new string[] { " " };
            foreach (var c in charsToRemove)
            {
                price = price.Replace(c, string.Empty);
            }
            return price;
        }

        private static void SurikiavimasIrSurasymas(List<Item> prices, DataGrid dataGridas2)
        {
            List<Item> SortedPricesList = prices.OrderBy(o => o.Priceaa).ToList();
            foreach (Item item in SortedPricesList)
            {
                dataGridas2.Items.Add(item);
            }
            prices.Clear();
        }

        private void DataGridTest_Initialized(object sender, EventArgs e)
        {
            GetHtmlAssync(dataGridas2);
        }

        private void ImageClick_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string link = (((Image)sender).DataContext as Item).Linkk;
            if (link != null)
            {
                System.Diagnostics.Process.Start(link);
            }
        }

        private void LinkButton_Click(object sender, RoutedEventArgs e)
        {
            string link = (((Button)sender).DataContext as Item).Linkk;
            if (link != null)
            {
                System.Diagnostics.Process.Start(link);
            }
        }

    }
}
