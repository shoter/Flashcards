using Flashcards.Entities;
using Flashcards.Entities.Repositories;
using Ninject;
using Ninject.Web.Common;
using Services.Implementation;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardCommon
{
    public class NinjectCommon
    {
        public static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<FlashcardsEntities>().ToSelf().InRequestScope();

            kernel.Bind<IFlashcardRepository>().To<FlashcardRepository>().InRequestScope();
            kernel.Bind<ILanguageRepository>().To<LanguageRepository>().InRequestScope();
            kernel.Bind<IFileRepository>().To<FileRepository>().InRequestScope();
            kernel.Bind<IFlashcardImageRepository>().To<FlashcardImageRepository>().InRequestScope();
            kernel.Bind<IPopupService>().To<PopupService>().InRequestScope();
            kernel.Bind<IFlashcardTranslationService>().To<FlashcardTranslationService>().InRequestScope();
            kernel.Bind<IFlashcardTranslationRepository>().To<FlashcardTranslationRepository>().InRequestScope();
            kernel.Bind<IFlashcardImageService>().To<FlashcardImageService>().InRequestScope();
            kernel.Bind<IUploadService>().To<UploadService>().InRequestScope();
            kernel.Bind<IFlashcardUnit>().To<FlashcardUnit>().InRequestScope();
            kernel.Bind<ITrainingRepository>().To<TrainingRepository>().InRequestScope();
            kernel.Bind<IInfoRepository>().To<InfoRepository>().InRequestScope();
            kernel.Bind<ITrainingReviewService>().To<TrainingReviewService>().InRequestScope();
            kernel.Bind<ISessionService>().To<SessionService>().InRequestScope();
            kernel.Bind<IReviewCardRepository>().To<ReviewCardRepository>().InRequestScope();
            kernel.Bind<IInternalReviewRepository>().To<InternalReviewRepository>().InRequestScope();
            kernel.Bind<IInternalReviewService>().To<InternalReviewService>().InRequestScope();
            kernel.Bind<IUserFLashcardMemoryService>().To<UserFLashcardMemoryService>().InRequestScope();
            kernel.Bind<IStrengthService>().To<StrengthService>().InRequestScope();
            kernel.Bind<IIntervalService>().To<IntervalService>().InRequestScope();
            kernel.Bind<IQuestionService>().To<QuestionService>().InRequestScope();
            
        }
    }
}
