using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieScrapper.Business;
using MovieScrapper.Data.Interfaces;
using MovieScrapper.Entities;
using MovieScrapper.Entities.StatisticsModels;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
    [TestClass]
    public class BetsStatisticServiceTests
    {
        [TestMethod]
        public void GetData_ShouldCallViewModelsRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {

            //Arrange
            var viewModelsRepositoryMock = MockRepository.GenerateMock<IViewModelsRepository>();         

            List<BetsStatistic> betStatisticList = new List<BetsStatistic>();
            List<WatchedMovies> watchedMovies = new List<WatchedMovies>();
            List<Winners> winners = new List<Winners>();

            viewModelsRepositoryMock.Expect(dao => dao.GetBetsData()).Return(betStatisticList).Repeat.Once();
            viewModelsRepositoryMock.Expect(dao => dao.GetWinner()).Return(winners).Repeat.Once();

            var betService = new BetsStatisticService(viewModelsRepositoryMock);

            //Act
            betService.GetData();

            //Assert
            viewModelsRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetWinner_ShouldCallViewModelsRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            //Arrange
            var viewModelsRepositoryMock = MockRepository.GenerateMock<IViewModelsRepository>();

            List<BetsStatistic> betStatisticList = new List<BetsStatistic>();
            List<WatchedMovies> watchedMovies = new List<WatchedMovies>();
            List<Winners> winners = new List<Winners>();

            viewModelsRepositoryMock.Expect(dao => dao.GetBetsData()).Return(betStatisticList).Repeat.Once();
            viewModelsRepositoryMock.Expect(dao => dao.GetWinner()).Return(winners).Repeat.Once();

            var betService = new BetsStatisticService(viewModelsRepositoryMock);

            //Act
            betService.GetWinner();

            //Assert
            viewModelsRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetCategories_ShouldCallViewModelsRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var viewModelsRepositoryMock = MockRepository.GenerateMock<IViewModelsRepository>();
            var watchedMovieRepositoryMock = MockRepository.GenerateMock<IWatchedMovieRepository>();
            List<BetsStatistic> betStatisticList = new List<BetsStatistic>();
            //Arrange
            viewModelsRepositoryMock.Expect(dao => dao.GetBetsData()).Return(betStatisticList).Repeat.Once(); ;

            var betService = new BetsStatisticService(viewModelsRepositoryMock);

            //Act
            betService.GetCategories();

            //Assert
            viewModelsRepositoryMock.VerifyAllExpectations();
        }
    }
}
