using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Price_comparison_engine.Klases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

    public partial class Item
    {

        public String nuotrauka { get; set; }
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

        private static async void getHtmlAssync(DataGrid dataGrid)
        {
            var prices = new List<Item>();
            /*
            var chromeOptions = new ChromeOptions();
            //var user_agent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.116 Safari/537.36";
            //chromeOptions.AddArgument("f'user-agent="+user_agent);
            //chromeOptions.AddArgument("headless");
            //chromeOptions.AddArgument("window-size=1920,1080");
            //chromeOptions.AddArgument("log-level=3");
            IWebDriver driver = new ChromeDriver(chromeOptions);
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.topocentras.lt/");
            var element = driver.FindElement(By.XPath("/html/body/div[1]/header[1]/div[1]/div[2]/div[1]/div[1]/input"));
            element.SendKeys(MainWindow.zodis);
            driver.FindElement(By.XPath("/html/body/div[1]/header[1]/div[1]/div[2]/div[1]/div[1]/button")).Click();
            var prekes = driver.FindElements(By.ClassName("ProductGrid-productName-1JN"));
            foreach(var preke in prekes)
            {
                var Itemas = new Item { Seller = "Topo  Centras", Name = preke.Text};
                prices.Add(Itemas);
            }
            */
            var BigBoxItem = BigBoxSearch(await BigBoxHtml());
            var BarboraItems = BarboraSearch(await BarboraHtml());
            var PiguItems = PiguSearch(await piguHtml());
            var AvitelaItems = AvitelaSearch(await avitelosHtml());
            var ElektromarktItems = ElektromarktSearch(await elektromarktHtml());
            WriteDataFromBarbora(BarboraItems, prices);
            WriteDataFromBigBox(BigBoxItem, prices);
            WriteDataFromAvitela(AvitelaItems, prices);
            WriteDataFromElektromarkt(ElektromarktItems, prices);
            WriteDataFromPigu(PiguItems, prices);
            SortAndInsert(prices, dataGrid);
           
        }

        private static async Task<HtmlDocument> BarboraHtml()
        {
            var url = "https://pagrindinis.barbora.lt/paieska?q=" + MainWindow.zodis;
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            return htmlDocument;
        }

        private static async Task<HtmlDocument> BigBoxHtml()
        {
            Regex regEx = new Regex(" ");
            var urlgalas = regEx.Replace(MainWindow.zodis, "+");
            var url = "https://bigbox.lt/paieska?controller=search&orderby=position&orderway=desc&ssa_submit=&search_query="+ urlgalas;
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            return htmlDocument;
        }

        private static async Task<HtmlDocument> avitelosHtml()
        {
            var url = "https://avitela.lt/paieska/" + MainWindow.zodis;
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            return htmlDocument;
        }

        private static async Task<HtmlDocument> elektromarktHtml()
        {
            Regex regEx = new Regex(" ");
            var urllast = regEx.Replace(MainWindow.zodis, "+");
            var url2 = "https://www.elektromarkt.lt/lt/catalogsearch/result/?order=price&dir=desc&q=" + urllast;
            var httpClient2 = new HttpClient();
            var html2 = await httpClient2.GetStringAsync(url2);
            var htmlDocument2 = new HtmlDocument();
            htmlDocument2.LoadHtml(html2);
            return htmlDocument2;
        }

        private static async Task<HtmlDocument> piguHtml()
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

        private static List<HtmlNode> AvitelaSearch(HtmlDocument htmlDocument)
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

        private static List<HtmlNode> ElektromarktSearch(HtmlDocument htmlDocument2)
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

        public static int soldOutBarbora = 0;

        private static List<HtmlNode> BarboraSearch(HtmlDocument htmlDocument2)
        {
            try
            {
                var ProductsHtml2 = htmlDocument2.DocumentNode.Descendants("div")
               .Where(node => node.GetAttributeValue("class", "")
               .Equals("b-page-specific-content")).ToList();

                var ProductListItems2 = ProductsHtml2[0].Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Contains("b-product--wrap2 b-product--desktop-grid")).ToList();
                var ProductListItemsSoldOut = ProductsHtml2[0].Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Contains("b-product-out-of-stock-backdrop")).ToList();
                foreach (var ProductListItemsSold in ProductListItemsSoldOut)
                {
                    soldOutBarbora++;
                }
                return ProductListItems2;
            }
            catch
            {
                return null;
            }
        }
        public static int soldOut = 0;
        private static List<HtmlNode> PiguSearch(HtmlDocument htmlDocument2)
        {

            if (htmlDocument2 != null )
            {
                var ProductsHtml2 = htmlDocument2.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("main-block fr")).ToList();
               
                var ProductListItems2 = ProductsHtml2[0].Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Contains("product-list-item")).ToList();
                var ProductListItemsSoldOut = ProductsHtml2[0].Descendants("span")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Contains("label-soldout")).ToList();
                foreach (var ProductListItemsSold in ProductListItemsSoldOut)
                 {
                    soldOut++;
                 }
                return ProductListItems2;
            }
            
            else
                return null;

        }
        private static void WriteDataFromAvitela(List<HtmlNode> ProductListItems, List<Item> prices)
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

                    var imgLink = ProductListItem.Descendants("img").FirstOrDefault().GetAttributeValue("data-echo", "");
                   
                    if (price != "")
                    {
                        price = pasalinimasTrikdanciuSimboliu(price);
                        var priceAtsarg = price;
                        priceAtsarg = pasalinimasEuroSimbol(priceAtsarg);
                        double pricea = Convert.ToDouble(priceAtsarg);
                        if (imgLink!=(""))
                            {
                            var Itemas = new Item { nuotrauka = imgLink, Seller = "Avitela", Name = name, Pricea = pricea, Price = price, Link = link };
                            prices.Add(Itemas);
                            }
                        else
                            {
                            var Itemas = new Item { nuotrauka = "https://avitela.lt/image/no_image.jpg", Seller = "Avitela", Name = name, Pricea = pricea, Price = price, Link = link };
                            prices.Add(Itemas);
                        }
                        
                    }
                }
            }
            else
            {
                var Itemas = new Item { Seller = "Avitela", Name = "tokios prekės " + MainWindow.zodis + " nėra šioje parduotuvėje" };
                prices.Add(Itemas);
            }
        }

        private static void WriteDataFromBigBox(List<HtmlNode> ProductListItems, List<Item> prices)
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
                        price = pasalinimasTarpuPigu(price);
                        var priceAtsarg = price;
                        price = pasalinimasEuroSimbol(price);
                        price = price + "€";
                        priceAtsarg = pasalinimasEuroSimbol(priceAtsarg);
                        double pricea = Convert.ToDouble(priceAtsarg);
                         var Itemas = new Item { nuotrauka = imgLink, Seller = "BigBox", Name = name, Pricea = pricea, Price = price, Link =link };

                        prices.Add(Itemas);
                    }
                }
            }
             else
            {
                var Itemas = new Item { Seller = "BigBox", Name = "tokios prekės " + MainWindow.zodis + " nėra šioje parduotuvėje" };
                prices.Add(Itemas);
            }
        }

        private static void WriteDataFromBarbora(List<HtmlNode> ProductListItems, List<Item> prices)
        {
            if (ProductListItems != null)
            {
                int countItems = ProductListItems.Count - soldOut;

                foreach (var ProductListItem in ProductListItems)
                {
                    if (countItems != 0)
                    {
                        var price = ProductListItem.Descendants("span")
                           .Where(node => node.GetAttributeValue("class", "")
                                .Equals("b-product-price-current-number")).FirstOrDefault().InnerText.Trim();

                        var name = ProductListItem.Descendants("span")
                           .Where(node => node.GetAttributeValue("itemprop", "")
                                 .Equals("name")).FirstOrDefault().InnerText.Trim();

                        var link = ProductListItem.Descendants("a").FirstOrDefault().GetAttributeValue("href", "");

                        string imgLink = ProductListItem.Descendants("img")
                          .Where(node => node.GetAttributeValue("itemprop", "")
                                .Contains("image")).FirstOrDefault().GetAttributeValue("src", "");

                        if (price != "")
                        {
                            price = pasalinimasTrikdanciuSimboliu(price);
                            var priceTemporary = price;
                            priceTemporary = pasalinimasEuroSimbol(priceTemporary);
                            double pricea = Convert.ToDouble(priceTemporary);
                            var Item1 = new Item { nuotrauka = "https://pagrindinis.barbora.lt/" + imgLink, Seller = "Barbora", Name = name, Pricea = pricea, Price = price, Link = "https://pagrindinis.barbora.lt/" + link };
                            prices.Add(Item1);
                        }

                        countItems--;
                    }
                }
            }
            else 
            {
                var Itemas = new Item { Seller = "Barbora", Name = "tokios prekės " + MainWindow.zodis + " nėra šioje parduotuvėje" };
                prices.Add(Itemas);
            }
        }
        private static void WriteDataFromPigu(List<HtmlNode> ProductListItems, List<Item> prices)
        {
            if (ProductListItems != null)
            {
                int countItems = ProductListItems.Count - soldOut;
               
                    foreach (var ProductListItem in ProductListItems)
                    {
                        if (countItems != 0)
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
                            price = pasalinimasTarpuPigu(price);
                            var priceAtsarg = price;
                            price = pasalinimasEuroSimbol(price);
                            price = price + "€";
                            priceAtsarg = pasalinimasEuroSimbol(priceAtsarg);



                            double pricea = Convert.ToDouble(priceAtsarg);
                            var Itemas = new Item { nuotrauka = imgLink, Seller = "Pigu", Name = name, Pricea = pricea, Price = price, Link = link };
                            prices.Add(Itemas);
                            RasytiData(link, imgLink);
                            countItems--;
                        }
                }
            }
            else
            {
                var Itemas = new Item { Seller = "Pigu", Name = "tokios prekės " + MainWindow.zodis + " nėra šioje parduotuvėje" };
                prices.Add(Itemas);
            }
        }
        private static void WriteDataFromElektromarkt(List<HtmlNode> ProductListItems2, List<Item> prices)
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

                    price = pasalinimasTarpu(price);
                    var priceAtsarg = price;
                    priceAtsarg = pasalinimasEuroSimbol(priceAtsarg);

                    double pricea = Double.Parse(priceAtsarg);
                    var Item1 = new Item { nuotrauka = imgLink, Seller = "Elektromarkt", Name = name, Pricea = pricea, Price = price, Link = link };
                    prices.Add(Item1);
                    RasytiData(link, imgLink);

                }
            }
            else
            {
                var Item1 = new Item { Seller = "Elektromarkt", Name = "tokios prekės " + MainWindow.zodis + " nėra šioje parduotuvėje" };
                prices.Add(Item1);
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

        private static string pasalinimasTarpuPigu(string price)
        {
            var charsToRemove = new string[] { " " };
            foreach (var c in charsToRemove)
            {
                price = price.Replace(c, string.Empty);
            }
            return price;
        }

        private static void SortAndInsert(List<Item> prices, DataGrid dataGridas)
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

        private void LinkButton_Click(object sender, RoutedEventArgs e)
        {
            string link = (((Button)sender).DataContext as Item).Link;
            if (link != null)
            {
                System.Diagnostics.Process.Start(link);
            }
        }
        private static void RasytiData(string siteURL, string imgURL)
        {
            using (var kontekstas = new DuomenuBazesKontekstas())
            {
                var rezultatas = kontekstas.PuslapiuDuomenys.SingleOrDefault(c => c.PuslapioURL == siteURL && c.ImgURL == imgURL);

                if (rezultatas == null)
                {
                    var puslapiuDuomenys = new PuslapiuDuomenys()
                    {
                        PuslapioURL = siteURL,
                        ImgURL = imgURL
                    };
                    kontekstas.PuslapiuDuomenys.Add(puslapiuDuomenys);
                    kontekstas.SaveChanges();

                }
            }
        }

        private void ImageClick_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string name= (((Image)sender).DataContext as Item).Name;
            KonkretiPreke langas = new KonkretiPreke(name);
            langas.Show();
        }
    }
}


