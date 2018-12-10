using CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CRUD.Controllers
{
    
    public class ClienteController : Controller
    {
        [Authorize]
        public ActionResult CadastrarCliente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarCliente(String campoNome, String campoEmail, String campoCPF, String campoTelefone, String campoCEP, String campoEstado, String campoCidade, String campoBairro, String campoEndereco)
        {


            ClienteModel objCliente = new ClienteModel()
            {
                Nome = campoNome,
                Email = campoEmail,
                CPF = campoCPF,
                Telefone = campoTelefone,
                CEP = campoCEP,
                Estado = campoEstado,
                Cidade = campoCidade,
                Bairro = campoBairro,
                Endereco = campoEndereco

            };


            var resultado = "Sucesso";
            var mensagens = new List<string>();
            var idSalvo = string.Empty;
            if (!ModelState.IsValid)
            {
                resultado = "Aviso";
                mensagens = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                try
                {
                    //var id = objCliente.getInstancia().Salvar();
                }
                catch (Exception ex)
                {
                    resultado = "Erro";
                }
            }
            return Json(new { Resultado = resultado, Mensagens = mensagens, IdSalvo = idSalvo });
        }

        public ActionResult ConsultarCliente()
        {
            return View();
        }

        
    }
}