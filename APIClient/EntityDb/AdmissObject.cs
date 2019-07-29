using System;
using System.Runtime.Serialization;

namespace APIClient.EntityDb
{
    [DataContract(Namespace = "MyDataContract")]
    public class AdmissObject
    {
        public string Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Patronymic { get; set; }
        [DataMember]
        public DateTime StartData { get; set; }
        [DataMember]
        public DateTime EndData { get; set; }
        [DataMember]
        public int SecretNumberCode { get; set; }
        [DataMember]
        public string PhotoPass { get; set; }
        [DataMember]
        public string PhotoDrive { get; set; }
    }
}