﻿using CRUD.Helpers;
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

        [Required(ErrorMessage ="Informe login")]
        public string Login { get; set; }

        [Required(ErrorMessage ="Informe senha")]
        public string Senha { get; set; }

        [Required(ErrorMessage ="Informe nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage ="Informe email")]
        public string Email { get; set; }
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
                cmd.Parameters.AddWithValue("@prmLogin",SqlDbType.VarChar).Value = login;
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
                        Email = (string)reader["email"]
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

        public static UsuarioModel RecuperarPeloId(int id)
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;
            UsuarioModel resposta = null;
            SqlDataReader reader = null;
            try
            {
                conexao = Conexao.getInstancia().ConexaoBD();
                cmd = new SqlCommand("STP_RECUPERAR_PELO_ID", conexao);
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
                        Email = (string)reader["email"]
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

        public bool CadastrarUsuario()
        {
            SqlConnection conexao = null;
            SqlCommand cmd = null;

            bool resposta = false;
            var model = RecuperarPeloId(this.Id);

            try
            {
                if (model == null)
                {
                    conexao = Conexao.getInstancia().ConexaoBD();
                    cmd = new SqlCommand("STP_CADASTRAR_USUARIO", conexao);
                    cmd.Parameters.AddWithValue("@prmLogin", SqlDbType.VarChar).Value = this.Nome;
                    cmd.Parameters.AddWithValue("@prmSenha", SqlDbType.VarChar).Value = CriptoHelper.HashMD5(this.Senha);
                    cmd.Parameters.AddWithValue("@prmNome", SqlDbType.VarChar).Value = this.Nome;
                    cmd.Parameters.AddWithValue("@prmEmail", SqlDbType.VarChar).Value = this.Email;
                    cmd.CommandType = CommandType.StoredProcedure;
                    conexao.Open();

                    int dados = cmd.ExecuteNonQuery();
                    if (dados > 0)
                    {
                        resposta = true;
                    }
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

    }
}