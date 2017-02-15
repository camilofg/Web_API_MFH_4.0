using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace Web_API_MFH_4._0.Helpers
{
    public class GmailEmailService
    {
        public string sendEmail(string emailDestination, string idUser, string code, string callbackUrl)
        {

            try
            {
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("no-replay@toolsoft.co");
                msg.To.Add(new MailAddress(emailDestination));
                msg.Subject = ConfigurationManager.AppSettings["HostGatorSubject"].ToString();
                string msgBody = string.Format("{0} {1}", ConfigurationManager.AppSettings["HostGatorBody"].ToString(), "<a href=\"" + string.Format("{0}?code={1}&UserID={2}", callbackUrl, code, idUser) + "\">clicking here</a>");
                msg.Body = msgBody;
                msg.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Send(msg);
                return "Email has been sent";
            }
            catch (Exception ex)
            {
                return ex.InnerException.StackTrace.ToString() + " // " + ex.InnerException.ToString();
            }
        }

        public string sendEmail(string emailDestination, string callbackUrl)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("no-replay@toolsoft.co");
            mailMessage.To.Add(new MailAddress("camilo@toolsoft.co"));//emailDestination));
            string emailBody = string.Format("{0} {1}", ConfigurationManager.AppSettings["HostGatorBody"], "<a href=\"" + callbackUrl + "\">clicking here</a>");
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = "Asp.Net Send mail";
            mailMessage.Body = emailBody;
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(mailMessage);
            return "Email has been sent";
        }
    }
}