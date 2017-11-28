using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Operations;
using Flashcards.Entities;
using Flashcards.Entities.Repositories;

namespace Services.Implementation
{
    public class FlashcardTranslationService : IFlashcardTranslationService
    {
        private readonly IFlashcardTranslationRepository flashcardTranslationRepository;

        public FlashcardTranslationService(IFlashcardTranslationRepository flashcardTranslationRepository)
        {
            this.flashcardTranslationRepository = flashcardTranslationRepository;
        }

        public void AddTranslation(Flashcard flashcard, string translation, string pronounciation, double significance)
        {
            var dbTranslation = new FlashcardTranslation()
            {
                FlashcardID = flashcard.ID,
                Translation = translation,
                Pronounciation = pronounciation,
                Significance = (decimal)significance
            };

            flashcardTranslationRepository.Add(dbTranslation);
            flashcardTranslationRepository.SaveChanges();
        }

        public MethodResult CanAddTranslation(Flashcard flashcard, string translation, string pronounciation, double significance)
        {
            if (flashcard == null)
                return new MethodResult("Flashcard does not exist!");
            return checkCorrectnesOfTranslation(translation, pronounciation, significance);

        }

        private static MethodResult checkCorrectnesOfTranslation(string translation, string pronounciation, double significance)
        {
            if (string.IsNullOrEmpty(translation))
                return new MethodResult("Translation is empty!");
            if (string.IsNullOrEmpty(pronounciation))
                return new MethodResult("Pronounciation is empty!");
            if (significance < 0)
                return new MethodResult("Significance cannot be less than 0!");
            if (significance > 1.0)
                return new MethodResult("Significance cannot be bigger than 0!");
            return MethodResult.Success;
        }

        public MethodResult CanChangeTranslation(FlashcardTranslation flashTranslation, string translation, string pronounciation, double significance)
        {
            if (flashTranslation == null)
                return new MethodResult("Translation does not exist!");
            return checkCorrectnesOfTranslation(translation, pronounciation, significance);
        }

        public MethodResult CanRemoveTranslation(FlashcardTranslation translation)
        {
            if (translation == null)
                return new MethodResult("Translation does not exist!");
            return MethodResult.Success;
        }

        public void ChangeTranslation(FlashcardTranslation flashTranslation, string translation, string pronounciation, double significance)
        {
            flashTranslation.Translation = translation;
            flashTranslation.Pronounciation = pronounciation;
            flashTranslation.Significance = (decimal)significance;

            flashcardTranslationRepository.SaveChanges();
        }

        public void RemoveTranslation(FlashcardTranslation translation)
        {
            flashcardTranslationRepository.Remove(translation);
        }
    }
}
