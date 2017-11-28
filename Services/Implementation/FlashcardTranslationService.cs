using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Operations;
using Flashcards.Entities;

namespace Services.Implementation
{
    public class FlashcardTranslationService : IFlashcardTranslationService
    {
        public void AddTranslation(Flashcard flashcard, string translation, string pronounciation, double significance)
        {
            throw new NotImplementedException();
        }

        public MethodResult CanAddTranslation(Flashcard flashcard, string translation, string pronounciation, double significance)
        {
            throw new NotImplementedException();
        }

        public MethodResult CanChangeTranslation(FlashcardTranslation flashTranslation, string translation, string pronounciation, double significance)
        {
            throw new NotImplementedException();
        }

        public MethodResult CanRemoveTranslation(FlashcardTranslation translation)
        {
            throw new NotImplementedException();
        }

        public void ChangeTranslation(FlashcardTranslation flashTranslation, string translation, string pronounciation, double significance)
        {
            throw new NotImplementedException();
        }

        public void RemoveTranslation(FlashcardTranslation translation)
        {
            throw new NotImplementedException();
        }
    }
}
