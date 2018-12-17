using CRUD.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRUD.Models
{
    public class UsuarioModel
    {
        #region Atributos   

        public int Id { get; set; }

        [Required(ErrorMessage = "Campo nome é obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo email é obrigatório!")]
        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$", ErrorMessage = "Formato do E-mail Inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo login é obrigatório!")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Campo senha é obrigatório!")]
        public string Senha { get; set; }

        [Required(ErrorMessage ="Informe o perfil")]
        public int IdPerfil { get; set; }
        #endregion

        public static UsuarioModel ValidarUsuario(string login, string senha)
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            UsuarioModel resposta = null;
            SqlDataReader reader = null;

            try
            {
                conexao = Conexao.getInstancia().ConexaoBD();
                cmd = new SqlCommand("STP_VALIDAR_USUARIO", conexao);
                cmd.Parameters.AddWithValue("@prmLogin", SqlDbType.VarChar).Value = login;
                cmd.Parameters.AddWithValue("@prmSenha", SqlDbType.VarChar).Value = CriptoHelper.HashMD5(senha);
                cmd.CommandType = CommandType.StoredProcedure;
                conexao.Open();

                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    resposta = new UsuarioModel
                    {
                        Id = (int)reader["id"],
                        Login = (string)reader["login"],
                        Senha = (string)reader["senha"],
                        Nome = (string)reader["nome"],
                        Email = (string)reader["email"],
                        IdPerfil = (int)reader["id_perfil"]
                    };
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

        public static List<UsuarioModel> RecuperarUsuario()
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            var resposta = new List<UsuarioModel>();
            SqlDataReader reader = null;

            try
            {
                conexao = Conexao.getInstancia().ConexaoBD();
                cmd = new SqlCommand("STP_RECUPERAR_USUARIO", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                conexao.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resposta.Add(new UsuarioModel
                    {
                        Id = (int)reader["id"],
                        Nome = (string)reader["nome"],
                        Email = (string)reader["email"],
                        Login = (string)reader["login"],
                        Senha = (string)reader["senha"],
                       IdPerfil = (int)reader["id_perfil"]
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

        public static UsuarioModel RecuperarPeloId(int id)
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            UsuarioModel resposta = null;
            SqlDataReader reader = null;
            try
            {
                conexao = Conexao.getInstancia().ConexaoBD();
                cmd = new SqlCommand("STP_RECUPERAR_USUARIO_PELO_ID", conexao);
                cmd.Parameters.AddWithValue("@prmId", SqlDbType.Int).Value = id;
                cmd.CommandType = CommandType.StoredProcedure;
                conexao.Open();

                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    resposta = new UsuarioModel
                    {
                        Id = (int)reader["id"],
                        Login = (string)reader["login"],
                        Senha = (string)reader["senha"],
                        Nome = (string)reader["nome"],
                        Email = (string)reader["email"],
                        IdPerfil = (int)reader["id_perfil"]
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

        public bool SalvarUsuario()
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            bool retorno = false;
            try
            {
                conexao = Conexao.getInstancia().ConexaoBD();
                cmd = new SqlCommand("STP_CADASTRAR_USUARIO", conexao);
                cmd.Parameters.AddWithValue("@prmNome", SqlDbType.VarChar).Value = this.Nome;
                cmd.Parameters.AddWithValue("@prmEmail", SqlDbType.VarChar).Value = this.Email;
                cmd.Parameters.AddWithValue("@prmLogin", SqlDbType.VarChar).Value = this.Login;
                cmd.Parameters.AddWithValue("@prmSenha", SqlDbType.VarChar).Value = CriptoHelper.HashMD5(this.Senha);
                cmd.Parameters.AddWithValue("@prmIdPerfil", SqlDbType.Int).Value = this.IdPerfil;
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

        public static bool ExcluirUsuario(int id)
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            bool resposta = false;

            if (RecuperarPeloId(id) != null)
            {
                try
                {
                    conexao = Conexao.getInstancia().ConexaoBD();
                    cmd = new SqlCommand("STP_REMOVER_USUARIO", conexao);
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
        
        public int EditarUsuario()
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            var retorno = 0;

            var model = RecuperarPeloId(this.Id);

            try
            {
                conexao = Conexao.getInstancia().ConexaoBD();
                cmd = new SqlCommand("STP_ALTERAR_USUARIO", conexao);
                cmd.Parameters.AddWithValue("@prmId", SqlDbType.Int).Value = this.Id;
                cmd.Parameters.AddWithValue("@prmNome", SqlDbType.VarChar).Value = this.Nome;
                cmd.Parameters.AddWithValue("@prmEmail", SqlDbType.VarChar).Value = this.Email;
                cmd.Parameters.AddWithValue("@prmLogin", SqlDbType.VarChar).Value = this.Login;
                cmd.Parameters.AddWithValue("@prmIdPerfil", SqlDbType.Int).Value = this.IdPerfil;
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