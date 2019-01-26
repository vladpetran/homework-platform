using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IEmailSender
    {
        /// <summary>
        /// Send an email to a recipient.
        /// </summary>
        /// <param name="to">Address of the recipient.</param>
        /// <param name="subject">Subject of the email.</param>
        /// <param name="body">Body of the email.</param>
        void SendEmail(string to, string subject, string body);
    }
}
