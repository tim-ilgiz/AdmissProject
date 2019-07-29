//namespace AdminAPIAdmiss.DataBase
//{
//    using System;
//    using System.Data.Entity;
//    using System.Linq;

//    public class admissDb : DbContext
//    {
//        // Контекст настроен для использования строки подключения "admissDb" из файла конфигурации  
//        // приложения (App.config или Web.cD:\Projects\admissproject\AdminAPIAdmiss\DataBase\admissDb.csonfig). По умолчанию эта строка подключения указывает на базу данных 
//        // "APIaspConnection.DataBase.admissDb" в экземпляре LocalDb. 
//        // 
//        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "admissDb" 
//        // в файле конфигурации приложения.

//        //Add-Migration "MigrationX" -StartUpProjectName AdminAPIAdmiss -ProjectName AdminAPIAdmiss
//        //Update-Database -StartUpProjectName AdminAPIAdmiss -ProjectName AdminAPIAdmiss
//        public admissDb()
//            : base("name=admissDb")
//        {
//            Database.SetInitializer(new CreateDatabaseIfNotExists<admissDb>());
//            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<admissDb>());
//        }

//        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
//        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

//        public virtual DbSet<Person> Persons{ get; set; }
//    }
//}