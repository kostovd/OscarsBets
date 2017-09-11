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
    public class WatcheMoviesStatisticServiceTests
    {
        [TestMethod]
        public void GetData_ShouldCalledViewModelsRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var viewModelsRepositoryMock = MockRepository.GenerateMock<IViewModelsRepository>();
            List<WatchedMovies> watchedMovies = new List<WatchedMovies>();

            //Arrange

            viewModelsRepositoryMock.Expect(dao => dao.GetWatchedMoviesData()).Return(watchedMovies).Repeat.Once(); ;

            var watcheMoviesStatisticService = new WatcheMoviesStatisticService(viewModelsRepositoryMock);

            //Act
            watcheMoviesStatisticService.GetData();

            //Assert
            viewModelsRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetTitles_ShouldCalledViewModelsRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var viewModelsRepositoryMock = MockRepository.GenerateMock<IViewModelsRepository>();

            //Arrange
            List<WatchedMovies> watchedMovies = new List<WatchedMovies>();
            viewModelsRepositoryMock.Expect(dao => dao.GetWatchedMoviesData()).Return(watchedMovies).Repeat.Once(); ;

            var watcheMoviesStatisticService = new WatcheMoviesStatisticService(viewModelsRepositoryMock);

            //Act
            watcheMoviesStatisticService.GetTitles();

            //Assert
            viewModelsRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetUsers_ShouldCalledViewModelsRepositoryMockOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var viewModelsRepositoryMock = MockRepository.GenerateMock<IViewModelsRepository>();

            //Arrange
            List<WatchedMovies> watchedMovies = new List<WatchedMovies>();
            viewModelsRepositoryMock.Expect(dao => dao.GetWatchedMoviesData()).Return(watchedMovies).Repeat.Once(); 

            var watcheMoviesStatisticService = new WatcheMoviesStatisticService(viewModelsRepositoryMock);

            //Act
            watcheMoviesStatisticService.GetUsers();

            //Assert
            viewModelsRepositoryMock.VerifyAllExpectations();
        }

    }
}
