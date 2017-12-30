using Common.utilities;
using Flashcards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSuite.Dummies
{
    public class FlashcardDummyCreator : IDummyCreator<Flashcard>
    {
        private static UniqueIDGenerator uniqueID = new UniqueIDGenerator();
        private FlashcardTranslationDummyCreator translationCreator;
        private Flashcard flashcard;

        public FlashcardDummyCreator()
        {
            flashcard = create();
        }

        private Flashcard create()
        {
            var flash = new Flashcard()
            {
                ID = uniqueID,
                Name = RandomGenerator.GenerateString(10)
            };

            translationCreator = new FlashcardTranslationDummyCreator(flash);

            return flash;
        }

        public FlashcardDummyCreator AddRandomTranslation()
        {
            translationCreator.Create();
            return this;
        }

        public FlashcardDummyCreator AddRandomTranslations(int count)
        {
            for(int i = 0; i < count;++i)
                translationCreator.Create();
            return this;
        }

        public FlashcardDummyCreator SetName(string name)
        {
            flashcard.Name = name;
            return this;
        }

        public Flashcard Create()
        {
            var temp = flashcard;
            flashcard = create();
            return temp;
        }
    }
}
