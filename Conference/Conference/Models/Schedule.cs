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
        [Display(Name = "Data e Hora")]
        public DateTime DateHour { get; set; }
        [Display(Name = "Local da Palestra")]
        public string Host { get; set; }
        [Display(Name = "Título da Palestra")]
        public string TitlePresentation { get; set; }
    }
}