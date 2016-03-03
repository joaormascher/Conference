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
        [Display(Name = "Título")]
        public string Title { get; set; } //Presentation's title
        [Display(Name = "Resumo")]
        public string Abstract { get; set; } //Presentation's Abstract
        [Display(Name = "Tipo de Palestra")]
        public string Kind { get; set; } //Presentation type: keynote or track
        [Display(Name = "Palestrante")]
        public int PresenterId { get; set; }
        //public string testeEmail { get; set; }
        //public int Presenter { get; set; }
    }
}