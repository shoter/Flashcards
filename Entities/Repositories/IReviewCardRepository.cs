using Common.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Entities.Repositories
{
    public interface IReviewCardRepository : IRepository<ReviewCard>
    {
        void UpdateCardLossCount(long reviewID, int flashcardID, int lossCount);
        ReviewCard AddCardBasedOnMemory(UserFlashcardMemory memory, long reviewID);
        ReviewCard GetCardForReview(long reviewID, int cardID);
    }
}
