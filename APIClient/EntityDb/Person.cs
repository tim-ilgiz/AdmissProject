using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace APIClient.EntityDb
{
    [DataContract(Namespace = "MyDataContract")]
    public class Person
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        [DisplayName("Имя")]
        public string FirstName { get; set; }

        [DataMember]
        [DisplayName("Фамилия")]
        public string LastName { get; set; }

        [DataMember]
        [DisplayName("Отчество")]
        public string Patronymic { get; set; }

        [DataMember]
        [DisplayName("Дата выдачи")]
        public DateTime StartData { get; set; }

        [DataMember]
        [DisplayName("Дата окончания")]
        public DateTime EndData { get; set; }

        [DataMember]
        [DisplayName("Секретный ключ")]
        public int SecretNumberCode { get; set; }
    }
}