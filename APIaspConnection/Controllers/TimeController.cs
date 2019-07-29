using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using APIaspConnection.DataBase; 

namespace APIaspConnection.Controllers
{
    public class TimeController : ApiController
    {
        private admissDb db = new admissDb(); 

        //api/Time
        public DateTime Get ()
        {
            var path = db.Persons.Max(x => x.StartData);

            return path; 
        }
    }
}
