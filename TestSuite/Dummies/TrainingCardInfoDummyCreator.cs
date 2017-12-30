using Flashcards.Entities;
using Services.Session.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSuite.Dummies
{
    public class TrainingCardInfoDummyCreator : IDummyCreator<TrainingCardInfo>
    {
        private FlashcardDummyCreator flashcardCreator = new FlashcardDummyCreator();
        private Language language;
        public TrainingCardInfoDummyCreator(Language language)
        {
            this.language = language;
        }

        public TrainingCardInfo Create()
        {
            return new TrainingCardInfo(new Flashcards.Entities.Models.TrainingCardInfo()
            {
                FlashcardID = flashcardCreator.Create().ID,
                InternalLossCount = 0,
                LanguageID = language.ID
            });
        }
    }
}
