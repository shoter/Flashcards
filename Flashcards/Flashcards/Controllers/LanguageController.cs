using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Services.Interfaces;
using System.Web.Mvc;
using Flashcards.Code;
using Flashcards.Entities;

namespace Flashcards.Controllers
{
    public class LanguageController : ControllerBase
    {
        private readonly IFlashcardUnit unit;
        public LanguageController(IPopupService popupService, ISessionService sessionService, IFlashcardUnit unit) : base(popupService, sessionService)
        {
            this.unit = unit;
        }
        [Authorize(Roles = Groups.User)]
        [HttpPost]
        public ActionResult ChangeLanguage(int currentLanguageID)
        {
            var language = unit.LanguageRepository.GetById(currentLanguageID);

            if (language == null)
                return RedirectBackWithError("Language does not exist!");

            sessionService.LanguageID = language.ID;
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}