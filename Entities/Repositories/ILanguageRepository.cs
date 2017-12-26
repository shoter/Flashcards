using Common.EntityFramework;

namespace Flashcards.Entities.Repositories
{
    public interface ILanguageRepository : IRepository<Language>
    {
        /// <summary>
        /// Returns null if symbol with given language is not found!
        /// </summary>
        Language GetBySymbol(string symbol);
    }
}