using Flashcards.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Entities
{
    public class FlashcardUnit : IDisposable, IFlashcardUnit
    {
        public IFlashcardRepository FlashcardRepository { get; private set; }
        public ILanguageRepository LanguageRepository { get; private set; }
        public IFlashcardTranslationRepository FlashcardTranslationRepository { get; private set; }
        public IFlashcardImageRepository FlashcardImageRepository { get; private set; }
        public ITrainingCardRepository TrainingCardRepository { get; private set; }
        public IUserFlashcardMemoryRepository UserFlashcardMemoryRepository { get; private set; }

        public ITrainingRepository TrainingRepository { get; private set; }
        public IInfoRepository InfoRepository { get; private set; }

        private readonly FlashcardsEntities context;

        public FlashcardUnit(FlashcardsEntities context)
        {
            this.context = context;
            this.FlashcardRepository = new FlashcardRepository(context); ;
            this.LanguageRepository = new LanguageRepository(context);
            this.FlashcardTranslationRepository = new FlashcardTranslationRepository(context);
            this.FlashcardImageRepository = new FlashcardImageRepository(context);
            this.TrainingCardRepository = new TrainingCardRepository(context);
            this.UserFlashcardMemoryRepository = new UserFlashcardMemoryRepository(context);
            this.TrainingRepository = new TrainingRepository(context);
            this.InfoRepository = new InfoRepository(context);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        bool disposed = false;
        public void Dispose()
        {
            if (disposed == false)
                context.Dispose();
            disposed = true;
        }
    }
}
