using RecruitmentAgency.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecruitmentAgency.Models.ViewModels
{
    public class VacancyViewModel 
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [Display(Name = "Компания")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [Display(Name = "Опыт работы")]
        public Expirience WorkExpirience { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [Display(Name = "Требования")]
        public string Requirements { get; set; }

        [Display(Name = "Зарплата")]
        [RegularExpression(@"[0-9]*", ErrorMessage = "Поле может содержать только цифры")]
        public string Payment { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [Display(Name = "Ключевые навыки")]
        [RegularExpression(@"([А-Яа-яA-Za-z]+\s*)+[,]+([А-Яа-яA-Za-z]*\s*)+([А-Яа-яA-Za-z,.]*\s*)*", ErrorMessage = "Введите хотя бы 2 ключевых навыка, через запятую")]
        public string KeyWords { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [Display(Name = "Дата закрытия")]
        public DateTime CloseTime { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [Display(Name = "Вакансия открыта")]
        public  bool IsOpen { get; set; }
    }
}