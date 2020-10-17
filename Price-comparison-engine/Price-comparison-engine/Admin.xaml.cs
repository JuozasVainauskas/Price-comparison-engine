using Price_comparison_engine.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace Price_comparison_engine
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        private MainWindowLoggedIn mainWindowLoggedIn;

        public Admin(MainWindowLoggedIn mainWindowLoggedIn)
        {
            InitializeComponent();
            UpdateStatistics();
            this.mainWindowLoggedIn = mainWindowLoggedIn;
        }

        private void UpdateStatistics()
        {
            List<int> ls = Count();
            registeredUsers.Text = ls[0].ToString();
            admins.Text = ls[1].ToString();
            users.Text = ls[2].ToString();
            goods.Text = ls[3].ToString();
        }

        private partial class User
        {
            public int ID { get; set; }
            public string Email { get; set; }   
            public string Role { get; set; }
        }

        private static string _role = "0";
        private void SetRole(string email, string role)
        {
            using (var context = new DatabaseContext())
            {
                var result = context.UserData.SingleOrDefault(b => b.Email == email);
                if (result != null)
                {
                    result.Role = role;
                    context.SaveChanges();
                    UpdateStatistics();
                }
                else
                {
                    MessageBox.Show("Vartotojas tokiu emailu neegzistuoja arba nebuvo rastas.");
                }
            }
        }

        private void RoleSetter(object sender, RoutedEventArgs e)
        {
            if (selectRole.SelectedIndex != -1 && EmailVerification(email.Text))
            {
                _role = selectRole.SelectedIndex.ToString();
                SetRole(email.Text, _role);
                MessageBox.Show(email.Text + " priskirta nauja rolė!");
                email.Text = "";
                UsersTable.Items.Clear();
                Email.Clear();
                Role.Clear();
                Read(ref Email, ref Role);
                ToTable();
            }
        }

        private void Delete(string email)
        {
            using (var context = new DatabaseContext())
            {
                var savedItems = context.SavedItems.Where(c => c.Email == email).ToList();

                foreach (var savedItem in savedItems)
                {
                    context.SavedItems.Remove(savedItem);
                }

                var result = context.UserData.SingleOrDefault(c => c.Email == email);

                if (result != null)
                {
                    context.UserData.Remove(result);
                    UpdateStatistics();
                    MessageBox.Show("Vartotojas " + email + " buvo ištrintas iš duomenų bazės!");
                }
                else
                {
                    MessageBox.Show("Vartotojas tokiu emailu neegzistuoja arba nebuvo rastas.");
                }

                var comments = context.CommentsTable.Where(c => c.Email == email).ToList();

                foreach (var comment in comments)
                {
                    context.CommentsTable.Remove(comment);
                }

                context.SaveChanges();
            }

            if (email == LoginWindow.email)
            {
                LoginWindow.email = "";
                LoginWindow.userRole = Classes.Role.User;
                var mainWindow = new MainWindow();
                mainWindow.Show();
                mainWindowLoggedIn.Close();
                this.Close();
            }
        }

        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            if(EmailVerification(emailToDelete.Text))
            {
                Delete(emailToDelete.Text);
                emailToDelete.Clear();
                UsersTable.Items.Clear();
                Email.Clear();
                Role.Clear();
                Read(ref Email, ref Role);
                ToTable();
                UpdateStatistics();
            }
        }

        private void Create(string email, string password)
        {
            var passwordSalt = GenerateHash.CreateSalt(10);
            var passwordHash = GenerateHash.GenerateSHA256Hash(PasswordToCreate.Password, passwordSalt);

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Prašome užpildyti visus laukus.");
            }
            else if (!EmailVerification(email))
            {
                MessageBox.Show("Neteisingai suformatuotas el. paštas!");
            }
            else
            {
                var context = new DatabaseContext();
                var result = context.UserData.SingleOrDefault(c => c.Email == email);
                if (result != null)
                {
                    MessageBox.Show("Toks email jau panaudotas. Pabandykite kitą.");
                }
                else
                {
                    var userData = new UserData()
                    {
                        Email = email,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        Role = "0"
                    };
                    context.UserData.Add(userData);
                    context.SaveChanges();
                    UpdateStatistics();
                    MessageBox.Show("Vartotojas sekmingai sukurtas!");
                }
            }
        }

        private void CreateUser(object sender, RoutedEventArgs e)
        {
            Create(emailToCreate.Text,PasswordToCreate.Password);
            emailToCreate.Clear();
            PasswordToCreate.Clear();
            UsersTable.Items.Clear();
            Email.Clear();
            Role.Clear();
            Read(ref Email, ref Role);
            ToTable();
        }

        private bool EmailVerification(string email)
        {
            var pattern = new Regex(@"([a-zA-Z0-9]+)(@gmail.com)$", RegexOptions.Compiled);
            if (email == "")
            {
                return false;
            }
            else if (!pattern.IsMatch(email))
            {
                return false;
            }
            else return true;

        }

        private static List<string> Email = new List<string>();
        private static List<string> Role = new List<string>();
        private void ShowUsers(object sender, EventArgs e)
        {
          Read(ref Email, ref Role);
          ToTable();
        }

        private void ToTable()
        {
            for (int i = 0; i < Email.Count; i++)
            {
                var user = new User { ID = i, Email = Email[i], Role = Role[i] };
                UsersTable.Items.Add(user);
            }
        }

        private static void Read(ref List<string> Email, ref List<string> Role)
        {
            using (var context = new DatabaseContext())
            {
                var tempEmail = context.UserData.Select(column => column.Email).ToList();
                var tempRole = context.UserData.Select(column => column.Role).ToList();

                for(int i = 0; i < tempRole.Count; i++)
                {
                    if(tempRole[i] == "0")
                    {
                        tempRole[i] = "Vartotojas";
                    }
                    else if(tempRole[i] == "1")
                    {
                        tempRole[i] = "Administratorius";
                    }
                }
                Email = tempEmail;
                Role = tempRole;
            }
        }
        private static List<int> Count()
        {
            using (var context = new DatabaseContext())
            {
                List<int> statisticList = new List<int>();

                var allUsers = context.UserData
               .Where(o => o.UserId >= 0)
               .Count();
                statisticList.Add(allUsers);

                var admins = context.UserData
               .Where(o => o.Role == "1")
               .Count();
                statisticList.Add(admins);

                var regularUsers = context.UserData
               .Where(o => o.Role == "0")
               .Count();
                statisticList.Add(regularUsers);

                var goods = context.ItemsTable
               .Where(o => o.ItemId >= 0)
               .Count();
                statisticList.Add(goods);

                return statisticList;
            }
        }
    }
}
