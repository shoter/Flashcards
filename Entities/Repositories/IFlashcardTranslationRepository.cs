using Common.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Entities.Repositories
{
    public interface IFlashcardTranslationRepository : IRepository<FlashcardTranslation>
    {
        List<FlashcardTranslation> GetTranslationsForFlashcard(int flashcardID);
    }
}
