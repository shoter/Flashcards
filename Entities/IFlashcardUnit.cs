using Flashcards.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Entities
{
    public interface IFlashcardUnit
    {
        IFlashcardRepository FlashcardRepository { get; }
        ILanguageRepository LanguageRepository { get; }
        IFlashcardTranslationRepository FlashcardTranslationRepository { get; }
        IFlashcardImageRepository FlashcardImageRepository { get; }
        ITrainingCardRepository TrainingCardRepository { get; }
        IUserFlashcardMemoryRepository UserFlashcardMemoryRepository { get; }
        ITrainingRepository TrainingRepository { get; }
        IInfoRepository InfoRepository { get; }

        void SaveChanges();
    }
}
