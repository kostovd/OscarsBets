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
            var viewModelsRepositoryMock = MockRepository.GenerateMock<IViewModelsRepository>();
            var watchedMovieRepositoryMock = MockRepository.GenerateMock<IWatchedMovieRepository>();

            //Arrange
            viewModelsRepositoryMock.Expect(dao => dao.GetBetsData()).Return(Arg<List<BetsStatistic>>.Is.Anything).Repeat.Once(); ;

            var betService = new BetsStatisticService(viewModelsRepositoryMock, watchedMovieRepositoryMock);

            //Act
            betService.GetData();

            //Assert
            viewModelsRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetWinner_ShouldCallViewModelsRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var viewModelsRepositoryMock = MockRepository.GenerateMock<IViewModelsRepository>();
            var watchedMovieRepositoryMock = MockRepository.GenerateMock<IWatchedMovieRepository>();

            //Arrange
            viewModelsRepositoryMock.Expect(dao => dao.GetBetsData()).Return(Arg<List<BetsStatistic>>.Is.Anything).Repeat.Once(); ;

            var betService = new BetsStatisticService(viewModelsRepositoryMock, watchedMovieRepositoryMock);

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

            //Arrange
            viewModelsRepositoryMock.Expect(dao => dao.GetBetsData()).Return(Arg<List<BetsStatistic>>.Is.Anything).Repeat.Once(); ;

            var betService = new BetsStatisticService(viewModelsRepositoryMock, watchedMovieRepositoryMock);

            //Act
            betService.GetCategories();

            //Assert
            viewModelsRepositoryMock.VerifyAllExpectations();
        }
    }
}
