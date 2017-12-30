using Common.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Entities.Repositories
{
    public class ReviewCardRepository : RepositoryBase<ReviewCard, FlashcardsEntities>, IReviewCardRepository
    {
        public ReviewCardRepository(FlashcardsEntities context) : base(context)
        {
        }

        public void UpdateCardLossCount(long reviewID, int flashcardID, int lossCount)
        {
            UpdateSingleField(x => x.InternallLossCount,
                x =>
                {
                    x.ReviewID = reviewID;
                    x.FlashcardID = flashcardID;
                },
                lossCount);
        }

        public ReviewCard AddCardBasedOnMemory(UserFlashcardMemory memory, long reviewID)
        {
            var card = new ReviewCard()
            {
                ReviewID = reviewID,
                FlashcardID = memory.FlashcardID,
                InternallLossCount = 0
            };
            Add(card);
            return card;
        }

        public ReviewCard GetCardForReview(long reviewID, int cardID)
        {
            return FirstOrDefault(
                c =>  c.FlashcardID == cardID && c.ReviewID == reviewID);
        }
    }
}
