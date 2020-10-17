﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Price_comparison_engine.Klases
{
    class SiustiEmail
    {
        public SiustiEmail(string kodas, string email)
        {
            var client = new SmtpClient()
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
            var fromEmail = new MailAddress("smartshopautobot@gmail.com", "Smart Shop");
            var toEmail = new MailAddress(email, "Naudotojas");
            var laiskas = new MailMessage()
            {
                IsBodyHtml = true,
                From = fromEmail,
                Subject = "Email patvirtinimas",
                Body = "Sveiki,<br>kad patvirtintumėte, jog tai yra jūsų email adresas, prašome įvesti šį kodą:<br><br><b>" + kodas + "</b><br><br>Jei jūs nesinaudojote mūsų paslaugomis ir niekur nesiregistravote, prašome ignoruoti šį laišką.<br><img src=\"https://i.pinimg.com/originals/d4/2a/8c/d42a8c4e83f0fb3750af810be2abbb23.png\" alt =\"SmartShop\" width=\"50\" height=\"50\"><br><i>Pasirašo,<br>Smart Shop komanda.</i>"
            };
            laiskas.To.Add(toEmail);
            client.SendCompleted += ClientSendCompleted;
            client.SendMailAsync(laiskas);
        }

        private static void ClientSendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if(e.Error != null)
            {
                MessageBox.Show("Įvyko klaida: "+e.Error.Message, "Klaida.");
                return;
            }
        }
    }
}
