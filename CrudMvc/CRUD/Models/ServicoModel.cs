using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRUD.Models
{
    public class ServicoModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Campo descrição é obrigatório.")]
        public String Descricao { get; set; }

        [Required(ErrorMessage = "Campo valor é obrigatório.")]
        public Decimal Valor { get; set; }

        public static List<ServicoModel> RecuperarServico()
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            var resposta = new List<ServicoModel>();
            SqlDataReader reader = null;
            try
            {
                conexao = Conexao.getInstancia().ConexaoBD();
                cmd = new SqlCommand("STP_RECUPERAR_SERVICO", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                conexao.Open();

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resposta.Add(new ServicoModel
                    {
                        Id = (int)reader["id"],
                        Descricao = (string)reader["descricao"],
                        Valor = (decimal)reader["valor"]
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

        public static ServicoModel RecuperarPeloId(int id)
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            ServicoModel resposta = null;
            SqlDataReader reader = null;

            try
            {
                conexao = Conexao.getInstancia().ConexaoBD();
                cmd = new SqlCommand("STP_RECUPERAR_SERVICO_PELO_ID", conexao);
                cmd.Parameters.AddWithValue("@prmId", SqlDbType.Int).Value = id;
                cmd.CommandType = CommandType.StoredProcedure;
                conexao.Open();

                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    resposta = new ServicoModel
                    {
                        Id = (int)reader["id"],
                        Descricao = (string)reader["descricao"],
                        Valor = (decimal)reader["valor"]
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

        public bool SalvarServico()
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            bool retorno = false;

            try
            {
                conexao = Conexao.getInstancia().ConexaoBD();
                cmd = new SqlCommand("STP_CADASTRAR_SERVICO", conexao);
                cmd.Parameters.AddWithValue("@prmDescricao", SqlDbType.VarChar).Value = this.Descricao;
                cmd.Parameters.AddWithValue("@prmValor", SqlDbType.Decimal).Value = this.Valor;
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

        public int EditarServico()
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            var retorno = 0;

            var model = RecuperarPeloId(this.Id);

            try
            {
                conexao = Conexao.getInstancia().ConexaoBD();
                cmd = new SqlCommand("STP_ALTERAR_SERVICO", conexao);
                cmd.Parameters.AddWithValue("@prmId", SqlDbType.Int).Value = this.Id;
                cmd.Parameters.AddWithValue("@prmDescricao", SqlDbType.VarChar).Value = this.Descricao;
                cmd.Parameters.AddWithValue("@prmValor", SqlDbType.Decimal).Value = this.Valor;
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

        public static bool ExcluirServico(int id)
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            bool resposta = false;

            if (RecuperarPeloId(id) != null)
            {
                try
                {
                    conexao = Conexao.getInstancia().ConexaoBD();
                    cmd = new SqlCommand("STP_REMOVER_SERVICO", conexao);
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

    }
}