using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using AdminAPIAdmiss.DataBase; 

namespace AdminAPIAdmiss.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public string CompanyId{ get; set; }

        public string AboutUs { get; set; }

        public string CompanyInfo { get; set; }

        public string Logo { get; set; }

        public string Image { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("admissDb", throwIfV1Schema: false)
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ApplicationDbContext>());
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
        }

        public virtual DbSet<Person> Persons { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class AppDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        //protected override void Seed(admissDb context)
        public AppDbInitializer (ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем две роли
            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "user" };

            // добавляем роли в бд
            roleManager.Create(role1);
            roleManager.Create(role2);

            // создаем пользователей
            var admin = new ApplicationUser { Email = "admin@mail.ru", UserName = "admin@mail.ru" };
            string password = "Dkflbvbh19900@";
            var result = userManager.Create(admin, password);

            // если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
            }
        }
        #region по Метанит
        //protected override void Seed(ApplicationDbContext context)
        //{
        //    var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

        //    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

        //    // создаем две роли
        //    var role1 = new IdentityRole { Name = "admin" };
        //    var role2 = new IdentityRole { Name = "user" };

        //    // добавляем роли в бд
        //    roleManager.Create(role1);
        //    roleManager.Create(role2);

        //    // создаем пользователей
        //    var admin = new ApplicationUser { Email = "admin@mail.ru", UserName = "admin@mail.ru" };
        //    string password = "Dkflbvbh19900@";
        //    var result = userManager.Create(admin, password);

        //    // если создание пользователя прошло успешно
        //    if (result.Succeeded)
        //    {
        //        // добавляем для пользователя роль
        //        userManager.AddToRole(admin.Id, role1.Name);
        //        userManager.AddToRole(admin.Id, role2.Name);
        //    }

        //    base.Seed(context);
        //}
        #endregion
    }
}