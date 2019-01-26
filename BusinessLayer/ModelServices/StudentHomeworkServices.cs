using BusinessLayer.EmailNotificationService;
using BusinessLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.ModelServices
{
    public class StudentHomeworkServices
    {
        //get all homeworks by student id
        public List<StudentHomeworkViewModel> GetStudentHomeWorks(int id)
        {
            var studentHomeworks = new List<StudentHomework>();
            StudentService instance = new StudentService();
            Student student = instance.GetStudentByID(id);
            studentHomeworks = StudentHomeworkRepository.GetStudentHomeWorks(id);
            List<StudentHomeworkViewModel> ShowList = new List<StudentHomeworkViewModel>();
            foreach (var studentHomework in studentHomeworks)
            {
                if ((studentHomework.StatusID != "Rejected") && (studentHomework.EndDate < DateTime.Now))
                {
                    studentHomework.StatusID = "Rejected";
                    studentHomework.Grade = 1;
                    StudentHomeworkRepository.UpdateStudentHomeworkBySHomeworkID(studentHomework.SHomeID, studentHomework.Grade, studentHomework.StatusID, "");//update in DB
                    EmailNotification notification = new EmailNotification(new EmailSender());

                    notification.NotifystudentWhenHomeworkIsRejected(student.Email, student.FirstName, studentHomework.SHomeID);
                }

                StudentHomeworkViewModel homework = new StudentHomeworkViewModel(studentHomework.SHomeID, studentHomework.Grade, studentHomework.StatusID, studentHomework.StudentID, studentHomework.StartDate, studentHomework.EndDate, studentHomework.Requirements, studentHomework.UploadedFiles);
                homework.StudentName = student.UserName;
                homework.StudentID = id;
                homework.Comments = studentHomework.Comments;
                ShowList.Add(homework);
            }

            return ShowList;
        }

        public StudentHomeworkViewModel GetHomeworkDetails(int studentId, int SHId)
        {
            StudentHomeworkServices service = new StudentHomeworkServices();
            List<StudentHomeworkViewModel> homeworks = service.GetStudentHomeWorks(studentId);
            StudentHomeworkViewModel homework = homeworks.Where(a => a.SHomeID == SHId).FirstOrDefault();
            return homework;
        }

        public void UpdateStudentHomework(int ShomeworkId, int grade, string status, string path)
        {
            var studentHomework = new StudentHomework();
            studentHomework = StudentHomeworkRepository.UpdateStudentHomeworkBySHomeworkID(ShomeworkId, grade, status, path);


        }
        public List<StudentHomeworkViewModel> GetBestHomeWorks(int id)
        {
            StudentHomeworkServices service = new StudentHomeworkServices();
            List<StudentHomeworkViewModel> List = service.GetStudentHomeWorks(id);
            List<StudentHomeworkViewModel> bestHM = List.OrderBy(a => a.Grade).Take(10).ToList();
            return List;
        }

        public void AddGrade(int ShomeworkId, int grade, int studentId)
        {
            var studentHomework = new StudentHomework();
            StudentHomeworkRepository repository = new StudentHomeworkRepository();
            repository.AddGrade(ShomeworkId, grade);
            EmailNotification notification = new EmailNotification(new EmailSender());
            StudentService service = new StudentService();
            Student student = service.GetStudentByID(studentId);
            notification.NotifystudentWhenHomeworkIsAccepted(student.Email, student.FirstName, ShomeworkId);
        }

        public void AddComment(int ShomeworkId, string comments, int studentId)
        {
            var studentHomework = new StudentHomework();
            StudentHomeworkRepository repository = new StudentHomeworkRepository();
            repository.AddComments(ShomeworkId, comments);
            EmailNotification notification = new EmailNotification(new EmailSender());
            StudentService service = new StudentService();
            Student student = service.GetStudentByID(studentId);
            notification.NotifystudentWhenHomeworkIsAccepted(student.Email, student.FirstName, ShomeworkId);
        }

        public void Rejected(int ShomeworkId, string comments, int studentId)
        {
            var studentHomework = new StudentHomework();
            StudentHomeworkRepository repository = new StudentHomeworkRepository();
            repository.Rejected(ShomeworkId, comments);
            EmailNotification notification = new EmailNotification(new EmailSender());
            StudentService service = new StudentService();
            Student student = service.GetStudentByID(studentId);
            notification.NotifystudentWhenHomeworkIsRejected(student.Email, student.FirstName, ShomeworkId);
        }
    }
}