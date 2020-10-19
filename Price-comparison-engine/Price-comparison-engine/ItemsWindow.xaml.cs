using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using HtmlAgilityPack;
using Price_comparison_engine.Classes;

namespace Price_comparison_engine
{

    
    public partial class Item
    {
        public string Picture { get; set; }

        public string Seller { get; set; }

        public double Pricea { get; set; }

        public string Price { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }
    }

    public partial class ItemsWindow
    {
        public static int soldOutBarbora;
        public static int soldOut;
        public static int loggedIn;
        private static readonly Regex RegEx = new Regex(" ");
        readonly MainWindowLoggedIn _grLoggedIn;

        protected Htmle.HtmlE html = new Htmle.HtmlE(0,RegEx.Replace(MainWindow.word, "+"));

        public Htmle.HtmlE Htmla
        {
            get => html;
            set => html = value;
        }
   
        public ItemsWindow(MainWindowLoggedIn grid)
        {
            InitializeComponent();
            this._grLoggedIn = grid;
            if (string.IsNullOrWhiteSpace(LoginWindow.Email))
            {
                dataGrid.Columns[5].Visibility = Visibility.Collapsed;
                loggedIn = 0;
            }
            else
            {
                dataGrid.Columns[5].Visibility = Visibility.Visible;
                loggedIn = 1;
            }
        }

        private async void GetHtmlAssync(DataGrid dataGrid)
        {
            var prices = new List<Item>();

            if (ReadItems(MainWindow.word).Any())
            {
                foreach (var item in ReadItems(MainWindow.word)) dataGrid.Items.Add(item);
            }
            else
            {
                var httpClient = new HttpClient();
                var urlRde = "https://www.rde.lt/search_result/lt/word/" + Htmla.htmlEnd  + "/page/1";
                var urlBarbora = "https://pagrindinis.barbora.lt/paieska?q=" + MainWindow.word;
                var urlPigu = "https://pigu.lt/lt/search?q=" + Htmla.htmlEnd;
                var urlBigBox = "https://bigbox.lt/paieska?controller=search&orderby=position&orderway=desc&ssa_submit=&search_query=" + Htmla.htmlEnd;
                var urlAvitela = "https://avitela.lt/paieska/" + MainWindow.word;
                var urlElektromarkt = "https://www.elektromarkt.lt/lt/catalogsearch/result/?order=price&dir=desc&q=" + Htmla.htmlEnd;
                var urlGintarineVaistine = "https://www.gintarine.lt/search?adv=false&cid=0&mid=0&vid=0&q="+ MainWindow.word + "%5D&sid=false&isc=true&orderBy=0";
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
                var gintarineVaistineItems = GintarineVaistineSearch(await Html(httpClient, urlGintarineVaistine));
                WriteDataFromgintarineVaistine(gintarineVaistineItems, prices);

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

        private static List<HtmlNode> GintarineVaistineSearch(HtmlDocument htmlDocument)
        {
            try
            {
                var productsHtml = htmlDocument.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("item-grid")).ToList();

                var productListItems = productsHtml[0].Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Contains("item-box")).ToList();
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
                foreach (var unused in productListItemsSoldOut)
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
                foreach (var unused in productListItemsSoldOut)
                {
                    soldOut++;
                }
                return productListItems2;
            }

            return null;
        }

