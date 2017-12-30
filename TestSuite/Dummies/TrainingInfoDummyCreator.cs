using Common.utilities;
using Flashcards.Entities;
using Services.Session.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSuite.Dummies
{
    public class TrainingInfoDummyCreator : IDummyCreator<TrainingInfo>
    {
        private static UniqueIDGenerator uniqueID = new UniqueIDGenerator();
        private LanguageDummyCreator languageDummyCreator = new LanguageDummyCreator();
        private TrainingInfo trainingInfo;
        private Language language;

        private int cardCount = 5;

        public TrainingInfoDummyCreator()
        {
            trainingInfo = create();
        }

        private TrainingInfo create()
        {
            language = languageDummyCreator.Create();

            return new TrainingInfo(new Flashcards.Entities.Models.UserInfo()
            {
                TrainingDateStarted = DateTime.Now,
                TrainingID = (long)uniqueID,
                TrainingCards = new List<Flashcards.Entities.Models.TrainingCardInfo>()
            });
        }

        public TrainingInfoDummyCreator SetCardCount(int count)
        {
            cardCount = count;
            return this;
        }
        public TrainingInfoDummyCreator SetLanguage(Language language)
        {
            this.language = language;
            return this;
        }
        public TrainingInfo Create()
        {
            var cardCreator = new TrainingCardInfoDummyCreator(language);
            for (int i = 0; i < cardCount; ++i)
                trainingInfo.Cards.Add(cardCreator.Create());

            var temp = trainingInfo;
            trainingInfo = create();
            return temp;
        }
    }
}
