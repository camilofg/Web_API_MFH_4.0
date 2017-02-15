using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_API_MFH_4._0.Controllers
{
    public class ValidationsController : Controller
    {
        //
        // GET: /Validations/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public bool ValidateSeico() {

            string finalDate = "28/02/2017";
            if (DateTime.Now < Convert.ToDateTime(finalDate))
                return true;

            else return false;
        }

    }
}
