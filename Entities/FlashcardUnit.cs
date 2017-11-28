using Flashcards.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Entities
{
    public class FlashcardUnit
    {
        public readonly IFlashcardRepository FlashcardRepository;
        public readonly ILanguageRepository LanguageRepository;
        public readonly IFlashcardTranslationRepository FlashcardTranslationRepository;
        public FlashcardUnit(IFlashcardRepository flashcardRepository, ILanguageRepository languageRepository, IFlashcardTranslationRepository flashcardTranslationRepository)
        {
            this.FlashcardRepository = flashcardRepository;
            this.LanguageRepository = languageRepository;
            this.FlashcardTranslationRepository = flashcardTranslationRepository;
        }
    }
}
