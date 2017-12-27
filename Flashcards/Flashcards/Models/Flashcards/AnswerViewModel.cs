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
        public double Correctness { get; set; }
        public AnswerViewModel(int flashcardID, Language language, bool isCorrect, double correctness)
        {
            TranslationList = new AnswerTranslationList(flashcardID, language);
            IsCorrect = isCorrect;
            Correctness = correctness;
        }
    }
}