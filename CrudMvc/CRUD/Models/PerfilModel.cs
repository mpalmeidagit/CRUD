using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRUD.Models
{
    public class PerfilModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Campo nome é obrigatório!")]
        public String Nome { get; set; }

        public static PerfilModel RecuperarPeloId(int id)
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            PerfilModel resposta = null;
            SqlDataReader reader = null;
            try
            {
                conexao = Conexao.getInstancia().ConexaoBD();
                cmd = new SqlCommand("STP_RECUPERAR_PERFIL_PELO_ID", conexao);
                cmd.Parameters.AddWithValue("@prmId", SqlDbType.Int).Value = id;
                cmd.CommandType = CommandType.StoredProcedure;
                conexao.Open();

                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    resposta = new PerfilModel
                    {
                        Id = (int)reader["id"],                      
                        Nome = (string)reader["nome"]                    
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

        public static List<PerfilModel> RecuperarPerfil()
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            var resposta = new List<PerfilModel>();
            SqlDataReader reader = null;

            try
            {
                conexao = Conexao.getInstancia().ConexaoBD();
                cmd = new SqlCommand("STP_RECUPERAR_PERFIL", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                conexao.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resposta.Add(new PerfilModel
                    {
                        Id = (int)reader["id"],
                        Nome = (string)reader["nome"]
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

        public static List<PerfilModel> RecuperarListaAtivos()
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            var resposta = new List<PerfilModel>();
            SqlDataReader reader = null;

            try
            {
                conexao = Conexao.getInstancia().ConexaoBD();
                cmd = new SqlCommand("STP_RECUPERAR_PERFIL", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                conexao.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resposta.Add(new PerfilModel
                    {
                        Id = (int)reader["id"],
                        Nome = (string)reader["nome"]
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

        public bool SalvarPerfil()
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            bool retorno = false;
            try
            {
                conexao = Conexao.getInstancia().ConexaoBD();
                cmd = new SqlCommand("STP_CADASTRAR_PERFIL", conexao);
                cmd.Parameters.AddWithValue("@prmNome", SqlDbType.VarChar).Value = this.Nome;        
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

        public int EditarPerfil()
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            var retorno = 0;

            var model = RecuperarPeloId(this.Id);

            try
            {
                conexao = Conexao.getInstancia().ConexaoBD();
                cmd = new SqlCommand("STP_ALTERAR_PERFIL", conexao);
                cmd.Parameters.AddWithValue("@prmId", SqlDbType.Int).Value = this.Id;
                cmd.Parameters.AddWithValue("@prmNome", SqlDbType.VarChar).Value = this.Nome;       
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

        public static bool ExcluirPerfil(int id)
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            bool resposta = false;

            if (RecuperarPeloId(id) != null)
            {
                try
                {
                    conexao = Conexao.getInstancia().ConexaoBD();
                    cmd = new SqlCommand("STP_REMOVER_PERFIL", conexao);
                    cmd.Parameters.AddWithValue("@prmId", SqlDbType.Int).Value = id;
                    cmd.CommandType = CommandType.StoredProcedure;

                    conexao.Open();
                    cmd.ExecuteNonQuery();

                    resposta = true;
                }
                catch (Exception ex)
                {
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