using RecruitmentAgency.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentAgency.Models
{
    public class Candidate
    {
        public virtual int Id { get; set; }
        public virtual string FullName { get; set; }
        public virtual DateTime Birthday { get; set; }
        public virtual Expirience WorkExpirience { get; set; }
        public virtual byte[] Photo { get; set; }
        public virtual string KeyWords { get; set; }
        public virtual User UserId { get; set; }
    }
}