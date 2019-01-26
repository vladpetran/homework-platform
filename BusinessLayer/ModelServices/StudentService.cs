using BusinessLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.ModelServices
{
    public class StudentService
    {


        public StudentInformationBL GetStudentByStudentName(string studentName)
        {
            StudentRepository repository = new StudentRepository();
            StudentInformationDAL newStudent = repository.GetStudentByStudentName(studentName);

            StudentInformationBL student = new StudentInformationBL()
            {
                StudentID = newStudent.StudentID,
                Email = newStudent.Email,
                StudentName = newStudent.StudentName
            };

            return student;
        }

        public void UpdateStudentTeacher(int sid, int tid)
        {
            StudentRepository repo = new StudentRepository();
            List<Student> stList = repo.GetAllStudents();
            Student student = stList.Where(a => a.StudentID == sid).FirstOrDefault();
            student.TeacherID = tid;
            repo.UpdateStudent(student);

        }


        public void UpdateStudentByID(int id)
        {

            StudentRepository repository = new StudentRepository();
            List<Student> studentList = repository.GetAllStudents();
            Student student = studentList.Where(a => a.StudentID == id).FirstOrDefault();
            student.EmailConfirmation = true;
            repository.UpdateStudent(student);
        }
        public void AddStudent(StudentViewModel item)
        {
            StudentRepository repo = new StudentRepository();
            Student newStudent = new Student();

            newStudent.UserName = item.UserName;
            newStudent.Password = item.Password;
            newStudent.Email = item.Email;
            newStudent.PhoneNumber = item.PhoneNumber;
            newStudent.EmailConfirmation = false;
            newStudent.FirstName = item.Firstname;
            newStudent.LastName = item.LastName;
            newStudent.HomeAddress = item.HomeAdress;
            newStudent.RoleID = "Student";
            newStudent.TeacherID = item.TeacherID;
            repo.AddStudent(newStudent);

        }
        //o lista cu toti studentii pentru fiecare profesor
        public List<StudInfoViewModel> GetStudentsByTeacherID(int id)
        {
            StudentRepository repo = new StudentRepository();
            List<Student> studList = repo.GetAllStudentsByTeacherID(id);//lista cu toti studentii profesorului
            List<StudInfoViewModel> newList = new List<StudInfoViewModel>();
            StudentHomeworkServices service = new StudentHomeworkServices();
            foreach (var item in studList)
            {
                int sum = 0;
                double average = 0;
                int count = 0;
                var newStud = new StudInfoViewModel(item.StudentID, item.FirstName.ToString(), item.LastName.ToString(), item.Email.ToString(), item.PhoneNumber);

                List<StudentHomeworkViewModel> list = service.GetStudentHomeWorks(item.StudentID);//lista cu toate temele studentului
                foreach (var homework in list)
                {
                    if (homework.StatusID == "Accepted")
                    {
                        sum = sum + homework.Grade;
                        count += 1;
                    }

                }
                if (count != 0)
                {
                    average = (double)sum / (double)count;


                    newStud.GradeAverage = average;
                }
                else
                    newStud.GradeAverage = 1;
                newList.Add(newStud);
            }
            return newList;
        }
        public Student GetStudentByID(int studentId)
        {
            Student student = new Student();
            StudentRepository repository = new StudentRepository();
            student = repository.GetStudentByID(studentId);

            return student;
        }

        public StudInfoViewModel GetStudByID(int studentId)
        {
            StudentRepository repo = new StudentRepository();
            Student student = new Student();
            StudInfoViewModel newStudent = new StudInfoViewModel();
            student = repo.GetStudentByID(studentId);
            newStudent.StudentID = student.StudentID;
            newStudent.FirstName = student.FirstName;
            newStudent.LastName = student.LastName;
            newStudent.Email = student.Email;
            newStudent.PhoneNumber = student.PhoneNumber;
            newStudent.TeacherID = student.TeacherID;
            return newStudent;

        }

        public List<StudInfoViewModel> GetAllStudents()
        {
            StudentRepository repo = new StudentRepository();
            List<Student> sList = repo.GetAllStudents();
            List<StudInfoViewModel> studList = new List<StudInfoViewModel>();
            foreach (var item in sList)
            {
                var newStud = new StudInfoViewModel(item.StudentID, item.FirstName, item.LastName, item.Email, item.PhoneNumber, item.TeacherID);
                studList.Add(newStud);
            }
            return studList;
        }
    }
}