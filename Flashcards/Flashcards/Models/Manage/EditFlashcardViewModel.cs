using Flashcards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flashcards.Models.Manage
{
    public class EditFlashcardViewModel
    {
        public int FlashcardID { get; set; }
        public List<EditTranslationViewModel> Translations { get; set; } = new List<EditTranslationViewModel>();

        public EditTranslationViewModel NewTranslation { get; set; } = new EditTranslationViewModel();
        public EditFlashcardViewModel(Flashcard flashcard)
        {
            FlashcardID = flashcard.ID;
            Translations = flashcard.FlashcardTranslations.ToList()
                .Select(t => new EditTranslationViewModel(t)).ToList();
        }
    }
}