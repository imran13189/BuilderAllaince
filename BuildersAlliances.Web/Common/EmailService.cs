using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace BuildersAlliances.Web.Common
{
    public class EmailService
    {
        static public string SendQouteInEmail(string To,string subject, string contents, bool isHtml, bool sendAsync)
        {
            try
            {
                MailMessage mm = new MailMessage();
                mm.Subject = "Registration Confirmation";
                mm.Body = contents;
                mm.IsBodyHtml = true;
                mm.To.Add(To);
                SmtpClient smtp = new SmtpClient();
                //smtp.Host = "smtp.gmail.com";
                //smtp.EnableSsl = true;
                //string FromEmail = ConfigurationManager.AppSettings["SmtpEmail"];
                //string Password = ConfigurationManager.AppSettings["SmtpPassword"];
                //NetworkCredential NetworkCred = new NetworkCredential(FromEmail, Password);
                //smtp.UseDefaultCredentials = false;
                //smtp.Credentials = NetworkCred;
                //smtp.Port = 587;
                smtp.Send(mm);
                return "Email sent successfully";
            }
            catch (Exception ex)
            {
                return String.Format("There was a problem sending the email: {0}", ex.Message);
            }
        }
    }
}