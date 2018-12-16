using CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD.Controllers
{
    [Authorize(Roles = "Administrador,Gerente,Operador")]
    public class PerfilController : Controller
    {

        public ActionResult ConsultarPerfil()
        {
            ViewBag.ListaUsuario = UsuarioModel.RecuperarUsuario();
            return View(PerfilModel.RecuperarPerfil());
        }

        public ActionResult CadastrarPerfil()
        {
            ViewBag.ListaUsuario = UsuarioModel.RecuperarUsuario();
            return View(PerfilModel.RecuperarPerfil());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult RecuperarPorId(int id)
        {            
            return Json(PerfilModel.RecuperarPeloId(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SalvarPerfil(PerfilModel model)
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
                    var id = model.SalvarPerfil();
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
        public JsonResult EditarPerfil(PerfilModel model)
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
                    var id = model.EditarPerfil();
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
        public JsonResult ExcluirPerfil(int id)
        {
            return Json(PerfilModel.ExcluirPerfil(id));
        }


    }
}