using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web_API_MFH_4._0.Models
{
    interface IUserRepository
    {
        string ValidUser(string userName, string pass);
        TokenUser Add(TokenUser item);
        void SendMail(string emailDestination, string idUser, string code, string callbackUrl);
        string SendMail(string callbackUrl);
        string ConfirmEmail(string guid, string userId);
        //string RememberPassword(string email);
        void Remove(int id);
        bool Update(User item);
        List<Repository.OptionsMenu> GetMenu();
        List<Repository.ActivitiesByView_Result> GetUserActivities(string userId, int state);
        string SetDateToActivity(Repository.UpdateActivity updAct);
        bool DeleteActivity(Repository.Activity actId);
        int CreateActivity(Repository.Activity activity);
    }
}
