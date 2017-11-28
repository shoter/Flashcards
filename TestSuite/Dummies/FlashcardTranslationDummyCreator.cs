using Common.utilities;
using Flashcards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSuite.Dummies
{
    public class FlashcardTranslationDummyCreator : IDummyCreator<FlashcardTranslation>
    {
        private static UniqueIDGenerator uniqueID = new UniqueIDGenerator();
        private FlashcardTranslation flashcardTranslation;
        private LanguageDummyCreator languageDummyCreator = new LanguageDummyCreator();
        private Flashcard parent;

        public FlashcardTranslationDummyCreator(Flashcard parent)
        {
            this.parent = parent;
            this.flashcardTranslation = create();
        }

        private FlashcardTranslation create()
        {
            var tr = new FlashcardTranslation()
            {
                ID = uniqueID,
                Flashcard = parent,
                FlashcardID = parent.ID,
                Significance = 1.0m,
                Pronounciation = RandomGenerator.GenerateString(10),
                Translation = RandomGenerator.GenerateString(10),
                SpecificImageFileID = null
            };

            var lang = languageDummyCreator.Create();

            tr.Language = lang;
            tr.LanguageID = lang.ID;
            parent.FlashcardTranslations.Add(tr);
            lang.FlashcardTranslations.Add(tr);

            return tr;
        }
        public FlashcardTranslation Create()
        {
            var temp = flashcardTranslation;
            flashcardTranslation = create();
            return temp;
        }
    }
}
