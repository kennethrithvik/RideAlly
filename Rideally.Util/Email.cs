using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Rideally.Util
{
    public class Email
    {
        public static bool SendMail(string email, string sub, string body)
        {
            bool issent = true; ;
            try
            {
                MailMessage _mailMessageObj = new MailMessage();
                _mailMessageObj.From = new MailAddress("carpoolingfortesco@gmail.com");
                _mailMessageObj.To.Add(email);
                _mailMessageObj.Subject = sub;
                _mailMessageObj.Body = body;

                SmtpClient _smtpClientObj = new SmtpClient("smtp.gmail.com");
                _smtpClientObj.Port = 587;
                _smtpClientObj.Credentials = new System.Net.NetworkCredential("carpoolingfortesco@gmail.com", "carpooling2015");
                _smtpClientObj.EnableSsl = true;
                _smtpClientObj.Send(_mailMessageObj);
            }
            catch (Exception ex)
            {
                issent = false;
            }

            return issent;
        }
    }
}
