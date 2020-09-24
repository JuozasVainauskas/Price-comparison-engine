using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        public string Seller { get; set; }
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

            var url = "https://avitela.lt/paieska/" + MainWindow.zodis;
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var ProductsHtml = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("product-grid active")).ToList();

            var ProductListItems = ProductsHtml[0].Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .Contains("col-6 col-md-4 col-lg-4")).ToList();

            string pattern = " ";
            string replacement = "+";

            Regex regEx = new Regex(pattern);
            var urlgalas = regEx.Replace(MainWindow.zodis, replacement);

            var url2 = "https://www.elektromarkt.lt/lt/catalogsearch/result/?order=price&dir=desc&q=" + urlgalas;
            var httpClient2 = new HttpClient();
            var html2 = await httpClient2.GetStringAsync(url2);

            var htmlDocument2 = new HtmlDocument();
            htmlDocument2.LoadHtml(html2);

            var ProductsHtml2 = htmlDocument2.DocumentNode.Descendants("div")
               .Where(node => node.GetAttributeValue("class", "")
               .Equals("manafilters-category-products category-products")).ToList();

            var ProductListItems2 = ProductsHtml2[0].Descendants("li")
                .Where(node => node.GetAttributeValue("class", "")
                .Contains("item js-ua-item")).ToList();

            foreach (var ProductListItem in ProductListItems)

            {
                var price = ProductListItem.Descendants("div")
                   .Where(node => node.GetAttributeValue("class", "")
                   .Equals("price")).FirstOrDefault().InnerText.Trim();

                var name = ProductListItem.Descendants("div")
                   .Where(node => node.GetAttributeValue("class", "")
                   .Equals("name")).FirstOrDefault().InnerText.Trim();

                var link = ProductListItem.Descendants("a").FirstOrDefault().GetAttributeValue("href", "");
                var Itemas = new Item { Seller = "Avitela", Name = name, Price = price, Link = link };
                prices.Add(Itemas);
            }

            foreach (var ProductListItem in ProductListItems2)
            {

                var name = ProductListItem.Descendants("h2")
                   .Where(node => node.GetAttributeValue("class", "")
                   .Equals("product-name")).FirstOrDefault().InnerText.Trim();

                var price = ProductListItem.Descendants("span")
                   .Where(node => node.GetAttributeValue("class", "")
                   .Equals("price")).FirstOrDefault().InnerText.Trim();

                var link = ProductListItem.Descendants("a").FirstOrDefault().GetAttributeValue("href", "");

                var Itemas = new Item { Seller = "Elektromarkt", Name = name, Price = price, Link = link };

                prices.Add(Itemas);
            }
            List<Item> SortedPricesList = prices.OrderBy(o => o.Price).ToList();
            foreach (Item item in SortedPricesList)
            {
                dataGridas.Items.Add(item);
            }
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
    }
}
