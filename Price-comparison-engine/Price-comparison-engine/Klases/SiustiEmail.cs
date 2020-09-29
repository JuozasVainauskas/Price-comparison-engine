using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;

namespace Price_comparison_engine.Klases
{
    class SiustiEmail
    {
        public SiustiEmail(string kodas, string email)
        {
            kodas = "asdkhaksdhjasdhjkhkjashdkhaskjhdkjakjsdhjkadskjdh";
            SmtpClient Client = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = "smartshopautobot@gmail.com",
                    Password = "adminNull0"
                }
            };
            MailAddress FromEmail = new MailAddress("smartshopautobot@gmail.com", "Smart Shop");
            MailAddress ToEmail = new MailAddress("ernestas20111@gmail.com", "Email patvirtinimas");
            MailMessage Laiskas = new MailMessage()
            {
                From = FromEmail,
                Subject = "Email patvirtinimas",
                Body = "Sveiki,\nkad patvirtintumėte, jog tai yra jūsų email adresas, prašome įvesti šį kodą:\n" + kodas + "\nPasirašo,\nSmart Shop komanda."
            };
            Laiskas.To.Add(ToEmail);
            Client.SendCompleted += ClientSendCompleted;
            Client.SendMailAsync(Laiskas);

            try
            {
                Client.Send(Laiskas);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Atsiprašome, įvyko klaida.", "Klaida.");
            }
        }

        private void ClientSendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if(e.Error != null)
            {
                MessageBox.Show("Įvyko klaida: "+e.Error.Message, "Klaida.");
                return;
            }
        }
    }
}
