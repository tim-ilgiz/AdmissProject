using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using APIClient.EntityDb;
using System.Web;
using System.Globalization;
using System.IO;

namespace APIClient
{
    public class Program
    {
        //Инфа для подключения 
        //public const string URI = "https://localhost:44301";
        public const string URI = "http://195.133.1.197:52292";
        private static string token; 
        public const string login = "Admin";
        public const string password = "Admin";
        static HttpClient client = new HttpClient();
        static string l = "";
        static bool res;
        public static DateTime UpdateDbDate { get; set; }

        static void Main(string[] args)
        {
            //foreach (var i in GetHttpDb())
            //{
            //    Console.WriteLine(i.LastName); 
            //}
            //Console.ReadKey(); 
        }

        public static IEnumerable<Person> GetHttpDb()
        {
            HttpResponseMessage response = null;
            using (var client = new HttpClient())
            {
                var str = client.GetAsync(URI + "/api/People").Result;
                var i = str.Content.ReadAsAsync<IEnumerable<Person>>().Result;
                return i; 
            }
        }

        public static DateTime GetTime()
        {
            HttpResponseMessage response = null;
            using (var client = new HttpClient())
            {
                response = client.GetAsync(URI + "/api/Time").Result;
                return response.Content.ReadAsAsync<DateTime>().Result;
            }
        }

        //public async static void TestGet()  
        //{
        //    Person element = null; 
        //    HttpResponseMessage response = null;
        //    using (var client = new HttpClient())
        //    {
        //        response = await client.GetAsync(URI + "/api/people");
        //        if (response.IsSuccessStatusCode)
        //        {
        //            element = await response.Content.ReadAsAsync<Person>();
        //            Console.WriteLine("имя первого человека - " + element.FirstName);
        //        }
        //        else
        //        {
        //            Console.Write("Ответ не приходит");
        //        }
        //    }
        //}

        static async Task<Uri> CreateProductAsync(AdmissObject product)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(URI + "/api/People", product);
            response.EnsureSuccessStatusCode();
            // return URI of the created resource.
            return response.Headers.Location;
        }
    }
}
