using Flashcards.Code;
using Flashcards.Entities;
using Flashcards.Models.Manage;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUtils.Attributes;

namespace Flashcards.Controllers
{
    public class ManagementController : ControllerBase
    {
        private readonly FlashcardUnit unit;
        private readonly IFlashcardTranslationService flashcardTranslationService;

        public ManagementController(FlashcardUnit unit, IPopupService popupService, IFlashcardTranslationService flashcardTranslationService) : base(popupService)
        {
            this.unit = unit;
            this.flashcardTranslationService = flashcardTranslationService;
        }

        [Authorize(Roles = Groups.Administrator)]
        [Route("Management/")]
        public ActionResult Index()
        {
            return View();
        }


        [Authorize(Roles = Groups.Administrator)]
        [Route("Management/{flashcardID:int}")]
        public ActionResult EditFlashcard(int flashcardID)
        {
            var flashcard = unit.FlashcardRepository.GetById(flashcardID);
            if (flashcard == null)
                return RedirectBackWithError("Flashcard not found!");

            var vm = new EditFlashcardViewModel(flashcard);
            return View(vm);
        }

        public ActionResult AddTranslation(int flashcardID, EditTranslationViewModel vm)
        {
            var flashcard = unit.FlashcardRepository.GetById(flashcardID);
            var result = flashcardTranslationService.CanAddTranslation(flashcard, vm.Translation, vm.Pronounciation, vm.Significance);

            if (result.IsError)
                return RedirectBackWithError(result);

            flashcardTranslationService.AddTranslation(flashcard, vm.Translation, vm.Pronounciation, vm.Significance);

            AddSuccess("Translation has been added!");
            return RedirectBack();

        }

        [AjaxOnly]
        [Authorize(Roles = Groups.Administrator)]
        [HttpPost]
        public JsonResult Search(string query)
        {
            try
            {
                var flashcards = unit.FlashcardRepository.SearchForFlashcard(query)
                    .Take(10).ToList();

                return JsonData(flashcards.Select(f => new
                {
                    Name = f.Name,
                    ID = f.ID
                }));

            }
            catch (Exception e)
            {
                return UndefinedJsonError(e);
            }
        }


    }
}