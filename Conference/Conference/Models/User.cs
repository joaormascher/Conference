using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Conference.Models
{
    public class User
    {
        [Key]
        [Display(Name ="Login")]
        [Required(ErrorMessage ="Informe o nome de usuário", AllowEmptyStrings = false)]
        public string Username { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage ="Informe a senha de usuário", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Password { get; set; }
        public int Kind { get; set; }
    }
}