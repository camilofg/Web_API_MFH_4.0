using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Web_API_MFH_4._0.Models;
using Newtonsoft.Json;

namespace Web_API_MFH_4._0.Controllers
{
    public class UsersController : Controller
    {

        static readonly IUserRepository repository = new UserRepository();

        public ActionResult RememberPassword()
        {
            return View();
        }

        [HttpPost]
        public string ValidateUser(User user)
        {
            var algo = repository.ValidUser(user.UserName, user.PasswordHash);
            return algo;
        }
        

        [HttpPost]
        public string PostUser(TokenUser user)
        {
            user = repository.Add(user);
            var callbackUrl = string.Format("{0}://{1}:{2}/{3}ValidateEmail", Request.Url.Scheme, Request.Url.Host, Request.Url.Port, Request.Url.Segments[1], Request.Url.Segments[2]);// this.Url.Action("ValidateEmail", "Users", new { idUser = user.Id, code = user.ValidEmailTempToken });
            repository.SendMail(user.SessionUser.Email, user.SessionUser.Id, user.SessionUser.ValidEmailTempToken, callbackUrl);

            string salida = JsonConvert.SerializeObject(user);
            return salida;
        }

        [HttpPost]
        public string ValidateEmail(UserToken userTk)//string UserID, string code)
        {
            return repository.ConfirmEmail(userTk.UsToken, userTk.UsID);//code, UserID);
        }

        [HttpGet]
        public string GetMenu()
        {
            var myObjectResponse = repository.GetMenu();
            return JsonConvert.SerializeObject(myObjectResponse);
        }

        [HttpGet]
        public string GetUserActivities(string userId, int state) {
            var userActivities = repository.GetUserActivities(userId, state);
            return JsonConvert.SerializeObject(userActivities);
        }

        [HttpPost]
        public string SetDateToActivity(Repository.UpdateActivity act)
        {
            repository.SetDateToActivity(act);
            return "algo";
        }

        [HttpPost]
        public string CreateActivity(Repository.Activity act)
        {
            repository.CreateActivity(act);
            return "algo";
        }
        //public class UpdateAct {
        //    public int ActId { get; set; }
        //    public DateTime ActDate { get; set; }
        //}

        [HttpPost]
        public bool DeleteActivity(Repository.Activity act)
        {
            return repository.DeleteActivity(act);
        }


        //[HttpGet]
        //public string TestGet()
        //{
        //    return "string de prueba";
        //}

        //[HttpGet]
        //public string ConfirmEmail() {
        //    return "test";
        //}

        //public ActionResult Index()
        //{
        //    return View();
        //}

    }

}
