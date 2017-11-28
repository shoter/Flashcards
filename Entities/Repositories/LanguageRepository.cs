using Common.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Entities.Repositories
{
    public class LanguageRepository : RepositoryBase<Language, FlashcardsEntities>, ILanguageRepository
    {
        public LanguageRepository(FlashcardsEntities context) : base(context)
        {
        }
    }
}
