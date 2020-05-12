using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using kinotiki.Domain.Concrete;

namespace kinotiki.BLL.EmailHelper
{
    public class EmailBusiness
    {
        private kinotikiDbContext context = new kinotikiDbContext();
        private SmtpClient smtp;
        private string gsemail; // our email (acProger@gmail.com)

        //public MailAddress to { get; set; }
        //public MailAddress from { get; set; }
        //public string sub { get; set; }
        //public string body { get; set; }

        EmailBusiness()
        {
            var gs = context.GlobalSettings.FirstOrDefault();
            smtp = new SmtpClient
            {
                Host = gs.smtpIP,
                Port = gs.smtpPort,
                Credentials = new NetworkCredential(gs.smtpMail, gs.smtpPassword),
                EnableSsl = true 
            };
            gsemail = gs.smtpMail;
        }

        EmailBusiness(string host, int port, string userName,string password, bool enableSsl = true)
        {
            smtp = new SmtpClient
            {
                Host = host,
                Port = port,
                Credentials = new NetworkCredential(userName, password),
                EnableSsl = enableSsl
            };
            gsemail = userName;
        }

        public void SendMail(string theme, string body, string addressTo)
        {
            try
            {
                var msg = new MailMessage(gsemail, addressTo, theme, body);
                msg.To.Add(addressTo);
                msg.IsBodyHtml = true;
                smtp.Send(msg);
            }
            catch(Exception ex)
            {
                //TODO: Log
            }
        }

        //public string ToAdmin()
        //{
        //    string feedback = "";
        //    EmailBusiness me = new EmailBusiness();

        //    var m = new MailMessage()
        //    {

        //        Subject = sub,
        //        Body = body,
        //        IsBodyHtml = true
        //    };
        //    to = new MailAddress("21241112@dut4life.ac.za", "Administrator");
        //    m.To.Add(to);
        //    m.From = new MailAddress(from.ToString());
        //    m.Sender = to;

        //    try
        //    {
        //        smtp.Send(m);
        //        feedback = "Message sent to insurance";
        //    }
        //    catch (Exception e)
        //    {
        //        feedback = "Message not sent retry" + e.Message;
        //    }
        //    return feedback;
        //}
    }
}
