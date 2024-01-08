using ComplectPlus.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace ComplectPlus.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {


            using var context = new
                ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>> ());
            if (context.Staffs.Any())
            {
                return;
            }

            //Сomponent сomponent1 = new Сomponent
            //{ ComponentsName = "KFA2 GeForce 210",  YearRelease = 2021, CategoryId = 1, ManufacturerId = 1,Description = "нет"  };
            //context.Components.Add(сomponent1);
            //context.SaveChanges();

            Category category1 = new Category() { CategoryName ="Оперативная память"};
            context.Categories.Add(category1);
            context.SaveChanges();

            Manufacturer manufacturer1 = new Manufacturer() { ManufacturerName = "AMD", ManufacturerPName = "Advanced Micro Devices" };
            context.Manufacturers.Add(manufacturer1);
            context.SaveChanges();

            Сomponent сomponent2 = new Сomponent
            { ComponentsName = "AMD Radeon R9 Gamer Series", YearRelease = 2020, CategoryId = category1.CategoryId, ManufacturerId = manufacturer1.ManufacturerId, Description = "Оперативная память AMD Radeon R9 Gamer Series представляет из себя 16-гигабайтный комплект, состоящий из пары 8-гигабайтных модулей DDR4" };
            context.Components.Add(сomponent2);
            context.SaveChanges();

            Category category2 = new Category() { CategoryName = "Блоки питания" };
            context.Categories.Add(category2);
            context.SaveChanges();

            Manufacturer manufacturer2 = new Manufacturer() {ManufacturerName= "DEEPCOOL", ManufacturerPName= "DeepCool Industries" };
            context.Manufacturers.Add(manufacturer2);
            context.SaveChanges();

            Manufacturer manufacturer3 = new Manufacturer() { ManufacturerName = "Intel", ManufacturerPName = "Intel Corp." };
            context.Manufacturers.Add(manufacturer3);
            context.SaveChanges();

            Сomponent сomponent3 = new Сomponent
            { ComponentsName = "DeepCool PX1000G", YearRelease = 2020, CategoryId = category2.CategoryId, ManufacturerId = manufacturer2.ManufacturerId, Description = "Блок питания DeepCool PX1000G номинальной мощностью 1000 Вт, используется разъем 20+4 pin в качестве интерфейса питания" };
            context.Components.Add(сomponent3);
            context.SaveChanges();

            Category category3 = new Category() { CategoryName = "Процессоры" };
            context.Categories.Add(category3);
            context.SaveChanges();

            Position position1 = new Position { PositionName = "Администратор" };
            context.Positions.Add(position1);
            context.SaveChanges();

            Position position2 = new Position { PositionName = "Директор" };
            context.Positions.Add(position2);
            context.SaveChanges();

            string[] roles = new string[] { "Administrator", "Guest" };

            foreach (string role in roles)
            {
                CreateRole(serviceProvider, role);
            }

            CustomUser customUser1 = new() { Age = 24, Surname = "Barinova", Ima = "Daria", Secsurname = "Alexandrovna", UserName = "barinova@mail.ru", Email = "barinova@mail.ru", PositionId = position1.PositionId };

            AddUserToRole(serviceProvider, "Password123!", "Administrator", customUser1);

            CustomUser customUser2 = new() { Age = 32, Surname = "Ivanov", Ima = "Ivan", Secsurname = "Ivanovich", UserName = "ivanov@mail.ru", Email = "ivanov@mail.ru", PositionId = position2.PositionId };

            AddUserToRole(serviceProvider, "Password123!", "Administrator", customUser2);


            Doljnost doljnost1 = new Doljnost { DoljnostName = "Кладовщик" };
            context.Doljnosts.Add(doljnost1);
            context.SaveChanges();

            Staff staff1 = new Staff() {Name="Сергей",Surname="Петров",DoljnostId=doljnost1.DoljnostId };
            context.Staffs.Add(staff1);
            context.SaveChanges();
            Staff staff3 = new Staff() { Name = "Дмитрий", Surname = "Захаров", DoljnostId = doljnost1.DoljnostId };
            context.Staffs.Add(staff3);
            context.SaveChanges();

            Doljnost doljnost2 = new Doljnost { DoljnostName = "Завхоз" };
            context.Doljnosts.Add(doljnost2);
            context.SaveChanges();

            Staff staff2 = new Staff() { Name = "Владимир",Surname = "Сидоров",  DoljnostId = doljnost2.DoljnostId };
            context.Staffs.Add(staff2);
            context.SaveChanges();




        }

        private static void CreateRole(IServiceProvider serviceProvider, string roleName)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            Task<bool> roleExists = roleManager.RoleExistsAsync(roleName);
            roleExists.Wait();

            if (!roleExists.Result)
            {
                Task<IdentityResult> roleResult = roleManager.CreateAsync(new IdentityRole(roleName));
                roleResult.Wait();
            }

        }

        private static void AddUserToRole(IServiceProvider serviceProvider, string userPwd, string roleName, CustomUser customUser)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<CustomUser>>();

            Task<CustomUser> checkAppUser = userManager.FindByEmailAsync(customUser.Email); ;

            checkAppUser.Wait();

            if (checkAppUser.Result == null)
            {

                Task<IdentityResult> taskCreateAppUser = userManager.CreateAsync(customUser, userPwd);

                taskCreateAppUser.Wait();

                if (taskCreateAppUser.Result.Succeeded)
                {
                    Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(customUser, roleName);
                    newUserRole.Wait();

                }
            }
        }
    }
}
