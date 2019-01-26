using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer.Repository;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models
{
    public class StudentHomeworkViewModel
    {
        
        public int SHomeID { get; set; }
        public int HomeworkID { get; set; }
        [Display(Name="Homework Status")]
        public string StatusID { get; set; }
        public int StudentID { get; set; }
        public int Grade { get; set; }
        public string Path { get; set; }
        [Display(Name="Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name="Due Date")]
        public DateTime EndDate { get; set; }
        public string Requirements { get; set; }
        public string TeacherName { get; set; }
        public string StudentName { get; set; }
        public bool exist { get; set; }
        public string Comments { get; set; }
        

        public IEnumerable<HttpPostedFileBase> files { get; set; }

       // public string UploadedFiles { get; set; }

        public StudentHomeworkViewModel() { }

       public StudentHomeworkViewModel(int SHomeID, int HomeworkID, string StatusID, int StudentID)
        {
            this.SHomeID = SHomeID;
            this.HomeworkID = HomeworkID;
            this.StatusID = StatusID;
            this.StudentID = StudentID;
          //  this.UploadedFiles = UploadedFiles;
        }

       public StudentHomeworkViewModel(int SHomeID, int grade, string StatusID,int studentId,DateTime startDate,DateTime endDate,string requirements,string path)
       {
           this.SHomeID = SHomeID;
           this.Grade = grade;
           this.StatusID = StatusID;
           this.StartDate = startDate;
           this.EndDate = endDate;
           this.Requirements = requirements;
           this.StudentID = studentId;
           this.Path = path;
           
       }

      
    }
}
