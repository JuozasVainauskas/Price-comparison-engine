﻿using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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

    public class Item
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

        private static async void getHtmlAssync(DataGrid dataGridas)
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
            var piguDaiktai = piguPaieska(await piguHtmlPaemimas());
            var avitelosDaiktai = avitelosPaieska(await avitelosHtmlPaemimas());
            var elektromarktDaiktai = elektromarktPaieska(await elektromarktHtmlPaemimas());
            surasymasIsAvitelos(avitelosDaiktai, prices);
            surasymasIsElektromarkt(elektromarktDaiktai, prices);
            surasymasIsPigu(piguDaiktai, prices);
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

                /* catch()
                 {
                     return null;
                 }*/

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
                    if (price != "")
                    {
                        price = pasalinimasTrikdanciuSimboliu(price);
                        var priceAtsarg = price;
                        priceAtsarg = pasalinimasEuroSimbol(priceAtsarg);
                        double pricea = Convert.ToDouble(priceAtsarg);
                        var Itemas = new Item { Seller = "Avitela", Name = name, Pricea = pricea, Price = price, Link = link };
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

                    price = pasalinimasTarpu(price);
                    var priceAtsarg = price;
                    priceAtsarg = pasalinimasEuroSimbol(priceAtsarg);

                    double pricea = Double.Parse(priceAtsarg);
                    var Itemas = new Item { nuotrauka = imgLink, Seller = "Elektromarkt", Name = name, Pricea = pricea, Price = price, Link = link };
                    prices.Add(Itemas);
                    RasytiData(link, imgLink);

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

        private static string pasalinimasTarpuPigu(string price)
        {
            var charsToRemove = new string[] { " " };
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
        private static void RasytiData(string puslapioURL, string imgURL)
        {
            var sqlPrisijungti = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\PCEDatabase.mdf;Integrated Security=SSPI;Connect Timeout=30");
            try
            {
                if (sqlPrisijungti.State == ConnectionState.Closed)
                {
                    sqlPrisijungti.Open();
                }

                var duomenuAdapteris = new SqlDataAdapter("SELECT PuslapioURL, ImgURL FROM PuslapiuDuomenys WHERE PuslapioURL ='" + puslapioURL + "' AND ImgURL ='" + imgURL + "' ", sqlPrisijungti);
                var duomenuLentele = new DataTable();
                duomenuAdapteris.Fill(duomenuLentele);
                if (duomenuLentele.Rows.Count == 0)
                {
                    try
                    {
                        if (sqlPrisijungti.State == ConnectionState.Closed)
                        {
                            sqlPrisijungti.Open();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    var eile = "INSERT INTO PuslapiuDuomenys(PuslapioURL,ImgURL) VALUES (@PuslapioURL, @ImgURL)";
                    var sqlKomanda = new SqlCommand(eile, sqlPrisijungti);
                    sqlKomanda.CommandType = CommandType.Text;
                    sqlKomanda.Parameters.AddWithValue("@PuslapioURL", puslapioURL);
                    sqlKomanda.Parameters.AddWithValue("@ImgURL", imgURL);
                    sqlKomanda.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlPrisijungti.Close();
            }
        }
     }
}


