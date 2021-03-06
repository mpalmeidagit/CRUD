﻿using CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD.Controllers
{
    [Authorize(Roles = "Administrador,Gerente,Operador")]
    public class ServicoController : Controller
    {
        
        public ActionResult CadastrarServico()
        {
            return View(ServicoModel.RecuperarServico());
        }

        public ActionResult ConsultarServico()
        {
            return View(ServicoModel.RecuperarServico());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RecuperarPorId(int id)
        {
            return Json(ServicoModel.RecuperarPeloId(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SalvarServico(ServicoModel model)
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
                    var id = model.SalvarServico();
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
                    retorno = ex.Message;
                    resultado = retorno;
                }
            }

            return Json(new { Resultado=resultado, Mensagens=mensagens, IdSalvo=idSalvo});

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EditarServico(ServicoModel model)
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
                    var id = model.EditarServico();
                    if (id > 0)
                    {
                        idSalvo = id.ToString();
                    }
                }
                catch (Exception ex)
                {
                    retorno = ex.Message;
                    resultado = retorno;
                }
            }
            return Json(new { Resultado = resultado, Mensagens = mensagens, IdSalvo = idSalvo });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Gerente")]
        public JsonResult ExcluirServico(int id)
        {
            return Json(ServicoModel.ExcluirServico(id));
        }

    }
}