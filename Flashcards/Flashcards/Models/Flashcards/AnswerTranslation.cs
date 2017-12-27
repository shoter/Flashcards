using Flashcards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flashcards.Models.Flashcards
{
    public class AnswerTranslation
    {
        public string Translation { get; set; }
        public string Pronounciation { get; set; }
        public double Significance { get; set; }
        public AnswerTranslation(FlashcardTranslation translation)
        {
            Translation = translation.Translation;
            Pronounciation = translation.Pronounciation;
            Significance = (double)translation.Significance;
        }
    }
}