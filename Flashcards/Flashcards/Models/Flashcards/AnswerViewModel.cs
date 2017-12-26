using Flashcards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flashcards.Models.Flashcards
{
    public class AnswerViewModel
    {
        public AnswerTranslationList TranslationList { get; set; }
        public bool IsCorrect { get; set; }
        public AnswerViewModel(Flashcard flashcard, Language language, bool isCorrect)
        {
            TranslationList = new AnswerTranslationList(flashcard, language);
            IsCorrect = isCorrect;
        }
    }
}