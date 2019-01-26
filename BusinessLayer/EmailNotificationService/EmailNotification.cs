using BusinessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.EmailNotificationService
{
    public class EmailNotification : IEmailNotification
    {
        private IEmailSender _sender;

        public EmailNotification(IEmailSender sender)
        {
            _sender = sender;
        }


        public void NotifyUserOnPassReset(string contact, string password)
        {
            string subject = String.Format("Your password has been reset.");
            string body = String.Format("<br><br>Dear user,<br><br>Please note that your password has been reset. Next time you try to log in use this temporary password: {0}. To view it, please log in to <a href=\"http://localhost:54335/User/Login\">Check</a>.<br><br>Thank you!<br>",
               
                password
                );
            _sender.SendEmail(contact, subject, body);
        }

        public void NotifystudentWhenHomeworkIsCreated(string contact, string studentName,DateTime EndDate)
        {
            string subject = String.Format("Another homework was assigned to you.");
            string body = String.Format("<br><br>Dear {0},<br><br>Please note that a new homework was now created and will expire in {1}. Please <a href=\"http://localhost:54335/User/Login\">log in</a> for more details.<br><br>Thank you!<br>",
                studentName,               
                EndDate
                );
            _sender.SendEmail(contact, subject, body);
        }

        public void SendValidationEmail(string contact, string studentName, int studentId)
        {
            string subject = String.Format("Thank you for enrolling in our school ");
            string body = String.Format("<br><br>Dear {0},<br><br>Please note that your account  with Id={1} has not been validated.To activate please <a href=\"http://localhost:54335/Register/ActivateAccount/{2}\">click here!</a>. <br><br>Thank you!<br>",
                studentName,
                studentId,
                studentId
                );
            _sender.SendEmail(contact, subject, body);
        }

        public void NotifystudentWhenHomeworkIsRejected(string contact, string studentName, int homeworkId)
        {
            string subject = String.Format("Rejected homework");
            string body = String.Format("<br><br>Dear {0},<br><br>Please note that the homework with Id={1} has expired.   <a href=\"http://localhost:54335/\">Log in</a> for more details.<br><br>Thank you!<br>",
                studentName,
                homeworkId
                );
            _sender.SendEmail(contact, subject, body);
        }

        public void NotifystudentWhenHomeworkIsAccepted(string contact, string studentName, int homeworkId)
        {
            string subject = String.Format("Accepted homework");
            string body = String.Format("<br><br>Dear {0},<br><br>Please note that the homework with Id={1} has been accepted! Please <a href=\"http://localhost:54335/\">log in</a> for more details.<br><br>Congratulations!<br>",
                studentName,
                homeworkId
                );
            _sender.SendEmail(contact, subject, body);
        }

        public void NotifystudentWhenHomeworkIsCommented(string contact, string studentName, int homeworkId)
        {
            string subject = String.Format("Commented homework");
            string body = String.Format("<br><br>Dear {0},<br><br>Please note that your teacher has just commented on your homework with Id={1} . To view the commentary please click <a href=\"http://localhost:54335/\">here</a>.<br><br>Thank you!<br>",
                studentName,
                homeworkId                
                );
            _sender.SendEmail(contact, subject, body);
        }
    }
}