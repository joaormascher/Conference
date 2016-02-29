using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Conference.Models
{
    public class Schedule
    {
        [Key]
        public int IdSchedule { get; set; }
        public DateTime DateHour { get; set; }
        public string Host { get; set; }

    }
}