using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Entities.Models
{
    public class DbReviewCard
    {
        public int FlashcardID { get; set; }
        public int InternalLossCount { get; set; }

        public DbReviewCard() { }
        public DbReviewCard(ReviewCard card)
        {
            FlashcardID = card.FlashcardID;
            InternalLossCount = card.InternallLossCount;
        }
    }
}
