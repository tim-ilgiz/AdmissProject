using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AdminAPIAdmiss.DataBase
{
    public class Company
    {
        public string Id { get; set; }
        public string Email { get; set; }

        [DisplayName ("Название компании")]
        public string NameCompany { get; set; }

        [DisplayName("О нас")]
        public string AboutUs { get; set; }

        [DisplayName("Общая информация")]

        public string CompanyInfo { get; set; }

        [DisplayName("Логотив")]
        public string Logo { get; set; }

        [DisplayName("Контакты")]
        public string Phone { get; set; }

        public string Image { get; set; }

    }
}