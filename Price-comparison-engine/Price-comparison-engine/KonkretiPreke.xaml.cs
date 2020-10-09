using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HtmlAgilityPack;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace Price_comparison_engine
{
    /// <summary>
    /// Interaction logic for KonkretiPreke.xaml
    /// </summary>
    partial class Item
    {

        public string Nuotraukaa { get; set; }
        public string Sellerr { get; set; }
        public double Priceaa { get; set; }

        public string Pricee { get; set; }

        public string Namee { get; set; }

        public string Linkk { get; set; }

    }

    public partial class KonkretiPreke
    {
        public static CartesianChart CartesianChart;
        public static string Pav;
        public static string[] Isskaidyta;
        static readonly string[] PrekesPraleidimui = { "Šaldytuvas", "Išmanusis", "telefonas", "Kompiuteris","mobilusis","apsauginis","stiklas" };
        public KonkretiPreke(string pavadinimas)
        {
            Pav = pavadinimas;
            Isskaidyta = Pav.Split();
            InitializeComponent();
            CartesianChart = cartesianChart1;
        }

        private static async void GetHtmlAssync(DataGrid dataGridas2)
        {
            var prices = new List<Item>();
            var httpClient = new HttpClient();
            var regEx = new Regex(" ");
            var urlgalas = regEx.Replace(MainWindow.zodis, "+");
            var urlRde = "https://www.rde.lt/search_result/lt/word/" + urlgalas + "/page/1";
            var urlPigu = "https://pigu.lt/lt/search?q=" + urlgalas;
            var urlBigBox = "https://bigbox.lt/paieska?controller=search&orderby=position&orderway=desc&ssa_submit=&search_query=" + urlgalas;
            var urlAvitela = "https://avitela.lt/paieska/" + MainWindow.zodis;
            var urlElektromarkt = "https://www.elektromarkt.lt/lt/catalogsearch/result/?order=price&dir=desc&q=" + urlgalas;

            var rdeItems = RdeSearch(await Html(httpClient, urlRde));
            WriteDataFromRde(rdeItems, prices);
            var piguItems = PiguSearch(await Html(httpClient, urlPigu));
            WriteDataFromPigu(piguItems, prices);
            var bigBoxItem = BigBoxSearch(await Html(httpClient, urlBigBox));
            WriteDataFromBigBox(bigBoxItem, prices);
            var avitelaItems = AvitelaSearch(await Html(httpClient, urlAvitela));
            WriteDataFromAvitela(avitelaItems, prices);
            var elektromarktItems = ElektromarktSearch(await Html(httpClient, urlElektromarkt));
            WriteDataFromElektromarkt(elektromarktItems, prices);

            SurikiavimasIrSurasymas(prices, dataGridas2);
        }

        private static async Task<HtmlDocument> Html(HttpClient httpClient, string urlget)
        {
            try
            {
                var url = urlget;
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

        private static List<HtmlNode> RdeSearch(HtmlDocument htmlDocument)
        {
            if (htmlDocument != null)
            {
                var productsHtml = htmlDocument.DocumentNode.Descendants("div")
                    .Where(node => node.GetAttributeValue("id", "")
                        .Equals("body_div")).ToList();

                var productListItems = productsHtml[0].Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                        .Contains("product_box_div")).ToList();
                return productListItems;
            }
            else
                return null;
        }

        private static List<HtmlNode> BigBoxSearch(HtmlDocument htmlDocument)
        {
            try
            {
                var productsHtml = htmlDocument.DocumentNode.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                        .Equals("col-lg-9 col-md-8")).ToList();

                var productListItems = productsHtml[0].Descendants("li")
                    .Where(node => node.GetAttributeValue("class", "")
                        .StartsWith("category-item ajax_block_product col-xs-12 col-sm-6 col-md-4 col-lg-3")).ToList();
                return productListItems;

            }
            catch
            {
                return null;
            }
        }
        private static List<HtmlNode> AvitelaSearch(HtmlDocument htmlDocument)
        {
            try
            {
                var productsHtml = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("product-grid active")).ToList();

                var productListItems = productsHtml[0].Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Contains("col-6 col-md-4 col-lg-4")).ToList();
                return productListItems;

            }
            catch
            {
                return null;
            }
        }

        private static List<HtmlNode> ElektromarktSearch(HtmlDocument htmlDocument2)
        {

            try
            {
                var productsHtml2 = htmlDocument2.DocumentNode.Descendants("div")
               .Where(node => node.GetAttributeValue("class", "")
               .Equals("manafilters-category-products category-products")).ToList();

                var productListItems2 = productsHtml2[0].Descendants("li")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Contains("item js-ua-item")).ToList();

                return productListItems2;
            }
            catch
            {
                return null;
            }
        }

        private static List<HtmlNode> PiguSearch(HtmlDocument htmlDocument2)
        {


            if (htmlDocument2 != null)
            {
                var productsHtml2 = htmlDocument2.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("main-block fr")).ToList();

                var productListItems2 = productsHtml2[0].Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Contains("product-list-item")).ToList();

                return productListItems2;
            }
            else
                return null;

        }

        private static void WriteDataFromRde(List<HtmlNode> productListItems, List<Item> prices)
        {
            if (productListItems != null)
            {
                foreach (var productListItem in productListItems)
                {

                    var price = productListItem
                        .Descendants("div").FirstOrDefault(node => node.GetAttributeValue("class", "")
                            .Equals("product_price_wo_discount_listing"))
                        ?.InnerText.Trim();

                    var name = productListItem
                        .Descendants("div").FirstOrDefault(node => node.GetAttributeValue("class", "")
                            .Equals("product_name"))
                        ?.InnerText.Trim();

                    var link = productListItem.Descendants("a").FirstOrDefault()?.GetAttributeValue("href", "");

                    var productListItems2 = productListItem.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Contains("photo_box")).ToList();

                    foreach (var productListItem2 in productListItems2)
                    {
                        var imgLink = productListItem2.Descendants("img").FirstOrDefault()?.GetAttributeValue("src", "");

                        if (price != "")
                        {
                            price = PasalinimasTrikdanciuSimboliu2(price);
                            var priceAtsarg = price;
                            priceAtsarg = PasalinimasTrikdanciuSimboliu2(priceAtsarg);
                            priceAtsarg = PasalinimasEuroSimbol(priceAtsarg);
                            double pricea = Convert.ToDouble(priceAtsarg);
                            if (name != null)
                            {
                                var pavArray = name.Split();
                                int kiekSutiko=algoritmasKiekZodziuSutinka(pavArray);
                                if (kiekSutiko >= Isskaidyta.Length / 2)
                                {
                                    var itemas = new Item
                                    {
                                        Nuotraukaa = "https://www.rde.lt/" + imgLink, Sellerr = "Rde", Namee = name,
                                        Priceaa = pricea, Pricee = price, Linkk = "https://www.rde.lt/" + link
                                    };
                                    prices.Add(itemas);
                                }
                            }
                        
                        }
                    }
                }
            }
            else
            {
                var itemas = new Item { Seller = "Rde", Name = "tokios prekės " + MainWindow.zodis + " nėra šioje parduotuvėje" };
                prices.Add(itemas);
            }
        }

        private static void WriteDataFromBigBox(List<HtmlNode> productListItems, List<Item> prices)
        {
            if (productListItems != null)
            {
                foreach (var productListItem in productListItems)
                {

                    var price = productListItem
                        .Descendants("span").FirstOrDefault(node => node.GetAttributeValue("class", "")
                            .Equals("price product-price"))
                        ?.InnerText.Trim();

                    var name = productListItem
                        .Descendants("a").FirstOrDefault(node => node.GetAttributeValue("class", "")
                            .Equals("product-name"))
                        ?.InnerText.Trim();

                    var link = productListItem.Descendants("a").FirstOrDefault()?.GetAttributeValue("href", "");

                    string imgLink = productListItem
                        .Descendants("img").FirstOrDefault(node => node.GetAttributeValue("class", "")
                            .Contains("replace-2x img-responsive")).GetAttributeValue("src", "");

                    if (price != "")
                    {
                        price = PasalinimasTarpuPigu(price);
                        var priceAtsarg = price;
                        price = PasalinimasEuroSimbol(price);
                        price = price + "€";
                        priceAtsarg = PasalinimasEuroSimbol(priceAtsarg);
                        var pricea = Convert.ToDouble(priceAtsarg);
                        var pavArray = name.Split();
                        int kiekSutiko = algoritmasKiekZodziuSutinka(pavArray);
                        if (kiekSutiko >= Isskaidyta.Length/2)
                        {
                            var itemas = new Item
                            {
                                Nuotraukaa = imgLink, Sellerr = "BigBox", Namee = name, Priceaa = pricea,
                                Pricee = price, Linkk = link
                            };
                            prices.Add(itemas);
                        }
                    }
                }
            }
            else
            {
                var itemas = new Item { Seller = "Barbora", Name = "tokios prekės " + MainWindow.zodis + " nėra šioje parduotuvėje" };
                prices.Add(itemas);
            }
        }
        private static void WriteDataFromAvitela(List<HtmlNode> productListItems, List<Item> prices)
        {
            if (productListItems != null)
            {
                foreach (var productListItem in productListItems)
                {

                    var price = productListItem
                        .Descendants("div").FirstOrDefault(node => node.GetAttributeValue("class", "")
                            .Equals("price"))
                        ?.InnerText.Trim();

                    var name = productListItem
                        .Descendants("div").FirstOrDefault(node => node.GetAttributeValue("class", "")
                            .Equals("name"))
                        ?.InnerText.Trim();

                    var link = productListItem.Descendants("a").FirstOrDefault()?.GetAttributeValue("href", "");
                    if (price != "")
                    {
                        price = PasalinimasTrikdanciuSimboliu(price);
                        var priceAtsarg = price;
                        priceAtsarg = PasalinimasEuroSimbol(priceAtsarg);
                        var pricea = Convert.ToDouble(priceAtsarg);
                        if (name != null)
                        {
                            var pavArray = name.Split();
                            int kiekSutiko = algoritmasKiekZodziuSutinka(pavArray);
                            if (kiekSutiko >= Isskaidyta.Length/2)
                            {
                                var itemas = new Item
                                    {Sellerr = "Avitela", Namee = name, Priceaa = pricea, Pricee = price, Linkk = link};
                                prices.Add(itemas);
                            }
                        }
                    }
                }
            }
            else
            {
                var itemas = new Item { Nuotraukaa = "https://avitela.lt/image/no_image.jpg",Seller = "Avitela", Name = "tokios prekės " + MainWindow.zodis + " nėra šioje parduotuvėje" };
                prices.Add(itemas);
            }
        }
        private static void WriteDataFromPigu(List<HtmlNode> productListItems, List<Item> prices)
        {
            if (productListItems != null)
            {
                foreach (var productListItem in productListItems)
                {
                    var price = productListItem
                        .Descendants("span").FirstOrDefault(node => node.GetAttributeValue("class", "")
                            .Equals("price notranslate"))
                        ?.InnerText.Trim();

                    var name = productListItem
                        .Descendants("p").FirstOrDefault(node => node.GetAttributeValue("class", "")
                            .Equals("product-name"))
                        ?.InnerText.Trim();

                    var link = "https://pigu.lt/" + productListItem.Descendants("a").FirstOrDefault()?.GetAttributeValue("href", "");

                    string imgLink = productListItem
                        .Descendants("img").FirstOrDefault(node => node.GetAttributeValue("src", "")
                            .Contains("jpg"))
                        ?.GetAttributeValue("src", "");

                    price = PasalinimasTarpuPigu(price);
                    var priceAtsarg = price;
                    price = PasalinimasEuroSimbol(price);
                    price = price + "€";
                    priceAtsarg = PasalinimasEuroSimbol(priceAtsarg);

                    var pricea = Convert.ToDouble(priceAtsarg);
                    if (name != null)
                    {
                        var pavArray = name.Split();
                        int kiekSutiko = algoritmasKiekZodziuSutinka(pavArray);
                        if (kiekSutiko >= Isskaidyta.Length/2)
                        {
                            var itemas = new Item
                            {
                                Nuotraukaa = imgLink, Sellerr = "Pigu", Namee = name, Priceaa = pricea, Pricee = price,
                                Linkk = link
                            };
                            prices.Add(itemas);
                        }
                    }
                }
            }
            else
            {
                var itemas = new Item { Seller = "Pigu", Name = "tokios prekės " + MainWindow.zodis + " nėra šioje parduotuvėje" };
                prices.Add(itemas);
            }
        }
        private static void WriteDataFromElektromarkt(List<HtmlNode> productListItems2, List<Item> prices)
        {
            if (productListItems2 != null)
            {
                foreach (var productListItem in productListItems2)
                {

                    var name = productListItem
                        .Descendants("h2").FirstOrDefault(node => node.GetAttributeValue("class", "")
                            .Equals("product-name"))
                        ?.InnerText.Trim();

                    var price = productListItem
                        .Descendants("span").FirstOrDefault(node => node.GetAttributeValue("class", "")
                            .Equals("price"))
                        ?.InnerText.Trim();

                    var link = productListItem.Descendants("a").FirstOrDefault()?.GetAttributeValue("href", "");

                    var imgLink = productListItem.Descendants("img").FirstOrDefault().GetAttributeValue("src", "");

                    price = PasalinimasTarpu(price);
                    price = PasalinimasTarpuElektromarkt(price);
                    var priceAtsarg = price;
                    priceAtsarg = PasalinimasEuroSimbol(priceAtsarg);

                    var pricea = Double.Parse(priceAtsarg);
                    var pavArray = name.Split();
                    int kiekSutiko = algoritmasKiekZodziuSutinka(pavArray);
                    if (kiekSutiko >= Isskaidyta.Length/2)
                    {
                        var itemas = new Item
                        {
                            Nuotraukaa = imgLink, Sellerr = "Elektromarkt", Namee = name, Priceaa = pricea,
                            Pricee = price, Linkk = link
                        };
                        prices.Add(itemas);
                    }

                }
            }
            else
            {
                var itemas = new Item { Seller = "Elektromarkt", Name = "tokios prekės " + MainWindow.zodis + " nėra šioje parduotuvėje" };
                prices.Add(itemas);
            }
        }

        private static int algoritmasKiekZodziuSutinka(string[] pavArray)
        {
            int kiekSutiko = 0;
            foreach (var t in pavArray)
            {
                foreach (var t1 in Isskaidyta)
                {
                    if (t.Equals(t1, StringComparison.CurrentCultureIgnoreCase))
                    {
                        int arIrasyti = 1;
                        foreach (var t2 in PrekesPraleidimui)
                        {
                            if (t.Equals(t2, StringComparison.CurrentCultureIgnoreCase))
                            {
                                arIrasyti = 0;
                            }
                        }
                        if (arIrasyti == 1)
                        {
                            kiekSutiko++;
                        }
                    }
                }
            }

            return kiekSutiko;
        }

        private static string PasalinimasEuroSimbol(string priceAtsarg)
        {
            var charsToRemove = new[] { "€" };
            foreach (var c in charsToRemove)
            {
                priceAtsarg = priceAtsarg.Replace(c, string.Empty);
            }

            return priceAtsarg;
        }

        private static string PasalinimasTrikdanciuSimboliu(string price)
        {
            var index = price.IndexOf("\n");
            if (index > 0)
            {
                price = price.Substring(0, index);
            }

            var charsToChange = new[] { "." };
            foreach (var c in charsToChange)
            {
                price = price.Replace(c, ",");
            }
            return price;
        }

        private static string PasalinimasTrikdanciuSimboliu2(string price)
        {
            var index = price.IndexOf("\n");
            if (index > 0)
            {
                price = price.Substring(0, index);
            }

            var charsToChange = new[] { "." };
            foreach (var c in charsToChange)
            {
                price = price.Replace(c, ",");
            }
            var charsToChange2 = new[] { "&nbsp;" };
            foreach (var c in charsToChange2)
            {
                price = price.Replace(c, "");
            }
            var charsToChange3 = new[] { "Kaina: " };
            foreach (var c in charsToChange3)
            {
                price = price.Replace(c, "");
            }

            return price;
        }

        private static string PasalinimasTarpu(string price)
        {
            var charsToRemove = new[] { " " };
            foreach (var c in charsToRemove)
            {
                price = price.Replace(c, string.Empty);
            }
            return price;
        }

        private static string PasalinimasTarpuElektromarkt(string price)
        {
            var charsToRemove = new[] {" "};
            foreach (var c in charsToRemove)
            {
                price = price.Replace(c, string.Empty);
            }
            return price;
        }

        private static string PasalinimasTarpuPigu(string price)
        {
            var charsToRemove = new[] { " " };
            foreach (var c in charsToRemove)
            {
                price = price.Replace(c, string.Empty);
            }
            return price;
        }

        private static void SurikiavimasIrSurasymas(List<Item> prices, DataGrid dataGridas2)
        {
            
            var sortedPricesList = prices.OrderBy(o => o.Priceaa).ToList();
            var a = 0;
            var list1Points = new ChartValues<ObservablePoint>();

            foreach (var item in sortedPricesList)
            {
                
                list1Points.Add(new ObservablePoint
                {
                    X = a,
                    Y = item.Priceaa
                });
                a += 5;
            }
            CartesianChart.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Values = list1Points
                }
            };

            foreach (Item item in sortedPricesList)
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
            var link = (((Image)sender).DataContext as Item)?.Linkk;
            if (link != null)
            {
                System.Diagnostics.Process.Start(link);
            }
        }

        private void LinkButton_Click(object sender, RoutedEventArgs e)
        {
            var link = (((Button)sender).DataContext as Item)?.Linkk;
            if (link != null)
            {
                System.Diagnostics.Process.Start(link);
            }
        }
    }
}
