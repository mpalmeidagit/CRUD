using CRUD.Models;
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
        public JsonResult SalvarSevico(ServicoModel model)
        {

        }



    }
}