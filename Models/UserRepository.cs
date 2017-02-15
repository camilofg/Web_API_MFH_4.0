using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Repository;
using Web_API_MFH_4._0.Helpers;
using Newtonsoft.Json;

namespace Web_API_MFH_4._0.Models
{
    public class UserRepository : IUserRepository
    {

        MindFulness_Repository DB = new MindFulness_Repository();

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(User item)
        {
            throw new NotImplementedException();
        }

        public string ConfirmEmail(string guid, string userId)
        {
            return DB.ConfirmEmail(userId, guid).FirstOrDefault();
        }

        void IUserRepository.SendMail(string emailDestination, string idUser, string code, string callbackUrl)
        {
            GmailEmailService EmailService = new GmailEmailService();
            EmailService.sendEmail(emailDestination, idUser, code, callbackUrl);
        }

        string IUserRepository.SendMail(string callbackUrl)
        {
            GmailEmailService EmailService = new GmailEmailService();
            var resultMessage = EmailService.sendEmail("camus35@hotmail.com", callbackUrl);
            return resultMessage;
        }

        //public string RememberPassword(string email) {
        //    GmailEmailService EmailService = new GmailEmailService();
        //    EmailService.sendEmail(email, idUser, code, callbackUrl);
        //}

        TokenUser IUserRepository.Add(TokenUser usuario)
        {
            if (usuario.SessionUser == null)
            {
                throw new ArgumentNullException("usuario");
            }

            if (DB.Users.Where(x => x.UserName == usuario.SessionUser.UserName || x.Email == usuario.SessionUser.Email).FirstOrDefault() != null) {
                usuario.Msg = "The user already exist, remember your password";
                return usuario;
            }

            if((from a in DB.Users where a.UserName == usuario.SessionUser.UserName || a.Email == usuario.SessionUser.Email select a).Count() > 0) {
                usuario.Msg = "The user already exist, remember your password";
                return usuario;
            }

            var us = new Users();
            us.Id = usuario.SessionUser.Id ?? Guid.NewGuid().ToString();

            PasswordManager passManager = new PasswordManager();
            var passHash = passManager.GeneratePassword(usuario.SessionUser.PasswordHash, 36);

            us.PasswordHash = passHash[1];
            us.SecuritySalt = passHash[0];
            us.Email = usuario.SessionUser.Email;
            us.EmailConfirmed = usuario.SessionUser.EmailConfirmed.HasValue;
            us.PhoneNumber = usuario.SessionUser.PhoneNumber;
            us.UserName = usuario.SessionUser.UserName ?? usuario.SessionUser.Email;
            us.ValidEmailTempToken = usuario.SessionUser.EmailConfirmed.HasValue ? null : Guid.NewGuid().ToString();
            usuario.SessionUser.ValidEmailTempToken = us.ValidEmailTempToken;
            usuario.SessionUser.Id = us.Id;
            try
            {
                DB.Users.Add(us);
                DB.SaveChanges();
                usuario.ValidationToken = DB.InsertSession(us.Id).FirstOrDefault().ToString();
                usuario.Msg = "Please check your Email to validate your account";
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return usuario;
        }

        public User Get(string id)
        {
            throw new NotImplementedException();
        }

        public List<Repository.OptionsMenu> GetMenu()
        {
            var Menu = (from m in DB.OptionsMenu
                        select m).ToList();

            return Menu;
        }

        public List<Repository.ActivitiesByView_Result> GetUserActivities(string idUser, int state)
        {
            return DB.ActivitiesByView(idUser, state).ToList();
        }

        public string SetDateToActivity(UpdateActivity act)
        {
            DB.SetDateToActivity(act.ActId, act.UnixActDate);
            return "algo";
        }

        public bool DeleteActivity(Repository.Activity act)
        {
            try
            {
                var activity = new Activity { ActId = act.ActId };
                DB.Activity.Attach(activity);
                DB.Activity.Remove(activity);
                DB.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public int CreateActivity(Repository.Activity activity)
        {
            DB.Activity.Add(activity);
            DB.SaveChanges();
            return DB.Activity.Count();
        }

        string IUserRepository.ValidUser(string userName, string pass)
        {
            string validToken = string.Empty;
            User usu = new User();
            try
            {
                var usuario = (from u in DB.Users
                               where u.UserName == userName || u.Email == userName
                               select u).FirstOrDefault();

                if (usuario != null)
                {
                    var passManager = new PasswordManager();
                    string passHashed = passManager.GenerateSHA256Hash(pass, usuario.SecuritySalt);

                    if (passHashed == usuario.PasswordHash)
                    {
                        validToken = DB.InsertSession(usuario.Id).FirstOrDefault().ToString();
                    }
                }
                return JsonConvert.SerializeObject(usuario);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}