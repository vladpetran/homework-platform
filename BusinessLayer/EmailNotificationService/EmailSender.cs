using BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.ModelBinding;

namespace BusinessLayer.EmailNotificationService
{
    public class EmailSender : IEmailSender
    {
        /// <summary>
        /// Sets up and sends the email using the information provided through the properties.
        /// </summary>
        /// <exception cref="FormatException">
        ///		Thrown when:
        ///			Target address is invalid.
        ///			Source address is invalid.
        ///			Subject is empty.
        /// </exception>
        public void SendEmail(string to, string subject, string body)
        {
            var host = ConfigurationManager.AppSettings["SmtpHost"].ToString();
            var from = ConfigurationManager.AppSettings["SmtpEmailAddress"].ToString();
            var password = ConfigurationManager.AppSettings["SmtpEmailPassword"].ToString();
            int port = 587;

            if (!from.IsValidEmailAddress())
            {
                throw new FormatException(String.Format("{0} is not a valid email address.", from));
            }

            if (!to.IsValidEmailAddress())
            {
                throw new FormatException(String.Format("{0} is not a valid email address.", to));
            }

            if (String.IsNullOrEmpty(subject))
            {
                throw new FormatException("The subject cannot be empty.");
            }

            using (var mail = new MailMessage(from, to, subject, body))
            {
                using (var smtpClient = new SmtpClient(host, port))
                {
                    mail.IsBodyHtml = true;
                    smtpClient.Credentials = new NetworkCredential(from, password);
                    smtpClient.EnableSsl = true;
                    try
                    {
                        smtpClient.Send(mail);
                    }
                    catch (Exception)
                    {
                        // Do nothing - if an invalid email occurs continue and try to add the rest
                    }
                }
            }
        }

    }
}