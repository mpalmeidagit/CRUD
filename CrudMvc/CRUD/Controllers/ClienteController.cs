using CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CRUD.Controllers
{

    [Authorize(Roles = "Administrador,Gerente,Operador")]
    public class ClienteController : Controller
    {

        public ActionResult CadastrarCliente()
        {
            return View(ClienteModel.RecuperarCliente());
        }

        public ActionResult ConsultarCliente()
        {
            return View(ClienteModel.RecuperarCliente());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RecuperarPorId(int id)
        {
            return Json(ClienteModel.RecuperarPeloId(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SalvarCliente(ClienteModel model)
        {
            var resultado = "SUCESSO";
            var mensagens = new List<string>();
            var idSalvo = string.Empty;
            var retorno = string.Empty;

            if (!ModelState.IsValid)
            {
                resultado = "AVISO";
                mensagens = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                try
                {
                    var id = model.SalvarCliente();
                    if (id == true)
                    {
                        idSalvo = id.ToString();
                    }
                    else
                    {
                        resultado = "ERRO";
                    }
                }
                catch (Exception ex)
                {
                    //resultado = "ERRO";
                    retorno = ex.Message;
                    resultado = retorno;
                }
            }
            return Json(new { Resultado = resultado, Mensagens = mensagens, IdSalvo = idSalvo });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EditarCliente(ClienteModel model)
        {
            var resultado = "SUCESSO";
            var mensagens = new List<string>();
            var idSalvo = string.Empty;
            var retorno = string.Empty;

            if (!ModelState.IsValid)
            {
                resultado = "AVISO";
                mensagens = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                try
                {
                    var id = model.EditarCliente();
                    if (id > 0)
                    {
                        idSalvo = id.ToString();
                    }
                    else
                    {
                        resultado = "ERRO";
                    }
                }
                catch (Exception ex)
                {
                    //resultado = "ERRO";
                    retorno = ex.Message;
                    resultado = retorno;
                }
            }
            return Json(new { Resultado = resultado, Mensagens = mensagens, IdSalvo = idSalvo });
        }


        [HttpPost]
        [Authorize(Roles = "Administrador,Gerente")]
        [ValidateAntiForgeryToken]
        public JsonResult ExcluirCliente(int id)
        {
            return Json(ClienteModel.ExcluirCliente(id));
        }

        



    }
}