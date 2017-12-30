using Common.utilities;
using Flashcards.Entities;
using Flashcards.Entities.Models;
using Services.Session.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSuite.Dummies
{
    public class ReviewInfoDummyCreator : IDummyCreator<ReviewInfo>
    {
        private static UniqueIDGenerator uniqueID = new UniqueIDGenerator();
        private ReviewInfo reviewInfo;
        private Language language;
        private int cardCount = 30;

        public ReviewInfoDummyCreator()
        {
            this.reviewInfo = create();
        }

        private ReviewInfo create()
        {
            language = new LanguageDummyCreator().Create();
            return new ReviewInfo(new Flashcards.Entities.Models.UserInfo()
            {
                ReviewID = (long)uniqueID,
                ReviewDateStarted = DateTime.Now,
                ReviewLanguageID = 0,
                ReviewCards = new List<DbReviewCard>()
            });
        }

        public ReviewInfoDummyCreator SetLanguage(Language lang)
        {
            language = lang;
            return this;
        }

        public ReviewInfoDummyCreator SetCardCount(int count)
        {
            this.cardCount = count;
            return this;
        }


        public ReviewInfo Create()
        {
            var temp = reviewInfo;
            reviewInfo = create();

            temp.LanguageID = language.ID;
            var cardCreator = new ReviewCardInfoDummyCreator();
            for (int i = 0; i < cardCount; ++i)
                temp.ReviewCards.Add(cardCreator.Create());

            return temp;

        }
    }
}
