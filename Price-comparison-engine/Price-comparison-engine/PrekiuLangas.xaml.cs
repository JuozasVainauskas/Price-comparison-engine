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
            var url = "https://www.kaina24.lt/search?q=iphone+10";
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var ProductsHtml = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("product-list-horisontal clearfix")).ToList();


            var ProductListItems = ProductsHtml[0].Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .Contains("product-item-h-wrap")).ToList();


            foreach (var ProductListItem in ProductListItems)
            {

                var name = ProductListItem.Descendants("p")
                    .Where(node => node.GetAttributeValue("class", "'\r', '\n', '\t'")
                    .Equals("name")).FirstOrDefault().InnerText.Trim();

                textbox.AppendText(name + '\n');

                var link = ProductListItem.Descendants("a").FirstOrDefault().GetAttributeValue("href", "");

                textbox.AppendText(link + '\n');


                var price = ProductListItem.Descendants("p")
                   .Where(node => node.GetAttributeValue("class", "'\r', '\n', '\t'")
                   .Equals("price fl")).FirstOrDefault().InnerText.Trim();

                textbox.AppendText(price + '\n');


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
