using Flashcards.Entities;
using Services.Session.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSuite.Dummies
{
    public class ReviewCardInfoDummyCreator : IDummyCreator<ReviewCardInfo>
    {
        public ReviewCardInfoDummyCreator()
        {
        }

        public ReviewCardInfo Create()
        {

            return new ReviewCardInfo(new Flashcards.Entities.Models.DbReviewCard()
            {
                FlashcardID = new FlashcardDummyCreator().Create().ID,
                InternalLossCount = 0
            });
        }
    }
}
