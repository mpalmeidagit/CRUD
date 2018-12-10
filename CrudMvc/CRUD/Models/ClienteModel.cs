using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD.Models
{
   
    public class ClienteModel
    {
        private static ClienteModel objCliente = null;

        public ClienteModel()
        {

        }

        public static ClienteModel getInstancia()
        {
            if (objCliente == null)
            {
                objCliente = new ClienteModel();
            }
            return objCliente;
        }

        public int Id { get; set; }

        [Required(ErrorMessage ="Campo nome é obrigatório.")]
        [MaxLength(60, ErrorMessage ="O nome pode ter no máximo 100 caracteres.")]
        public String Nome { get; set; }

        [Required(ErrorMessage = "Campo email é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O email pode ter no máximo 50 caracteres.")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Campo cpf é obrigatório.")]
        [MaxLength(20, ErrorMessage = "O cpf pode ter no máximo 20 caracteres.")]
        public String CPF { get; set; }

        [Required(ErrorMessage = "Campo telefone é obrigatório.")]
        [MaxLength(20, ErrorMessage = "O telefone pode ter no máximo 20 caracteres.")]
        public String Telefone { get; set; }

        [Required(ErrorMessage = "Campo cep é obrigatório.")]
        [MaxLength(12, ErrorMessage = "O cep pode ter no máximo 12 caracteres.")]
        public String CEP { get; set; }

        [Required(ErrorMessage = "Campo estado é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O estado pode ter no máximo 50 caracteres.")]
        public String Estado { get; set; }

        [Required(ErrorMessage = "Campo cidade é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O cidade pode ter no máximo 50 caracteres.")]
        public String Cidade { get; set; }

        [Required(ErrorMessage = "Campo bairro é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O bairro pode ter no máximo 50 caracteres.")]
        public String Bairro { get; set; }

        [Required(ErrorMessage = "Campo endereço é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O endereço pode ter no máximo 50 caracteres.")]
        public String Endereco { get; set; }


        public int Salvar()
        {
            var ret = 0;
                    

            return ret;
        }


    }
}