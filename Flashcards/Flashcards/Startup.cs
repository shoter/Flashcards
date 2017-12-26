using Common.EntityFramework.Enums;
using Flashcards.Entities;
using Flashcards.Entities.Enums.Files;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Flashcards.Startup))]
namespace Flashcards
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            startEnumators();
        }

        private void startEnumators()
        {
            using (new Enumator<FileType, FileTypeEnum, FlashcardsEntities>().CreateNewIfAble()) { }
        }
    }
}
