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
    /// Interaction logic for VertinimoLangas.xaml
    /// </summary>
    public partial class EvaluationWindow : Window
    {
        public EvaluationWindow()
        {
            InitializeComponent();
        }

        class Comment
        {
            public string Text { get; set; }
        }

        private static int votes = 0;
        private static int votesCount = 0;

        private static List<CommentsTable> commentsData = new List<CommentsTable>();


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

        private void Seller(object sender, SelectionChangedEventArgs e)
        {
            if(seller.SelectedIndex == 0)
            {
                FillList(commentsData.Where(c => c.ShopId == 0).ToList(), listView);
                Read("Avitela", ref votes, ref votesCount);
                sellerImg.Source = new BitmapImage(new Uri("Nuotraukos/avitela.png", UriKind.RelativeOrAbsolute));
                var calc = (double)votes / (3 * votesCount);
                evaluationBlock.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                seller.IsEnabled = false;
            }
            if (seller.SelectedIndex == 1)
            {
                FillList(commentsData.Where(c => c.ShopId == 1).ToList(), listView);
                Read("Elektromarkt", ref votes, ref votesCount);
                sellerImg.Source = new BitmapImage(new Uri("Nuotraukos/elektromarkt.png", UriKind.RelativeOrAbsolute));
                var calc = (double)votes / (3 * votesCount);
                evaluationBlock.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                seller.IsEnabled = false;
            }
            if (seller.SelectedIndex == 2)
            {
                FillList(commentsData.Where(c => c.ShopId == 2).ToList(), listView);
                Read("Pigu", ref votes, ref votesCount);
                sellerImg.Source = new BitmapImage(new Uri("Nuotraukos/avitela.png", UriKind.RelativeOrAbsolute));
                var calc = (double)votes / (3 * votesCount);
                evaluationBlock.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                seller.IsEnabled = false;
            }
            if (seller.SelectedIndex == 3)
            {
                FillList(commentsData.Where(c => c.ShopId == 3).ToList(), listView);
                Read("Barbora", ref votes, ref votesCount);
                sellerImg.Source = new BitmapImage(new Uri("Nuotraukos/avitela.png", UriKind.RelativeOrAbsolute));
                var calc = (double)votes / (3 * votesCount);
                evaluationBlock.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                seller.IsEnabled = false;
            }
            if (seller.SelectedIndex == 4)
            {
                FillList(commentsData.Where(c => c.ShopId == 4).ToList(), listView);
                Read("Bigbox", ref votes, ref votesCount);
                sellerImg.Source = new BitmapImage(new Uri("Nuotraukos/avitela.png", UriKind.RelativeOrAbsolute));
                var calc = (double)votes / (3 * votesCount);
                evaluationBlock.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                seller.IsEnabled = false;
            }
            if (seller.SelectedIndex == 5)
            {
                FillList(commentsData.Where(c => c.ShopId == 5).ToList(), listView);
                Read("Rde", ref votes, ref votesCount);
                sellerImg.Source = new BitmapImage(new Uri("Nuotraukos/avitela.png", UriKind.RelativeOrAbsolute));
                var calc = (double)votes / (3 * votesCount);
                evaluationBlock.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                seller.IsEnabled = false;
            }
            if (seller.SelectedIndex == 6)
            {
                FillList(commentsData.Where(c => c.ShopId == 6).ToList(), listView);
                Read("GintarineVaistine", ref votes, ref votesCount);
                sellerImg.Source = new BitmapImage(new Uri("Nuotraukos/avitela.png", UriKind.RelativeOrAbsolute));
                var calc = (double)votes / (3 * votesCount);
                evaluationBlock.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                seller.IsEnabled = false;
            }
        }

        private void Evaluate(object sender, RoutedEventArgs e)
        {
            if (seller.SelectedIndex == -1)
            {
                MessageBox.Show("Turite pasirinkti parduotuvę.");
            }
            else if (rating["Aptarnavimas"] == 0 || rating["PrekiuKokybe"] == 0 || rating["Pristatymas"] == 0)
            {
                MessageBox.Show("Turite pažymėti vertinimą prie visų kriterijų.");
            }
            else
            {
                if (seller.SelectedIndex == 0 && commentsData.SingleOrDefault(c => c.Email == LoginWindow.email && c.ShopId == 0) == null)
                {
                    //Rating
                    votesCount++;
                    votes += rating["Aptarnavimas"] + rating["PrekiuKokybe"] + rating["Pristatymas"];
                    singlePersonRatings["Avitela"]["Aptarnavimas"] = rating["Aptarnavimas"];
                    singlePersonRatings["Avitela"]["PrekiuKokybe"] = rating["PrekiuKokybe"];
                    singlePersonRatings["Avitela"]["Pristatymas"] = rating["Pristatymas"];
                    var calc = (double)votes / (3 * votesCount);
                    Write("Avitela", votes, votesCount);
                    Reset(calc);
                    //Comment
                    WriteComments(LoginWindow.email, 0, singlePersonRatings["Avitela"]["Aptarnavimas"], singlePersonRatings["Avitela"]["PrekiuKokybe"], singlePersonRatings["Avitela"]["Pristatymas"], commentBox.Text);
                    commentBox.Clear();
                    commentsData = ReadComments();
                    FillList(commentsData.Where(c => c.ShopId == 0).ToList(), listView);
                }
                else if (seller.SelectedIndex == 1 && commentsData.SingleOrDefault(c => c.Email == LoginWindow.email && c.ShopId == 1) == null)
                {
                    //Rating
                    votesCount++;
                    votes += rating["Aptarnavimas"] + rating["PrekiuKokybe"] + rating["Pristatymas"];
                    singlePersonRatings["Elektromarkt"]["Aptarnavimas"] = rating["Aptarnavimas"];
                    singlePersonRatings["Elektromarkt"]["PrekiuKokybe"] = rating["PrekiuKokybe"];
                    singlePersonRatings["Elektromarkt"]["Pristatymas"] = rating["Pristatymas"];
                    var calc = (double)votes / (3 * votesCount);
                    Write("Elektromarkt", votes, votesCount);
                    Reset(calc);
                    //Comment
                    WriteComments(LoginWindow.email, 1, singlePersonRatings["Elektromarkt"]["Aptarnavimas"], singlePersonRatings["Elektromarkt"]["PrekiuKokybe"], singlePersonRatings["Elektromarkt"]["Pristatymas"], commentBox.Text);
                    commentBox.Clear();
                    commentsData = ReadComments();
                    FillList(commentsData.Where(c => c.ShopId == 1).ToList(), listView);
                }
                else if (seller.SelectedIndex == 2 && commentsData.SingleOrDefault(c => c.Email == LoginWindow.email && c.ShopId == 2) == null)
                {
                    //Rating
                    votesCount++;
                    votes += rating["Aptarnavimas"] + rating["PrekiuKokybe"] + rating["Pristatymas"];
                    singlePersonRatings["Pigu"]["Aptarnavimas"] = rating["Aptarnavimas"];
                    singlePersonRatings["Pigu"]["PrekiuKokybe"] = rating["PrekiuKokybe"];
                    singlePersonRatings["Pigu"]["Pristatymas"] = rating["Pristatymas"];
                    var calc = (double)votes / (3 * votesCount);
                    Write("Pigu", votes, votesCount);
                    Reset(calc);
                    //Comment
                    WriteComments(LoginWindow.email, 2, singlePersonRatings["Pigu"]["Aptarnavimas"], singlePersonRatings["Pigu"]["PrekiuKokybe"], singlePersonRatings["Pigu"]["Pristatymas"], commentBox.Text);
                    commentBox.Clear();
                    commentsData = ReadComments();
                    FillList(commentsData.Where(c => c.ShopId == 2).ToList(), listView);
                }
                else if (seller.SelectedIndex == 3 && commentsData.SingleOrDefault(c => c.Email == LoginWindow.email && c.ShopId == 3) == null)
                {
                    //Rating
                    votesCount++;
                    votes += rating["Aptarnavimas"] + rating["PrekiuKokybe"] + rating["Pristatymas"];
                    singlePersonRatings["Barbora"]["Aptarnavimas"] = rating["Aptarnavimas"];
                    singlePersonRatings["Barbora"]["PrekiuKokybe"] = rating["PrekiuKokybe"];
                    singlePersonRatings["Barbora"]["Pristatymas"] = rating["Pristatymas"];
                    var calc = (double)votes / (3 * votesCount);
                    Write("Barbora", votes, votesCount);
                    Reset(calc);
                    //Comment
                    WriteComments(LoginWindow.email, 3, singlePersonRatings["Barbora"]["Aptarnavimas"], singlePersonRatings["Barbora"]["PrekiuKokybe"], singlePersonRatings["Barbora"]["Pristatymas"], commentBox.Text);
                    commentBox.Clear();
                    commentsData = ReadComments();
                    FillList(commentsData.Where(c => c.ShopId == 3).ToList(), listView);
                }
                else if (seller.SelectedIndex == 4 && commentsData.SingleOrDefault(c => c.Email == LoginWindow.email && c.ShopId == 4) == null)
                {
                    //Rating
                    votesCount++;
                    votes += rating["Aptarnavimas"] + rating["PrekiuKokybe"] + rating["Pristatymas"];
                    singlePersonRatings["Bigbox"]["Aptarnavimas"] = rating["Aptarnavimas"];
                    singlePersonRatings["Bigbox"]["PrekiuKokybe"] = rating["PrekiuKokybe"];
                    singlePersonRatings["Bigbox"]["Pristatymas"] = rating["Pristatymas"];
                    var calc = (double)votes / (3 * votesCount);
                    Write("Bigbox", votes, votesCount);
                    Reset(calc);
                    //Comment
                    WriteComments(LoginWindow.email, 4, singlePersonRatings["Bigbox"]["Aptarnavimas"], singlePersonRatings["Bigbox"]["PrekiuKokybe"], singlePersonRatings["Bigbox"]["Pristatymas"], commentBox.Text);
                    commentBox.Clear();
                    commentsData = ReadComments();
                    FillList(commentsData.Where(c => c.ShopId == 4).ToList(), listView);
                }
                else if (seller.SelectedIndex == 5 && commentsData.SingleOrDefault(c => c.Email == LoginWindow.email && c.ShopId == 5) == null)
                {
                    //Rating
                    votesCount++;
                    votes += rating["Aptarnavimas"] + rating["PrekiuKokybe"] + rating["Pristatymas"];
                    singlePersonRatings["Rde"]["Aptarnavimas"] = rating["Aptarnavimas"];
                    singlePersonRatings["Rde"]["PrekiuKokybe"] = rating["PrekiuKokybe"];
                    singlePersonRatings["Rde"]["Pristatymas"] = rating["Pristatymas"];
                    var calc = (double)votes / (3 * votesCount);
                    Write("Rde", votes, votesCount);
                    Reset(calc);
                    //Comment
                    WriteComments(LoginWindow.email, 5, singlePersonRatings["Rde"]["Aptarnavimas"], singlePersonRatings["Rde"]["PrekiuKokybe"], singlePersonRatings["Rde"]["Pristatymas"], commentBox.Text);
                    commentBox.Clear();
                    commentsData = ReadComments();
                    FillList(commentsData.Where(c => c.ShopId == 5).ToList(), listView);
                }
                else if (seller.SelectedIndex == 6 && commentsData.SingleOrDefault(c => c.Email == LoginWindow.email && c.ShopId == 6) == null)
                {
                    //Rating
                    votesCount++;
                    votes += rating["Aptarnavimas"] + rating["PrekiuKokybe"] + rating["Pristatymas"];
                    singlePersonRatings["GintarineVaistine"]["Aptarnavimas"] = rating["Aptarnavimas"];
                    singlePersonRatings["GintarineVaistine"]["PrekiuKokybe"] = rating["PrekiuKokybe"];
                    singlePersonRatings["GintarineVaistine"]["Pristatymas"] = rating["Pristatymas"];
                    var calc = (double)votes / (3 * votesCount);
                    Write("GintarineVaistine", votes, votesCount);
                    Reset(calc);
                    //Comment
                    WriteComments(LoginWindow.email, 6, singlePersonRatings["GintarineVaistine"]["Aptarnavimas"], singlePersonRatings["GintarineVaistine"]["PrekiuKokybe"], singlePersonRatings["GintarineVaistine"]["Pristatymas"], commentBox.Text);
                    commentBox.Clear();
                    commentsData = ReadComments();
                    FillList(commentsData.Where(c => c.ShopId == 6).ToList(), listView);
                }
                else
                {
                    MessageBox.Show("Jau esate palikęs atsiliepimą už šią parduotuvę!");
                    seller.IsEnabled = true;
                    seller.SelectedIndex = -1;
                    commentBox.Clear();
                }
                ChangeImgSource(service1, "serviceImg1", "Nuotraukos/Star_0.png");
                ChangeImgSource(service2, "serviceImg2", "Nuotraukos/Star_0.png");
                ChangeImgSource(service3, "serviceImg3", "Nuotraukos/Star_0.png");
                ChangeImgSource(service4, "serviceImg4", "Nuotraukos/Star_0.png");
                ChangeImgSource(service5, "serviceImg5", "Nuotraukos/Star_0.png");
                ChangeImgSource(quality1, "qualityImg1", "Nuotraukos/Star_0.png");
                ChangeImgSource(quality2, "qualityImg2", "Nuotraukos/Star_0.png");
                ChangeImgSource(quality3, "qualityImg3", "Nuotraukos/Star_0.png");
                ChangeImgSource(quality4, "qualityImg4", "Nuotraukos/Star_0.png");
                ChangeImgSource(quality5, "qualityImg5", "Nuotraukos/Star_0.png");
                ChangeImgSource(delivery1, "deliveryImg1", "Nuotraukos/Star_0.png");
                ChangeImgSource(delivery2, "deliveryImg2", "Nuotraukos/Star_0.png");
                ChangeImgSource(delivery3, "deliveryImg3", "Nuotraukos/Star_0.png");
                ChangeImgSource(delivery4, "deliveryImg4", "Nuotraukos/Star_0.png");
                ChangeImgSource(delivery5, "deliveryImg5", "Nuotraukos/Star_0.png");
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
                    listView.Items.Add(new Comment() {Text = rating.Comment});
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

        //Funkcija parasyta su ref, tai jei nori grazinti values, rasyti - Skaityti(pavadinimas, ref balsuSuma, ref balsavusiuSkaicius);
        private static void Read(string shopName, ref int votesNumber, ref int votersNumber)
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

        private static void Write(string shopName, int votesNumber, int votersNumber)
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

        private void Reset(double calc)
        {
            evaluationBlock.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
            seller.IsEnabled = true;
            seller.SelectedIndex = -1;
        }

        private void LoadListview(object sender, RoutedEventArgs e)
        {
            commentsData = ReadComments();
            FillList(commentsData.Where(c => c.ShopId == 0).ToList(), listView);
        }

        private void ChangeComments(object sender, SelectionChangedEventArgs e)
        {
            if(chooseCommentBox.SelectedIndex == 0)
            {
                FillList(commentsData.Where(c => c.ShopId == 0).ToList(), listView);
            }
            else if(chooseCommentBox.SelectedIndex == 1)
            {
                FillList(commentsData.Where(c => c.ShopId == 1).ToList(), listView);
            }
            else if(chooseCommentBox.SelectedIndex == 2)
            {
                FillList(commentsData.Where(c => c.ShopId == 2).ToList(), listView);
            }
            else if (chooseCommentBox.SelectedIndex == 3)
            {
                FillList(commentsData.Where(c => c.ShopId == 3).ToList(), listView);
            }
            else if (chooseCommentBox.SelectedIndex == 4)
            {
                FillList(commentsData.Where(c => c.ShopId == 4).ToList(), listView);
            }
            else if (chooseCommentBox.SelectedIndex == 5)
            {
                FillList(commentsData.Where(c => c.ShopId == 5).ToList(), listView);
            }
            else if (chooseCommentBox.SelectedIndex == 6)
            {
                FillList(commentsData.Where(c => c.ShopId == 6).ToList(), listView);
            }
        }

        private void ChangeImgSource(Button button, string imgSrc, string imgToChangeSrc)
        {
            var ct = button.Template;
            var btnImage = (Image)ct.FindName(imgSrc, button);
            btnImage.Source = new BitmapImage(new Uri(imgToChangeSrc, UriKind.RelativeOrAbsolute));
        }

        private bool CheckImgSource(Button button, string imgSrc)
        {
            var imgBool = false;
            var imgToChangeSrc = "Nuotraukos/Star_1.png";

            var ct = button.Template;
            var btnImage = (Image)ct.FindName(imgSrc, button);
            var btnImageSrc = btnImage.Source.ToString();

            btnImageSrc =  btnImageSrc.Substring(Math.Max(0, btnImageSrc.Length-imgToChangeSrc.Length));

            if (btnImageSrc.Equals(imgToChangeSrc))
            {
                imgBool = true;
            }
            return imgBool;
        }

        private void ServiceStar1(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(service1, "serviceImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(service2, "serviceImg2", "Nuotraukos/Star_0.png");
            ChangeImgSource(service3, "serviceImg3", "Nuotraukos/Star_0.png");
            ChangeImgSource(service4, "serviceImg4", "Nuotraukos/Star_0.png");
            ChangeImgSource(service5, "serviceImg5", "Nuotraukos/Star_0.png");
            rating["Aptarnavimas"] = 1;
        }

        private void ServiceStar2(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(service1, "serviceImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(service2, "serviceImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(service3, "serviceImg3", "Nuotraukos/Star_0.png");
            ChangeImgSource(service4, "serviceImg4", "Nuotraukos/Star_0.png");
            ChangeImgSource(service5, "serviceImg5", "Nuotraukos/Star_0.png");
            rating["Aptarnavimas"] = 2;
        }

        private void ServiceStar3(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(service1, "serviceImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(service2, "serviceImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(service3, "serviceImg3", "Nuotraukos/Star_1.png");
            ChangeImgSource(service4, "serviceImg4", "Nuotraukos/Star_0.png");
            ChangeImgSource(service5, "serviceImg5", "Nuotraukos/Star_0.png");
            rating["Aptarnavimas"] = 3;
        }

        private void ServiceStar4(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(service1, "serviceImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(service2, "serviceImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(service3, "serviceImg3", "Nuotraukos/Star_1.png");
            ChangeImgSource(service4, "serviceImg4", "Nuotraukos/Star_1.png");
            ChangeImgSource(service5, "serviceImg5", "Nuotraukos/Star_0.png");
            rating["Aptarnavimas"] = 4;
        }

        private void ServiceStar5(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(service1, "serviceImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(service2, "serviceImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(service3, "serviceImg3", "Nuotraukos/Star_1.png");
            ChangeImgSource(service4, "serviceImg4", "Nuotraukos/Star_1.png");
            ChangeImgSource(service5, "serviceImg5", "Nuotraukos/Star_1.png");
            rating["Aptarnavimas"] = 5;
        }

        private void QualityStar1(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(quality1, "qualityImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(quality2, "qualityImg2", "Nuotraukos/Star_0.png");
            ChangeImgSource(quality3, "qualityImg3", "Nuotraukos/Star_0.png");
            ChangeImgSource(quality4, "qualityImg4", "Nuotraukos/Star_0.png");
            ChangeImgSource(quality5, "qualityImg5", "Nuotraukos/Star_0.png");
            rating["PrekiuKokybe"] = 1;
        }

        private void QualityStar2(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(quality1, "qualityImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(quality2, "qualityImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(quality3, "qualityImg3", "Nuotraukos/Star_0.png");
            ChangeImgSource(quality4, "qualityImg4", "Nuotraukos/Star_0.png");
            ChangeImgSource(quality5, "qualityImg5", "Nuotraukos/Star_0.png");
            rating["PrekiuKokybe"] = 2;
        }

        private void QualityStar3(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(quality1, "qualityImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(quality2, "qualityImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(quality3, "qualityImg3", "Nuotraukos/Star_1.png");
            ChangeImgSource(quality4, "qualityImg4", "Nuotraukos/Star_0.png");
            ChangeImgSource(quality5, "qualityImg5", "Nuotraukos/Star_0.png");
            rating["PrekiuKokybe"] = 3;
        }

        private void QualityStar4(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(quality1, "qualityImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(quality2, "qualityImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(quality3, "qualityImg3", "Nuotraukos/Star_1.png");
            ChangeImgSource(quality4, "qualityImg4", "Nuotraukos/Star_1.png");
            ChangeImgSource(quality5, "qualityImg5", "Nuotraukos/Star_0.png");
            rating["PrekiuKokybe"] = 4;
        }

        private void QualityStar5(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(quality1, "qualityImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(quality2, "qualityImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(quality3, "qualityImg3", "Nuotraukos/Star_1.png");
            ChangeImgSource(quality4, "qualityImg4", "Nuotraukos/Star_1.png");
            ChangeImgSource(quality5, "qualityImg5", "Nuotraukos/Star_1.png");
            rating["PrekiuKokybe"] = 5;
        }

        private void DeliveryStar1(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(delivery1, "deliveryImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(delivery2, "deliveryImg2", "Nuotraukos/Star_0.png");
            ChangeImgSource(delivery3, "deliveryImg3", "Nuotraukos/Star_0.png");
            ChangeImgSource(delivery4, "deliveryImg4", "Nuotraukos/Star_0.png");
            ChangeImgSource(delivery5, "deliveryImg5", "Nuotraukos/Star_0.png");
            rating["Pristatymas"] = 1;
        }

        private void DeliveryStar2(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(delivery1, "deliveryImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(delivery2, "deliveryImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(delivery3, "deliveryImg3", "Nuotraukos/Star_0.png");
            ChangeImgSource(delivery4, "deliveryImg4", "Nuotraukos/Star_0.png");
            ChangeImgSource(delivery5, "deliveryImg5", "Nuotraukos/Star_0.png");
            rating["Pristatymas"] = 2;
        }

        private void DeliveryStar3(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(delivery1, "deliveryImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(delivery2, "deliveryImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(delivery3, "deliveryImg3", "Nuotraukos/Star_1.png");
            ChangeImgSource(delivery4, "deliveryImg4", "Nuotraukos/Star_0.png");
            ChangeImgSource(delivery5, "deliveryImg5", "Nuotraukos/Star_0.png");
            rating["Pristatymas"] = 3;
        }

        private void DeliveryStar4(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(delivery1, "deliveryImg", "Nuotraukos/Star_1.png");
            ChangeImgSource(delivery2, "deliveryImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(delivery3, "deliveryImg3", "Nuotraukos/Star_1.png");
            ChangeImgSource(delivery4, "deliveryImg4", "Nuotraukos/Star_1.png");
            ChangeImgSource(delivery5, "deliveryImg5", "Nuotraukos/Star_0.png");
            rating["Pristatymas"] = 4;
        }

        private void DeliveryStar5(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(delivery1, "deliveryImg1", "Nuotraukos/Star_1.png");
            ChangeImgSource(delivery2, "deliveryImg2", "Nuotraukos/Star_1.png");
            ChangeImgSource(delivery3, "deliveryImg3", "Nuotraukos/Star_1.png");
            ChangeImgSource(delivery4, "deliveryImg4", "Nuotraukos/Star_1.png");
            ChangeImgSource(delivery5, "deliveryImg5", "Nuotraukos/Star_1.png");
            rating["Pristatymas"] = 5;
        }

        private void ServiceImg1_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(service1, "serviceImg1"))
            {
                ChangeImgSource(service1, "serviceImg1", "Nuotraukos/Star_2.png");
            }
        }

        private void ServiceImg1_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(service1, "serviceImg1"))
            {
                ChangeImgSource(service1, "serviceImg1", "Nuotraukos/Star_0.png");
            }
        }

        private void ServiceImg2_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(service2, "serviceImg2") && !CheckImgSource(service1, "servicesImg1"))
            {
                ChangeImgSource(service1, "serviceImg1", "Nuotraukos/Star_2.png");
                ChangeImgSource(service2, "serviceImg2", "Nuotraukos/Star_2.png");
            }
        }

        private void ServiceImg2_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(service2, "serviceImg2") && !CheckImgSource(service1, "serviceImg1"))
            {
                ChangeImgSource(service1, "serviceImg1", "Nuotraukos/Star_0.png");
                ChangeImgSource(service2, "serviceImg2", "Nuotraukos/Star_0.png");
            }
        }

        private void ServiceImg3_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(service3, "serviceImg3") && !CheckImgSource(service2, "serviceImg2") && !CheckImgSource(service1, "serviceImg1"))
            {
                ChangeImgSource(service1, "serviceImg1", "Nuotraukos/Star_2.png");
                ChangeImgSource(service2, "serviceImg2", "Nuotraukos/Star_2.png");
                ChangeImgSource(service3, "serviceImg3", "Nuotraukos/Star_2.png");
            }
        }

        private void ServiceImg3_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(service3, "serviceImg3") && !CheckImgSource(service2, "serviceImg2") && !CheckImgSource(service1, "serviceImg1"))
            {
                ChangeImgSource(service1, "serviceImg1", "Nuotraukos/Star_0.png");
                ChangeImgSource(service2, "serviceImg2", "Nuotraukos/Star_0.png");
                ChangeImgSource(service3, "serviceImg3", "Nuotraukos/Star_0.png");
            }
        }

        private void ServiceImg4_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(service4, "serviceImg4") && !CheckImgSource(service3, "serviceImg3") && !CheckImgSource(service2, "serviceImg2") && !CheckImgSource(service1, "serviceImg1"))
            {
                ChangeImgSource(service1, "serviceImg1", "Nuotraukos/Star_2.png");
                ChangeImgSource(service2, "serviceImg2", "Nuotraukos/Star_2.png");
                ChangeImgSource(service3, "serviceImg3", "Nuotraukos/Star_2.png");
                ChangeImgSource(service4, "serviceImg4", "Nuotraukos/Star_2.png");
            }
        }

        private void ServiceImg4_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(service4, "serviceImg4") && !CheckImgSource(service3, "serviceImg3") && !CheckImgSource(service2, "serviceImg2") && !CheckImgSource(service1, "serviceImg1"))
            {
                ChangeImgSource(service1, "serviceImg1", "Nuotraukos/Star_0.png");
                ChangeImgSource(service2, "serviceImg2", "Nuotraukos/Star_0.png");
                ChangeImgSource(service3, "serviceImg3", "Nuotraukos/Star_0.png");
                ChangeImgSource(service4, "serviceImg4", "Nuotraukos/Star_0.png");
            }
        }

        private void ServiceImg5_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(service5, "serviceImg5") && !CheckImgSource(service4, "serviceImg4") && !CheckImgSource(service3, "serviceImg3") && !CheckImgSource(service2, "serviceImg2") && !CheckImgSource(service1, "serviceImg1"))
            {
                ChangeImgSource(service1, "serviceImg1", "Nuotraukos/Star_2.png");
                ChangeImgSource(service2, "serviceImg2", "Nuotraukos/Star_2.png");
                ChangeImgSource(service3, "serviceImg3", "Nuotraukos/Star_2.png");
                ChangeImgSource(service4, "serviceImg4", "Nuotraukos/Star_2.png");
                ChangeImgSource(service5, "serviceImg5", "Nuotraukos/Star_2.png");
            }
        }

        private void ServiceImg5_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(service5, "serviceImg5") && !CheckImgSource(service4, "serviceImg4") && !CheckImgSource(service3, "serviceImg3") && !CheckImgSource(service2, "serviceImg2") && !CheckImgSource(service1, "serviceImg1"))
            {
                ChangeImgSource(service1, "serviceImg1", "Nuotraukos/Star_0.png");
                ChangeImgSource(service2, "serviceImg2", "Nuotraukos/Star_0.png");
                ChangeImgSource(service3, "serviceImg3", "Nuotraukos/Star_0.png");
                ChangeImgSource(service4, "serviceImg4", "Nuotraukos/Star_0.png");
                ChangeImgSource(service5, "serviceImg5", "Nuotraukos/Star_0.png");
            }
        }

        private void QualityImg1_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(quality1, "qualityImg1"))
            {
                ChangeImgSource(quality1, "qualityImg1", "Nuotraukos/Star_2.png");
            }
        }

        private void QualityImg1_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(quality1, "qualityImg1"))
            {
                ChangeImgSource(quality1, "qualityImg1", "Nuotraukos/Star_0.png");
            }
        }

        private void QualityImg2_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(quality2, "qualityImg2") && !CheckImgSource(quality1, "qualityImg1"))
            {
                ChangeImgSource(quality1, "qualityImg1", "Nuotraukos/Star_2.png");
                ChangeImgSource(quality2, "qualityImg2", "Nuotraukos/Star_2.png");
            }
        }

        private void QualityImg2_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(quality2, "qualityImg2") && !CheckImgSource(quality1, "qualityImg1"))
            {
                ChangeImgSource(quality1, "qualityImg1", "Nuotraukos/Star_0.png");
                ChangeImgSource(quality2, "qualityImg2", "Nuotraukos/Star_0.png");
            }
        }

        private void QualityImg3_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(quality3, "qualityImg3") && !CheckImgSource(quality2, "qualityImg2") && !CheckImgSource(quality1, "qualityImg1"))
            {
                ChangeImgSource(quality1, "qualityImg1", "Nuotraukos/Star_2.png");
                ChangeImgSource(quality2, "qualityImg2", "Nuotraukos/Star_2.png");
                ChangeImgSource(quality3, "qualityImg3", "Nuotraukos/Star_2.png");
            }
        }

        private void QualityImg3_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(quality3, "qualityImg3") && !CheckImgSource(quality2, "qualityImg2") && !CheckImgSource(quality1, "qualityImg1"))
            {
                ChangeImgSource(quality1, "qualityImg1", "Nuotraukos/Star_0.png");
                ChangeImgSource(quality2, "qualityImg2", "Nuotraukos/Star_0.png");
                ChangeImgSource(quality3, "qualityImg3", "Nuotraukos/Star_0.png");
            }
        }

        private void QualityImg4_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(quality4, "qualityImg4") && !CheckImgSource(quality3, "qualityImg3") && !CheckImgSource(quality2, "qualityImg2") && !CheckImgSource(quality1, "qualityImg1"))
            {
                ChangeImgSource(quality1, "qualityImg1", "Nuotraukos/Star_2.png");
                ChangeImgSource(quality2, "qualityImg2", "Nuotraukos/Star_2.png");
                ChangeImgSource(quality3, "qualityImg3", "Nuotraukos/Star_2.png");
                ChangeImgSource(quality4, "qualityImg4", "Nuotraukos/Star_2.png");
            }
        }

        private void QualityImg4_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(quality4, "qualityImg4") && !CheckImgSource(quality3, "qualityImg3") && !CheckImgSource(quality2, "qualityImg2") && !CheckImgSource(quality1, "qualityImg1"))
            {
                ChangeImgSource(quality1, "qualityImg1", "Nuotraukos/Star_0.png");
                ChangeImgSource(quality2, "qualityImg2", "Nuotraukos/Star_0.png");
                ChangeImgSource(quality3, "qualityImg3", "Nuotraukos/Star_0.png");
                ChangeImgSource(quality4, "qualityImg4", "Nuotraukos/Star_0.png");
            }
        }

        private void QualityImg5_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(quality5, "qualityImg5") && !CheckImgSource(quality4, "qualityImg4") && !CheckImgSource(quality3, "qualityImg3") && !CheckImgSource(quality2, "qualityImg2") && !CheckImgSource(quality1, "qualityImg1"))
            {
                ChangeImgSource(quality1, "qualityImg1", "Nuotraukos/Star_2.png");
                ChangeImgSource(quality2, "qualityImg2", "Nuotraukos/Star_2.png");
                ChangeImgSource(quality3, "qualityImg3", "Nuotraukos/Star_2.png");
                ChangeImgSource(quality4, "qualityImg4", "Nuotraukos/Star_2.png");
                ChangeImgSource(quality5, "qualityImg5", "Nuotraukos/Star_2.png");
            }
        }

        private void QualityImg5_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(quality5, "qualityImg5") && !CheckImgSource(quality4, "qualityImg4") && !CheckImgSource(quality3, "qualityImg3") && !CheckImgSource(quality2, "qualityImg2") && !CheckImgSource(quality1, "qualityImg1"))
            {
                ChangeImgSource(quality1, "qualityImg1", "Nuotraukos/Star_0.png");
                ChangeImgSource(quality2, "qualityImg2", "Nuotraukos/Star_0.png");
                ChangeImgSource(quality3, "qualityImg3", "Nuotraukos/Star_0.png");
                ChangeImgSource(quality4, "qualityImg4", "Nuotraukos/Star_0.png");
                ChangeImgSource(quality5, "qualityImg5", "Nuotraukos/Star_0.png");
            }
        }

        private void DeliveryImg1_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(delivery1, "deliveryImg1"))
            {
                ChangeImgSource(delivery1, "deliveryImg1", "Nuotraukos/Star_2.png");
            }
        }

        private void DeliveryImg1_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(delivery1, "deliveryImg1"))
            {
                ChangeImgSource(delivery1, "deliveryImg1", "Nuotraukos/Star_0.png");
            }
        }
        
        private void DeliveryImg2_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(delivery2, "deliveryImg2") && !CheckImgSource(delivery1, "deliveryImg1"))
            {
                ChangeImgSource(delivery1, "deliveryImg1", "Nuotraukos/Star_2.png");
                ChangeImgSource(delivery2, "deliveryImg2", "Nuotraukos/Star_2.png");
            }
        }

        private void DeliveryImg2_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(delivery2, "deliveryImg2") && !CheckImgSource(delivery1, "deliveryImg1"))
            {
                ChangeImgSource(delivery1, "deliveryImg1", "Nuotraukos/Star_0.png");
                ChangeImgSource(delivery2, "deliveryImg2", "Nuotraukos/Star_0.png");
            }
        }

        private void DeliveryImg3_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(delivery3, "deliveryImg3") && !CheckImgSource(delivery2, "deliveryImg2") && !CheckImgSource(delivery1, "deliveryImg1"))
            {
                ChangeImgSource(delivery1, "deliveryImg1", "Nuotraukos/Star_2.png");
                ChangeImgSource(delivery2, "deliveryImg2", "Nuotraukos/Star_2.png");
                ChangeImgSource(delivery3, "deliveryImg3", "Nuotraukos/Star_2.png");
            }
        }

        private void DeliveryImg3_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(delivery3, "deliveryImg3") && !CheckImgSource(delivery2, "deliveryImg2") && !CheckImgSource(delivery1, "deliveryImg1"))
            {
                ChangeImgSource(delivery1, "deliveryImg1", "Nuotraukos/Star_0.png");
                ChangeImgSource(delivery2, "deliveryImg2", "Nuotraukos/Star_0.png");
                ChangeImgSource(delivery3, "deliveryImg3", "Nuotraukos/Star_0.png");
            }
        }

        private void DeliveryImg4_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(delivery4, "deliveryImg4") && !CheckImgSource(delivery3, "deliveryImg3") && !CheckImgSource(delivery2, "deliveryImg2") && !CheckImgSource(delivery1, "deliveryImg1"))
            {
                ChangeImgSource(delivery1, "deliveryImg1", "Nuotraukos/Star_2.png");
                ChangeImgSource(delivery2, "deliveryImg2", "Nuotraukos/Star_2.png");
                ChangeImgSource(delivery3, "deliveryImg3", "Nuotraukos/Star_2.png");
                ChangeImgSource(delivery4, "deliveryImg4", "Nuotraukos/Star_2.png");
            }
        }

        private void DeliveryImg4_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(delivery4, "deliveryImg4") && !CheckImgSource(delivery3, "deliveryImg3") && !CheckImgSource(delivery2, "deliveryImg2") && !CheckImgSource(delivery1, "deliveryImg1"))
            {
                ChangeImgSource(delivery1, "deliveryImg1", "Nuotraukos/Star_0.png");
                ChangeImgSource(delivery2, "deliveryImg2", "Nuotraukos/Star_0.png");
                ChangeImgSource(delivery3, "deliveryImg3", "Nuotraukos/Star_0.png");
                ChangeImgSource(delivery4, "deliveryImg4", "Nuotraukos/Star_0.png");
            }
        }

        private void DeliveryImg5_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(delivery5, "deliveryImg5") && !CheckImgSource(delivery4, "deliveryImg4") && !CheckImgSource(delivery3, "deliveryImg3") && !CheckImgSource(delivery2, "deliveryImg2") && !CheckImgSource(delivery1, "deliveryImg1"))
            {
                ChangeImgSource(delivery1, "deliveryImg1", "Nuotraukos/Star_2.png");
                ChangeImgSource(delivery2, "deliveryImg2", "Nuotraukos/Star_2.png");
                ChangeImgSource(delivery3, "deliveryImg3", "Nuotraukos/Star_2.png");
                ChangeImgSource(delivery4, "deliveryImg4", "Nuotraukos/Star_2.png");
                ChangeImgSource(delivery5, "deliveryImg5", "Nuotraukos/Star_2.png");
            }
        }

        private void DeliveryImg5_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(delivery5, "deliveryImg5") && !CheckImgSource(delivery4, "deliveryImg4") && !CheckImgSource(delivery3, "deliveryImg3") && !CheckImgSource(delivery2, "deliveryImg2") && !CheckImgSource(delivery1, "deliveryImg1"))
            {
                ChangeImgSource(delivery1, "deliveryImg1", "Nuotraukos/Star_0.png");
                ChangeImgSource(delivery2, "deliveryImg2", "Nuotraukos/Star_0.png");
                ChangeImgSource(delivery3, "deliveryImg3", "Nuotraukos/Star_0.png");
                ChangeImgSource(delivery4, "deliveryImg4", "Nuotraukos/Star_0.png");
                ChangeImgSource(delivery5, "deliveryImg5", "Nuotraukos/Star_0.png");
            }
        }
    }
}