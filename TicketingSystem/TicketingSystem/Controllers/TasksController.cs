﻿using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TicketingSystem.DAL;
using TicketingSystem.DAL.Models;
using TicketingSystem.DTOs;
//Aleksa prvi commit
namespace TicketingSystem.Controllers
{
    //[Authorize(Users = "qwerty")]
    public class TasksController : ApiController
    {
        private TicketingSystemDBContext db = new TicketingSystemDBContext();
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: api/Tasks
        public async Task<IHttpActionResult> GetTasks()
        {
            bool isAdmin = await UserManager.IsInRoleAsync(User.Identity.Name, "Admin");

            if (isAdmin)
            {
                return Ok(db.Tickets);
            }
            return Ok((from t in db.Tickets
                    where t.UserAssignedID == User.Identity.Name
                    select t).AsQueryable());
        }

        private static readonly Expression<Func<DAL.Models.Ticket, TaskDto>> AsTaskDto =
            x => new TaskDto
            {
                TaskName = x.TaskName,
                TaskFrom = x.TaskFrom,
                TaskUntil = x.TaskUntil,
                TaskPriority = x.TaskPriority,
                TaskDescription = x.TaskDescription,
                TaskStatus = x.TaskStatus,
                UserAssigned = x.UserAssigned.UserName,
                UserCreated = x.UserCreated.UserName,
                TicketId = x.TicketID





            };

        [Route("api/Projects/{projectId}/tasks")]
        public async Task<IQueryable<DTOs.TaskDto>> GetTasksOfProject(int projectId)
        {
            var data = (from p in db.Projects.Include(p => p.AssignedUsers)
                        where p.AssignedUsers.Any(u => u.Email == User.Identity.Name) && p.ProjectID == projectId
                        select p).Count();

            bool isAdmin = await UserManager.IsInRoleAsync(User.Identity.Name, "Admin");

            if (data == 0 && !isAdmin)
            {
                return null;
            }

            return db.Tickets.Include(b => b.Project)
                .Where(b => b.ProjectID == projectId)
                .Select(AsTaskDto);
        }


        [Route("api/Projects/{projectId}/tasks/{taskId}")]
        [ResponseType(typeof(DAL.Models.Ticket))]
        public async Task<IHttpActionResult> GetTaskDetails(int projectId, int taskId)
        {
            var data = (from p in db.Projects.Include(p => p.AssignedUsers)
                        where p.AssignedUsers.Any(u => u.Id == User.Identity.Name) && p.ProjectID == projectId
                        select p).Count();

            bool isAdmin = await UserManager.IsInRoleAsync(User.Identity.Name, "Admin");

            if (data == 0 && !isAdmin)
            {
                return NotFound();
            }

            DAL.Models.Ticket task = await db.Tickets.Include(t => t.Comments).Include(t => t.Changes).SingleOrDefaultAsync(t => t.TicketID == taskId);
            //DAL.Models.Ticket task = await db.Tickets.Include(t => t.Changes).SingleOrDefaultAsync(t => t.TicketID == taskId);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // GET: api/Tasks/5
        [ResponseType(typeof(DAL.Models.Ticket))]
        public async Task<IHttpActionResult> GetTask(int id)
        {
            DAL.Models.Ticket task = await db.Tickets.Include(t => t.Comments).Include(t => t.Changes).SingleOrDefaultAsync(t => t.TicketID == id);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // PUT: api/Projects/5/Tasks/5
        [Route("api/Projects/{projectId}/tasks/{taskId}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTask(int projectId, int taskId, DAL.Models.Ticket task)
        {
            bool isAdmin = await UserManager.IsInRoleAsync(User.Identity.Name, "Admin");

            if (!isAdmin)
            {
                var data = (from p in db.Projects.Include(p => p.AssignedUsers)
                            where p.AssignedUsers.Any(u => u.Id == User.Identity.Name) && p.ProjectID == projectId
                            select p).Count();
                if (data == 0)
                {
                    return StatusCode(HttpStatusCode.Forbidden);
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (taskId != task.TicketID || projectId != task.ProjectID)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }

            db.Entry(task).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(taskId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Projects/5/Tasks
        [Route("api/Projects/{projectId}/tasks", Name = "PostTask")]
        [ResponseType(typeof(DAL.Models.Ticket))]
        public async Task<IHttpActionResult> PostTask(int projectId, DAL.Models.Ticket task)
        {
            bool isAdmin = await UserManager.IsInRoleAsync(User.Identity.Name, "Admin");

            if (!isAdmin)
            {
                var data = (from p in db.Projects.Include(p => p.AssignedUsers)
                            where p.AssignedUsers.Any(u => u.Id == User.Identity.Name) && p.ProjectID == projectId
                            select p).Count();
                if (data == 0)
                {
                    return StatusCode(HttpStatusCode.Forbidden);
                }
            }

            if (projectId != task.ProjectID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cnt = (from p in db.Projects.Include(p => p.AssignedUsers)
                       where p.AssignedUsers.Any(u => u.Id == task.UserAssignedID) && p.ProjectID == projectId
                        select p).Count();
            if (cnt == 0)
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            

            db.Tickets.Add(task);
            await db.SaveChangesAsync();

            return CreatedAtRoute("PostTask", new { id = task.TicketID }, task);
        }

        // DELETE:  api/Projects/5/Tasks/5
        [Route("api/Projects/{projectId}/tasks/{taskId}")]
        [ResponseType(typeof(DAL.Models.Ticket))]
        public async Task<IHttpActionResult> DeleteTask(int taskId, int projectId)
        {
            bool isAdmin = await UserManager.IsInRoleAsync(User.Identity.Name, "Admin");

            if (!isAdmin)
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }

            DAL.Models.Ticket task = await db.Tickets.FindAsync(new object[] { taskId, projectId });
            if (task == null || task.ProjectID != projectId)
            {
                return NotFound();
            }

            db.Tickets.Remove(task);
            await db.SaveChangesAsync();

            return Ok(task);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TaskExists(int id)
        {
            return db.Tickets.Count(e => e.TicketID == id) > 0;
        }
    }
}