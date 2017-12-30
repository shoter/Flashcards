using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Session.Models;
using Flashcards.Entities;
using System.Web;
using System.Runtime.CompilerServices;
using System.Web.Mvc;

namespace Services.Implementation
{
    public class SessionService : ISessionService
    {
        private readonly IFlashcardUnit unit;
        public SessionService(IFlashcardUnit unit)
        {
            this.unit = unit;
        }

        public UserInfo UserInfo
        {
            get
            {
                var userInfo = GetValue<UserInfo>();
                if (userInfo != null)
                    return userInfo;

                var dbUserInfo = unit.InfoRepository.GetUserInfo(UserID, LanguageID);

                userInfo = new UserInfo(dbUserInfo);
                SetValue(userInfo);

                return userInfo;
            }
        }

        private int? languageID = null;
        public int LanguageID
        {
            get
            {
                if (GetObjectValue() == null)
                    SetValue(unit.LanguageRepository.FirstOrDefault().ID);

                return (int)GetObjectValue();
            }
            set
            {
                SetValue(value);

                //try to find review/training for new language
                var review = unit.InternalReviewRepository.GetReviewForUser(UserID, value);
                var training = unit.TrainingRepository.GetTrainingForUser(UserID, value);
                if (review != null)
                    UserInfo.ReviewInfo = new ReviewInfo(review);
                else
                    UserInfo.ReviewInfo = null;
                if (training != null)
                    UserInfo.TrainingInfo = new TrainingInfo(training);
                else
                    UserInfo.TrainingInfo = null;

                
            }
        }

        public string UserID
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public static void SetValue(object value, [CallerMemberName] string name = "")
        {
            HttpContext.Current.Session[name] = value;
        }

        public static T GetValue<T>([CallerMemberName] string name = "")
            where T : class
        {
            if (HttpContext.Current.Session[name] == null)
                return null;

            return (T)HttpContext.Current.Session[name];
        }

        public static object GetObjectValue([CallerMemberName] string name = "")
        {
            if (HttpContext.Current.Session[name] == null)
                return null;

            return HttpContext.Current.Session[name];
        }

    }
}
