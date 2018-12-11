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
            new UsuarioModel() {Id=1,Login="marivaldo",Senha="123",Nome="Marivaldo almeida",Email="marivaldo@yahoo.com.br" },
            new UsuarioModel() {Id=1,Login="jamily",Senha="123",Nome="Jamily Almeida",Email="marivaldo@yahoo.com.br" },
            new UsuarioModel() {Id=1,Login="marcos",Senha="123",Nome="Marcos Silva",Email="marivaldo@yahoo.com.br" },
            new UsuarioModel() {Id=1,Login="rafael",Senha="123",Nome="Rafael Almeida",Email="marivaldo@yahoo.com.br" },           
            new UsuarioModel() {Id=1,Login="joaovitor",Senha="123",Nome="João Vitor Almeida",Email="marivaldo@yahoo.com.br" },
            new UsuarioModel() {Id=1,Login="raquel",Senha="123",Nome="Raquel Ramos",Email="marivaldo@yahoo.com.br" },           
            new UsuarioModel() {Id=1,Login="lucio",Senha="123",Nome="Lucio brito",Email="marivaldo@yahoo.com.br" },          
        };

        public ActionResult Index()
        {
            return View(UsuarioModel.RecuperarUsuario());
        }
     
        [HttpPost]
        [Authorize]   
        public ActionResult RecuperarPorId(int id)
        {
            return Json(UsuarioModel.RecuperarPeloId(id));
        }

        //[HttpPost]
        //[Authorize]     
        //public ActionResult RecuperarUsuario(int id)
        //{
        //    return Json(_listaUsuario.Find(x => x.Id == id));
        //}

        //[HttpPost]
        //[Authorize]
        //public ActionResult SalvarUsuario(UsuarioModel model)
        //{
        //    var registroBD = _listaUsuario.Find(x => x.Id == model.Id);
        //    if (registroBD == null)
        //    {
        //        registroBD = model;
        //        registroBD.Id = _listaUsuario.Max(x => x.Id) + 1;
        //        _listaUsuario.Add(registroBD);
        //    }
        //    else
        //    {
        //        registroBD.Nome = model.Nome;
        //        registroBD.Email = model.Email;
        //        registroBD.Login = model.Login;
        //        registroBD.Senha = model.Senha;
        //    }

        //    return Json(registroBD);
        //}

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
                    throw ex;
                }
            }
            return Json(new { Resultado = resultado, Mensagens = mensagens, IdSalvo = idSalvo });
        }


    }
}