using RecruitmentAgency.Data_Access_Layer;
using RecruitmentAgency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentAgency.Helpers
{
    public class ValidationHelper
    {
        public static string Encode(string source)
        {
            var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(source ?? string.Empty);
            return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", string.Empty);
        }


        public static bool ValidateUser(User user)
        {
            var UserDB = UserDAL.GetUserByName(user.UserName);
            if (UserDB == null)
            {
                return false;
            }
            return user.Password.Replace(" ", string.Empty).Equals(UserDB.Password.Replace(" ", string.Empty));
        }

        public static bool CheckUserName(User user)
        {
            var UserDB = UserDAL.GetUserByName(user.UserName);
            if (UserDB == null)
            {
                return true;
            }
            return false;
        }
    }
}