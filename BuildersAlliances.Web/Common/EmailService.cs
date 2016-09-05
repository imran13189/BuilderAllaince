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
                mm.Subject = subject;
                mm.Body = contents;
                mm.IsBodyHtml = true;
                mm.To.Add(To);
                SmtpClient smtp = new SmtpClient();
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