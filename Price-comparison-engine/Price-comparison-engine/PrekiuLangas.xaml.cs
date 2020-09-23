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
            var url = "https://www.ebay.com/sch/i.html?_nkw=iphone+11&_in_kw=1&_ex_kw=&_sacat=0&LH_Complete=1&_udlo=&_udhi=&_samilow=&_samihi=&_sadis=15&_stpos=&_sargn=-1%26saslc%3D1&_salic=1&_sop=12&_dmd=1&_ipg=50&_fosrp=1";
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var ProductsHtml = htmlDocument.DocumentNode.Descendants("ul")
                .Where(node => node.GetAttributeValue("id","")
                .Equals("ListViewInner")).ToList();
            
            var ProductListItems = ProductsHtml[0].Descendants("li")
                .Where(node => node.GetAttributeValue("id", "")
                .Contains("item")).ToList();


            foreach (var ProductListItem in ProductListItems)
            {
                var listingId = ProductListItem.GetAttributeValue("listingId", "");
                textbox.AppendText(listingId+'\n');

                var name = ProductListItem.Descendants("h3")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("lvtitle")).FirstOrDefault().InnerText.Trim('\r', '\n', '\t');
                textbox.AppendText(name + '\n');

                var price = Regex.Match(
                     ProductListItem.Descendants("li")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Equals("lvprice prc")).FirstOrDefault().InnerText.Trim('\r', '\n', '\t')
                    , @"\d+.\d+");

                var priceString = price.ToString();
                textbox.AppendText(priceString + '\n');

                var link = ProductListItem.Descendants("a").FirstOrDefault().GetAttributeValue("href", "");
                textbox.AppendText(link + '\n');
            }
        }

        private void loadTextboxData(object sender, EventArgs e)
        {
            getHtmlAssync(testbox);
        }
    }
}
