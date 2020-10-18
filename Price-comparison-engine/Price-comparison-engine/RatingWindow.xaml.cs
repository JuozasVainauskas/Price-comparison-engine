using Price_comparison_engine.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Price_comparison_engine
{
    /// <summary>
    /// Interaction logic for RatingWindow.xaml
    /// </summary>
    public partial class RatingWindow : Window
    {
        public RatingWindow()
        {
            InitializeComponent();
        }

        class Comment
        {
            public string Text { get; set; }
        }

        private static int _votes = 0;
        private static int _votersCount = 0;

        private static List<CommentsTable> _commentsData = new List<CommentsTable>();


        private static Dictionary<string, int> rating = new Dictionary<string, int>()
        {
            {"Aptarnavimas", 0},
            {"PrekiuKokybe", 0},
            {"Pristatymas", 0}
        };

        private static Dictionary<string, Dictionary<string, int>> singlePersonRatings = new Dictionary<string, Dictionary<string, int>>()
        {
            {"Avitela", new Dictionary<string, int>()
                {
                    {"Aptarnavimas", 0},
                    {"PrekiuKokybe", 0},
                    {"Pristatymas", 0}
                }
            },
            {"Elektromarkt", new Dictionary<string, int>()
                {
                    {"Aptarnavimas", 0},
                    {"PrekiuKokybe", 0},
                    {"Pristatymas", 0}
                }
            },
            {"Pigu", new Dictionary<string, int>()
                {
                    {"Aptarnavimas", 0},
                    {"PrekiuKokybe", 0},
                    {"Pristatymas", 0}
                }
            },
            {"Barbora", new Dictionary<string, int>()
                {
                    {"Aptarnavimas", 0},
                    {"PrekiuKokybe", 0},
                    {"Pristatymas", 0}
                }
            },
            {"Bigbox", new Dictionary<string, int>()
                {
                    {"Aptarnavimas", 0},
                    {"PrekiuKokybe", 0},
                    {"Pristatymas", 0}
                }
            },
            {"Rde", new Dictionary<string, int>()
                {
                    {"Aptarnavimas", 0},
                    {"PrekiuKokybe", 0},
                    {"Pristatymas", 0}
                }
            },
            {"GintarineVaistine", new Dictionary<string, int>()
                {
                    {"Aptarnavimas", 0},
                    {"PrekiuKokybe", 0},
                    {"Pristatymas", 0}
                }
            }
        };

        private void Shop(object sender, SelectionChangedEventArgs e)
        {
            if (ShopComboBox.SelectedIndex == 0)
            {
                FillList(_commentsData.Where(c => c.ShopId == 0).ToList(), ListView);
                ReadRatings("Avitela", ref _votes, ref _votersCount);
                ShopImg.Source = new BitmapImage(new Uri("Assets/avitela.png", UriKind.RelativeOrAbsolute));
                var calc = (double)_votes / (3 * _votersCount);
                RatingAverage.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                ShopComboBox.IsEnabled = false;
            }
            if (ShopComboBox.SelectedIndex == 1)
            {
                FillList(_commentsData.Where(c => c.ShopId == 1).ToList(), ListView);
                ReadRatings("Elektromarkt", ref _votes, ref _votersCount);
                ShopImg.Source = new BitmapImage(new Uri("Assets/elektromarkt.png", UriKind.RelativeOrAbsolute));
                var calc = (double)_votes / (3 * _votersCount);
                RatingAverage.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                ShopComboBox.IsEnabled = false;
            }
            if (ShopComboBox.SelectedIndex == 2)
            {
                FillList(_commentsData.Where(c => c.ShopId == 2).ToList(), ListView);
                ReadRatings("Pigu", ref _votes, ref _votersCount);
                ShopImg.Source = new BitmapImage(new Uri("Assets/avitela.png", UriKind.RelativeOrAbsolute));
                var calc = (double)_votes / (3 * _votersCount);
                RatingAverage.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                ShopComboBox.IsEnabled = false;
            }
            if (ShopComboBox.SelectedIndex == 3)
            {
                FillList(_commentsData.Where(c => c.ShopId == 3).ToList(), ListView);
                ReadRatings("Barbora", ref _votes, ref _votersCount);
                ShopImg.Source = new BitmapImage(new Uri("Assets/avitela.png", UriKind.RelativeOrAbsolute));
                var calc = (double)_votes / (3 * _votersCount);
                RatingAverage.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                ShopComboBox.IsEnabled = false;
            }
            if (ShopComboBox.SelectedIndex == 4)
            {
                FillList(_commentsData.Where(c => c.ShopId == 4).ToList(), ListView);
                ReadRatings("Bigbox", ref _votes, ref _votersCount);
                ShopImg.Source = new BitmapImage(new Uri("Assets/avitela.png", UriKind.RelativeOrAbsolute));
                var calc = (double)_votes / (3 * _votersCount);
                RatingAverage.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                ShopComboBox.IsEnabled = false;
            }
            if (ShopComboBox.SelectedIndex == 5)
            {
                FillList(_commentsData.Where(c => c.ShopId == 5).ToList(), ListView);
                ReadRatings("Rde", ref _votes, ref _votersCount);
                ShopImg.Source = new BitmapImage(new Uri("Assets/avitela.png", UriKind.RelativeOrAbsolute));
                var calc = (double)_votes / (3 * _votersCount);
                RatingAverage.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                ShopComboBox.IsEnabled = false;
            }
            if (ShopComboBox.SelectedIndex == 6)
            {
                FillList(_commentsData.Where(c => c.ShopId == 6).ToList(), ListView);
                ReadRatings("GintarineVaistine", ref _votes, ref _votersCount);
                ShopImg.Source = new BitmapImage(new Uri("Assets/avitela.png", UriKind.RelativeOrAbsolute));
                var calc = (double)_votes / (3 * _votersCount);
                RatingAverage.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                ShopComboBox.IsEnabled = false;
            }
        }

        private void RateClick(object sender, RoutedEventArgs e)
        {
            if (ShopComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Turite pasirinkti parduotuvę.");
            }
            else if (rating["Aptarnavimas"] == 0 || rating["PrekiuKokybe"] == 0 || rating["Pristatymas"] == 0)
            {
                MessageBox.Show("Turite pažymėti vertinimą prie visų kriterijų.");
            }
            else
            {
                if (ShopComboBox.SelectedIndex == 0 && _commentsData.SingleOrDefault(c => c.Email == LoginWindow.Email && c.ShopId == 0) == null)
                {
                    //Rating
                    _votersCount++;
                    _votes += rating["Aptarnavimas"] + rating["PrekiuKokybe"] + rating["Pristatymas"];
                    singlePersonRatings["Avitela"]["Aptarnavimas"] = rating["Aptarnavimas"];
                    singlePersonRatings["Avitela"]["PrekiuKokybe"] = rating["PrekiuKokybe"];
                    singlePersonRatings["Avitela"]["Pristatymas"] = rating["Pristatymas"];
                    var calc = (double)_votes / (3 * _votersCount);
                    WriteRatings("Avitela", _votes, _votersCount);
                    Atstatyti(calc);
                    //Comment
                    WriteComments(LoginWindow.Email, 0, singlePersonRatings["Avitela"]["Aptarnavimas"], singlePersonRatings["Avitela"]["PrekiuKokybe"], singlePersonRatings["Avitela"]["Pristatymas"], CommentBox.Text);
                    CommentBox.Clear();
                    _commentsData = ReadComments();
                    FillList(_commentsData.Where(c => c.ShopId == 0).ToList(), ListView);
                }
                else if (ShopComboBox.SelectedIndex == 1 && _commentsData.SingleOrDefault(c => c.Email == LoginWindow.Email && c.ShopId == 1) == null)
                {
                    //Rating
                    _votersCount++;
                    _votes += rating["Aptarnavimas"] + rating["PrekiuKokybe"] + rating["Pristatymas"];
                    singlePersonRatings["Elektromarkt"]["Aptarnavimas"] = rating["Aptarnavimas"];
                    singlePersonRatings["Elektromarkt"]["PrekiuKokybe"] = rating["PrekiuKokybe"];
                    singlePersonRatings["Elektromarkt"]["Pristatymas"] = rating["Pristatymas"];
                    var calc = (double)_votes / (3 * _votersCount);
                    WriteRatings("Elektromarkt", _votes, _votersCount);
                    Atstatyti(calc);
                    //Comment
                    WriteComments(LoginWindow.Email, 1, singlePersonRatings["Elektromarkt"]["Aptarnavimas"], singlePersonRatings["Elektromarkt"]["PrekiuKokybe"], singlePersonRatings["Elektromarkt"]["Pristatymas"], CommentBox.Text);
                    CommentBox.Clear();
                    _commentsData = ReadComments();
                    FillList(_commentsData.Where(c => c.ShopId == 1).ToList(), ListView);
                }
                else if (ShopComboBox.SelectedIndex == 2 && _commentsData.SingleOrDefault(c => c.Email == LoginWindow.Email && c.ShopId == 2) == null)
                {
                    //Rating
                    _votersCount++;
                    _votes += rating["Aptarnavimas"] + rating["PrekiuKokybe"] + rating["Pristatymas"];
                    singlePersonRatings["Pigu"]["Aptarnavimas"] = rating["Aptarnavimas"];
                    singlePersonRatings["Pigu"]["PrekiuKokybe"] = rating["PrekiuKokybe"];
                    singlePersonRatings["Pigu"]["Pristatymas"] = rating["Pristatymas"];
                    var calc = (double)_votes / (3 * _votersCount);
                    WriteRatings("Pigu", _votes, _votersCount);
                    Atstatyti(calc);
                    //Comment
                    WriteComments(LoginWindow.Email, 2, singlePersonRatings["Pigu"]["Aptarnavimas"], singlePersonRatings["Pigu"]["PrekiuKokybe"], singlePersonRatings["Pigu"]["Pristatymas"], CommentBox.Text);
                    CommentBox.Clear();
                    _commentsData = ReadComments();
                    FillList(_commentsData.Where(c => c.ShopId == 2).ToList(), ListView);
                }
                else if (ShopComboBox.SelectedIndex == 3 && _commentsData.SingleOrDefault(c => c.Email == LoginWindow.Email && c.ShopId == 3) == null)
                {
                    //Rating
                    _votersCount++;
                    _votes += rating["Aptarnavimas"] + rating["PrekiuKokybe"] + rating["Pristatymas"];
                    singlePersonRatings["Barbora"]["Aptarnavimas"] = rating["Aptarnavimas"];
                    singlePersonRatings["Barbora"]["PrekiuKokybe"] = rating["PrekiuKokybe"];
                    singlePersonRatings["Barbora"]["Pristatymas"] = rating["Pristatymas"];
                    var calc = (double)_votes / (3 * _votersCount);
                    WriteRatings("Barbora", _votes, _votersCount);
                    Atstatyti(calc);
                    //Comment
                    WriteComments(LoginWindow.Email, 3, singlePersonRatings["Barbora"]["Aptarnavimas"], singlePersonRatings["Barbora"]["PrekiuKokybe"], singlePersonRatings["Barbora"]["Pristatymas"], CommentBox.Text);
                    CommentBox.Clear();
                    _commentsData = ReadComments();
                    FillList(_commentsData.Where(c => c.ShopId == 3).ToList(), ListView);
                }
                else if (ShopComboBox.SelectedIndex == 4 && _commentsData.SingleOrDefault(c => c.Email == LoginWindow.Email && c.ShopId == 4) == null)
                {
                    //Rating
                    _votersCount++;
                    _votes += rating["Aptarnavimas"] + rating["PrekiuKokybe"] + rating["Pristatymas"];
                    singlePersonRatings["Bigbox"]["Aptarnavimas"] = rating["Aptarnavimas"];
                    singlePersonRatings["Bigbox"]["PrekiuKokybe"] = rating["PrekiuKokybe"];
                    singlePersonRatings["Bigbox"]["Pristatymas"] = rating["Pristatymas"];
                    var calc = (double)_votes / (3 * _votersCount);
                    WriteRatings("Bigbox", _votes, _votersCount);
                    Atstatyti(calc);
                    //Comment
                    WriteComments(LoginWindow.Email, 4, singlePersonRatings["Bigbox"]["Aptarnavimas"], singlePersonRatings["Bigbox"]["PrekiuKokybe"], singlePersonRatings["Bigbox"]["Pristatymas"], CommentBox.Text);
                    CommentBox.Clear();
                    _commentsData = ReadComments();
                    FillList(_commentsData.Where(c => c.ShopId == 4).ToList(), ListView);
                }
                else if (ShopComboBox.SelectedIndex == 5 && _commentsData.SingleOrDefault(c => c.Email == LoginWindow.Email && c.ShopId == 5) == null)
                {
                    //Rating
                    _votersCount++;
                    _votes += rating["Aptarnavimas"] + rating["PrekiuKokybe"] + rating["Pristatymas"];
                    singlePersonRatings["Rde"]["Aptarnavimas"] = rating["Aptarnavimas"];
                    singlePersonRatings["Rde"]["PrekiuKokybe"] = rating["PrekiuKokybe"];
                    singlePersonRatings["Rde"]["Pristatymas"] = rating["Pristatymas"];
                    var calc = (double)_votes / (3 * _votersCount);
                    WriteRatings("Rde", _votes, _votersCount);
                    Atstatyti(calc);
                    //Comment
                    WriteComments(LoginWindow.Email, 5, singlePersonRatings["Rde"]["Aptarnavimas"], singlePersonRatings["Rde"]["PrekiuKokybe"], singlePersonRatings["Rde"]["Pristatymas"], CommentBox.Text);
                    CommentBox.Clear();
                    _commentsData = ReadComments();
                    FillList(_commentsData.Where(c => c.ShopId == 5).ToList(), ListView);
                }
                else if (ShopComboBox.SelectedIndex == 6 && _commentsData.SingleOrDefault(c => c.Email == LoginWindow.Email && c.ShopId == 6) == null)
                {
                    //Rating
                    _votersCount++;
                    _votes += rating["Aptarnavimas"] + rating["PrekiuKokybe"] + rating["Pristatymas"];
                    singlePersonRatings["GintarineVaistine"]["Aptarnavimas"] = rating["Aptarnavimas"];
                    singlePersonRatings["GintarineVaistine"]["PrekiuKokybe"] = rating["PrekiuKokybe"];
                    singlePersonRatings["GintarineVaistine"]["Pristatymas"] = rating["Pristatymas"];
                    var calc = (double)_votes / (3 * _votersCount);
                    WriteRatings("GintarineVaistine", _votes, _votersCount);
                    Atstatyti(calc);
                    //Comment
                    WriteComments(LoginWindow.Email, 6, singlePersonRatings["GintarineVaistine"]["Aptarnavimas"], singlePersonRatings["GintarineVaistine"]["PrekiuKokybe"], singlePersonRatings["GintarineVaistine"]["Pristatymas"], CommentBox.Text);
                    CommentBox.Clear();
                    _commentsData = ReadComments();
                    FillList(_commentsData.Where(c => c.ShopId == 6).ToList(), ListView);
                }
                else
                {
                    MessageBox.Show("Jau esate palikęs atsiliepimą už šią parduotuvę!");
                    ShopComboBox.IsEnabled = true;
                    ShopComboBox.SelectedIndex = -1;
                    CommentBox.Clear();
                }
                ChangeImgSource(Service1, "ServiceImg1", "Assets/Star_0.png");
                ChangeImgSource(Service2, "ServiceImg2", "Assets/Star_0.png");
                ChangeImgSource(Service3, "ServiceImg3", "Assets/Star_0.png");
                ChangeImgSource(Service4, "ServiceImg4", "Assets/Star_0.png");
                ChangeImgSource(Service5, "ServiceImg5", "Assets/Star_0.png");
                ChangeImgSource(Quality1, "QualityImg1", "Assets/Star_0.png");
                ChangeImgSource(Quality2, "QualityImg2", "Assets/Star_0.png");
                ChangeImgSource(Quality3, "QualityImg3", "Assets/Star_0.png");
                ChangeImgSource(Quality4, "QualityImg4", "Assets/Star_0.png");
                ChangeImgSource(Quality5, "QualityImg5", "Assets/Star_0.png");
                ChangeImgSource(Delivery1, "DeliveryImg1", "Assets/Star_0.png");
                ChangeImgSource(Delivery2, "DeliveryImg2", "Assets/Star_0.png");
                ChangeImgSource(Delivery3, "DeliveryImg3", "Assets/Star_0.png");
                ChangeImgSource(Delivery4, "DeliveryImg4", "Assets/Star_0.png");
                ChangeImgSource(Delivery5, "DeliveryImg5", "Assets/Star_0.png");
                rating["Aptarnavimas"] = 0;
                rating["PrekiuKokybe"] = 0;
                rating["Pristatymas"] = 0;
            }
        }

        private static void FillList(List<CommentsTable> ratings, ListView listView)
        {
            listView.Items.Clear();
            foreach (var rating in ratings)
            {
                listView.Items.Add(new Comment()
                {
                    Text = rating.Email + " " + rating.Date + " Apt.: " + rating.ServiceRating + " Pr. Kok.: "
                              + rating.ProductsQualityRating + " Prist.: " + rating.DeliveryRating

                });

                if (!string.IsNullOrWhiteSpace(rating.Comment))
                {
                    listView.Items.Add(new Comment() { Text = rating.Comment });
                }
            }
        }

        private static List<CommentsTable> ReadComments()
        {
            List<CommentsTable> temp;
            using (var context = new DatabaseContext())
            {
                temp = context.CommentsTable.ToList();
            }
            return temp;
        }

        private static void WriteComments(string email, int shopId, int serviceRating, int productsQualityRating, int deliveryRating, string comment)
        {
            using (var context = new DatabaseContext())
            {
                var result = context.CommentsTable.SingleOrDefault(b => b.Email == email && b.ShopId == shopId);

                if (result == null)
                {
                    var commentsTable = new CommentsTable()
                    {
                        Email = email,
                        ShopId = shopId,
                        Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
                        ServiceRating = serviceRating,
                        ProductsQualityRating = productsQualityRating,
                        DeliveryRating = deliveryRating,
                        Comment = comment
                    };
                    context.CommentsTable.Add(commentsTable);
                    context.SaveChanges();
                }
            }
        }

        private static void ReadRatings(string shopName, ref int votesNumber, ref int votersNumber)
        {
            using (var context = new DatabaseContext())
            {
                var result = context.ShopRatingTable.SingleOrDefault(c => c.ShopName == shopName);

                if (result != null)
                {
                    votesNumber = result.VotesNumber;
                    votersNumber = result.VotersNumber;
                }
            }
        }

        private static void WriteRatings(string shopName, int votesNumber, int votersNumber)
        {
            using (var context = new DatabaseContext())
            {
                var result = context.ShopRatingTable.SingleOrDefault(b => b.ShopName == shopName);
                if (result != null)
                {
                    result.VotersNumber = votersNumber;
                    result.VotesNumber = votesNumber;
                    context.SaveChanges();
                }
            }
        }

        private void Atstatyti(double calc)
        {
            RatingAverage.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
            ShopComboBox.IsEnabled = true;
            ShopComboBox.SelectedIndex = -1;
        }

        private void ListViewLoaded(object sender, RoutedEventArgs e)
        {
            _commentsData = ReadComments();
            FillList(_commentsData.Where(c => c.ShopId == 0).ToList(), ListView);
        }

        private void ChangeComments(object sender, SelectionChangedEventArgs e)
        {
            if (CommentComboBox.SelectedIndex == 0)
            {
                FillList(_commentsData.Where(c => c.ShopId == 0).ToList(), ListView);
            }
            else if (CommentComboBox.SelectedIndex == 1)
            {
                FillList(_commentsData.Where(c => c.ShopId == 1).ToList(), ListView);
            }
            else if (CommentComboBox.SelectedIndex == 2)
            {
                FillList(_commentsData.Where(c => c.ShopId == 2).ToList(), ListView);
            }
            else if (CommentComboBox.SelectedIndex == 3)
            {
                FillList(_commentsData.Where(c => c.ShopId == 3).ToList(), ListView);
            }
            else if (CommentComboBox.SelectedIndex == 4)
            {
                FillList(_commentsData.Where(c => c.ShopId == 4).ToList(), ListView);
            }
            else if (CommentComboBox.SelectedIndex == 5)
            {
                FillList(_commentsData.Where(c => c.ShopId == 5).ToList(), ListView);
            }
            else if (CommentComboBox.SelectedIndex == 6)
            {
                FillList(_commentsData.Where(c => c.ShopId == 6).ToList(), ListView);
            }
        }

        private static void ChangeImgSource(Button button, string imgSrc, string imgToChangeSrc)
        {
            var ct = button.Template;
            var btnImage = (Image)ct.FindName(imgSrc, button);
            btnImage.Source = new BitmapImage(new Uri(imgToChangeSrc, UriKind.RelativeOrAbsolute));
        }

        private static bool CheckImgSource(Button button, string imgSrc)
        {
            var imgBool = false;
            const string imgToChangeSrc = "Assets/Star_1.png";

            var ct = button.Template;
            var btnImage = (Image)ct.FindName(imgSrc, button);
            var btnImageSrc = btnImage.Source.ToString();

            btnImageSrc = btnImageSrc.Substring(Math.Max(0, btnImageSrc.Length - imgToChangeSrc.Length));

            if (btnImageSrc.Equals(imgToChangeSrc))
            {
                imgBool = true;
            }
            return imgBool;
        }

        private void ServiceStar1(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Service1, "ServiceImg1", "Assets/Star_1.png");
            ChangeImgSource(Service2, "ServiceImg2", "Assets/Star_0.png");
            ChangeImgSource(Service3, "ServiceImg3", "Assets/Star_0.png");
            ChangeImgSource(Service4, "ServiceImg4", "Assets/Star_0.png");
            ChangeImgSource(Service5, "ServiceImg5", "Assets/Star_0.png");
            rating["Aptarnavimas"] = 1;
        }

        private void ServiceStar2(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Service1, "ServiceImg1", "Assets/Star_1.png");
            ChangeImgSource(Service2, "ServiceImg2", "Assets/Star_1.png");
            ChangeImgSource(Service3, "ServiceImg3", "Assets/Star_0.png");
            ChangeImgSource(Service4, "ServiceImg4", "Assets/Star_0.png");
            ChangeImgSource(Service5, "ServiceImg5", "Assets/Star_0.png");
            rating["Aptarnavimas"] = 2;
        }

        private void ServiceStar3(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Service1, "ServiceImg1", "Assets/Star_1.png");
            ChangeImgSource(Service2, "ServiceImg2", "Assets/Star_1.png");
            ChangeImgSource(Service3, "ServiceImg3", "Assets/Star_1.png");
            ChangeImgSource(Service4, "ServiceImg4", "Assets/Star_0.png");
            ChangeImgSource(Service5, "ServiceImg5", "Assets/Star_0.png");
            rating["Aptarnavimas"] = 3;
        }

        private void ServiceStar4(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Service1, "ServiceImg1", "Assets/Star_1.png");
            ChangeImgSource(Service2, "ServiceImg2", "Assets/Star_1.png");
            ChangeImgSource(Service3, "ServiceImg3", "Assets/Star_1.png");
            ChangeImgSource(Service4, "ServiceImg4", "Assets/Star_1.png");
            ChangeImgSource(Service5, "ServiceImg5", "Assets/Star_0.png");
            rating["Aptarnavimas"] = 4;
        }

        private void ServiceStar5(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Service1, "ServiceImg1", "Assets/Star_1.png");
            ChangeImgSource(Service2, "ServiceImg2", "Assets/Star_1.png");
            ChangeImgSource(Service3, "ServiceImg3", "Assets/Star_1.png");
            ChangeImgSource(Service4, "ServiceImg4", "Assets/Star_1.png");
            ChangeImgSource(Service5, "ServiceImg5", "Assets/Star_1.png");
            rating["Aptarnavimas"] = 5;
        }

        private void QualityStar1(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Quality1, "QualityImg1", "Assets/Star_1.png");
            ChangeImgSource(Quality2, "QualityImg2", "Assets/Star_0.png");
            ChangeImgSource(Quality3, "QualityImg3", "Assets/Star_0.png");
            ChangeImgSource(Quality4, "QualityImg4", "Assets/Star_0.png");
            ChangeImgSource(Quality5, "QualityImg5", "Assets/Star_0.png");
            rating["PrekiuKokybe"] = 1;
        }

        private void QualityStar2(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Quality1, "QualityImg1", "Assets/Star_1.png");
            ChangeImgSource(Quality2, "QualityImg2", "Assets/Star_1.png");
            ChangeImgSource(Quality3, "QualityImg3", "Assets/Star_0.png");
            ChangeImgSource(Quality4, "QualityImg4", "Assets/Star_0.png");
            ChangeImgSource(Quality5, "QualityImg5", "Assets/Star_0.png");
            rating["PrekiuKokybe"] = 2;
        }

        private void QualityStar3(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Quality1, "QualityImg1", "Assets/Star_1.png");
            ChangeImgSource(Quality2, "QualityImg2", "Assets/Star_1.png");
            ChangeImgSource(Quality3, "QualityImg3", "Assets/Star_1.png");
            ChangeImgSource(Quality4, "QualityImg4", "Assets/Star_0.png");
            ChangeImgSource(Quality5, "QualityImg5", "Assets/Star_0.png");
            rating["PrekiuKokybe"] = 3;
        }

        private void QualityStar4(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Quality1, "QualityImg1", "Assets/Star_1.png");
            ChangeImgSource(Quality2, "QualityImg2", "Assets/Star_1.png");
            ChangeImgSource(Quality3, "QualityImg3", "Assets/Star_1.png");
            ChangeImgSource(Quality4, "QualityImg4", "Assets/Star_1.png");
            ChangeImgSource(Quality5, "QualityImg5", "Assets/Star_0.png");
            rating["PrekiuKokybe"] = 4;
        }

        private void QualityStar5(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Quality1, "QualityImg1", "Assets/Star_1.png");
            ChangeImgSource(Quality2, "QualityImg2", "Assets/Star_1.png");
            ChangeImgSource(Quality3, "QualityImg3", "Assets/Star_1.png");
            ChangeImgSource(Quality4, "QualityImg4", "Assets/Star_1.png");
            ChangeImgSource(Quality5, "QualityImg5", "Assets/Star_1.png");
            rating["PrekiuKokybe"] = 5;
        }

        private void DeliveryStar1(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Delivery1, "DeliveryImg1", "Assets/Star_1.png");
            ChangeImgSource(Delivery2, "DeliveryImg2", "Assets/Star_0.png");
            ChangeImgSource(Delivery3, "DeliveryImg3", "Assets/Star_0.png");
            ChangeImgSource(Delivery4, "DeliveryImg4", "Assets/Star_0.png");
            ChangeImgSource(Delivery5, "DeliveryImg5", "Assets/Star_0.png");
            rating["Pristatymas"] = 1;
        }

        private void DeliveryStar2(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Delivery1, "DeliveryImg1", "Assets/Star_1.png");
            ChangeImgSource(Delivery2, "DeliveryImg2", "Assets/Star_1.png");
            ChangeImgSource(Delivery3, "DeliveryImg3", "Assets/Star_0.png");
            ChangeImgSource(Delivery4, "DeliveryImg4", "Assets/Star_0.png");
            ChangeImgSource(Delivery5, "DeliveryImg5", "Assets/Star_0.png");
            rating["Pristatymas"] = 2;
        }

        private void DeliveryStar3(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Delivery1, "DeliveryImg1", "Assets/Star_1.png");
            ChangeImgSource(Delivery2, "DeliveryImg2", "Assets/Star_1.png");
            ChangeImgSource(Delivery3, "DeliveryImg3", "Assets/Star_1.png");
            ChangeImgSource(Delivery4, "DeliveryImg4", "Assets/Star_0.png");
            ChangeImgSource(Delivery5, "DeliveryImg5", "Assets/Star_0.png");
            rating["Pristatymas"] = 3;
        }

        private void DeliveryStar4(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Delivery1, "DeliveryImg1", "Assets/Star_1.png");
            ChangeImgSource(Delivery2, "DeliveryImg2", "Assets/Star_1.png");
            ChangeImgSource(Delivery3, "DeliveryImg3", "Assets/Star_1.png");
            ChangeImgSource(Delivery4, "DeliveryImg4", "Assets/Star_1.png");
            ChangeImgSource(Delivery5, "DeliveryImg5", "Assets/Star_0.png");
            rating["Pristatymas"] = 4;
        }

        private void DeliveryStar5(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Delivery1, "DeliveryImg1", "Assets/Star_1.png");
            ChangeImgSource(Delivery2, "DeliveryImg2", "Assets/Star_1.png");
            ChangeImgSource(Delivery3, "DeliveryImg3", "Assets/Star_1.png");
            ChangeImgSource(Delivery4, "DeliveryImg4", "Assets/Star_1.png");
            ChangeImgSource(Delivery5, "DeliveryImg5", "Assets/Star_1.png");
            rating["Pristatymas"] = 5;
        }

        private void ServiceImg1_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Service1, "ServiceImg1"))
            {
                ChangeImgSource(Service1, "ServiceImg1", "Assets/Star_2.png");
            }
        }

        private void ServiceImg1_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Service1, "ServiceImg1"))
            {
                ChangeImgSource(Service1, "ServiceImg1", "Assets/Star_0.png");
            }
        }

        private void ServiceImg2_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Service2, "ServiceImg2") && !CheckImgSource(Service1, "ServiceImg1"))
            {
                ChangeImgSource(Service1, "ServiceImg1", "Assets/Star_2.png");
                ChangeImgSource(Service2, "ServiceImg2", "Assets/Star_2.png");
            }
        }

        private void ServiceImg2_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Service2, "ServiceImg2") && !CheckImgSource(Service1, "ServiceImg1"))
            {
                ChangeImgSource(Service1, "ServiceImg1", "Assets/Star_0.png");
                ChangeImgSource(Service2, "ServiceImg2", "Assets/Star_0.png");
            }
        }

        private void ServiceImg3_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Service3, "ServiceImg3") && !CheckImgSource(Service2, "ServiceImg2") && !CheckImgSource(Service1, "ServiceImg1"))
            {
                ChangeImgSource(Service1, "ServiceImg1", "Assets/Star_2.png");
                ChangeImgSource(Service2, "ServiceImg2", "Assets/Star_2.png");
                ChangeImgSource(Service3, "ServiceImg3", "Assets/Star_2.png");
            }
        }

        private void ServiceImg3_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Service3, "ServiceImg3") && !CheckImgSource(Service2, "ServiceImg2") && !CheckImgSource(Service1, "ServiceImg1"))
            {
                ChangeImgSource(Service1, "ServiceImg1", "Assets/Star_0.png");
                ChangeImgSource(Service2, "ServiceImg2", "Assets/Star_0.png");
                ChangeImgSource(Service3, "ServiceImg3", "Assets/Star_0.png");
            }
        }

        private void ServiceImg4_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Service4, "ServiceImg4") && !CheckImgSource(Service3, "ServiceImg3") && !CheckImgSource(Service2, "ServiceImg2") && !CheckImgSource(Service1, "ServiceImg1"))
            {
                ChangeImgSource(Service1, "ServiceImg1", "Assets/Star_2.png");
                ChangeImgSource(Service2, "ServiceImg2", "Assets/Star_2.png");
                ChangeImgSource(Service3, "ServiceImg3", "Assets/Star_2.png");
                ChangeImgSource(Service4, "ServiceImg4", "Assets/Star_2.png");
            }
        }

        private void ServiceImg4_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Service4, "ServiceImg4") && !CheckImgSource(Service3, "ServiceImg3") && !CheckImgSource(Service2, "ServiceImg2") && !CheckImgSource(Service1, "ServiceImg1"))
            {
                ChangeImgSource(Service1, "ServiceImg1", "Assets/Star_0.png");
                ChangeImgSource(Service2, "ServiceImg2", "Assets/Star_0.png");
                ChangeImgSource(Service3, "ServiceImg3", "Assets/Star_0.png");
                ChangeImgSource(Service4, "ServiceImg4", "Assets/Star_0.png");
            }
        }

        private void ServiceImg5_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Service5, "ServiceImg5") && !CheckImgSource(Service4, "ServiceImg4") && !CheckImgSource(Service3, "ServiceImg3") && !CheckImgSource(Service2, "ServiceImg2") && !CheckImgSource(Service1, "ServiceImg1"))
            {
                ChangeImgSource(Service1, "ServiceImg1", "Assets/Star_2.png");
                ChangeImgSource(Service2, "ServiceImg2", "Assets/Star_2.png");
                ChangeImgSource(Service3, "ServiceImg3", "Assets/Star_2.png");
                ChangeImgSource(Service4, "ServiceImg4", "Assets/Star_2.png");
                ChangeImgSource(Service5, "ServiceImg5", "Assets/Star_2.png");
            }
        }

        private void ServiceImg5_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Service5, "ServiceImg5") && !CheckImgSource(Service4, "ServiceImg4") && !CheckImgSource(Service3, "ServiceImg3") && !CheckImgSource(Service2, "ServiceImg2") && !CheckImgSource(Service1, "ServiceImg1"))
            {
                ChangeImgSource(Service1, "ServiceImg1", "Assets/Star_0.png");
                ChangeImgSource(Service2, "ServiceImg2", "Assets/Star_0.png");
                ChangeImgSource(Service3, "ServiceImg3", "Assets/Star_0.png");
                ChangeImgSource(Service4, "ServiceImg4", "Assets/Star_0.png");
                ChangeImgSource(Service5, "ServiceImg5", "Assets/Star_0.png");
            }
        }

        private void QualityImg1_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Quality1, "QualityImg1"))
            {
                ChangeImgSource(Quality1, "QualityImg1", "Assets/Star_2.png");
            }
        }

        private void QualityImg1_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Quality1, "QualityImg1"))
            {
                ChangeImgSource(Quality1, "QualityImg1", "Assets/Star_0.png");
            }
        }

        private void QualityImg2_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Quality2, "QualityImg2") && !CheckImgSource(Quality1, "QualityImg1"))
            {
                ChangeImgSource(Quality1, "QualityImg1", "Assets/Star_2.png");
                ChangeImgSource(Quality2, "QualityImg2", "Assets/Star_2.png");
            }
        }

        private void QualityImg2_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Quality2, "QualityImg2") && !CheckImgSource(Quality1, "QualityImg1"))
            {
                ChangeImgSource(Quality1, "QualityImg1", "Assets/Star_0.png");
                ChangeImgSource(Quality2, "QualityImg2", "Assets/Star_0.png");
            }
        }

        private void QualityImg3_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Quality3, "QualityImg3") && !CheckImgSource(Quality2, "QualityImg2") && !CheckImgSource(Quality1, "QualityImg1"))
            {
                ChangeImgSource(Quality1, "QualityImg1", "Assets/Star_2.png");
                ChangeImgSource(Quality2, "QualityImg2", "Assets/Star_2.png");
                ChangeImgSource(Quality3, "QualityImg3", "Assets/Star_2.png");
            }
        }

        private void QualityImg3_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Quality3, "QualityImg3") && !CheckImgSource(Quality2, "QualityImg2") && !CheckImgSource(Quality1, "QualityImg1"))
            {
                ChangeImgSource(Quality1, "QualityImg1", "Assets/Star_0.png");
                ChangeImgSource(Quality2, "QualityImg2", "Assets/Star_0.png");
                ChangeImgSource(Quality3, "QualityImg3", "Assets/Star_0.png");
            }
        }

        private void QualityImg4_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Quality4, "QualityImg4") && !CheckImgSource(Quality3, "QualityImg3") && !CheckImgSource(Quality2, "QualityImg2") && !CheckImgSource(Quality1, "QualityImg1"))
            {
                ChangeImgSource(Quality1, "QualityImg1", "Assets/Star_2.png");
                ChangeImgSource(Quality2, "QualityImg2", "Assets/Star_2.png");
                ChangeImgSource(Quality3, "QualityImg3", "Assets/Star_2.png");
                ChangeImgSource(Quality4, "QualityImg4", "Assets/Star_2.png");
            }
        }

        private void QualityImg4_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Quality4, "QualityImg4") && !CheckImgSource(Quality3, "QualityImg3") && !CheckImgSource(Quality2, "QualityImg2") && !CheckImgSource(Quality1, "QualityImg1"))
            {
                ChangeImgSource(Quality1, "QualityImg1", "Assets/Star_0.png");
                ChangeImgSource(Quality2, "QualityImg2", "Assets/Star_0.png");
                ChangeImgSource(Quality3, "QualityImg3", "Assets/Star_0.png");
                ChangeImgSource(Quality4, "QualityImg4", "Assets/Star_0.png");
            }
        }

        private void QualityImg5_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Quality5, "QualityImg5") && !CheckImgSource(Quality4, "QualityImg4") && !CheckImgSource(Quality3, "QualityImg3") && !CheckImgSource(Quality2, "QualityImg2") && !CheckImgSource(Quality1, "QualityImg1"))
            {
                ChangeImgSource(Quality1, "QualityImg1", "Assets/Star_2.png");
                ChangeImgSource(Quality2, "QualityImg2", "Assets/Star_2.png");
                ChangeImgSource(Quality3, "QualityImg3", "Assets/Star_2.png");
                ChangeImgSource(Quality4, "QualityImg4", "Assets/Star_2.png");
                ChangeImgSource(Quality5, "QualityImg5", "Assets/Star_2.png");
            }
        }

        private void QualityImg5_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Quality5, "QualityImg5") && !CheckImgSource(Quality4, "QualityImg4") && !CheckImgSource(Quality3, "QualityImg3") && !CheckImgSource(Quality2, "QualityImg2") && !CheckImgSource(Quality1, "QualityImg1"))
            {
                ChangeImgSource(Quality1, "QualityImg1", "Assets/Star_0.png");
                ChangeImgSource(Quality2, "QualityImg2", "Assets/Star_0.png");
                ChangeImgSource(Quality3, "QualityImg3", "Assets/Star_0.png");
                ChangeImgSource(Quality4, "QualityImg4", "Assets/Star_0.png");
                ChangeImgSource(Quality5, "QualityImg5", "Assets/Star_0.png");
            }
        }

        private void DeliveryImg1_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Delivery1, "DeliveryImg1"))
            {
                ChangeImgSource(Delivery1, "DeliveryImg1", "Assets/Star_2.png");
            }
        }

        private void DeliveryImg1_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Delivery1, "DeliveryImg1"))
            {
                ChangeImgSource(Delivery1, "DeliveryImg1", "Assets/Star_0.png");
            }
        }

        private void DeliveryImg2_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Delivery2, "DeliveryImg2") && !CheckImgSource(Delivery1, "DeliveryImg1"))
            {
                ChangeImgSource(Delivery1, "DeliveryImg1", "Assets/Star_2.png");
                ChangeImgSource(Delivery2, "DeliveryImg2", "Assets/Star_2.png");
            }
        }

        private void DeliveryImg2_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Delivery2, "DeliveryImg2") && !CheckImgSource(Delivery1, "DeliveryImg1"))
            {
                ChangeImgSource(Delivery1, "DeliveryImg1", "Assets/Star_0.png");
                ChangeImgSource(Delivery2, "DeliveryImg2", "Assets/Star_0.png");
            }
        }

        private void DeliveryImg3_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Delivery3, "DeliveryImg3") && !CheckImgSource(Delivery2, "DeliveryImg2") && !CheckImgSource(Delivery1, "DeliveryImg1"))
            {
                ChangeImgSource(Delivery1, "DeliveryImg1", "Assets/Star_2.png");
                ChangeImgSource(Delivery2, "DeliveryImg2", "Assets/Star_2.png");
                ChangeImgSource(Delivery3, "DeliveryImg3", "Assets/Star_2.png");
            }
        }

        private void DeliveryImg3_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Delivery3, "DeliveryImg3") && !CheckImgSource(Delivery2, "DeliveryImg2") && !CheckImgSource(Delivery1, "DeliveryImg1"))
            {
                ChangeImgSource(Delivery1, "DeliveryImg1", "Assets/Star_0.png");
                ChangeImgSource(Delivery2, "DeliveryImg2", "Assets/Star_0.png");
                ChangeImgSource(Delivery3, "DeliveryImg3", "Assets/Star_0.png");
            }
        }

        private void DeliveryImg4_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Delivery4, "DeliveryImg4") && !CheckImgSource(Delivery3, "DeliveryImg3") && !CheckImgSource(Delivery2, "DeliveryImg2") && !CheckImgSource(Delivery1, "DeliveryImg1"))
            {
                ChangeImgSource(Delivery1, "DeliveryImg1", "Assets/Star_2.png");
                ChangeImgSource(Delivery2, "DeliveryImg2", "Assets/Star_2.png");
                ChangeImgSource(Delivery3, "DeliveryImg3", "Assets/Star_2.png");
                ChangeImgSource(Delivery4, "DeliveryImg4", "Assets/Star_2.png");
            }
        }

        private void DeliveryImg4_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Delivery4, "DeliveryImg4") && !CheckImgSource(Delivery3, "DeliveryImg3") && !CheckImgSource(Delivery2, "DeliveryImg2") && !CheckImgSource(Delivery1, "DeliveryImg1"))
            {
                ChangeImgSource(Delivery1, "DeliveryImg1", "Assets/Star_0.png");
                ChangeImgSource(Delivery2, "DeliveryImg2", "Assets/Star_0.png");
                ChangeImgSource(Delivery3, "DeliveryImg3", "Assets/Star_0.png");
                ChangeImgSource(Delivery4, "DeliveryImg4", "Assets/Star_0.png");
            }
        }

        private void DeliveryImg5_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Delivery5, "DeliveryImg5") && !CheckImgSource(Delivery4, "DeliveryImg4") && !CheckImgSource(Delivery3, "DeliveryImg3") && !CheckImgSource(Delivery2, "DeliveryImg2") && !CheckImgSource(Delivery1, "DeliveryImg1"))
            {
                ChangeImgSource(Delivery1, "DeliveryImg1", "Assets/Star_2.png");
                ChangeImgSource(Delivery2, "DeliveryImg2", "Assets/Star_2.png");
                ChangeImgSource(Delivery3, "DeliveryImg3", "Assets/Star_2.png");
                ChangeImgSource(Delivery4, "DeliveryImg4", "Assets/Star_2.png");
                ChangeImgSource(Delivery5, "DeliveryImg5", "Assets/Star_2.png");
            }
        }

        private void DeliveryImg5_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Delivery5, "DeliveryImg5") && !CheckImgSource(Delivery4, "DeliveryImg4") && !CheckImgSource(Delivery3, "DeliveryImg3") && !CheckImgSource(Delivery2, "DeliveryImg2") && !CheckImgSource(Delivery1, "DeliveryImg1"))
            {
                ChangeImgSource(Delivery1, "DeliveryImg1", "Assets/Star_0.png");
                ChangeImgSource(Delivery2, "DeliveryImg2", "Assets/Star_0.png");
                ChangeImgSource(Delivery3, "DeliveryImg3", "Assets/Star_0.png");
                ChangeImgSource(Delivery4, "DeliveryImg4", "Assets/Star_0.png");
                ChangeImgSource(Delivery5, "DeliveryImg5", "Assets/Star_0.png");
            }
        }
    }
}