using System.ComponentModel.DataAnnotations;

namespace Conference.Models {
    public class Presenter {
        [Key]
        public int Id { get; set; } //Presenter ID Primary Key
        [Display(Name = "Nome")]
        public string Name { get; set; } //Presenter Name
        [Display(Name = "E-mail")]
        public string Email { get; set; } //Presenter Email
        [Display(Name = "Título")]
        public string Title { get; set; } //Presentation Title [Foreing Key]
        [Display(Name = "Biografia")]
        public string Biography { get; set; }  //Presenter Biography
        [Display(Name = "Responsável pelo palestrante")]
        public string UserOwner { get; set; } //UserOwner (User who will manage the presentations)
    }
}