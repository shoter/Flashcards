using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Services.Interfaces;
using Flashcards.Entities;
using Flashcards.Models.Flashcards;
using System.Web.Mvc;

namespace Flashcards.Controllers
{
    public class QuestionController : ControllerBase
    {
        private readonly FlashcardUnit unit;
        public QuestionController(IPopupService popupService, ISessionService sessionService, FlashcardUnit unit) : base(popupService, sessionService)
        {
            this.unit = unit;
        }

        public ActionResult Test()
        {
            var flashcard = unit.FlashcardRepository.First();

            var vm = new QuestionWithAnswerViewModel(flashcard, 1);

            return View(vm);
        }
    }
}