using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessLayer.Repository;
using System.Text;
using System.Security.Cryptography;
using BusinessLayer.EmailNotificationService;

namespace BusinessLayer.Models
{
    public class UserViewModel
    {
        UserRepository userRepository = new UserRepository();

        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UserViewModel(string userName, string phoneNumber, string email, string password)
        {
            this.PhoneNumber = phoneNumber;
            this.UserName = userName;
            this.Email = email;
            this.Password = password;
        }

        public UserViewModel()
        {

        }

        public bool ResetPass(string email)
        {
            int RowsAffected;
            string password = RandomString(12);
            RowsAffected = userRepository.ResetUserPassword(email, password);
            if (RowsAffected > 0)
            {
                EmailNotification notification = new EmailNotification(new EmailSender());
                notification.NotifyUserOnPassReset(email, password);
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string EncodePasswordToBase64(string password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }


    }
}