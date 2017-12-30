using Flashcards.Entities;
using Flashcards.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Session.Models
{
    public class ReviewCardInfo
    {
        public int FlashcardID { get; set; }
        public int InternalLossCount { get; set; }

        public ReviewCardInfo(DbReviewCard reviewCard)
        {
            FlashcardID = reviewCard.FlashcardID;
            InternalLossCount = reviewCard.InternalLossCount;
        }

        public ReviewCardInfo(ReviewCard reviewCard)
        {
            FlashcardID = reviewCard.FlashcardID;
            InternalLossCount = reviewCard.InternallLossCount;
        }



    }
}
