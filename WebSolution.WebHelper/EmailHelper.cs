using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace WebSolution.WebHelper
{
    public class EmailHelper
    {
        public static void SendMail(string userName, string password, string fromEmail,string toEmail,string subject, string body, string host, int port)
        {
            try
            {
                var message = new MailMessage(fromEmail, toEmail, subject, body);
                message.IsBodyHtml = true;
                var client = new SmtpClient(host, port);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(userName, password);
                client.Send(message);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
