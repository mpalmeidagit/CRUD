using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRUD.Models
{
   
    public class ReciboModal
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o data")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Informe o cliente")]
        public int IdCliente { get; set; }
        public ClienteModel ClienteModel { get; set; }

        [Required(ErrorMessage = "Informe o servico")]
        public int IdServico { get; set; }
        public ServicoModel ServicoModal { get; set; }

        public static List<ReciboModal> RecuperarRecibo()
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            var resposta = new List<ReciboModal>();
            SqlDataReader reader = null;

            try
            {
                conexao = Conexao.getInstancia().ConexaoBD();
                cmd = new SqlCommand("STP_RECUPERAR_RECIBO", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                conexao.Open();

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resposta.Add(new ReciboModal
                    {
                        Id = (int)reader["id"],
                        Data = Convert.ToDateTime(reader["data"].ToString()),
                        ClienteModel = new ClienteModel()
                        {
                            Nome = (string)reader["nome"],
                            CPF = (string)reader["cpf"],
                            Telefone = (string)reader["telefone"]
                        },
                        ServicoModal = new ServicoModel()
                        {
                            Descricao = (string)reader["descricao"],
                            Valor = (decimal)reader["valor"]
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexao.Close();
            }

            return resposta;
        }

        public bool SalvarRecibo()
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            bool retorno = false;
            try
            {
                conexao = Conexao.getInstancia().ConexaoBD();
                cmd = new SqlCommand("STP_CADASTRAR_RECIBO", conexao);
                cmd.Parameters.AddWithValue("@prmData", SqlDbType.DateTime).Value = this.Data;
                cmd.Parameters.AddWithValue("@prmServicoId", SqlDbType.Int).Value = this.IdServico;
                cmd.Parameters.AddWithValue("@prmClienteId", SqlDbType.Int).Value = this.IdCliente;
                cmd.CommandType = CommandType.StoredProcedure;
                conexao.Open();

                int dados = cmd.ExecuteNonQuery();
                if (dados > 0)
                {
                    retorno = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexao.Close();
            }
            return retorno;
        }

        public static ReciboModal RecuperarPeloId(int id)
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            ReciboModal resposta = null;
            SqlDataReader reader = null;
            try
            {
                conexao = Conexao.getInstancia().ConexaoBD();
                cmd = new SqlCommand("STP_RECUPERAR_RECIBO_PELO_ID", conexao);
                cmd.Parameters.AddWithValue("@prmId", SqlDbType.Int).Value = id;
                cmd.CommandType = CommandType.StoredProcedure;
                conexao.Open();

                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    resposta = new ReciboModal
                    {
                        Id = (int)reader["id"],
                        Data = Convert.ToDateTime(reader["data"].ToString()),
                        IdServico = (int)reader["servico_id"],
                        IdCliente = (int)reader["cliente_id"]                  
                    };
                }
            }
            catch (Exception ex)
            {
                resposta = null;
                throw ex;
            }
            finally
            {
                conexao.Close();
            }

            return resposta;
        }

        public static bool ExcluirRecibo(int id)
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            bool resposta = false;

            if (RecuperarPeloId(id) != null)
            {
                try
                {
                    conexao = Conexao.getInstancia().ConexaoBD();
                    cmd = new SqlCommand("STP_REMOVER_RECIBO", conexao);
                    cmd.Parameters.AddWithValue("@prmId", SqlDbType.Int).Value = id;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conexao.Open();
                    cmd.ExecuteNonQuery();

                    resposta = true;
                }
                catch (Exception ex)
                {
                    resposta = false;
                    throw ex;
                }
                finally
                {
                    conexao.Close();
                }

            }
            return resposta;
        }

        public int EditarRecibo()
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            var retorno = 0;

            var model = RecuperarPeloId(this.Id);

            try
            {
                conexao = Conexao.getInstancia().ConexaoBD();
                cmd = new SqlCommand("STP_ALTERAR_RECIBO", conexao);
                cmd.Parameters.AddWithValue("@prmId", SqlDbType.Int).Value = this.Id;
                cmd.Parameters.AddWithValue("@prmData", SqlDbType.VarChar).Value = this.Data;
                cmd.Parameters.AddWithValue("@prmServicoId", SqlDbType.VarChar).Value = this.IdServico;
                cmd.Parameters.AddWithValue("@prmClienteId", SqlDbType.VarChar).Value = this.IdCliente;
                cmd.CommandType = CommandType.StoredProcedure;
                conexao.Open();

                if (cmd.ExecuteNonQuery() > 0)
                {
                    retorno = this.Id;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexao.Close();
            }

            return retorno;

        }


    }
}