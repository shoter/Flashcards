using Flashcards.Entities;
using Flashcards.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Flashcards.Models.Flashcards
{
    public class AnswerTranslationList
    {
        public List<AnswerTranslation> Translations { get; set; } = new List<AnswerTranslation>();

        public AnswerTranslationList(Flashcard flashcard, Language language)
        {
            var translationRepository = DependencyResolver.Current.GetService<IFlashcardTranslationRepository>();

            var translations = translationRepository.GetTRanslationsForFlashcard(flashcard.ID, language.ID);

            foreach (var translation in translations)
            {
                Translations.Add(new AnswerTranslation(translation));
            }
        }
    }
}