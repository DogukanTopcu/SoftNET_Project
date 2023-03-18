using System;
using System.Linq;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using SoftNET_Project.Models;

namespace SoftNET_Project.Data
{
    public class DbInitializer
    {
        //IServiceProvider serviceProvider
        public static void Initialize(SystemContext context)
        {
            context.Database.EnsureCreated();


            if (context.Users.Any())
            {
                return;
            }

            //var roles = serviceProvider.GetRequiredService<RoleManager<Roles>>();
            //await roles.CreateAsync(new Roles { Name="Admin" });

            
            context.Role.Add(new Role { Name = "Admin" });
            context.SaveChanges();

            //var users = new User[]
            //{
            //new User{Name="Carson",NickName="Alexander",Role="user", Password="123456"},
            //new User{Name="Carson",NickName="Alexander",Role="user", Password="123456"},
            //new User{Name="Carson",NickName="Alexander",Role="user", Password="123456"},
            //new User{Name="Carson",NickName="Alexander",Role="user", Password="123456"},
            //new User{Name="Carson",NickName="Alexander",Role="user", Password="123456"},
            //};
            //foreach (User u in users)
            //{
            //    context.Users.Add(u);
            //}
            //context.SaveChanges();

        }
    }
}
