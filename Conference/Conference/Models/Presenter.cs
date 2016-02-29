using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Conference.Models {
    public class Presenter {
        [Key]
        public int Id { get; set; } //Presenter ID Primary Key
        public string Name { get; set; } //Presenter Name
        public string Email { get; set; } //Presenter Email
        public string Title { get; set; } //Presentation Title [Foreing Key]
        public string Biography { get; set; }  //Presenter Biography
        public string UserOwner { get; set; } //UserOwner (User who will manage the presentations
    }
}