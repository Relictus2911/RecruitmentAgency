using RecruitmentAgency.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentAgency.Models.ViewModels
{
    public class CandidateViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "Введите ФИО")]
        [Display(Name = "ФИО")]
        [RegularExpression(@"[^\/:*?<>|""0-9]+", ErrorMessage = "ФИО может содержать только буквы")]
        public virtual string FullName { get; set; }

        [Required(ErrorMessage = "Выберите дату рождения")]
        [Display(Name = "Дата рождения")]
        public virtual DateTime Birthday { get; set; }

        [Display(Name = "Фото")]
        public virtual byte[] Photo { get; set; }

        [RegularExpression(@"([А-Яа-яA-Za-z]+\s*)+[,]+([А-Яа-яA-Za-z]*\s*)+([А-Яа-яA-Za-z,.]*\s*)*", ErrorMessage = "Введите хотя бы 2 ключевых навыка, через запятую")]
        [Required(ErrorMessage = "Введите ключевые навыки, через запятую")]
        [Display(Name = "Ключевые навыки")]
        public virtual string KeyWords { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [Display(Name = "Опыт работы")]
        public virtual Expirience WorkExpirience { get; set; }

    }
}