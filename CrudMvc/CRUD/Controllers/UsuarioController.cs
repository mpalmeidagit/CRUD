﻿using CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD.Controllers
{
    [Authorize(Roles = "Administrador,Gerente,Operador")]
    public class UsuarioController : Controller
    {

        private const string _senhaDefault = "{$127;$188}";

        public ActionResult CadastrarUsuario()
        {
            ViewBag.ListarPerfil = PerfilModel.RecuperarListaAtivos();
            return View(UsuarioModel.RecuperarUsuario());
        }

        public ActionResult ConsultarUsuario()
        {
            ViewBag.ListarPerfil = PerfilModel.RecuperarListaAtivos();
            return View(UsuarioModel.RecuperarUsuario());
        }

        [HttpPost]       
        [ValidateAntiForgeryToken]
        public ActionResult RecuperarPorId(int id)
        {
            return Json(UsuarioModel.RecuperarPeloId(id));
        }
             
        [HttpPost]       
        [ValidateAntiForgeryToken]
        public JsonResult SalvarUsuario(UsuarioModel model)
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
                    if (model.Senha == _senhaDefault)
                    {
                        model.Senha = "";
                    }
                    var id = model.SalvarUsuario();
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
            return Json(new { Resultado = resultado, Mensagens = mensagens, IdSalvo = idSalvo});
        }

        [HttpPost]        
        [ValidateAntiForgeryToken]
        public JsonResult EditarUsuario(UsuarioModel model)
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
                    if (model.Senha == _senhaDefault)
                    {
                        model.Senha = "";
                    }
                    var id = model.EditarUsuario();
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
        public JsonResult ExcluirUsuario(int id)
        {
            return Json(UsuarioModel.ExcluirUsuario(id));
        }

    }
}