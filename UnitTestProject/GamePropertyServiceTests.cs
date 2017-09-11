using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieScrapper.Business;
using MovieScrapper.Data.Interfaces;
using MovieScrapper.Entities;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    [TestClass]
    public class GamePropertyServiceTests
    {
        [TestMethod]
        public void ChangeGameStartDate_ShouldCallGamePropertyRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var gamePropertyRepositoryMock = MockRepository.GenerateMock<IGamePropertyRepository>();

            //Arrange
            gamePropertyRepositoryMock.Expect(dao => dao.ChangeGameStartDate(Arg<DateTime>.Is.Anything)).Repeat.Once();
            var date = DateTime.Now;

            var gamePropertyService = new GamePropertyService(gamePropertyRepositoryMock);

            //Act
            gamePropertyService.ChangeGameStartDate(date);

            //Assert
            gamePropertyRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void ChangeGameStopDate_ShouldCallGamePropertyRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var gamePropertyRepositoryMock = MockRepository.GenerateMock<IGamePropertyRepository>();

            //Arrange
            gamePropertyRepositoryMock.Expect(dao => dao.ChangeGameStopDate(Arg<DateTime>.Is.Anything)).Repeat.Once();
            var date = DateTime.Now;

            var gamePropertyService = new GamePropertyService(gamePropertyRepositoryMock);

            //Act
            gamePropertyService.ChangeGameStopDate(date);

            //Assert
            gamePropertyRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetGameStartDate_ShouldCallGamePropertyRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var gamePropertyRepositoryMock = MockRepository.GenerateMock<IGamePropertyRepository>();

            //Arrange
            gamePropertyRepositoryMock.Expect(dao => dao.GetGameStartDate()).Return(Arg<DateTime>.Is.Anything).Repeat.Once();

            var gamePropertyService = new GamePropertyService(gamePropertyRepositoryMock);

            //Act
            gamePropertyService.GetGameStartDate();

            //Assert
            gamePropertyRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetGameStopDate_ShouldCallGamePropertyRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var gamePropertyRepositoryMock = MockRepository.GenerateMock<IGamePropertyRepository>();

            //Arrange
            gamePropertyRepositoryMock.Expect(dao => dao.GetGameStopDate()).Return(Arg<DateTime>.Is.Anything).Repeat.Once();

            var gamePropertyService = new GamePropertyService(gamePropertyRepositoryMock);

            //Act
            gamePropertyService.GetGameStopDate();

            //Assert
            gamePropertyRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void IsGameStopped_ShouldCallGamePropertyRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var gamePropertyRepositoryMock = MockRepository.GenerateMock<IGamePropertyRepository>();

            //Arrange
            gamePropertyRepositoryMock.Expect(dao => dao.GetDate()).Return(Arg<GameProperties>.Is.Anything).Repeat.Once();

            var gamePropertyService = new GamePropertyService(gamePropertyRepositoryMock);

            //Act
            gamePropertyService.IsGameStopped();

            //Assert
            gamePropertyRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void IsGameNotStartedYet_ShouldCallGamePropertyRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var gamePropertyRepositoryMock = MockRepository.GenerateMock<IGamePropertyRepository>();

            //Arrange
            gamePropertyRepositoryMock.Expect(dao => dao.GetDate()).Return(Arg<GameProperties>.Is.Anything).Repeat.Once();

            var gamePropertyService = new GamePropertyService(gamePropertyRepositoryMock);

            //Act
            gamePropertyService.IsGameNotStartedYet();

            //Assert
            gamePropertyRepositoryMock.VerifyAllExpectations();
        }
    }
}
