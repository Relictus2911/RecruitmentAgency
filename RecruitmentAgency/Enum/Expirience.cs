using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RecruitmentAgency.Enum
{
    public enum Expirience
    {
        [Display(Name = "Меньше 1 года")]
        LessThanYear,
        [Display(Name = "От 1 до 3 лет")]
        OneToThree,
        [Display(Name = "От 3 до 5 лет")]
        ThreeToFive,
        [Display(Name = "Более 5 лет")]
        MoreThanFive,
    }
}