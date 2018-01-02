using Common.Extensions;
using Common.Operations;
using Flashcards.Code;
using Flashcards.Entities;
using Flashcards.Models.Manage;
using Microsoft.AspNet.Identity;
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
        private readonly IFlashcardImageService flashcardImageService;
        private readonly IUploadService uploadService;
        public ManagementController(FlashcardUnit unit, IPopupService popupService, IFlashcardTranslationService flashcardTranslationService,
            IFlashcardImageService flashcardImageService, ISessionService sessionService, IUploadService uploadService) : base(popupService, sessionService)
        {
            this.unit = unit;
            this.flashcardTranslationService = flashcardTranslationService;
            this.flashcardImageService = flashcardImageService;
            this.uploadService = uploadService;
        }

        [Authorize(Roles = Groups.Administrator)]
        [Route("Management/")]
        public ActionResult Index()
        {
            return View();
        }


        [Authorize(Roles = Groups.Administrator)]
        [Route("Management/{flashcardID:int}/{languageSymbol}")]
        public ActionResult EditFlashcard(int flashcardID, string languageSymbol = null)
        {
            if (languageSymbol == "auto")
                languageSymbol = null;

            var flashcard = unit.FlashcardRepository.GetById(flashcardID);
            var language = unit.LanguageRepository.GetBySymbol(languageSymbol);

            if (flashcard == null)
                return RedirectBackWithError("Flashcard not found!");

            var languages = unit.LanguageRepository.GetAll();

            var vm = new EditFlashcardViewModel(flashcard, language, languages);
            return View(vm);
        }

        [Authorize(Roles = Groups.Administrator)]
        [HttpGet]
        public ActionResult AddFlashcard()
        {
            return View();
        }
        [Authorize(Roles = Groups.Administrator)]
        [HttpPost]
        public ActionResult AddFlashcard(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                AddError("Flashcard name is empty!");
            else
            {
                name = name.ToLower();
                var flash = unit.FlashcardRepository.FirstOrDefault(f => f.Name.ToLower() == name);
                if (flash != null)
                    AddError("Flashcard with this name exists!");
                else
                {
                    flash = new Flashcard() { Name = name.FirstUpper() };
                    unit.FlashcardRepository.Add(flash);
                    unit.FlashcardRepository.SaveChanges();
                   // return EditFlashcard(flash.ID);
                    return RedirectToAction("EditFlashcard", "Management", new { flashcardID = flash.ID, languageSymbol = (string)null });
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = Groups.Administrator)]
        public ActionResult RemoveImage(int flashcardID, int fileID)
        {
            var image = unit.FlashcardImageRepository.First(f => f.FlashcardID == flashcardID && f.FileID == fileID);
            uploadService.RemoveUploadFile(image.File);
            AddSuccess("Image removed!");
            return RedirectBack();
        }

        [Authorize(Roles = Groups.Administrator)]
        [HttpPost]
        public ActionResult PostEditFlashcard(int flashcardID, string languageSymbol)
        {
            return RedirectToAction(nameof(EditFlashcard), new { flashcardID = flashcardID, languageSymbol = languageSymbol });
        }

        [Authorize(Roles = Groups.Administrator)]
        [HttpPost]
        public ActionResult AddTranslation(int flashcardID, int languageID, EditTranslationViewModel NewTranslation)
        {
            var flashcard = unit.FlashcardRepository.GetById(flashcardID);
            var language = unit.LanguageRepository.GetById(languageID);
            var result = flashcardTranslationService.CanAddTranslation(flashcard, language, NewTranslation.Translation, NewTranslation.Pronounciation, NewTranslation.Significance);

            if (result.IsError)
                return RedirectBackWithError(result);

            flashcardTranslationService.AddTranslation(flashcard, language, NewTranslation.Translation, NewTranslation.Pronounciation, NewTranslation.Significance);

            AddSuccess("Translation has been added!");
            return RedirectBack();

        }
        [Authorize(Roles = Groups.Administrator)]
        [HttpPost]
        public ActionResult ChangeTranslations(List<EditTranslationViewModel> translations)
        {
            MethodResult globalResult = new MethodResult();

            foreach (var vm in translations)
            {
                var translation = unit.FlashcardTranslationRepository.GetById(vm.TranslationID);

                var result = flashcardTranslationService.CanChangeTranslation(translation, vm.Translation, vm.Pronounciation, vm.Significance);
                if (result.IsError)
                    globalResult.AddError($"Translation {vm.TranslationID} - {result.Errors[0]}");
            }

            if (globalResult.IsError)
                return RedirectBackWithError(globalResult);

            foreach (var vm in translations)
            {
                var translation = unit.FlashcardTranslationRepository.GetById(vm.TranslationID);
                flashcardTranslationService.ChangeTranslation(translation, vm.Translation, vm.Pronounciation, vm.Significance);
                AddSuccess($"Translation {translation.ID} has been changed!");
            }

            return RedirectBack();
        }
        [Authorize(Roles = Groups.Administrator)]
        [HttpPost]
        public ActionResult AddNewImageToFlashcard(int flashcardID, HttpPostedFileBase file)
        {
            var flashcard = unit.FlashcardRepository.GetById(flashcardID);
            var result = flashcardImageService.CanUploadNewImage(flashcard, file);
            if (result.IsError)
                return RedirectBackWithError(result);

            flashcardImageService.UploadNewImage(flashcard, file, User.Identity.GetUserId());

            return RedirectBackWithSuccess("Image successfully uploaded!");
        }

        [AjaxOnly]
        [Authorize(Roles = Groups.Administrator)]
        [HttpPost]
        public JsonResult Search(string query)
        {
            try
            {

                var flashcards = unit.FlashcardRepository.SearchForFlashcard(query)
                    .Take(100).ToList();

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