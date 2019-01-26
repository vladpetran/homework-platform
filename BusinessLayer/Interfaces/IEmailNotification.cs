using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Interfaces
{
    public interface IEmailNotification
    {
        /// <summary>
        /// Interface for a notification service used to notify students of homework details.
        /// </summary>

        /// <summary>
        /// Notifies an students .
        /// </summary>
        /// <param name="contact">Contact details of the student.</param>
        /// <param name="studentName">Full name of the student</param>
        /// <param name="homeworkId">ID of the homework order.</param>
        void NotifystudentWhenHomeworkIsCreated(string contact, string studentName, DateTime EndDate);

        void SendValidationEmail(string contact, string studentName, int studentId);
    }
}