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
            kernel.Bind<IPopupService>().To<PopupService>().InRequestScope();
            kernel.Bind<IFlashcardTranslationRepository>().To<FlashcardTranslationRepository>().InRequestScope();
            kernel.Bind<FlashcardUnit>().To<FlashcardUnit>().InRequestScope();
        }
    }
}
