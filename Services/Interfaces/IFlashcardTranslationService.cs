using Common.Operations;
using Flashcards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IFlashcardTranslationService
    {
        MethodResult CanAddTranslation(Flashcard flashcard, Language language, string translation, string pronounciation, double significance);
        void AddTranslation(Flashcard flashcard, Language language, string translation, string pronounciation, double significance);

        MethodResult CanRemoveTranslation(FlashcardTranslation translation);
        void RemoveTranslation(FlashcardTranslation translation);

        MethodResult CanChangeTranslation(FlashcardTranslation flashTranslation, string translation, string pronounciation, double significance);
        void ChangeTranslation(FlashcardTranslation flashTranslation, string translation, string pronounciation, double significance);
    }
}
