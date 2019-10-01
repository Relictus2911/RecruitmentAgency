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
    public class CandidateDAL
    {
        public static bool Create(Candidate candidate)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                IQuery query = session.CreateSQLQuery("exec create_candidate @FullName=:fullName, @Birthday=:birthday, @Photo=:photo, @KeyWords=:keyWords, @UserId=:userId, @WorkExpirience=:workExpirience");
                query.SetString("fullName", candidate.FullName);               
                query.SetDateTime("birthday", candidate.Birthday);
                query.SetParameter("photo", candidate.Photo, NHibernateUtil.BinaryBlob);
                query.SetString("keyWords", candidate.KeyWords);
                query.SetInt32("userId", candidate.UserId.Id);
                query.SetParameter("workExpirience", candidate.WorkExpirience);
                var result = query.UniqueResult();
                return Convert.ToInt32(result) == 0;
            }
        }

        public static IEnumerable<Candidate> GetCandidates()
        {
            using (var session = NHibernateHelper.OpenSession())
            {

                var candidate =
                    session.Query<Candidate>().OrderBy(x => x.FullName)
                        .ToList();
                return candidate;
            }
        }

        public static Candidate GetCandidateById(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var candidate = session.Query<Candidate>().Where(x => x.UserId.Id == id).FirstOrDefault();
                return candidate;
            }
        }

        public static IEnumerable<Candidate> SearchForCandidate(string keyWords, Expirience expirience)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                string[] keyWordsSplited = keyWords.Split(',');
                List<Candidate> candidates = new List<Candidate>();
                for (int i = 0; i < keyWordsSplited.Count(); i++)
                {
                    var candidateByKeyWord = session.Query<Candidate>().Where(x => x.KeyWords.Contains(keyWordsSplited[i].ToLower()) && x.WorkExpirience >= expirience).ToList();
                    if (candidateByKeyWord != null)
                    {
                        foreach (var item in candidateByKeyWord)
                        {
                            candidates.Add(item);
                        }
                    }
                }
                var candidateUnique = candidates.Distinct();               
                return candidateUnique;
            }
        }
    }
}