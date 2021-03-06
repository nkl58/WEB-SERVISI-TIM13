namespace TicketingSystem.DAL.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TicketingSystem.DAL.Models.TicketingSystemDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TicketingSystem.DAL.Models.TicketingSystemDBContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "User"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "User" };

                manager.Create(role);
            }

            var userStore = new UserStore<TicketingSystemUser>(context);
            var userManager = new UserManager<TicketingSystemUser>(userStore);

            if (!context.Users.Any(u => u.UserName == "admin"))
            {
                var user = new TicketingSystemUser { Id = "admin", UserName = "admin", Email = "admin@yahoo.com", FirstName = "Admin", LastName = "Adminovic", PasswordHash = TicketingSystemUser.HashPassword("admin"), UserType = TicketingSystemUser.UserTypes.ADMINISTRATOR };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "Admin");
            }
            if (!context.Users.Any(u => u.UserName == "admin1"))
            {
                var user = new TicketingSystemUser { Id = "admin1", UserName = "admin1", Email = "admin1@yahoo.com", FirstName = "Admin1", LastName = "Adminovic1", PasswordHash = TicketingSystemUser.HashPassword("admin1"), UserType = TicketingSystemUser.UserTypes.ADMINISTRATOR };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "Admin");
            }
            if (!context.Users.Any(u => u.UserName == "vlada"))
            {
                var user = new TicketingSystemUser { Id = "vlada", UserName = "vlada", Email = "vlada@yahoo.com", FirstName = "Vladimir", LastName = "Ivkovic", PasswordHash = TicketingSystemUser.HashPassword("vlada"), UserType = TicketingSystemUser.UserTypes.USER };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "User");
            }
            if (!context.Users.Any(u => u.UserName == "aleksa"))
            {
                var user = new TicketingSystemUser { Id = "aleksa", UserName = "aleksa", Email = "aleksa@yahoo.com", FirstName = "Aleksa", LastName = "Mirkovic", PasswordHash = TicketingSystemUser.HashPassword("aleksa"), UserType = TicketingSystemUser.UserTypes.USER };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "User");
            }
            if (!context.Users.Any(u => u.UserName == "nikola"))
            {
                var user = new TicketingSystemUser { Id = "nikola", UserName = "nikola", Email = "nikola@yahoo.com", FirstName = "Nikola", LastName = "Todorovic", PasswordHash = TicketingSystemUser.HashPassword("nikola"), UserType = TicketingSystemUser.UserTypes.USER };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "User");
            }
            if (!context.Users.Any(u => u.UserName == "petPet"))
            {
                var user = new TicketingSystemUser { Id = "petPet", UserName = "petPet", Email = "petPet@yahoo.com", FirstName = "Petar", LastName = "Petrovic", PasswordHash = TicketingSystemUser.HashPassword("petPet"), UserType = TicketingSystemUser.UserTypes.USER };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "User");
            }
            if (!context.Users.Any(u => u.UserName == "jovJov"))
            {
                var user = new TicketingSystemUser { Id = "jovJov", UserName = "jovJov", Email = "jovJov@yahoo.com", FirstName = "Jovan", LastName = "Jovanovic", PasswordHash = TicketingSystemUser.HashPassword("jovJov"), UserType = TicketingSystemUser.UserTypes.USER };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "User");
            }
            if (!context.Users.Any(u => u.UserName == "milMil"))
            {
                var user = new TicketingSystemUser { Id = "milMil", UserName = "milMil", Email = "milMil@yahoo.com", FirstName = "Milos", LastName = "Milosevic", PasswordHash = TicketingSystemUser.HashPassword("milMil"), UserType = TicketingSystemUser.UserTypes.USER };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "User");
            }
            if (!context.Users.Any(u => u.UserName == "nikNik"))
            {
                var user = new TicketingSystemUser { Id = "nikNik", UserName = "nikNik", Email = "nikNik@yahoo.com", FirstName = "Nikola", LastName = "Nikolic", PasswordHash = TicketingSystemUser.HashPassword("nikNik"), UserType = TicketingSystemUser.UserTypes.USER };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "User");
            }
            if (!context.Users.Any(u => u.UserName == "stefStef"))
            {
                var user = new TicketingSystemUser { Id = "stefStef", UserName = "stefStef", Email = "stefStef@yahoo.com", FirstName = "Stefan", LastName = "Stefanovic", PasswordHash = TicketingSystemUser.HashPassword("stefStef"), UserType = TicketingSystemUser.UserTypes.USER };
                userManager.Create(user);
                userManager.AddToRole(user.Id, "User");
            }
            context.SaveChanges();

            context.Projects.AddOrUpdate(
                new Project { ProjectName = "Web Servisi", ProjectCode = "WS", ProjectDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." },
                new Project { ProjectName = "XML", ProjectCode = "XML", ProjectDescription = "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat." },
                new Project { ProjectName = "Inzenjering informacionih sistema", ProjectCode = "IIS", ProjectDescription = "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur." },
                new Project { ProjectName = "Sistemi baza podataka", ProjectCode = "SBP", ProjectDescription = "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum." },
                new Project { ProjectName = "Diplomski", ProjectCode = "DPL", ProjectDescription = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam." }
            );

            context.SaveChanges();

            context.Tickets.AddOrUpdate(
                new Ticket { TaskCreated = DateTime.Now, ProjectID = 1, TaskName = "Projekat", TaskDescription = "Eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.", TaskFrom = DateTime.Today, TaskUntil = DateTime.Today.AddDays(5), TaskPriority = "Blocker", TaskStatus = "In Progress", UserCreatedID = "admin", UserAssignedID = "vlada" },
                new Ticket { TaskCreated = DateTime.Now, ProjectID = 1, TaskName = "Usmeni", TaskDescription = "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.", TaskFrom = DateTime.Today, TaskUntil = DateTime.Today.AddDays(8), TaskPriority = "Critical", TaskStatus = "Verify", UserCreatedID = "admin1" },
                new Ticket { TaskCreated = DateTime.Now, ProjectID = 2, TaskName = "Projekat", TaskDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", TaskFrom = DateTime.Today.AddDays(5), TaskUntil = DateTime.Today.AddDays(10), TaskPriority = "Blocker", TaskStatus = "To Do", UserCreatedID = "admin", UserAssignedID = "nikola" },
                new Ticket { TaskCreated = DateTime.Now, ProjectID = 2, TaskName = "Usmeni", TaskDescription = "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.", TaskFrom = DateTime.Today.AddDays(15), TaskUntil = DateTime.Today.AddDays(25), TaskPriority = "Critical", TaskStatus = "To Do", UserCreatedID = "admin1" },
                new Ticket { TaskCreated = DateTime.Now, ProjectID = 3, TaskName = "Projekat Programerski", TaskDescription = "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", TaskFrom = DateTime.Today.AddDays(10), TaskUntil = DateTime.Today.AddDays(18), TaskPriority = "Blocker", TaskStatus = "In Progress", UserCreatedID = "admin", UserAssignedID = "vlada" },
                new Ticket { TaskCreated = DateTime.Now, ProjectID = 3, TaskName = "Projekat Menadzerski 1", TaskDescription = "Eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.", TaskFrom = DateTime.Today.AddDays(6), TaskUntil = DateTime.Today.AddDays(8), TaskPriority = "Major", TaskStatus = "To Do", UserCreatedID = "admin" },
                new Ticket { TaskCreated = DateTime.Now, ProjectID = 3, TaskName = "Projekat Menadzerski 2", TaskDescription = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam.", TaskFrom = DateTime.Today.AddDays(8), TaskUntil = DateTime.Today.AddDays(10), TaskPriority = "Minor", TaskStatus = "To Do", UserCreatedID = "admin1", UserAssignedID = "aleksa" },
                new Ticket { TaskCreated = DateTime.Now, ProjectID = 3, TaskName = "Specifikacija zahteva", TaskDescription = "Opsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.", TaskFrom = DateTime.Today.AddDays(-30), TaskUntil = DateTime.Today.AddDays(-20), TaskPriority = "Critical", TaskStatus = "Done", UserCreatedID = "admin" },
                new Ticket { TaskCreated = DateTime.Now, ProjectID = 4, TaskName = "Kolokvijum", TaskDescription = "Veritatis et quasi architecto beatae vitae dicta sunt explicabo.", TaskFrom = DateTime.Today.AddDays(8), TaskUntil = DateTime.Today.AddDays(8), TaskPriority = "Critical", TaskStatus = "To Do", UserCreatedID = "admin1" },
                new Ticket { TaskCreated = DateTime.Now, ProjectID = 4, TaskName = "Projekat", TaskDescription = "Lo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.", TaskFrom = DateTime.Today.AddDays(8), TaskUntil = DateTime.Today.AddDays(10), TaskPriority = "Blocker", TaskStatus = "In Progress", UserCreatedID = "admin" },
                new Ticket { TaskCreated = DateTime.Now, ProjectID = 4, TaskName = "Usmeni II", TaskDescription = "Eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.", TaskFrom = DateTime.Today, TaskUntil = DateTime.Today.AddDays(5), TaskPriority = "Blocker", TaskStatus = "In Progress", UserCreatedID = "admin1", UserAssignedID = "aleksa" },
                new Ticket { TaskCreated = DateTime.Now, ProjectID = 5, TaskName = "Prijava", TaskDescription = "Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur.", TaskFrom = DateTime.Today.AddDays(35), TaskUntil = DateTime.Today.AddDays(45), TaskPriority = "Minor", TaskStatus = "Verify", UserCreatedID = "admin" },
                new Ticket { TaskCreated = DateTime.Now, ProjectID = 5, TaskName = "Izrada", TaskDescription = "Inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo.", TaskFrom = DateTime.Today, TaskUntil = DateTime.Today.AddDays(5), TaskPriority = "Critical", TaskStatus = "InProgress", UserCreatedID = "admin" },
                new Ticket { TaskCreated = DateTime.Now, ProjectID = 5, TaskName = "Odbrana", TaskDescription = "Autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur", TaskFrom = DateTime.Today, TaskUntil = DateTime.Today.AddDays(5), TaskPriority = "Blocker", TaskStatus = "To Do", UserCreatedID = "admin1", UserAssignedID = "nikola" }
            );

            context.SaveChanges();
        }
    }
}
