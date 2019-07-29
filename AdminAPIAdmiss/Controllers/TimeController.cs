using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AdminAPIAdmiss.DataBase;
using AdminAPIAdmiss.Models;

namespace AdminAPIAdmiss.Controllers
{
    public class TimeController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext(); 

        //api/Time
        public DateTime Get ()
        {
            var path = db.Persons.Max(x => x.StartData);

            return path; 
        }
    }
}
