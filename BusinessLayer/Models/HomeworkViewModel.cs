using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer.Repository;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models
{
    public class HomeworkViewModel
    {
        HomeworkRepository homeworkRepository = new HomeworkRepository();
        [Display(Name="Start Date")]
        public DateTime startDate { get; set; }
        [Display(Name="Due Date")]
        public DateTime endDate { get; set; }
        public string Requirements { get; set; }
        public int HomeworkID { get; set; }
        public int TeacherID { get; set; }

        public HomeworkViewModel(DateTime startDate, DateTime endDate, int TeacherId, string Requirements)
        {
            this.startDate = startDate;
            this.endDate = endDate;
            this.TeacherID = TeacherId;
            this.Requirements = Requirements;
        }

        public HomeworkViewModel()
        {

        }
    }
}