using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Price_comparison_engine.Classes
{
    class SendEmail
    {
        public SendEmail(string code, string email)
        {
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
            MailAddress ToEmail = new MailAddress("ernestas20111@gmail.com", "Naudotojas");//reiks pakeisti į email
            MailMessage Message = new MailMessage()
            {
                IsBodyHtml = true,
                From = FromEmail,
                Subject = "Email patvirtinimas",
                Body = "Sveiki,<br>kad patvirtintumėte, jog tai yra jūsų email adresas, prašome įvesti šį kodą:<br><br><b>" + code + "</b><br><br>Jei jūs nesinaudojote mūsų paslaugomis ir niekur nesiregistravote, prašome ignoruoti šį laišką.<br><img src=\"https://i.pinimg.com/originals/d4/2a/8c/d42a8c4e83f0fb3750af810be2abbb23.png\" alt =\"SmartShop\" width=\"50\" height=\"50\"><br><i>Pasirašo,<br>Smart Shop komanda.</i>"
            };
            Message.To.Add(ToEmail);
            Client.SendCompleted += ClientSendCompleted;
            Client.SendMailAsync(Message);
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
