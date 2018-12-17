using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRUD.Models
{

    public class ClienteModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo nome é obrigatório.")]
        [MaxLength(60, ErrorMessage = "O nome pode ter no máximo 100 caracteres.")]
        public String Nome { get; set; }

        [Required(ErrorMessage = "Campo email é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O email pode ter no máximo 50 caracteres.")]
        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$", ErrorMessage = "Formato do E-mail Inválido.")]
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
        [MaxLength(150, ErrorMessage = "O endereço pode ter no máximo 150 caracteres.")]
        public String Endereco { get; set; }


        public static List<ClienteModel> RecuperarCliente()
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            var resposta = new List<ClienteModel>();
            SqlDataReader reader = null;

            try
            {
                conexao = Conexao.getInstancia().ConexaoBD();
                cmd = new SqlCommand("STP_RECUPERAR_CLIENTE", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                conexao.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    resposta.Add(new ClienteModel
                    {
                        Id = (int)reader["id"],
                        Nome = (string)reader["nome"],
                        Email = (string)reader["email"],
                        CPF = (string)reader["cpf"],
                        Telefone = (string)reader["telefone"],
                        CEP = (string)reader["cep"],
                        Estado = (string)reader["estado"],
                        Cidade = (string)reader["cidade"],
                        Bairro = (string)reader["bairro"],
                        Endereco = (string)reader["endereco"]

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

        public static ClienteModel RecuperarPeloId(int id)
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            ClienteModel resposta = null;
            SqlDataReader reader = null;
            try
            {
                conexao = Conexao.getInstancia().ConexaoBD();
                cmd = new SqlCommand("STP_RECUPERAR_CLIENTE_PELO_ID", conexao);
                cmd.Parameters.AddWithValue("@prmId", SqlDbType.Int).Value = id;
                cmd.CommandType = CommandType.StoredProcedure;
                conexao.Open();

                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    resposta = new ClienteModel
                    {
                        Id = (int)reader["id"],
                        Nome = (string)reader["nome"],
                        Email = (string)reader["email"],
                        CPF = (string)reader["cpf"],
                        Telefone = (string)reader["telefone"],
                        CEP = (string)reader["cep"],
                        Estado = (string)reader["estado"],
                        Cidade = (string)reader["cidade"],
                        Bairro = (string)reader["bairro"],
                        Endereco = (string)reader["endereco"]

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

        public bool SalvarCliente()
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            bool retorno = false;
            try
            {
                conexao = Conexao.getInstancia().ConexaoBD();
                cmd = new SqlCommand("STP_CADASTRAR_CLIENTE", conexao);
                cmd.Parameters.AddWithValue("@prmNome", SqlDbType.VarChar).Value = this.Nome;
                cmd.Parameters.AddWithValue("@prmEmail", SqlDbType.VarChar).Value = this.Email;
                cmd.Parameters.AddWithValue("@prmCpf", SqlDbType.VarChar).Value = this.CPF;
                cmd.Parameters.AddWithValue("@prmTelefone", SqlDbType.VarChar).Value = this.Telefone;
                cmd.Parameters.AddWithValue("@prmCep", SqlDbType.VarChar).Value = this.CEP;
                cmd.Parameters.AddWithValue("@prmEstado", SqlDbType.VarChar).Value = this.Estado;
                cmd.Parameters.AddWithValue("@prmCidade", SqlDbType.VarChar).Value = this.Cidade;
                cmd.Parameters.AddWithValue("@prmBairro", SqlDbType.VarChar).Value = this.Bairro;
                cmd.Parameters.AddWithValue("@prmEndereco", SqlDbType.VarChar).Value = this.Endereco;

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

        public int EditarCliente()
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            var retorno = 0;

            var model = RecuperarPeloId(this.Id);

            try
            {
                conexao = Conexao.getInstancia().ConexaoBD();
                cmd = new SqlCommand("STP_ALTERAR_CLIENTE", conexao);
                cmd.Parameters.AddWithValue("@prmId", SqlDbType.Int).Value = this.Id;
                cmd.Parameters.AddWithValue("@prmNome", SqlDbType.VarChar).Value = this.Nome;
                cmd.Parameters.AddWithValue("@prmEmail", SqlDbType.VarChar).Value = this.Email;
                cmd.Parameters.AddWithValue("@prmCpf", SqlDbType.VarChar).Value = this.CPF;
                cmd.Parameters.AddWithValue("@prmTelefone", SqlDbType.VarChar).Value = this.Telefone;
                cmd.Parameters.AddWithValue("@prmCep", SqlDbType.VarChar).Value = this.CEP;
                cmd.Parameters.AddWithValue("@prmEstado", SqlDbType.VarChar).Value = this.Estado;
                cmd.Parameters.AddWithValue("@prmCidade", SqlDbType.VarChar).Value = this.Cidade;
                cmd.Parameters.AddWithValue("@prmBairro", SqlDbType.VarChar).Value = this.Bairro;
                cmd.Parameters.AddWithValue("@prmEndereco", SqlDbType.VarChar).Value = this.Endereco;
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

        public static bool ExcluirCliente(int id)
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            bool resposta = false;

            if (RecuperarPeloId(id) != null)
            {
                try
                {
                    conexao = Conexao.getInstancia().ConexaoBD();
                    cmd = new SqlCommand("STP_REMOVER_CLIENTE", conexao);
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