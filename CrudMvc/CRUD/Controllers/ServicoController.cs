using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD.Controllers
{
    public class ServicoController : Controller
    {
        
        public ActionResult CadastrarServico()
        {
            return View();
        }

        public ActionResult ConsultarServico()
        {
            return View();
        }

        public ActionResult RecuperarPorId(int id)
        {
            return null;
        }
    }
}