using Flashcards.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.ServicesTests
{
    [TestClass]
    public class UserFLashcardMemoryServiceTests
    {
        Mock<IFlashcardUnit> unit = new Mock<IFlashcardUnit>();
        Mock<UserFLashcardMemoryService> mockUserFlashcardMemoryService;
        UserFLashcardMemoryService userFLashcardMemoryService => mockUserFlashcardMemoryService.Object;

        public UserFLashcardMemoryServiceTests()
        {
            mockUserFlashcardMemoryService = new Mock<UserFLashcardMemoryService>(unit.Object);
            mockUserFlashcardMemoryService.CallBase = true;
        }

        
    }
}
