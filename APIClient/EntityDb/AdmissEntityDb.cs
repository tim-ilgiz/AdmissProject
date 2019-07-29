namespace APIClient.EntityDb
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class AdmissEntityDb : DbContext
    {
        // Контекст настроен для использования строки подключения "AdmissEntityDb" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "APIClient.EntityDb.AdmissEntityDb" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "AdmissEntityDb" 
        // в файле конфигурации приложения.
        public AdmissEntityDb()
            : base("name=AdmissEntityDb")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<AdmissEntityDb>());
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AdmissEntityDb>());
        }
        //Add-Migration "Migration" -StartUpProjectName Admiss -ProjectName Admiss
        public virtual DbSet<AdmissObject> AdmissObjects { get; set; }
    }
}