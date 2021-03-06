﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketingSystem.DAL.Models;

namespace TicketingSystem.DTOs
{
    public class ProjectDTO
    {
        public int ProjectID { get; set; }

        public String ProjectName { get; set; }

        public String ProjectCode { get; set; }

        public String ProjectDescription { get; set; }

        public List<TaskDto> Tasks { get; set; }

        public ProjectDTO(Project p)
        {
            ProjectID = p.ProjectID;
            ProjectName = p.ProjectName;
            ProjectDescription = p.ProjectDescription;
            ProjectCode = p.ProjectCode;

            Tasks = new List<TaskDto>();

            if(p.Tasks != null)
            {
                foreach (Ticket t in p.Tasks)
                {
                    Tasks.Add(new TaskDto(t));
                }
            }
        }

        public ProjectDTO() { }
    }
}