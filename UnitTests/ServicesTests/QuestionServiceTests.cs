using Flashcards.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services.Implementation;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.ServicesTests
{
    [TestClass]
    public class QuestionServiceTests
    {
        private Mock<IFlashcardUnit> unit = new Mock<IFlashcardUnit>();
        private Mock<QuestionService> mockQuestionService;
        private QuestionService questionService => mockQuestionService.Object;

        public QuestionServiceTests()
        {
            mockQuestionService = new Mock<QuestionService>(unit.Object);
            mockQuestionService.CallBase = true;
        }

        [TestMethod]
        public void IsAnswerCorrect_correct_test()
        {
            Assert.AreEqual(1.0,
                questionService.CalculateCorrectnessOfAnswer("test_answer", "test_answer"));
        }

        [TestMethod]
        public void IsAnswerCorrect_correct_mixedCase_test()
        {
            Assert.AreEqual(1.0,
                questionService.CalculateCorrectnessOfAnswer("test_AnsWer", "Test_answeR"));
        }

        [TestMethod]
        public void IsAnswerCorrect_correct2_test()
        {
            Assert.AreEqual(1.0,
                questionService.CalculateCorrectnessOfAnswer("test_answer12345", "test_answer12345"));
        }

        [TestMethod]
        public void IsAnswerCorrect_not_correct_test()
        {
            Assert.IsTrue(questionService.CalculateCorrectnessOfAnswer("zupa", "testowa") < 1.0);
        }
    }
}
