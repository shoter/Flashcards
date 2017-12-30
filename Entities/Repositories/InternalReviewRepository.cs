using Common.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Entities.Repositories
{
    public class InternalReviewRepository : RepositoryBase<InternalReview, FlashcardsEntities>, IInternalReviewRepository
    {
        public InternalReviewRepository(FlashcardsEntities context) : base(context)
        {
        }

        public InternalReview GetReviewForUser(string userID, int languageID)
        {
            return FirstOrDefault(r => r.UserID == userID && r.LanguageID == languageID);
        }
    }
}
