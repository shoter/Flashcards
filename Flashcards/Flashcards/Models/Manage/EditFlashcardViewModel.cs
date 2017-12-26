using Flashcards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Flashcards.Models.Manage
{
    public class EditFlashcardViewModel
    {
        public int FlashcardID { get; set; }
        public string LanguageSymbol { get; set; }
        public int? LanguageID { get; set; }

        public List<SelectListItem> Languages { get; set; } = new List<SelectListItem>();
        public List<EditTranslationViewModel> Translations { get; set; } = new List<EditTranslationViewModel>();
        public List<EditFlashcardImageViewModel> Images { get; set; }

        public EditTranslationViewModel NewTranslation { get; set; } = new EditTranslationViewModel();

        /// <summary>
        /// Will have disabled as content if there is need for disable
        /// </summary>
        public string Disabled { get; set; }
        public EditFlashcardViewModel(Flashcard flashcard, Language currentLanguage, IEnumerable<Language> languages)
        {
            FlashcardID = flashcard.ID;
            if (currentLanguage != null)
            {
                var translations = flashcard.FlashcardTranslations.Where(t => t.LanguageID == currentLanguage.ID).ToList();
                Translations = translations.Select(t => new EditTranslationViewModel(t)).ToList();
            }
            else
                Translations = new List<EditTranslationViewModel>();

            Images = flashcard.FlashcardImages.ToList()
                .Select(img => new EditFlashcardImageViewModel(img)).ToList();

            Languages.Add(new SelectListItem()
            {
                Value = "",
                Text = "-- Select Language --"
            });

            Languages.AddRange(languages.Select(l => new SelectListItem()
            {
                Text = l.EnglishName,
                Value = l.Symbol
            }));

            LanguageSymbol = currentLanguage?.Symbol;

            Disabled = LanguageSymbol == null ? "disabled" : "";
            LanguageID = currentLanguage?.ID;

            NewTranslation.Significance = 1.0;
        }
    }
}