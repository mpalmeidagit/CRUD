using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD.Models
{
    public class PerfilModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Campo nome é obrigatório!")]
        public String Nome { get; set; }


    }

}