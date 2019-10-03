using NHibernate;
using RecruitmentAgency.Enum;
using RecruitmentAgency.Helpers;
using RecruitmentAgency.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentAgency.Data_Access_Layer
{
    public class VacancyDAL
    {
        public static bool Create(Vacancy vacancy)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                IQuery query = session.CreateSQLQuery("exec create_vacancy @Name=:name, @CompanyName=:companyName, @Description=:description, @Requirements=:requirements, @Payment=:payment, @KeyWords=:keyWords, @CloseTime=:closeTime, @IsOpen=:isOpen, @UserId=:userId, @WorkExpirience=:workExpirience");
                query.SetString("name", vacancy.Name);
                query.SetString("companyName", vacancy.CompanyName);
                query.SetString("description", vacancy.Description);
                query.SetString("requirements", vacancy.Requirements);
                query.SetString("payment", vacancy.Payment);
                query.SetString("keyWords", vacancy.KeyWords);
                query.SetDateTime("closeTime", vacancy.CloseTime);
                query.SetBoolean("isOpen", vacancy.IsOpen);
                query.SetInt32("userId", vacancy.UserId.Id);
                query.SetParameter("workExpirience", vacancy.WorkExpirience);
                var result = query.UniqueResult();
                return Convert.ToInt32(result) == 0;

            }
        }

        public static Vacancy GetVacancyById(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var vacancy = session.Query<Vacancy>().Where(x => x.Id == id).FirstOrDefault();
                return vacancy;
            }
        }

        public static Vacancy GetVacancyByUserId(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var vacancy = session.Query<Vacancy>().Where(x => x.UserId.Id == id).FirstOrDefault();
                return vacancy;
            }
        }


        public static IEnumerable<Vacancy> GetVacanciesByUserId(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var vacancies = session.Query<Vacancy>().Where(x => x.UserId.Id == id).ToList();
                return vacancies;
            }
        }

        public static bool CloseVacancy(int id, bool? isOpen)
        {
            bool isClose = true;
            if (isOpen == true)
            {
                isClose = true;
            }
            if (isOpen == false)
            {
                isClose = false;
            }
            using (var session = NHibernateHelper.OpenSession())
            {
                IQuery query = session.CreateSQLQuery("exec close_vacancy @Id=:id, @IsOpen=:isOpen");
                query.SetInt32("id", id);
                query.SetBoolean("isOpen", !isClose);
                var result = query.UniqueResult();
                return Convert.ToInt32(result) == 0;
            }
        }

        public static IEnumerable<Vacancy> SearchForVacancy(string keyWords, Expirience expirience)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                string[] keyWordsSplited = keyWords.Split(',');
                keyWordsSplited = keyWordsSplited.Where(x => x != " ").ToArray();
                List<Vacancy> vacancies = new List<Vacancy>();
                for (int i = 0; i < keyWordsSplited.Count(); i++)
                {
                    var vacanyByKeyWord = session.Query<Vacancy>().Where(x => x.KeyWords.Contains(keyWordsSplited[i]) && x.WorkExpirience <= expirience).ToList();
                    if (vacanyByKeyWord != null)
                    {
                        foreach (var item in vacanyByKeyWord)
                        {
                            vacancies.Add(item);
                        }
                    }
                }
                var vacanciesUnique = vacancies.Distinct();
                return vacanciesUnique;
            }
        }

        public static bool Remove(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                IQuery query = session.CreateSQLQuery("exec remove_vacancy @Id=:id");
                query.SetInt32("id", id);
                var result = query.UniqueResult();
                return Convert.ToInt32(result) == 0;
            }

        }

        public static IEnumerable<Vacancy> GetVacancies()
        {
            using (var session = NHibernateHelper.OpenSession())
            {

                var vacancies =
                    session.Query<Vacancy>().OrderBy(x => x.Name).Where(x=> x.IsOpen == true)
                        .ToList();
                return vacancies;
            }
        }
    }
}