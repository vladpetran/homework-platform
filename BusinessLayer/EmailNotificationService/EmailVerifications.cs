using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace BusinessLayer.EmailNotificationService
{
    public static class EmailVerifications
    {
        public static bool IsValidEmailAddress(this string address)
        {
            bool invalid = false;

            if (String.IsNullOrEmpty(address))
            {
                return false;
            }

            try
            {
                invalid = Regex.IsMatch(address,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            return invalid;
        }
    }
}