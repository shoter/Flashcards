using Flashcards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flashcards.Models.Manage
{
    public class EditTranslationViewModel
    {
        public long TranslationID { get; set; }

        public string Translation { get; set; }
        public string Pronounciation { get; set; }
        public double Significance { get; set; }

        public EditTranslationViewModel() { }
        public EditTranslationViewModel(FlashcardTranslation flashcardTranslation)
        {
            TranslationID = flashcardTranslation.ID;
            Translation = flashcardTranslation.Translation;
            Pronounciation = flashcardTranslation.Pronounciation;
            Significance = (double)flashcardTranslation.Significance;
        }
    }
}