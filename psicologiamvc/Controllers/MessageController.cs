using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace psicologiamvc.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        public ActionResult PsiExpNuevo()
        {
            return View();
        }

        public ActionResult PsiExpEditar(int sIdh)
        {
            //return RedirectToAction("printInvestigacionPsicologica", "Impresiones", new { sIdH = sIdh });
            return View();
        }

        public ActionResult PsiIndiceNuevo()
        {
            return View();
        }

        public ActionResult PsiIndiceEditar()
        {
            return View();
        }

        public ActionResult PsiProtocoloEditar()
        {
            return View();
        }
    }
}