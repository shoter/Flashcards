using Common.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Entities.Repositories
{
    public class FlashcardTranslationRepository : RepositoryBase<FlashcardTranslation, FlashcardsEntities>, IFlashcardTranslationRepository
    {
        public FlashcardTranslationRepository(FlashcardsEntities context) : base(context)
        {
        }

        public List<FlashcardTranslation> GetTranslationsForFlashcard(int flashcardID)
        {
            return Where(ft => ft.FlashcardID == flashcardID)
                .ToList();
        }
    }
}
