using Common.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Entities.Repositories
{
    public interface IFlashcardRepository : IRepository<Flashcard>
    {
        IQueryable<Flashcard> SearchForFlashcard(string universalName);
    }
}
