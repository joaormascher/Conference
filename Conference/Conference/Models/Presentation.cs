using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Conference.Models
{
    public class Presentation
    {
        [Key]
        public int Pid { get; set; } //Presentation ID
        public string Title { get; set; } //Presentation's title
        public string Abstract { get; set; } //Presentation's Abstract
        public string Kind { get; set; } //Presentation type: keynote or track
        public string Presenter { get; set; } //Presenter --> get from presenter's table
        //public string testeEmail { get; set; }
        //public int Presenter { get; set; }
    }
}