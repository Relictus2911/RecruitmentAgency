using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Reflection;

namespace RecruitmentAgency.Enum
{
    public enum Role  
    {
        [Display(Name = "Я работник")]
        candidate,
        [Display(Name = "Я работодатель")]
        employer,
        [Display(Name = "Администратор")]
        admin
    }
}