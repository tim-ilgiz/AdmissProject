using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Admiss.ViewModel;
using APIClient; 
using DB = APIClient.EntityDb;

namespace Admiss.Model
{
    public class InfoModel
    {
        public static bool IsTrueCode (int number)
        {
            using (DB.AdmissEntityDb db = new DB.AdmissEntityDb())
            {    
                return db.AdmissObjects.Any (x => x.SecretNumberCode == number);
            }
        }
    }
}
