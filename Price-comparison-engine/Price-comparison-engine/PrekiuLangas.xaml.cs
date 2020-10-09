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

        public string Nuotrauka { get; set; }
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

            if (string.IsNullOrWhiteSpace(PrisijungimoLangas.email))
            {
                VertinimoMygtukas.IsEnabled = false;
                VertinimoMygtukas.Visibility = Visibility.Collapsed;
            }
            else
            {
                VertinimoMygtukas.IsEnabled = true;
                VertinimoMygtukas.Visibility = Visibility.Visible;
            }

        }

        private static async void GetHtmlAssync(DataGrid dataGrid)
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
            if (SkaitytiPrekes(MainWindow.zodis).Any())
            {

                foreach (var item in SkaitytiPrekes(MainWindow.zodis))
                {
                    dataGrid.Items.Add(item);
                }

            }
            else
            {
                var httpClient = new HttpClient();
                var regEx = new Regex(" ");
                var urlgalas = regEx.Replace(MainWindow.zodis, "+");
                var urlRde = "https://www.rde.lt/search_result/lt/word/" + urlgalas + "/page/1";
                var urlBarbora = "https://pagrindinis.barbora.lt/paieska?q=" + MainWindow.zodis;
                var urlPigu = "https://pigu.lt/lt/search?q=" + urlgalas;
                var urlBigBox = "https://bigbox.lt/paieska?controller=search&orderby=position&orderway=desc&ssa_submit=&search_query=" + urlgalas;
                var urlAvitela = "https://avitela.lt/paieska/" + MainWindow.zodis;
                var urlElektromarkt = "https://www.elektromarkt.lt/lt/catalogsearch/result/?order=price&dir=desc&q=" + urlgalas;
                var rdeItems = RdeSearch(await Html(httpClient, urlRde));
                WriteDataFromRde(rdeItems, prices);
                var barboraItems = BarboraSearch(await Html(httpClient, urlBarbora));
                WriteDataFromBarbora(barboraItems, prices);
                var piguItems = PiguSearch(await Html(httpClient, urlPigu));
                WriteDataFromPigu(piguItems, prices);
                var bigBoxItem = BigBoxSearch(await Html(httpClient, urlBigBox));
                WriteDataFromBigBox(bigBoxItem, prices);
                var avitelaItems = AvitelaSearch(await Html(httpClient, urlAvitela));
                WriteDataFromAvitela(avitelaItems, prices);
                var elektromarktItems = ElektromarktSearch(await Html(httpClient, urlElektromarkt));
                WriteDataFromElektromarkt(elektromarktItems, prices);
             
                SortAndInsert(prices, dataGrid);
            }
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

        public static int soldOutBarbora = 0;

        private static List<HtmlNode> BarboraSearch(HtmlDocument htmlDocument2)
        {
            try
            {
                var productsHtml2 = htmlDocument2.DocumentNode.Descendants("div")
               .Where(node => node.GetAttributeValue("class", "")
               .Equals("b-page-specific-content")).ToList();

                var productListItems2 = productsHtml2[0].Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Contains("b-product--wrap2 b-product--desktop-grid")).ToList();
                var productListItemsSoldOut = productsHtml2[0].Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Contains("b-product-out-of-stock-backdrop")).ToList();
                foreach (var productListItemsSold in productListItemsSoldOut)
                {
                    soldOutBarbora++;
                }
                return productListItems2;
            }
            catch
            {
                return null;
            }
        }
        public static int soldOut = 0;
        private static List<HtmlNode> PiguSearch(HtmlDocument htmlDocument2)
        {

            if (htmlDocument2 != null)
            {
                var productsHtml2 = htmlDocument2.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("widget-old", "")
                .Equals("ContentLoader")).ToList();

                var productListItems2 = productsHtml2[0].Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Contains("product-list-item")).ToList();
                var productListItemsSoldOut = productsHtml2[0].Descendants("span")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Contains("label-soldout")).ToList();
                foreach (var productListItemsSold in productListItemsSoldOut)
                {
                    soldOut++;
                }
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
                            .Equals("product_price_wo_discount_listing")).InnerText.Trim();

                    var name = productListItem
                        .Descendants("div").FirstOrDefault(node => node.GetAttributeValue("class", "")
                            .Equals("product_name")).InnerText.Trim();

                    var link = productListItem.Descendants("a").FirstOrDefault().GetAttributeValue("href", "");
                    
                    var productListItems2 = productListItem.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Contains("photo_box")).ToList();
                    foreach (var ProductListItem2 in productListItems2)
                    {
                        var imgLink = ProductListItem2.Descendants("img").FirstOrDefault().GetAttributeValue("src", "");

                        if (price != "")
                        {
                            price = PasalinimasTrikdanciuSimboliu2(price);
                            var priceAtsarg = price;
                            priceAtsarg = PasalinimasTrikdanciuSimboliu2(priceAtsarg);
                            priceAtsarg = PasalinimasEuroSimbol(priceAtsarg);
                            var pricea = Convert.ToDouble(priceAtsarg);

                            var itemas = new Item { Nuotrauka = "https://www.rde.lt/" + imgLink, Seller = "Rde", Name = name, Pricea = pricea, Price = price, Link = "https://www.rde.lt/" + link };
                            prices.Add(itemas);


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
        private static void WriteDataFromAvitela(List<HtmlNode> productListItems, List<Item> prices)
        {
            if (productListItems != null)
            {
                foreach (var productListItem in productListItems)
                {

                    var price = productListItem
                        .Descendants("div").FirstOrDefault(node => node.GetAttributeValue("class", "")
                            .Equals("price")).InnerText.Trim();

                    var name = productListItem
                        .Descendants("div").FirstOrDefault(node => node.GetAttributeValue("class", "")
                            .Equals("name")).InnerText.Trim();

                    var link = productListItem.Descendants("a").FirstOrDefault().GetAttributeValue("href", "");

                    var imgLink = productListItem.Descendants("img").FirstOrDefault().GetAttributeValue("data-echo", "");

                    if (price != "")
                    {
                        price = PasalinimasTrikdanciuSimboliu(price);
                        var priceAtsarg = price;
                        priceAtsarg = PasalinimasEuroSimbol(priceAtsarg);
                        var pricea = Convert.ToDouble(priceAtsarg);
                        if (imgLink != (""))
                        {
                            var itemas = new Item { Nuotrauka = imgLink, Seller = "Avitela", Name = name, Pricea = pricea, Price = price, Link = link };
                            prices.Add(itemas);
                        }
                        else
                        {
                            var itemas = new Item { Nuotrauka = "https://avitela.lt/image/no_image.jpg", Seller = "Avitela", Name = name, Pricea = pricea, Price = price, Link = link };
                            prices.Add(itemas);
                        }

                    }
                }
            }
            else
            {
                var itemas = new Item { Seller = "Avitela", Name = "tokios prekės " + MainWindow.zodis + " nėra šioje parduotuvėje" };
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
                            .Equals("price product-price")).InnerText.Trim();

                    var name = productListItem
                        .Descendants("a").FirstOrDefault(node => node.GetAttributeValue("class", "")
                            .Equals("product-name")).InnerText.Trim();

                    var link = productListItem.Descendants("a").FirstOrDefault().GetAttributeValue("href", "");

                    var imgLink = productListItem
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
                        var itemas = new Item { Nuotrauka = imgLink, Seller = "BigBox", Name = name, Pricea = pricea, Price = price, Link = link };

                        prices.Add(itemas);
                    }
                }
            }
            else
            {
                var itemas = new Item { Seller = "BigBox", Name = "tokios prekės " + MainWindow.zodis + " nėra šioje parduotuvėje" };
                prices.Add(itemas);
            }
        }

        private static void WriteDataFromBarbora(List<HtmlNode> productListItems, List<Item> prices)
        {
            if (productListItems != null)
            {
                var countItems = productListItems.Count - soldOutBarbora;

                foreach (var productListItem in productListItems)
                {
                    if (countItems != 0)
                    {
                        var price = productListItem
                            .Descendants("span").FirstOrDefault(node => node.GetAttributeValue("class", "")
                                .Equals("b-product-price-current-number")).InnerText.Trim();

                        var name = productListItem
                            .Descendants("span").FirstOrDefault(node => node.GetAttributeValue("itemprop", "")
                                .Equals("name")).InnerText.Trim();

                        var link = productListItem.Descendants("a").FirstOrDefault().GetAttributeValue("href", "");

                        var imgLink = productListItem
                            .Descendants("img").FirstOrDefault(node => node.GetAttributeValue("itemprop", "")
                                .Contains("image")).GetAttributeValue("src", "");

                        if (price != "")
                        {
                            price = PasalinimasTrikdanciuSimboliu(price);
                            var priceTemporary = price;
                            priceTemporary = PasalinimasEuroSimbol(priceTemporary);
                            var pricea = Convert.ToDouble(priceTemporary);
                            var item1 = new Item { Nuotrauka = "https://pagrindinis.barbora.lt/" + imgLink, Seller = "Barbora", Name = name, Pricea = pricea, Price = price, Link = "https://pagrindinis.barbora.lt/" + link };
                            prices.Add(item1);
                        }

                        countItems--;
                    }
                }
            }
            else
            {
                var itemas = new Item { Seller = "Barbora", Name = "tokios prekės " + MainWindow.zodis + " nėra šioje parduotuvėje" };
                prices.Add(itemas);
            }
        }
        private static void WriteDataFromPigu(List<HtmlNode> productListItems, List<Item> prices)
        {
            if (productListItems != null)
            {
                var countItems = productListItems.Count - soldOut;

                foreach (var productListItem in productListItems)
                {
                    if (countItems != 0)
                    {
                        var price = productListItem
                            .Descendants("span").FirstOrDefault(node => node.GetAttributeValue("class", "")
                                .Equals("price notranslate")).InnerText.Trim();
                        var name = productListItem
                            .Descendants("p").FirstOrDefault(node => node.GetAttributeValue("class", "")
                                .Equals("product-name")).InnerText.Trim();

                        var link = "https://pigu.lt/" + productListItem.Descendants("a").FirstOrDefault().GetAttributeValue("href", "");

                        var imgLink = productListItem
                            .Descendants("img").FirstOrDefault(node => node.GetAttributeValue("src", "")
                                .Contains("jpg")).GetAttributeValue("src", "");

                        Console.WriteLine(imgLink);
                        price = PasalinimasTarpuPigu(price);
                        var priceAtsarg = price;
                        price = PasalinimasEuroSimbol(price);
                        price = price + "€";
                        priceAtsarg = PasalinimasEuroSimbol(priceAtsarg);



                        var pricea = Convert.ToDouble(priceAtsarg);
                        var itemas = new Item { Nuotrauka = imgLink, Seller = "Pigu", Name = name, Pricea = pricea, Price = price, Link = link };
                        prices.Add(itemas);
                        RasytiData(link, imgLink);
                        countItems--;
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
                            .Equals("product-name")).InnerText.Trim();

                    var price = productListItem
                        .Descendants("span").FirstOrDefault(node => node.GetAttributeValue("class", "")
                            .Equals("price")).InnerText.Trim();

                    var link = productListItem.Descendants("a").FirstOrDefault().GetAttributeValue("href", "");

                    var imgLink = productListItem.Descendants("img").FirstOrDefault().GetAttributeValue("src", "");

                    price = PasalinimasTarpu(price);
                    var priceAtsarg = price;
                    priceAtsarg = PasalinimasEuroSimbol(priceAtsarg);

                    var pricea = double.Parse(priceAtsarg);
                    var item1 = new Item { Nuotrauka = imgLink, Seller = "Elektromarkt", Name = name, Pricea = pricea, Price = price, Link = link };
                    prices.Add(item1);
                    RasytiData(link, imgLink);

                }
            }
            else
            {
                var item1 = new Item { Seller = "Elektromarkt", Name = "tokios prekės " + MainWindow.zodis + " nėra šioje parduotuvėje" };
                prices.Add(item1);
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
            var index = price.IndexOf("\n");
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
            var index = price.IndexOf("\n");
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
            var charsToRemove = new string[] { " " };
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

        private static void SortAndInsert(List<Item> prices, DataGrid dataGridas)
        {
            var SortedPricesList = prices.OrderBy(o => o.Pricea).ToList();
            foreach (var item in SortedPricesList)
            {
                dataGridas.Items.Add(item);
                RasytiPrekes(item.Link, item.Nuotrauka, item.Seller, item.Name, item.Price, MainWindow.zodis);
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
            GetHtmlAssync(DataGridas);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var vertinimoLangoAtidarymas = new VertinimoLangas();
            vertinimoLangoAtidarymas.Show();
        }

        private void LinkButton_Click(object sender, RoutedEventArgs e)
        {
            var link = (((Button)sender).DataContext as Item).Link;
            if (link != null)
            {
                System.Diagnostics.Process.Start(link);
            }
        }

        private static void RasytiPrekes(string siteURL, string imgURL, string parduotuvesVardas, string prekesVardas, string prekesKaina, string raktinisZodis)
        {
            using (var kontekstas = new DuomenuBazesKontekstas())
            {
                var rezultatas = kontekstas.PrekiuDuomenys.SingleOrDefault(c => c.PuslapioURL == siteURL && c.ImgURL == imgURL && c.ParduotuvesVardas == parduotuvesVardas && c.PrekesVardas == prekesVardas && c.PrekesKaina == prekesKaina && c.RaktinisZodis == raktinisZodis);

                if (rezultatas == null)
                {
                    var prekiuDuomenys = new PrekiuDuomenys()
                    {
                        PuslapioURL = siteURL,
                        ImgURL = imgURL,
                        ParduotuvesVardas = parduotuvesVardas,
                        PrekesVardas = prekesVardas,
                        PrekesKaina = prekesKaina,
                        RaktinisZodis = raktinisZodis
                    };
                    kontekstas.PrekiuDuomenys.Add(prekiuDuomenys);
                    kontekstas.SaveChanges();

                }
            }
        }

        private static List<Item> SkaitytiPrekes(string raktinisZodis)
        {
            var item = new List<Item>();
            using (var kontekstas = new DuomenuBazesKontekstas())
            {
                var tempSiteUrl = kontekstas.PrekiuDuomenys.Where(x => x.RaktinisZodis == raktinisZodis).Select(x => x.PuslapioURL).ToList();
                var tempImgUrl = kontekstas.PrekiuDuomenys.Where(x => x.RaktinisZodis == raktinisZodis).Select(x => x.ImgURL).ToList();
                var tempParduotuvesVardas = kontekstas.PrekiuDuomenys.Where(x => x.RaktinisZodis == raktinisZodis).Select(x => x.ParduotuvesVardas).ToList();
                var tempPrekesVardas = kontekstas.PrekiuDuomenys.Where(x => x.RaktinisZodis == raktinisZodis).Select(x => x.PrekesVardas).ToList();
                var tempPrekesKaina = kontekstas.PrekiuDuomenys.Where(x => x.RaktinisZodis == raktinisZodis).Select(x => x.PrekesKaina).ToList();

                if (tempSiteUrl != null && tempImgUrl != null && tempParduotuvesVardas != null && tempPrekesVardas != null && tempPrekesKaina != null)
                {
                    for (int i = 0; i < tempSiteUrl.Count; i++)
                    {
                        var itemas = new Item()
                        {
                            Link = tempSiteUrl.ElementAt(i),
                            Nuotrauka = tempImgUrl.ElementAt(i),
                            Seller = tempParduotuvesVardas.ElementAt(i),
                            Name = tempPrekesVardas.ElementAt(i),
                            Price = tempPrekesKaina.ElementAt(i)
                        };
                        item.Add(itemas);
                    }
                }
            }
            return item;

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
            var name = (((Image)sender).DataContext as Item).Name;
            var langas = new KonkretiPreke(name);
            langas.Show();
        }
    }
}


