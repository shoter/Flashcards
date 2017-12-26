﻿using Services.Interfaces;
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

                var dbUserInfo = unit.InfoRepository.GetUserInfo(UserID);

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
                if (languageID == null)
                    languageID = unit.LanguageRepository.FirstOrDefault().ID;

                return languageID.Value;
            }
            set
            {
                languageID = value;
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

    }
}
