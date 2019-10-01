using NHibernate;
using RecruitmentAgency.Helpers;
using RecruitmentAgency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentAgency.Data_Access_Layer
{
    public class UserDAL
    {
        public static bool Create(User user)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                IQuery query = session.CreateSQLQuery("exec add_user @userName=:username, @pass=:password, @role =:role");
                query.SetString("username", user.UserName);
                query.SetString("password", user.Password);
                query.SetParameter("role", user.Role);
                var result = query.UniqueResult();
                return Convert.ToInt32(result) == 0;
            }
        }

        public static User GetUserByName(string username)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var user = session.Query<User>().Where(x => x.UserName == username).FirstOrDefault();
                return user;
            }
        }

        public static IEnumerable<User> GetUsers()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var users = session.Query<User>().OrderBy(x => x.UserName).ToList();
                return users;
            }
        }
    }
}