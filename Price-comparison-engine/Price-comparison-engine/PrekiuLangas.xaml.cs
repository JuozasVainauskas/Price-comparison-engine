using HtmlAgilityPack;
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

namespace Price_comparison_engine
{
    /// <summary>
    /// Interaction logic for PrekiųLangas.xaml
    /// </summary>
    public partial class PrekiuLangas : Window
    {
        public PrekiuLangas()
        {
            InitializeComponent();
        }


        private static async void getHtmlAssync(TextBox textbox)
        {


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

                textbox.AppendText("Avitela:" + '\n');
                textbox.AppendText("" + '\n');

                var price = ProductListItem.Descendants("div")
                   .Where(node => node.GetAttributeValue("class", "")
                   .Equals("price")).FirstOrDefault().InnerText.Trim();

                textbox.AppendText(price + '\n');

                var name = ProductListItem.Descendants("div")
                   .Where(node => node.GetAttributeValue("class", "")
                   .Equals("name")).FirstOrDefault().InnerText.Trim();

                textbox.AppendText(name + '\n');

                var link = ProductListItem.Descendants("a").FirstOrDefault().GetAttributeValue("href", "");

                textbox.AppendText(link + '\n');

                textbox.AppendText("" + '\n');

            }

            foreach (var ProductListItem in ProductListItems2)
            {

                textbox.AppendText("Elektromarkt:" + '\n');
                textbox.AppendText("" + '\n');

                var name = ProductListItem.Descendants("h2")
                   .Where(node => node.GetAttributeValue("class", "")
                   .Equals("product-name")).FirstOrDefault().InnerText.Trim();

                textbox.AppendText(name + '\n');

                var price = ProductListItem.Descendants("span")
                   .Where(node => node.GetAttributeValue("class", "")
                   .Equals("price")).FirstOrDefault().InnerText.Trim();

                textbox.AppendText(price + '\n');

                var link = ProductListItem.Descendants("a").FirstOrDefault().GetAttributeValue("href", "");

                textbox.AppendText(link + '\n');

                textbox.AppendText("" + '\n');

            }
        }


        private void loadTextboxData(object sender, EventArgs e)
        {
            getHtmlAssync(textBoxLangas);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RectangleIstempimasAukstis(rectangle1);
            RectangleIstempimasPlotis(rectangle2);
            TextBoxIstempimasPlotis(textBoxLangas);
        }

        private void RectangleIstempimasAukstis(Rectangle plotelis)
        {
            plotelis.Height = this.ActualHeight;
        }

        private void RectangleIstempimasPlotis(Rectangle plotelis)
        {
            plotelis.Width = this.ActualWidth;
        }
        private void TextBoxIstempimasPlotis(TextBox tekstoBlokas)
        {
            tekstoBlokas.Width = this.ActualWidth-400;
        }
    }
}
