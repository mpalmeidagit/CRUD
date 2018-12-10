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
        private static List<UsuarioModel> _listaUsuario = new List<UsuarioModel>()
        {
            new UsuarioModel() {Id=1, Login="marivaldo",Senha="123",Nome="Marivaldo almeida",Email="marivaldo@yahoo.com.br" },
            new UsuarioModel() {Id=2, Login="marivaldo",Senha="123",Nome="Marivaldo almeida",Email="marivaldo@yahoo.com.br" },
            new UsuarioModel() {Id=3, Login="marivaldo",Senha="123",Nome="Marivaldo almeida",Email="marivaldo@yahoo.com.br" },
            new UsuarioModel() {Id=4, Login="marivaldo",Senha="123",Nome="Marivaldo almeida",Email="marivaldo@yahoo.com.br" },
            new UsuarioModel() {Id=5, Login="marivaldo",Senha="123",Nome="Marivaldo almeida",Email="marivaldo@yahoo.com.br" }
        };

        public ActionResult Index()
        {
            return View(_listaUsuario);
        }
                
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult RecuperarPorId(int id)
        {
            return Json(UsuarioModel.RecuperarPeloId(id));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult CadastrarUsuario(UsuarioModel model)
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
                    var id = model.CadastrarUsuario();
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
                    resultado = "ERRO";
                    throw ex;
                }
            }
            return Json(new { Resultado = resultado, Mensagens = mensagens, IdSalvo = idSalvo });
        }


    }
}