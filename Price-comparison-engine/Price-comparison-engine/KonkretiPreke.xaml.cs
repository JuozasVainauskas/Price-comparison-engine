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
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;

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
        public static CartesianChart cartesianChart;
        public static string pav;
        public KonkretiPreke(string pavadinimas)
        {
            pav = pavadinimas;
            var pavArray=pav.Split();
            string Items = pavArray[0] + ' ' + pavArray[1]; ;
            pav = Items;
            InitializeComponent();
            cartesianChart = cartesianChart1;
        }

        private static async void GetHtmlAssync(DataGrid dataGridas2)
        {
            var prices = new List<Item>();
            var bigBoxDaiktai = BigBoxSearch(await BigBoxHtmlPaemimas());
            var piguDaiktai = PiguPaieska(await PiguHtmlPaemimas());
            var avitelosDaiktai = AvitelosPaieska(await AvitelosHtmlPaemimas());
            var elektromarktDaiktai = ElektromarktPaieska(await ElektromarktHtmlPaemimas());
            var RdeItems = RdeSearch(await RdeHtml());
            SurasymasIsRde(RdeItems, prices);
            SurasymasIsAvitelos(avitelosDaiktai, prices);
            SurasymasIsElektromarkt(elektromarktDaiktai, prices);
            SurasymasIsPigu(piguDaiktai, prices);
            SurasymasIsBigBox(bigBoxDaiktai, prices);
            SurikiavimasIrSurasymas(prices, dataGridas2);
        }

        private static async Task<HtmlDocument> RdeHtml()
        {
            try
            {
                Regex regEx = new Regex(" ");
                var urlgalas = regEx.Replace(MainWindow.zodis, "+");
                var url = "https://www.rde.lt/search_result/lt/word/" + urlgalas + "/page/1";
                var httpClient = new HttpClient();
                var html = await httpClient.GetStringAsync(url);
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);
                return htmlDocument;
            }
            catch
            {
                return null;
            }
        }

        private static async Task<HtmlDocument> BigBoxHtmlPaemimas()
        {
            Regex regEx = new Regex(" ");
            var urlgalas = regEx.Replace(MainWindow.zodis, "+");
            var url = "https://bigbox.lt/paieska?controller=search&orderby=position&orderway=desc&ssa_submit=&search_query=" + urlgalas;
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            return htmlDocument;
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

        private static List<HtmlNode> RdeSearch(HtmlDocument htmlDocument)
        {
            if (htmlDocument != null)
            {
                var ProductsHtml = htmlDocument.DocumentNode.Descendants("div")
                    .Where(node => node.GetAttributeValue("id", "")
                        .Equals("body_div")).ToList();

                var ProductListItems = ProductsHtml[0].Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                        .Contains("product_box_div")).ToList();
                return ProductListItems;
            }
            else
                return null;
        }

        private static List<HtmlNode> BigBoxSearch(HtmlDocument htmlDocument)
        {
            try
            {
                var ProductsHtml = htmlDocument.DocumentNode.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                        .Equals("col-lg-9 col-md-8")).ToList();

                var ProductListItems = ProductsHtml[0].Descendants("li")
                    .Where(node => node.GetAttributeValue("class", "")
                        .StartsWith("category-item ajax_block_product col-xs-12 col-sm-6 col-md-4 col-lg-3")).ToList();
                return ProductListItems;

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

        private static void SurasymasIsRde(List<HtmlNode> ProductListItems, List<Item> prices)
        {
            if (ProductListItems != null)
            {
                foreach (var ProductListItem in ProductListItems)
                {

                    var price = ProductListItem.Descendants("div")
                       .Where(node => node.GetAttributeValue("class", "")
                            .Equals("product_price_wo_discount_listing")).FirstOrDefault().InnerText.Trim();

                    var name = ProductListItem.Descendants("div")
                       .Where(node => node.GetAttributeValue("class", "")
                             .Equals("product_name")).FirstOrDefault().InnerText.Trim();

                    var link = ProductListItem.Descendants("a").FirstOrDefault().GetAttributeValue("href", "");

                    var ProductListItems2 = ProductListItem.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Contains("photo_box")).ToList();
                    foreach (var ProductListItem2 in ProductListItems2)
                    {
                        var imgLink = ProductListItem2.Descendants("img").FirstOrDefault().GetAttributeValue("src", "");

                        if (price != "")
                        {
                            price = PasalinimasTrikdanciuSimboliu2(price);
                            var priceAtsarg = price;
                            priceAtsarg = PasalinimasTrikdanciuSimboliu2(priceAtsarg);
                            priceAtsarg = PasalinimasEuroSimbol(priceAtsarg);
                            double pricea = Convert.ToDouble(priceAtsarg);
                            var pavArray = name.Split();
                            string a = pavArray[0] + ' ' + pavArray[1]; ;
                            if (a == pav)
                            {
                                var Itemas = new Item
                                {
                                    nuotraukaa = "https://www.rde.lt/" + imgLink, Sellerr = "Rde", Namee = name,
                                    Priceaa = pricea, Pricee = price, Linkk = "https://www.rde.lt/" + link
                                };
                                prices.Add(Itemas);
                            }

                        }
                    }
                }
            }
            else
            {
                var Itemas = new Item { Seller = "Rde", Name = "tokios prekės " + MainWindow.zodis + " nėra šioje parduotuvėje" };
                prices.Add(Itemas);
            }
        }

        private static void SurasymasIsBigBox(List<HtmlNode> ProductListItems, List<Item> prices)
        {
            if (ProductListItems != null)
            {
                foreach (var ProductListItem in ProductListItems)
                {

                    var price = ProductListItem.Descendants("span")
                       .Where(node => node.GetAttributeValue("class", "")
                            .Equals("price product-price")).FirstOrDefault().InnerText.Trim();

                    var name = ProductListItem.Descendants("a")
                       .Where(node => node.GetAttributeValue("class", "")
                             .Equals("product-name")).FirstOrDefault().InnerText.Trim();

                    var link = ProductListItem.Descendants("a").FirstOrDefault().GetAttributeValue("href", "");

                    string imgLink = ProductListItem.Descendants("img")
                      .Where(node => node.GetAttributeValue("class", "")
                            .Contains("replace-2x img-responsive")).FirstOrDefault().GetAttributeValue("src", "");

                    if (price != "")
                    {
                        price = PasalinimasTarpuPigu(price);
                        var priceAtsarg = price;
                        price = PasalinimasEuroSimbol(price);
                        price = price + "€";
                        priceAtsarg = PasalinimasEuroSimbol(priceAtsarg);
                        double pricea = Convert.ToDouble(priceAtsarg);
                        var pavArray = name.Split();
                        string a = pavArray[0] + ' ' + pavArray[1]; ;
                        if (a == pav)
                        {
                            var Itemas = new Item
                            {
                                nuotraukaa = imgLink, Sellerr = "BigBox", Namee = name, Priceaa = pricea,
                                Pricee = price, Linkk = link
                            };
                            prices.Add(Itemas);
                        }
                    }
                }
            }
            else
            {
                var Itemas = new Item { Seller = "Barbora", Name = "tokios prekės " + MainWindow.zodis + " nėra šioje parduotuvėje" };
                prices.Add(Itemas);
            }
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
                        var pavArray = name.Split();
                        string a = pavArray[0] + ' ' + pavArray[1]; ;
                        if (a == pav)
                        {
                            var Itemas = new Item
                                {Sellerr = "Avitela", Namee = name, Priceaa = pricea, Pricee = price, Linkk = link};
                            prices.Add(Itemas);
                        }
                    }
                }
            }
            else
            {
                var Itemas = new Item { nuotraukaa = "https://avitela.lt/image/no_image.jpg",Seller = "Avitela", Name = "tokios prekės " + MainWindow.zodis + " nėra šioje parduotuvėje" };
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

                    price = PasalinimasTarpuPigu(price);
                    var priceAtsarg = price;
                    price = PasalinimasEuroSimbol(price);
                    price = price + "€";
                    priceAtsarg = PasalinimasEuroSimbol(priceAtsarg);

                    double pricea = Convert.ToDouble(priceAtsarg);
                    var pavArray = name.Split();
                    string a = pavArray[0] + ' ' + pavArray[1]; ;
                    if (a == pav)
                    {
                        var Itemas = new Item
                        {
                            nuotraukaa = imgLink, Sellerr = "Pigu", Namee = name, Priceaa = pricea, Pricee = price,
                            Linkk = link
                        };
                        prices.Add(Itemas);
                    }
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
                    price = PasalinimasTarpuElektromarkt(price);
                    var priceAtsarg = price;
                    priceAtsarg = PasalinimasEuroSimbol(priceAtsarg);

                    double pricea = Double.Parse(priceAtsarg);
                    var pavArray = name.Split();
                    string a = pavArray[0] + ' ' + pavArray[1]; ;
                    if (a == pav)
                    {
                        var Itemas = new Item
                        {
                            nuotraukaa = imgLink, Sellerr = "Elektromarkt", Namee = name, Priceaa = pricea,
                            Pricee = price, Linkk = link
                        };
                        prices.Add(Itemas);
                    }

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

        private static string PasalinimasTrikdanciuSimboliu2(string price)
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
            var charsToChange2 = new string[] { "&nbsp;" };
            foreach (var c in charsToChange2)
            {
                price = price.Replace(c, "");
            }
            var charsToChange3 = new string[] { "Kaina: " };
            foreach (var c in charsToChange3)
            {
                price = price.Replace(c, "");
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

        private static string PasalinimasTarpuElektromarkt(string price)
        {
            var charsToRemove = new string[] {" "};
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
            int a = 0;
            ChartValues<ObservablePoint> List1Points = new ChartValues<ObservablePoint>();

            foreach (Item item in SortedPricesList)
            {
                
                List1Points.Add(new ObservablePoint
                {
                    X = a,
                    Y = item.Priceaa
                });
                a=a+5;
            }
            cartesianChart.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = List1Points
                }
            };

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
