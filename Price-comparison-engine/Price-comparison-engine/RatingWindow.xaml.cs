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

        private static int votes = 0;
        private static int votersCount = 0;

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

        private void Shop(object sender, SelectionChangedEventArgs e)
        {
            if (parduotuve.SelectedIndex == 0)
            {
                FillList(commentsData.Where(c => c.ShopId == 0).ToList(), listViewas);
                ReadRatings("Avitela", ref votes, ref votersCount);
                ShopImg.Source = new BitmapImage(new Uri("Assets/avitela.png", UriKind.RelativeOrAbsolute));
                var calc = (double)votes / (3 * votersCount);
                ivertinimas.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                parduotuve.IsEnabled = false;
            }
            if (parduotuve.SelectedIndex == 1)
            {
                FillList(commentsData.Where(c => c.ShopId == 1).ToList(), listViewas);
                ReadRatings("Elektromarkt", ref votes, ref votersCount);
                ShopImg.Source = new BitmapImage(new Uri("Assets/elektromarkt.png", UriKind.RelativeOrAbsolute));
                var calc = (double)votes / (3 * votersCount);
                ivertinimas.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                parduotuve.IsEnabled = false;
            }
            if (parduotuve.SelectedIndex == 2)
            {
                FillList(commentsData.Where(c => c.ShopId == 2).ToList(), listViewas);
                ReadRatings("Pigu", ref votes, ref votersCount);
                ShopImg.Source = new BitmapImage(new Uri("Assets/avitela.png", UriKind.RelativeOrAbsolute));
                var calc = (double)votes / (3 * votersCount);
                ivertinimas.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                parduotuve.IsEnabled = false;
            }
            if (parduotuve.SelectedIndex == 3)
            {
                FillList(commentsData.Where(c => c.ShopId == 3).ToList(), listViewas);
                ReadRatings("Barbora", ref votes, ref votersCount);
                ShopImg.Source = new BitmapImage(new Uri("Assets/avitela.png", UriKind.RelativeOrAbsolute));
                var calc = (double)votes / (3 * votersCount);
                ivertinimas.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                parduotuve.IsEnabled = false;
            }
            if (parduotuve.SelectedIndex == 4)
            {
                FillList(commentsData.Where(c => c.ShopId == 4).ToList(), listViewas);
                ReadRatings("Bigbox", ref votes, ref votersCount);
                ShopImg.Source = new BitmapImage(new Uri("Assets/avitela.png", UriKind.RelativeOrAbsolute));
                var calc = (double)votes / (3 * votersCount);
                ivertinimas.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                parduotuve.IsEnabled = false;
            }
            if (parduotuve.SelectedIndex == 5)
            {
                FillList(commentsData.Where(c => c.ShopId == 5).ToList(), listViewas);
                ReadRatings("Rde", ref votes, ref votersCount);
                ShopImg.Source = new BitmapImage(new Uri("Assets/avitela.png", UriKind.RelativeOrAbsolute));
                var calc = (double)votes / (3 * votersCount);
                ivertinimas.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                parduotuve.IsEnabled = false;
            }
            if (parduotuve.SelectedIndex == 6)
            {
                FillList(commentsData.Where(c => c.ShopId == 6).ToList(), listViewas);
                ReadRatings("GintarineVaistine", ref votes, ref votersCount);
                ShopImg.Source = new BitmapImage(new Uri("Assets/avitela.png", UriKind.RelativeOrAbsolute));
                var calc = (double)votes / (3 * votersCount);
                ivertinimas.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
                parduotuve.IsEnabled = false;
            }
        }

        private void RateClick(object sender, RoutedEventArgs e)
        {
            if (parduotuve.SelectedIndex == -1)
            {
                MessageBox.Show("Turite pasirinkti parduotuvę.");
            }
            else if (rating["Aptarnavimas"] == 0 || rating["PrekiuKokybe"] == 0 || rating["Pristatymas"] == 0)
            {
                MessageBox.Show("Turite pažymėti vertinimą prie visų kriterijų.");
            }
            else
            {
                if (parduotuve.SelectedIndex == 0 && commentsData.SingleOrDefault(c => c.Email == LoginWindow.Email && c.ShopId == 0) == null)
                {
                    //Rating
                    votersCount++;
                    votes += rating["Aptarnavimas"] + rating["PrekiuKokybe"] + rating["Pristatymas"];
                    singlePersonRatings["Avitela"]["Aptarnavimas"] = rating["Aptarnavimas"];
                    singlePersonRatings["Avitela"]["PrekiuKokybe"] = rating["PrekiuKokybe"];
                    singlePersonRatings["Avitela"]["Pristatymas"] = rating["Pristatymas"];
                    var calc = (double)votes / (3 * votersCount);
                    WriteRatings("Avitela", votes, votersCount);
                    Atstatyti(calc);
                    //Comment
                    WriteComments(LoginWindow.Email, 0, singlePersonRatings["Avitela"]["Aptarnavimas"], singlePersonRatings["Avitela"]["PrekiuKokybe"], singlePersonRatings["Avitela"]["Pristatymas"], KomentaruLangelis.Text);
                    KomentaruLangelis.Clear();
                    commentsData = ReadComments();
                    FillList(commentsData.Where(c => c.ShopId == 0).ToList(), listViewas);
                }
                else if (parduotuve.SelectedIndex == 1 && commentsData.SingleOrDefault(c => c.Email == LoginWindow.Email && c.ShopId == 1) == null)
                {
                    //Rating
                    votersCount++;
                    votes += rating["Aptarnavimas"] + rating["PrekiuKokybe"] + rating["Pristatymas"];
                    singlePersonRatings["Elektromarkt"]["Aptarnavimas"] = rating["Aptarnavimas"];
                    singlePersonRatings["Elektromarkt"]["PrekiuKokybe"] = rating["PrekiuKokybe"];
                    singlePersonRatings["Elektromarkt"]["Pristatymas"] = rating["Pristatymas"];
                    var calc = (double)votes / (3 * votersCount);
                    WriteRatings("Elektromarkt", votes, votersCount);
                    Atstatyti(calc);
                    //Comment
                    WriteComments(LoginWindow.Email, 1, singlePersonRatings["Elektromarkt"]["Aptarnavimas"], singlePersonRatings["Elektromarkt"]["PrekiuKokybe"], singlePersonRatings["Elektromarkt"]["Pristatymas"], KomentaruLangelis.Text);
                    KomentaruLangelis.Clear();
                    commentsData = ReadComments();
                    FillList(commentsData.Where(c => c.ShopId == 1).ToList(), listViewas);
                }
                else if (parduotuve.SelectedIndex == 2 && commentsData.SingleOrDefault(c => c.Email == LoginWindow.Email && c.ShopId == 2) == null)
                {
                    //Rating
                    votersCount++;
                    votes += rating["Aptarnavimas"] + rating["PrekiuKokybe"] + rating["Pristatymas"];
                    singlePersonRatings["Pigu"]["Aptarnavimas"] = rating["Aptarnavimas"];
                    singlePersonRatings["Pigu"]["PrekiuKokybe"] = rating["PrekiuKokybe"];
                    singlePersonRatings["Pigu"]["Pristatymas"] = rating["Pristatymas"];
                    var calc = (double)votes / (3 * votersCount);
                    WriteRatings("Pigu", votes, votersCount);
                    Atstatyti(calc);
                    //Comment
                    WriteComments(LoginWindow.Email, 2, singlePersonRatings["Pigu"]["Aptarnavimas"], singlePersonRatings["Pigu"]["PrekiuKokybe"], singlePersonRatings["Pigu"]["Pristatymas"], KomentaruLangelis.Text);
                    KomentaruLangelis.Clear();
                    commentsData = ReadComments();
                    FillList(commentsData.Where(c => c.ShopId == 2).ToList(), listViewas);
                }
                else if (parduotuve.SelectedIndex == 3 && commentsData.SingleOrDefault(c => c.Email == LoginWindow.Email && c.ShopId == 3) == null)
                {
                    //Rating
                    votersCount++;
                    votes += rating["Aptarnavimas"] + rating["PrekiuKokybe"] + rating["Pristatymas"];
                    singlePersonRatings["Barbora"]["Aptarnavimas"] = rating["Aptarnavimas"];
                    singlePersonRatings["Barbora"]["PrekiuKokybe"] = rating["PrekiuKokybe"];
                    singlePersonRatings["Barbora"]["Pristatymas"] = rating["Pristatymas"];
                    var calc = (double)votes / (3 * votersCount);
                    WriteRatings("Barbora", votes, votersCount);
                    Atstatyti(calc);
                    //Comment
                    WriteComments(LoginWindow.Email, 3, singlePersonRatings["Barbora"]["Aptarnavimas"], singlePersonRatings["Barbora"]["PrekiuKokybe"], singlePersonRatings["Barbora"]["Pristatymas"], KomentaruLangelis.Text);
                    KomentaruLangelis.Clear();
                    commentsData = ReadComments();
                    FillList(commentsData.Where(c => c.ShopId == 3).ToList(), listViewas);
                }
                else if (parduotuve.SelectedIndex == 4 && commentsData.SingleOrDefault(c => c.Email == LoginWindow.Email && c.ShopId == 4) == null)
                {
                    //Rating
                    votersCount++;
                    votes += rating["Aptarnavimas"] + rating["PrekiuKokybe"] + rating["Pristatymas"];
                    singlePersonRatings["Bigbox"]["Aptarnavimas"] = rating["Aptarnavimas"];
                    singlePersonRatings["Bigbox"]["PrekiuKokybe"] = rating["PrekiuKokybe"];
                    singlePersonRatings["Bigbox"]["Pristatymas"] = rating["Pristatymas"];
                    var calc = (double)votes / (3 * votersCount);
                    WriteRatings("Bigbox", votes, votersCount);
                    Atstatyti(calc);
                    //Comment
                    WriteComments(LoginWindow.Email, 4, singlePersonRatings["Bigbox"]["Aptarnavimas"], singlePersonRatings["Bigbox"]["PrekiuKokybe"], singlePersonRatings["Bigbox"]["Pristatymas"], KomentaruLangelis.Text);
                    KomentaruLangelis.Clear();
                    commentsData = ReadComments();
                    FillList(commentsData.Where(c => c.ShopId == 4).ToList(), listViewas);
                }
                else if (parduotuve.SelectedIndex == 5 && commentsData.SingleOrDefault(c => c.Email == LoginWindow.Email && c.ShopId == 5) == null)
                {
                    //Rating
                    votersCount++;
                    votes += rating["Aptarnavimas"] + rating["PrekiuKokybe"] + rating["Pristatymas"];
                    singlePersonRatings["Rde"]["Aptarnavimas"] = rating["Aptarnavimas"];
                    singlePersonRatings["Rde"]["PrekiuKokybe"] = rating["PrekiuKokybe"];
                    singlePersonRatings["Rde"]["Pristatymas"] = rating["Pristatymas"];
                    var calc = (double)votes / (3 * votersCount);
                    WriteRatings("Rde", votes, votersCount);
                    Atstatyti(calc);
                    //Comment
                    WriteComments(LoginWindow.Email, 5, singlePersonRatings["Rde"]["Aptarnavimas"], singlePersonRatings["Rde"]["PrekiuKokybe"], singlePersonRatings["Rde"]["Pristatymas"], KomentaruLangelis.Text);
                    KomentaruLangelis.Clear();
                    commentsData = ReadComments();
                    FillList(commentsData.Where(c => c.ShopId == 5).ToList(), listViewas);
                }
                else if (parduotuve.SelectedIndex == 6 && commentsData.SingleOrDefault(c => c.Email == LoginWindow.Email && c.ShopId == 6) == null)
                {
                    //Rating
                    votersCount++;
                    votes += rating["Aptarnavimas"] + rating["PrekiuKokybe"] + rating["Pristatymas"];
                    singlePersonRatings["GintarineVaistine"]["Aptarnavimas"] = rating["Aptarnavimas"];
                    singlePersonRatings["GintarineVaistine"]["PrekiuKokybe"] = rating["PrekiuKokybe"];
                    singlePersonRatings["GintarineVaistine"]["Pristatymas"] = rating["Pristatymas"];
                    var calc = (double)votes / (3 * votersCount);
                    WriteRatings("GintarineVaistine", votes, votersCount);
                    Atstatyti(calc);
                    //Comment
                    WriteComments(LoginWindow.Email, 6, singlePersonRatings["GintarineVaistine"]["Aptarnavimas"], singlePersonRatings["GintarineVaistine"]["PrekiuKokybe"], singlePersonRatings["GintarineVaistine"]["Pristatymas"], KomentaruLangelis.Text);
                    KomentaruLangelis.Clear();
                    commentsData = ReadComments();
                    FillList(commentsData.Where(c => c.ShopId == 6).ToList(), listViewas);
                }
                else
                {
                    MessageBox.Show("Jau esate palikęs atsiliepimą už šią parduotuvę!");
                    parduotuve.IsEnabled = true;
                    parduotuve.SelectedIndex = -1;
                    KomentaruLangelis.Clear();
                }
                ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Assets/Star_0.png");
                ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Assets/Star_0.png");
                ChangeImgSource(Aptarnavimas3, "AptarnavimasImg3", "Assets/Star_0.png");
                ChangeImgSource(Aptarnavimas4, "AptarnavimasImg4", "Assets/Star_0.png");
                ChangeImgSource(Aptarnavimas5, "AptarnavimasImg5", "Assets/Star_0.png");
                ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Assets/Star_0.png");
                ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Assets/Star_0.png");
                ChangeImgSource(PrekiuKokybe3, "PrekiuKokybeImg3", "Assets/Star_0.png");
                ChangeImgSource(PrekiuKokybe4, "PrekiuKokybeImg4", "Assets/Star_0.png");
                ChangeImgSource(PrekiuKokybe5, "PrekiuKokybeImg5", "Assets/Star_0.png");
                ChangeImgSource(Pristatymas1, "PristatymasImg1", "Assets/Star_0.png");
                ChangeImgSource(Pristatymas2, "PristatymasImg2", "Assets/Star_0.png");
                ChangeImgSource(Pristatymas3, "PristatymasImg3", "Assets/Star_0.png");
                ChangeImgSource(Pristatymas4, "PristatymasImg4", "Assets/Star_0.png");
                ChangeImgSource(Pristatymas5, "PristatymasImg5", "Assets/Star_0.png");
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
            ivertinimas.Text = "Įvertinimas: " + calc.ToString("0.00") + "/5";
            parduotuve.IsEnabled = true;
            parduotuve.SelectedIndex = -1;
        }

        private void listViewas_Loaded(object sender, RoutedEventArgs e)
        {
            commentsData = ReadComments();
            FillList(commentsData.Where(c => c.ShopId == 0).ToList(), listViewas);
        }

        private void ChangeComments(object sender, SelectionChangedEventArgs e)
        {
            if (PasirinktiKomentara.SelectedIndex == 0)
            {
                FillList(commentsData.Where(c => c.ShopId == 0).ToList(), listViewas);
            }
            else if (PasirinktiKomentara.SelectedIndex == 1)
            {
                FillList(commentsData.Where(c => c.ShopId == 1).ToList(), listViewas);
            }
            else if (PasirinktiKomentara.SelectedIndex == 2)
            {
                FillList(commentsData.Where(c => c.ShopId == 2).ToList(), listViewas);
            }
            else if (PasirinktiKomentara.SelectedIndex == 3)
            {
                FillList(commentsData.Where(c => c.ShopId == 3).ToList(), listViewas);
            }
            else if (PasirinktiKomentara.SelectedIndex == 4)
            {
                FillList(commentsData.Where(c => c.ShopId == 4).ToList(), listViewas);
            }
            else if (PasirinktiKomentara.SelectedIndex == 5)
            {
                FillList(commentsData.Where(c => c.ShopId == 5).ToList(), listViewas);
            }
            else if (PasirinktiKomentara.SelectedIndex == 6)
            {
                FillList(commentsData.Where(c => c.ShopId == 6).ToList(), listViewas);
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
            var imgToChangeSrc = "Assets/Star_1.png";

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

        private void AptarnavimasStar1(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Assets/Star_1.png");
            ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Assets/Star_0.png");
            ChangeImgSource(Aptarnavimas3, "AptarnavimasImg3", "Assets/Star_0.png");
            ChangeImgSource(Aptarnavimas4, "AptarnavimasImg4", "Assets/Star_0.png");
            ChangeImgSource(Aptarnavimas5, "AptarnavimasImg5", "Assets/Star_0.png");
            rating["Aptarnavimas"] = 1;
        }

        private void AptarnavimasStar2(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Assets/Star_1.png");
            ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Assets/Star_1.png");
            ChangeImgSource(Aptarnavimas3, "AptarnavimasImg3", "Assets/Star_0.png");
            ChangeImgSource(Aptarnavimas4, "AptarnavimasImg4", "Assets/Star_0.png");
            ChangeImgSource(Aptarnavimas5, "AptarnavimasImg5", "Assets/Star_0.png");
            rating["Aptarnavimas"] = 2;
        }

        private void AptarnavimasStar3(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Assets/Star_1.png");
            ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Assets/Star_1.png");
            ChangeImgSource(Aptarnavimas3, "AptarnavimasImg3", "Assets/Star_1.png");
            ChangeImgSource(Aptarnavimas4, "AptarnavimasImg4", "Assets/Star_0.png");
            ChangeImgSource(Aptarnavimas5, "AptarnavimasImg5", "Assets/Star_0.png");
            rating["Aptarnavimas"] = 3;
        }

        private void AptarnavimasStar4(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Assets/Star_1.png");
            ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Assets/Star_1.png");
            ChangeImgSource(Aptarnavimas3, "AptarnavimasImg3", "Assets/Star_1.png");
            ChangeImgSource(Aptarnavimas4, "AptarnavimasImg4", "Assets/Star_1.png");
            ChangeImgSource(Aptarnavimas5, "AptarnavimasImg5", "Assets/Star_0.png");
            rating["Aptarnavimas"] = 4;
        }

        private void AptarnavimasStar5(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Assets/Star_1.png");
            ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Assets/Star_1.png");
            ChangeImgSource(Aptarnavimas3, "AptarnavimasImg3", "Assets/Star_1.png");
            ChangeImgSource(Aptarnavimas4, "AptarnavimasImg4", "Assets/Star_1.png");
            ChangeImgSource(Aptarnavimas5, "AptarnavimasImg5", "Assets/Star_1.png");
            rating["Aptarnavimas"] = 5;
        }

        private void PrekiuKokybeStar1(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Assets/Star_1.png");
            ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Assets/Star_0.png");
            ChangeImgSource(PrekiuKokybe3, "PrekiuKokybeImg3", "Assets/Star_0.png");
            ChangeImgSource(PrekiuKokybe4, "PrekiuKokybeImg4", "Assets/Star_0.png");
            ChangeImgSource(PrekiuKokybe5, "PrekiuKokybeImg5", "Assets/Star_0.png");
            rating["PrekiuKokybe"] = 1;
        }

        private void PrekiuKokybeStar2(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Assets/Star_1.png");
            ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Assets/Star_1.png");
            ChangeImgSource(PrekiuKokybe3, "PrekiuKokybeImg3", "Assets/Star_0.png");
            ChangeImgSource(PrekiuKokybe4, "PrekiuKokybeImg4", "Assets/Star_0.png");
            ChangeImgSource(PrekiuKokybe5, "PrekiuKokybeImg5", "Assets/Star_0.png");
            rating["PrekiuKokybe"] = 2;
        }

        private void PrekiuKokybeStar3(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Assets/Star_1.png");
            ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Assets/Star_1.png");
            ChangeImgSource(PrekiuKokybe3, "PrekiuKokybeImg3", "Assets/Star_1.png");
            ChangeImgSource(PrekiuKokybe4, "PrekiuKokybeImg4", "Assets/Star_0.png");
            ChangeImgSource(PrekiuKokybe5, "PrekiuKokybeImg5", "Assets/Star_0.png");
            rating["PrekiuKokybe"] = 3;
        }

        private void PrekiuKokybeStar4(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Assets/Star_1.png");
            ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Assets/Star_1.png");
            ChangeImgSource(PrekiuKokybe3, "PrekiuKokybeImg3", "Assets/Star_1.png");
            ChangeImgSource(PrekiuKokybe4, "PrekiuKokybeImg4", "Assets/Star_1.png");
            ChangeImgSource(PrekiuKokybe5, "PrekiuKokybeImg5", "Assets/Star_0.png");
            rating["PrekiuKokybe"] = 4;
        }

        private void PrekiuKokybeStar5(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Assets/Star_1.png");
            ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Assets/Star_1.png");
            ChangeImgSource(PrekiuKokybe3, "PrekiuKokybeImg3", "Assets/Star_1.png");
            ChangeImgSource(PrekiuKokybe4, "PrekiuKokybeImg4", "Assets/Star_1.png");
            ChangeImgSource(PrekiuKokybe5, "PrekiuKokybeImg5", "Assets/Star_1.png");
            rating["PrekiuKokybe"] = 5;
        }

        private void PristatymasStar1(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Pristatymas1, "PristatymasImg1", "Assets/Star_1.png");
            ChangeImgSource(Pristatymas2, "PristatymasImg2", "Assets/Star_0.png");
            ChangeImgSource(Pristatymas3, "PristatymasImg3", "Assets/Star_0.png");
            ChangeImgSource(Pristatymas4, "PristatymasImg4", "Assets/Star_0.png");
            ChangeImgSource(Pristatymas5, "PristatymasImg5", "Assets/Star_0.png");
            rating["Pristatymas"] = 1;
        }

        private void PristatymasStar2(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Pristatymas1, "PristatymasImg1", "Assets/Star_1.png");
            ChangeImgSource(Pristatymas2, "PristatymasImg2", "Assets/Star_1.png");
            ChangeImgSource(Pristatymas3, "PristatymasImg3", "Assets/Star_0.png");
            ChangeImgSource(Pristatymas4, "PristatymasImg4", "Assets/Star_0.png");
            ChangeImgSource(Pristatymas5, "PristatymasImg5", "Assets/Star_0.png");
            rating["Pristatymas"] = 2;
        }

        private void PristatymasStar3(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Pristatymas1, "PristatymasImg1", "Assets/Star_1.png");
            ChangeImgSource(Pristatymas2, "PristatymasImg2", "Assets/Star_1.png");
            ChangeImgSource(Pristatymas3, "PristatymasImg3", "Assets/Star_1.png");
            ChangeImgSource(Pristatymas4, "PristatymasImg4", "Assets/Star_0.png");
            ChangeImgSource(Pristatymas5, "PristatymasImg5", "Assets/Star_0.png");
            rating["Pristatymas"] = 3;
        }

        private void PristatymasStar4(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Pristatymas1, "PristatymasImg1", "Assets/Star_1.png");
            ChangeImgSource(Pristatymas2, "PristatymasImg2", "Assets/Star_1.png");
            ChangeImgSource(Pristatymas3, "PristatymasImg3", "Assets/Star_1.png");
            ChangeImgSource(Pristatymas4, "PristatymasImg4", "Assets/Star_1.png");
            ChangeImgSource(Pristatymas5, "PristatymasImg5", "Assets/Star_0.png");
            rating["Pristatymas"] = 4;
        }

        private void PristatymasStar5(object sender, RoutedEventArgs e)
        {
            ChangeImgSource(Pristatymas1, "PristatymasImg1", "Assets/Star_1.png");
            ChangeImgSource(Pristatymas2, "PristatymasImg2", "Assets/Star_1.png");
            ChangeImgSource(Pristatymas3, "PristatymasImg3", "Assets/Star_1.png");
            ChangeImgSource(Pristatymas4, "PristatymasImg4", "Assets/Star_1.png");
            ChangeImgSource(Pristatymas5, "PristatymasImg5", "Assets/Star_1.png");
            rating["Pristatymas"] = 5;
        }

        private void AptarnavimasImg1_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Aptarnavimas1, "AptarnavimasImg1"))
            {
                ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Assets/Star_2.png");
            }
        }

        private void AptarnavimasImg1_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Aptarnavimas1, "AptarnavimasImg1"))
            {
                ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Assets/Star_0.png");
            }
        }

        private void AptarnavimasImg2_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Aptarnavimas2, "AptarnavimasImg2") && !CheckImgSource(Aptarnavimas1, "AptarnavimasImg1"))
            {
                ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Assets/Star_2.png");
                ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Assets/Star_2.png");
            }
        }

        private void AptarnavimasImg2_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Aptarnavimas2, "AptarnavimasImg2") && !CheckImgSource(Aptarnavimas1, "AptarnavimasImg1"))
            {
                ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Assets/Star_0.png");
                ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Assets/Star_0.png");
            }
        }

        private void AptarnavimasImg3_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Aptarnavimas3, "AptarnavimasImg3") && !CheckImgSource(Aptarnavimas2, "AptarnavimasImg2") && !CheckImgSource(Aptarnavimas1, "AptarnavimasImg1"))
            {
                ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Assets/Star_2.png");
                ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Assets/Star_2.png");
                ChangeImgSource(Aptarnavimas3, "AptarnavimasImg3", "Assets/Star_2.png");
            }
        }

        private void AptarnavimasImg3_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Aptarnavimas3, "AptarnavimasImg3") && !CheckImgSource(Aptarnavimas2, "AptarnavimasImg2") && !CheckImgSource(Aptarnavimas1, "AptarnavimasImg1"))
            {
                ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Assets/Star_0.png");
                ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Assets/Star_0.png");
                ChangeImgSource(Aptarnavimas3, "AptarnavimasImg3", "Assets/Star_0.png");
            }
        }

        private void AptarnavimasImg4_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Aptarnavimas4, "AptarnavimasImg4") && !CheckImgSource(Aptarnavimas3, "AptarnavimasImg3") && !CheckImgSource(Aptarnavimas2, "AptarnavimasImg2") && !CheckImgSource(Aptarnavimas1, "AptarnavimasImg1"))
            {
                ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Assets/Star_2.png");
                ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Assets/Star_2.png");
                ChangeImgSource(Aptarnavimas3, "AptarnavimasImg3", "Assets/Star_2.png");
                ChangeImgSource(Aptarnavimas4, "AptarnavimasImg4", "Assets/Star_2.png");
            }
        }

        private void AptarnavimasImg4_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Aptarnavimas4, "AptarnavimasImg4") && !CheckImgSource(Aptarnavimas3, "AptarnavimasImg3") && !CheckImgSource(Aptarnavimas2, "AptarnavimasImg2") && !CheckImgSource(Aptarnavimas1, "AptarnavimasImg1"))
            {
                ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Assets/Star_0.png");
                ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Assets/Star_0.png");
                ChangeImgSource(Aptarnavimas3, "AptarnavimasImg3", "Assets/Star_0.png");
                ChangeImgSource(Aptarnavimas4, "AptarnavimasImg4", "Assets/Star_0.png");
            }
        }

        private void AptarnavimasImg5_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Aptarnavimas5, "AptarnavimasImg5") && !CheckImgSource(Aptarnavimas4, "AptarnavimasImg4") && !CheckImgSource(Aptarnavimas3, "AptarnavimasImg3") && !CheckImgSource(Aptarnavimas2, "AptarnavimasImg2") && !CheckImgSource(Aptarnavimas1, "AptarnavimasImg1"))
            {
                ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Assets/Star_2.png");
                ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Assets/Star_2.png");
                ChangeImgSource(Aptarnavimas3, "AptarnavimasImg3", "Assets/Star_2.png");
                ChangeImgSource(Aptarnavimas4, "AptarnavimasImg4", "Assets/Star_2.png");
                ChangeImgSource(Aptarnavimas5, "AptarnavimasImg5", "Assets/Star_2.png");
            }
        }

        private void AptarnavimasImg5_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Aptarnavimas5, "AptarnavimasImg5") && !CheckImgSource(Aptarnavimas4, "AptarnavimasImg4") && !CheckImgSource(Aptarnavimas3, "AptarnavimasImg3") && !CheckImgSource(Aptarnavimas2, "AptarnavimasImg2") && !CheckImgSource(Aptarnavimas1, "AptarnavimasImg1"))
            {
                ChangeImgSource(Aptarnavimas1, "AptarnavimasImg1", "Assets/Star_0.png");
                ChangeImgSource(Aptarnavimas2, "AptarnavimasImg2", "Assets/Star_0.png");
                ChangeImgSource(Aptarnavimas3, "AptarnavimasImg3", "Assets/Star_0.png");
                ChangeImgSource(Aptarnavimas4, "AptarnavimasImg4", "Assets/Star_0.png");
                ChangeImgSource(Aptarnavimas5, "AptarnavimasImg5", "Assets/Star_0.png");
            }
        }

        private void PrekiuKokybeImg1_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(PrekiuKokybe1, "PrekiuKokybeImg1"))
            {
                ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Assets/Star_2.png");
            }
        }

        private void PrekiuKokybeImg1_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(PrekiuKokybe1, "PrekiuKokybeImg1"))
            {
                ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Assets/Star_0.png");
            }
        }

        private void PrekiuKokybeImg2_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(PrekiuKokybe2, "PrekiuKokybeImg2") && !CheckImgSource(PrekiuKokybe1, "PrekiuKokybeImg1"))
            {
                ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Assets/Star_2.png");
                ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Assets/Star_2.png");
            }
        }

        private void PrekiuKokybeImg2_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(PrekiuKokybe2, "PrekiuKokybeImg2") && !CheckImgSource(PrekiuKokybe1, "PrekiuKokybeImg1"))
            {
                ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Assets/Star_0.png");
                ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Assets/Star_0.png");
            }
        }

        private void PrekiuKokybeImg3_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(PrekiuKokybe3, "PrekiuKokybeImg3") && !CheckImgSource(PrekiuKokybe2, "PrekiuKokybeImg2") && !CheckImgSource(PrekiuKokybe1, "PrekiuKokybeImg1"))
            {
                ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Assets/Star_2.png");
                ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Assets/Star_2.png");
                ChangeImgSource(PrekiuKokybe3, "PrekiuKokybeImg3", "Assets/Star_2.png");
            }
        }

        private void PrekiuKokybeImg3_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(PrekiuKokybe3, "PrekiuKokybeImg3") && !CheckImgSource(PrekiuKokybe2, "PrekiuKokybeImg2") && !CheckImgSource(PrekiuKokybe1, "PrekiuKokybeImg1"))
            {
                ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Assets/Star_0.png");
                ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Assets/Star_0.png");
                ChangeImgSource(PrekiuKokybe3, "PrekiuKokybeImg3", "Assets/Star_0.png");
            }
        }

        private void PrekiuKokybeImg4_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(PrekiuKokybe4, "PrekiuKokybeImg4") && !CheckImgSource(PrekiuKokybe3, "PrekiuKokybeImg3") && !CheckImgSource(PrekiuKokybe2, "PrekiuKokybeImg2") && !CheckImgSource(PrekiuKokybe1, "PrekiuKokybeImg1"))
            {
                ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Assets/Star_2.png");
                ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Assets/Star_2.png");
                ChangeImgSource(PrekiuKokybe3, "PrekiuKokybeImg3", "Assets/Star_2.png");
                ChangeImgSource(PrekiuKokybe4, "PrekiuKokybeImg4", "Assets/Star_2.png");
            }
        }

        private void PrekiuKokybeImg4_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(PrekiuKokybe4, "PrekiuKokybeImg4") && !CheckImgSource(PrekiuKokybe3, "PrekiuKokybeImg3") && !CheckImgSource(PrekiuKokybe2, "PrekiuKokybeImg2") && !CheckImgSource(PrekiuKokybe1, "PrekiuKokybeImg1"))
            {
                ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Assets/Star_0.png");
                ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Assets/Star_0.png");
                ChangeImgSource(PrekiuKokybe3, "PrekiuKokybeImg3", "Assets/Star_0.png");
                ChangeImgSource(PrekiuKokybe4, "PrekiuKokybeImg4", "Assets/Star_0.png");
            }
        }

        private void PrekiuKokybeImg5_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(PrekiuKokybe5, "PrekiuKokybeImg5") && !CheckImgSource(PrekiuKokybe4, "PrekiuKokybeImg4") && !CheckImgSource(PrekiuKokybe3, "PrekiuKokybeImg3") && !CheckImgSource(PrekiuKokybe2, "PrekiuKokybeImg2") && !CheckImgSource(PrekiuKokybe1, "PrekiuKokybeImg1"))
            {
                ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Assets/Star_2.png");
                ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Assets/Star_2.png");
                ChangeImgSource(PrekiuKokybe3, "PrekiuKokybeImg3", "Assets/Star_2.png");
                ChangeImgSource(PrekiuKokybe4, "PrekiuKokybeImg4", "Assets/Star_2.png");
                ChangeImgSource(PrekiuKokybe5, "PrekiuKokybeImg5", "Assets/Star_2.png");
            }
        }

        private void PrekiuKokybeImg5_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(PrekiuKokybe5, "PrekiuKokybeImg5") && !CheckImgSource(PrekiuKokybe4, "PrekiuKokybeImg4") && !CheckImgSource(PrekiuKokybe3, "PrekiuKokybeImg3") && !CheckImgSource(PrekiuKokybe2, "PrekiuKokybeImg2") && !CheckImgSource(PrekiuKokybe1, "PrekiuKokybeImg1"))
            {
                ChangeImgSource(PrekiuKokybe1, "PrekiuKokybeImg1", "Assets/Star_0.png");
                ChangeImgSource(PrekiuKokybe2, "PrekiuKokybeImg2", "Assets/Star_0.png");
                ChangeImgSource(PrekiuKokybe3, "PrekiuKokybeImg3", "Assets/Star_0.png");
                ChangeImgSource(PrekiuKokybe4, "PrekiuKokybeImg4", "Assets/Star_0.png");
                ChangeImgSource(PrekiuKokybe5, "PrekiuKokybeImg5", "Assets/Star_0.png");
            }
        }

        private void PristatymasImg1_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Pristatymas1, "PristatymasImg1"))
            {
                ChangeImgSource(Pristatymas1, "PristatymasImg1", "Assets/Star_2.png");
            }
        }

        private void PristatymasImg1_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Pristatymas1, "PristatymasImg1"))
            {
                ChangeImgSource(Pristatymas1, "PristatymasImg1", "Assets/Star_0.png");
            }
        }

        private void PristatymasImg2_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Pristatymas2, "PristatymasImg2") && !CheckImgSource(Pristatymas1, "PristatymasImg1"))
            {
                ChangeImgSource(Pristatymas1, "PristatymasImg1", "Assets/Star_2.png");
                ChangeImgSource(Pristatymas2, "PristatymasImg2", "Assets/Star_2.png");
            }
        }

        private void PristatymasImg2_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Pristatymas2, "PristatymasImg2") && !CheckImgSource(Pristatymas1, "PristatymasImg1"))
            {
                ChangeImgSource(Pristatymas1, "PristatymasImg1", "Assets/Star_0.png");
                ChangeImgSource(Pristatymas2, "PristatymasImg2", "Assets/Star_0.png");
            }
        }

        private void PristatymasImg3_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Pristatymas3, "PristatymasImg3") && !CheckImgSource(Pristatymas2, "PristatymasImg2") && !CheckImgSource(Pristatymas1, "PristatymasImg1"))
            {
                ChangeImgSource(Pristatymas1, "PristatymasImg1", "Assets/Star_2.png");
                ChangeImgSource(Pristatymas2, "PristatymasImg2", "Assets/Star_2.png");
                ChangeImgSource(Pristatymas3, "PristatymasImg3", "Assets/Star_2.png");
            }
        }

        private void PristatymasImg3_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Pristatymas3, "PristatymasImg3") && !CheckImgSource(Pristatymas2, "PristatymasImg2") && !CheckImgSource(Pristatymas1, "PristatymasImg1"))
            {
                ChangeImgSource(Pristatymas1, "PristatymasImg1", "Assets/Star_0.png");
                ChangeImgSource(Pristatymas2, "PristatymasImg2", "Assets/Star_0.png");
                ChangeImgSource(Pristatymas3, "PristatymasImg3", "Assets/Star_0.png");
            }
        }

        private void PristatymasImg4_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Pristatymas4, "PristatymasImg4") && !CheckImgSource(Pristatymas3, "PristatymasImg3") && !CheckImgSource(Pristatymas2, "PristatymasImg2") && !CheckImgSource(Pristatymas1, "PristatymasImg1"))
            {
                ChangeImgSource(Pristatymas1, "PristatymasImg1", "Assets/Star_2.png");
                ChangeImgSource(Pristatymas2, "PristatymasImg2", "Assets/Star_2.png");
                ChangeImgSource(Pristatymas3, "PristatymasImg3", "Assets/Star_2.png");
                ChangeImgSource(Pristatymas4, "PristatymasImg4", "Assets/Star_2.png");
            }
        }

        private void PristatymasImg4_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Pristatymas4, "PristatymasImg4") && !CheckImgSource(Pristatymas3, "PristatymasImg3") && !CheckImgSource(Pristatymas2, "PristatymasImg2") && !CheckImgSource(Pristatymas1, "PristatymasImg1"))
            {
                ChangeImgSource(Pristatymas1, "PristatymasImg1", "Assets/Star_0.png");
                ChangeImgSource(Pristatymas2, "PristatymasImg2", "Assets/Star_0.png");
                ChangeImgSource(Pristatymas3, "PristatymasImg3", "Assets/Star_0.png");
                ChangeImgSource(Pristatymas4, "PristatymasImg4", "Assets/Star_0.png");
            }
        }

        private void PristatymasImg5_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Pristatymas5, "PristatymasImg5") && !CheckImgSource(Pristatymas4, "PristatymasImg4") && !CheckImgSource(Pristatymas3, "PristatymasImg3") && !CheckImgSource(Pristatymas2, "PristatymasImg2") && !CheckImgSource(Pristatymas1, "PristatymasImg1"))
            {
                ChangeImgSource(Pristatymas1, "PristatymasImg1", "Assets/Star_2.png");
                ChangeImgSource(Pristatymas2, "PristatymasImg2", "Assets/Star_2.png");
                ChangeImgSource(Pristatymas3, "PristatymasImg3", "Assets/Star_2.png");
                ChangeImgSource(Pristatymas4, "PristatymasImg4", "Assets/Star_2.png");
                ChangeImgSource(Pristatymas5, "PristatymasImg5", "Assets/Star_2.png");
            }
        }

        private void PristatymasImg5_OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (!CheckImgSource(Pristatymas5, "PristatymasImg5") && !CheckImgSource(Pristatymas4, "PristatymasImg4") && !CheckImgSource(Pristatymas3, "PristatymasImg3") && !CheckImgSource(Pristatymas2, "PristatymasImg2") && !CheckImgSource(Pristatymas1, "PristatymasImg1"))
            {
                ChangeImgSource(Pristatymas1, "PristatymasImg1", "Assets/Star_0.png");
                ChangeImgSource(Pristatymas2, "PristatymasImg2", "Assets/Star_0.png");
                ChangeImgSource(Pristatymas3, "PristatymasImg3", "Assets/Star_0.png");
                ChangeImgSource(Pristatymas4, "PristatymasImg4", "Assets/Star_0.png");
                ChangeImgSource(Pristatymas5, "PristatymasImg5", "Assets/Star_0.png");
            }
        }
    }
}