using DataAccessLayer.Entities;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BusinessLayer.Models
{
    public class LogInViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Keep me logged in!")]
        public bool RememberUser { get; set; }

        public bool IsOK(string _username, string _password)
        {
            UserRepository usr = new UserRepository();
            var userList = new List<Tuple<string, string, string,int>> { };
            string password = EncodePasswordToBase64(_password);
            userList = usr.GetUserByUserName(_username);
            if (userList != null)
            {
                if (userList[0].Item2 == password)
                {
                    return true;
                }
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        public static string EncodePasswordToBase64(string password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }

        public string SetRoleCookie(string username)
        {
            UserRepository usr = new UserRepository();
            var userList = new List<Tuple<string, string, string,int>> { };
            userList = usr.GetUserByUserName(username);
            string role = userList[0].Item3;
            return role;
        }

        public bool SetConfirmCookie(string username)
        {
            UserRepository usr = new UserRepository();
            bool IsOK = usr.GetConfirm(username);
            
            return IsOK;
        }

        public int SetIDCookie(string username)
        {
            UserRepository usr = new UserRepository();
            var userList = new List<Tuple<string,string,string,int>> { };
            userList = usr.GetUserByUserName(username);
            int userid = userList[0].Item4;
            return userid;
        }
    }
}