using Common.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Entities.Repositories
{
    public class FileRepository : RepositoryBase<File, FlashcardsEntities>, IFileRepository
    {
        public FileRepository(FlashcardsEntities context) : base(context)
        {
        }
    }
}
