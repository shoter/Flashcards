using Common.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Entities.Repositories
{
    public class FlashcardImageRepository : RepositoryBase<FlashcardImage, FlashcardsEntities>, IFlashcardImageRepository
    {
        public FlashcardImageRepository(FlashcardsEntities context) : base(context)
        {
        }
    }
}
