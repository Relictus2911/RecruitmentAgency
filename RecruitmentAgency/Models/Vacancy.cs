using RecruitmentAgency.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecruitmentAgency.Models
{
    public class Vacancy
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string Description { get; set; }
        public virtual string Requirements { get; set; }
        public virtual Expirience WorkExpirience { get; set; }
        public virtual string Payment { get; set; }       
        public virtual string KeyWords { get; set; }
        public virtual DateTime CloseTime { get; set; }
        public virtual bool IsOpen { get; set; }
        public virtual User UserId { get; set; }
    }
}