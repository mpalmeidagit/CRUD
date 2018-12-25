using CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD.Controllers
{
    public class ReciboController : Controller
    {
       
        public ActionResult CadastrarRecibo()
        {
            ViewBag.ListarCliente = ClienteModel.RecuperarCliente();
            ViewBag.ListarServico = ServicoModel.RecuperarServico();
           
            return View();
        }

        public ActionResult ConsultarRecibo()
        {
            ViewBag.ListarCliente = ClienteModel.RecuperarCliente();
            ViewBag.ListarServico = ServicoModel.RecuperarServico();
            return View(ReciboModal.RecuperarRecibo());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Atualizar()
        {
            return Json(ReciboModal.RecuperarRecibo());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SalvarRecibo(ReciboModal model)
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
                    var id = model.SalvarRecibo();
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
        public ActionResult RecuperarPorId(int id)
        {
            return Json(ReciboModal.RecuperarPeloId(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EditarRecibo(ReciboModal model)
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
                  
                    var id = model.EditarRecibo();
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
        public JsonResult ExcluirRecibo(int id)
        {
            return Json(ReciboModal.ExcluirRecibo(id));
        }

    }
}