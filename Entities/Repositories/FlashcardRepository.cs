using Common.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Entities.Repositories
{
    public class FlashcardRepository : RepositoryBase<Flashcard, FlashcardsEntities>, IFlashcardRepository
    {
        public FlashcardRepository(FlashcardsEntities context) : base(context)
        {
        }

        public IQueryable<Flashcard> SearchForFlashcard(string universalName)
        {
            universalName = universalName.Trim().ToLower();
            return Where(f => f.Name.ToLower().Contains(universalName));
        }
    }
}
