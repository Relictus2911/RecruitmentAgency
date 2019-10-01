using RecruitmentAgency.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentAgency.Models
{
    public class User 
    {
        public virtual int Id { get; protected set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual Role Role { get; set; }
        public virtual Candidate Candidate { get; set; }

    }
}