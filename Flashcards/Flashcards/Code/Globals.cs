using Flashcards.Entities;
using Flashcards.Entities.Repositories;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Flashcards.Code
{
    public class Globals
    {
        private static List<Language> languages;
        public static List<Language> Languages
        {
            get
            {
                if (languages == null)
                {
                    var langRepository = DependencyResolver.Current.GetService<ILanguageRepository>();
                    languages = langRepository.GetAll().ToList();
                }
                return languages;
            }
        }

        public static List<SelectListItem> GenerateLanguageSelectList()
        {
            var sessionService = DependencyResolver.Current.GetService<ISessionService>();
            int currentLanguageID = sessionService.LanguageID;
            var ret = new List<SelectListItem>();
            foreach (var lang in Languages)
            {
                ret.Add(new SelectListItem()
                {
                    Text = lang.EnglishName,
                    Value = lang.ID.ToString(),
                    Selected = lang.ID == currentLanguageID
                });
            }
            return ret;
        }
    }

}