        private static void WriteDataFromgintarineVaistine(List<HtmlNode> productListItems, List<Item> prices)
        {
            if (productListItems != null)
            {
                foreach (var productListItem in productListItems)
                {

                    var price = productListItem
                        .Descendants("span").FirstOrDefault(node => node.GetAttributeValue("class", "")
                            .Equals("price actual-price"))
                        ?.InnerText.Trim();

                    var name = productListItem.Descendants("input").FirstOrDefault()?.GetAttributeValue("value", "");

                    var link = productListItem.Descendants("a").FirstOrDefault()?.GetAttributeValue("href", "");
                    var imgLink = productListItem.Descendants("img").FirstOrDefault()?.GetAttributeValue("data-lazyloadsrc", "");

                    if (price != "")
                    {
                        var regex = Regex.Match(price ?? string.Empty, @"[0-9]+\,[0-9][0-9]");
                            price = Convert.ToString(regex);
                            var pricea = Convert.ToDouble(price);

                        var singleItem = new Item { Picture = imgLink, Seller = "Gintarine vaistine", Name = name, Pricea = pricea, Price = price +'€', Link = "https://www.gintarine.lt/" + link };
                            prices.Add(singleItem);
                    }
                }
            }
            else
            {
                var singleItem = new Item { Seller = "Gintarine vaistine", Name = "tokios prekės " + MainWindow.word + " nėra šioje parduotuvėje" };
                prices.Add(singleItem);
            }
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
                            var pricea = Convert.ToDouble(priceAtsarg);

                            var singleItem = new Item { Picture = "https://www.rde.lt/" + imgLink, Seller = "Rde", Name = name, Pricea = pricea, Price = price, Link = "https://www.rde.lt/" + link };
                            prices.Add(singleItem);
                        }
                    }
                }
            }
            else
            {
                var singleItem = new Item { Seller = "Rde", Name = "tokios prekės " + MainWindow.word + " nėra šioje parduotuvėje" };
                prices.Add(singleItem);
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

                    var imgLink = productListItem.Descendants("img").FirstOrDefault()?.GetAttributeValue("data-echo", "");

                    if (price != "")
                    {
                        price = PasalinimasTrikdanciuSimboliu(price);
                        var priceAtsarg = price;
                        priceAtsarg = PasalinimasEuroSimbol(priceAtsarg);
                        var pricea = Convert.ToDouble(priceAtsarg);
                        if (imgLink != "")
                        {
                            var singleItem = new Item { Picture = imgLink, Seller = "Avitela", Name = name, Pricea = pricea, Price = price, Link = link };
                            prices.Add(singleItem);
                        }
                        else
                        {
                            var singleItem = new Item { Picture = "https://avitela.lt/image/no_image.jpg", Seller = "Avitela", Name = name, Pricea = pricea, Price = price, Link = link };
                            prices.Add(singleItem);
                        }

                    }
                }
            }
            else
            {
                var singleItem = new Item { Seller = "Avitela", Name = "tokios prekės " + MainWindow.word + " nėra šioje parduotuvėje" };
                prices.Add(singleItem);
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

                    var imgLink = productListItem
                        .Descendants("img").FirstOrDefault(node => node.GetAttributeValue("class", "")
                            .Contains("replace-2x img-responsive"))
                        ?.GetAttributeValue("src", "");

                    if (price != "")
                    {
                        price = PasalinimasTarpuPigu(price);
                        var priceAtsarg = price;
                        price = PasalinimasEuroSimbol(price);
                        price = price + "€";
                        priceAtsarg = PasalinimasEuroSimbol(priceAtsarg);
                        var pricea = Convert.ToDouble(priceAtsarg);
                        var singleItem = new Item { Picture = imgLink, Seller = "BigBox", Name = name, Pricea = pricea, Price = price, Link = link };

                        prices.Add(singleItem);
                    }
                }
            }
            else
            {
                var singleItem = new Item { Seller = "BigBox", Name = "tokios prekės " + MainWindow.word + " nėra šioje parduotuvėje" };
                prices.Add(singleItem);
            }
        }

        private static void WriteDataFromBarbora(List<HtmlNode> productListItems, List<Item> prices)
        {
            if (productListItems != null)
            {
                var countItems = productListItems.Count - soldOutBarbora;

                foreach (var productListItem in productListItems)
                    if (countItems != 0)
                    {
                        var price = productListItem
                            .Descendants("span").FirstOrDefault(node => node.GetAttributeValue("class", "")
                                .Equals("b-product-price-current-number"))
                            ?.InnerText.Trim();

                        var name = productListItem
                            .Descendants("span").FirstOrDefault(node => node.GetAttributeValue("itemprop", "")
                                .Equals("name"))
                            ?.InnerText.Trim();

                        var link = productListItem.Descendants("a").FirstOrDefault()?.GetAttributeValue("href", "");

                        var imgLink = productListItem
                            .Descendants("img").FirstOrDefault(node => node.GetAttributeValue("itemprop", "")
                                .Contains("image"))
                            ?.GetAttributeValue("src", "");

                        if (price != "")
                        {
                            price = PasalinimasTrikdanciuSimboliu(price);
                            var priceTemporary = price;
                            priceTemporary = PasalinimasEuroSimbol(priceTemporary);
                            var pricea = Convert.ToDouble(priceTemporary);
                            var item1 = new Item { Picture = "https://pagrindinis.barbora.lt/" + imgLink, Seller = "Barbora", Name = name, Pricea = pricea, Price = price, Link = "https://pagrindinis.barbora.lt/" + link };
                            prices.Add(item1);
                        }

                        countItems--;
                    }
            }
            else
            {
                var singleItem = new Item { Seller = "Barbora", Name = "tokios prekės " + MainWindow.word + " nėra šioje parduotuvėje" };
                prices.Add(singleItem);
            }
        }

        private static void WriteDataFromPigu(List<HtmlNode> productListItems, List<Item> prices)
        {
            if (productListItems != null)
            {
                var countItems = productListItems.Count - soldOut;

                foreach (var productListItem in productListItems)
                    if (countItems != 0)
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

                        var imgLink = productListItem
                            .Descendants("img").FirstOrDefault(node => node.GetAttributeValue("src", "")
                                .Contains("jpg"))
                            ?.GetAttributeValue("src", "");

                        Console.WriteLine(imgLink);
                        if (price != null)
                        {
                            price = PasalinimasTarpuPigu(price);
                            var priceAtsarg = price;
                            price = PasalinimasEuroSimbol(price);
                            price = price + "€";
                            priceAtsarg = PasalinimasEuroSimbol(priceAtsarg);

                            var pricea = Convert.ToDouble(priceAtsarg);
                            var singleItem = new Item
                            {
                                Picture = imgLink, Seller = "Pigu", Name = name, Pricea = pricea, Price = price,
                                Link = link
                            };
                            prices.Add(singleItem);
                            countItems--;
                        }
                    }
            }
            else
            {
                var singleItem = new Item { Seller = "Pigu", Name = "tokios prekės " + MainWindow.word + " nėra šioje parduotuvėje" };
                prices.Add(singleItem);
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

                    var imgLink = productListItem.Descendants("img").FirstOrDefault()?.GetAttributeValue("src", "");

                    price = PasalinimasTarpu(price);
                    var priceAtsarg = price;
                    priceAtsarg = PasalinimasEuroSimbol(priceAtsarg);

                    var pricea = double.Parse(priceAtsarg);
                    var item1 = new Item { Picture = imgLink, Seller = "Elektromarkt", Name = name, Pricea = pricea, Price = price, Link = link };
                    prices.Add(item1);

                }
            }
            else
            {
                var item1 = new Item { Seller = "Elektromarkt", Name = "tokios prekės " + MainWindow.word + " nėra šioje parduotuvėje" };
                prices.Add(item1);
            }
        }

        private static string PasalinimasEuroSimbol(string priceAtsarg)
        {
            var charsToRemove = new[] { "€" };
            foreach (var c in charsToRemove) priceAtsarg = priceAtsarg.Replace(c, string.Empty);

            return priceAtsarg;
        }

        private static string PasalinimasTrikdanciuSimboliu(string price)
        {
            var index = price.IndexOf("\n", StringComparison.Ordinal);
            if (index > 0) price = price.Substring(0, index);

            var charsToChange = new[] { "." };
            foreach (var c in charsToChange) price = price.Replace(c, ",");
            return price;
        }

        private static string PasalinimasTrikdanciuSimboliu2(string price)
        {
            var index = price.IndexOf("\n", StringComparison.Ordinal);
            if (index > 0) price = price.Substring(0, index);

            var charsToChange = new[] { "." };
            foreach (var c in charsToChange) price = price.Replace(c, ",");
            var charsToChange2 = new[] { "&nbsp;" };
            foreach (var c in charsToChange2) price = price.Replace(c, "");
            var charsToChange3 = new[] { "Kaina: " };
            foreach (var c in charsToChange3) price = price.Replace(c, "");

            return price;
        }

        private static string PasalinimasTarpu(string price)
        {
            var charsToRemove = new[] { " " };
            foreach (var c in charsToRemove) price = price.Replace(c, string.Empty);
            return price;
        }

        private static string PasalinimasTarpuPigu(string price)
        {
            var charsToRemove = new[] { " " };
            foreach (var c in charsToRemove) price = price.Replace(c, string.Empty);
            return price;
        }

        private static void SortAndInsert(List<Item> prices, DataGrid dataGrid)
        {
            var sortedPricesList = prices.OrderBy(o => o.Pricea).ToList();
            foreach (var item in sortedPricesList)
            {
                dataGrid.Items.Add(item);
                WriteItems(item.Link, item.Picture, item.Seller, item.Name, item.Price, MainWindow.word);
            }
            prices.Clear();
        }


        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RectangleChangingHeight(rectangle1);
            RectangleChangingWidth(rectangle2);
        }

        private void RectangleChangingHeight(Rectangle area)
        {
            area.Height = ActualHeight;
        }

        private void RectangleChangingWidth(Rectangle area)
        {
            area.Width = ActualWidth;
        }

        private void DataGridTest_Initialized(object sender, EventArgs e)
        {
            GetHtmlAssync(dataGrid);
        }

        private void LinkButton_Click(object sender, RoutedEventArgs e)
        {
            var link = (((Button)sender).DataContext as Item)?.Link;
            if (link != null) Process.Start(link);
        }
       
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            var link = (((Button)sender).DataContext as Item)?.Link;
            var shopName = (((Button)sender).DataContext as Item)?.Seller;
            var photoUrll = (((Button)sender).DataContext as Item)?.Picture;
            var itemName = (((Button)sender).DataContext as Item)?.Name;
            var price = (((Button)sender).DataContext as Item)?.Price;
            if (link != null)
            {
                WriteSavedItems(link, photoUrll, shopName, itemName, price, LoginWindow.Email);
            }

            var singleItem = new Item
            {
                Link = link,
                Picture = photoUrll,
                Seller = shopName,
                Name = itemName,
                Price = price
            };

            if (!_grLoggedIn.DataGridLoggedIn.Items.Cast<Item>().Any(t => t.Link == link && t.Picture == photoUrll && t.Seller == shopName && t.Name == itemName && t.Price == price))
            {
                MessageBox.Show("Prekė sėkmingai išsaugota palyginimui!");
                _grLoggedIn.DataGridLoggedIn.Items.Add(singleItem);
            }
            else
            {
                MessageBox.Show("Prekė jau buvo pridėta.");
            }
        }

        private static void WriteItems(string pageUrl, string imgUrl, string shopName, string itemName, string price, string keyword)
        {
            using (var context = new DatabaseContext())
            {
                var result = context.ItemsTable.SingleOrDefault(c => c.PageUrl == pageUrl && c.ImgUrl == imgUrl && c.ShopName == shopName && c.ItemName == itemName && c.Price == price && c.Keyword == keyword);

                if (result == null)
                {
                    var itemsTable = new ItemsTable
                    {
                        PageUrl = pageUrl,
                        ImgUrl = imgUrl,
                        ShopName = shopName,
                        ItemName = itemName,
                        Price = price,
                        Keyword = keyword
                    };
                    context.ItemsTable.Add(itemsTable);
                    context.SaveChanges();
                }
            }
        }

        private static List<Item> ReadItems(string keyword)
        {
            var item = new List<Item>();
            
            using (var context = new DatabaseContext())
            {
                var result = context.ItemsTable.Where(x => x.Keyword == keyword).Select(x => new Item { Link = x.PageUrl, Picture = x.ImgUrl, Seller = x.ShopName, Name = x.ItemName, Price = x.Price }).ToList();

                foreach (var singleItem in result)
                {
                    item.Add(singleItem);
                }
            }
            return item;
        }

        private static void WriteSavedItems(string pageUrl, string imgUrl, string shopName, string itemName, string price, string email)
        {
            using (var context = new DatabaseContext())
            {
                var result = context.SavedItems.SingleOrDefault(c => c.PageUrl == pageUrl && c.ImgUrl == imgUrl && c.ShopName == shopName && c.ItemName == itemName && c.Price == price && c.Email == email);

                if (result == null)
                {
                    var savedItems = new SavedItems()
                    {
                        PageUrl = pageUrl,
                        ImgUrl = imgUrl,
                        ShopName = shopName,
                        ItemName = itemName,
                        Price = price,
                        Email = email
                    };
                    context.SavedItems.Add(savedItems);
                    context.SaveChanges();
                }
            }
        }

        private void ImageClick_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var name = (((Image)sender).DataContext as Item)?.Name;
            var window = new ParticularItemWindow(name);
            window.Show();
        }
    }
}


