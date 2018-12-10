using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Informe usuário")]
        public string Usuario { get; set; }

        [Required(ErrorMessage ="Informe senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required]
        public bool LembrarMe { get; set; }
    }
}