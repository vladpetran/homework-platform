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
    public class HomeworkService
    {
        public void AddHomework(HomeworkViewModel homework)
        {
            Homework homeworkToDAL = new Homework(homework.startDate, homework.endDate, homework.TeacherID, homework.Requirements);
            HomeworkRepository repository = new HomeworkRepository();
            repository.AddHomework(homeworkToDAL);
        }


        public List<HomeworkViewModel> GetHomeworksByTeacherID(int id)
        {
            HomeworkRepository repo = new HomeworkRepository();
            List<Homework> homeworkList = repo.GetAllHomeworksByTeacherID(id);
            List<HomeworkViewModel> newList = new List<HomeworkViewModel>();
            foreach (var item in homeworkList)
            {
                HomeworkViewModel newHomework = new HomeworkViewModel()
                {

                    TeacherID = item.TeacherID,
                    HomeworkID = item.HomeworkID,
                    startDate = item.StartDate,
                    endDate = item.EndDate,
                    Requirements = item.Requirements.ToString(),
                                       
                };

                newList.Add(newHomework);
            }
            return newList;
        }
    }
}
