using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRUD.Models
{
    public class ServicoModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage ="Campo descrição é obrigatório.")]
        public String Descricao { get; set; }

        [Required(ErrorMessage ="Campo valor é obrigatório.")]
        public Decimal Valor { get; set; }

        public static List<ClienteModel> RecuperarServico()
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            var resposta = new List<ClienteModel>();
            SqlDataReader reader = null;
            try
            {
                conexao = Conexao.getInstancia().ConexaoBD();
                cmd = new SqlCommand("STP_RECUPERAR_SERVICO", conexao);


            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}