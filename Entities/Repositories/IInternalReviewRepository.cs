using Common.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Entities.Repositories
{
    public interface IInternalReviewRepository : IRepository<InternalReview>
    {
        InternalReview GetReviewForUser(string userID, int languageID);
    }
}
