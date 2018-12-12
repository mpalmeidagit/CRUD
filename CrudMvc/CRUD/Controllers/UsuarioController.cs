using CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD.Controllers
{

    public class UsuarioController : Controller
    {

        private const string _senhaDefault = "{$127;$188}";
        public ActionResult Index()
        {
            return View(UsuarioModel.RecuperarUsuario());
        }


        public ActionResult ConsultarUsuario()
        {
            return View(UsuarioModel.RecuperarUsuario());
        }

        [HttpPost]
        [Authorize]
        public ActionResult RecuperarPorId(int id)
        {
            return Json(UsuarioModel.RecuperarPeloId(id));
        }


        [HttpPost]
        [Authorize]
        public ActionResult ExcluirUsuario(int id)
        {
            return Json(UsuarioModel.ExcluirUsuario(id));
        }

        [HttpPost]
        [Authorize]
        public ActionResult SalvarUsuario(UsuarioModel model)
        {
            var resultado = "SUCESSO";
            var mensagens = new List<string>();
            var idSalvo = string.Empty;

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
                    resultado = "ERRO";
                }
            }
            return Json(new { Resultado = resultado, Mensagens = mensagens, IdSalvo = idSalvo });
        }

        
    }
}