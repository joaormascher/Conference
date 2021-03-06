﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Conference.Models
{
    public class ConferenceContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ConferenceContext() : base("name=ConferenceContext")
        {
        }

        public System.Data.Entity.DbSet<Conference.Models.Presenter> Presenters { get; set; }

        public System.Data.Entity.DbSet<Conference.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<Conference.Models.Schedule> Schedules { get; set; }

        public System.Data.Entity.DbSet<Conference.Models.Presentation> Presentations { get; set; }
    }
}
