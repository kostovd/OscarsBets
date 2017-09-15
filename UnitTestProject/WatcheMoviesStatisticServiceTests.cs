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
        public void GetData_ShouldReturnTheCorrectListOfWatchedObjectsWithTheCorrectUsers_WhenTheRepositoryPassWatchedMoviesData()
        {
            var viewModelsRepositoryMock = MockRepository.GenerateMock<IViewModelsRepository>();

            //Arrange
            List<WatchedMovies> watchedMovies = new List<WatchedMovies>();
            WatchedMovies entity1 = new WatchedMovies { Id = 1, Email = "Email1", Title = "Title1" };
            WatchedMovies entity2 = new WatchedMovies { Id = 2, Email = "Email2", Title = "Title2" };
            watchedMovies.Add(entity1);
            watchedMovies.Add(entity2);
            //List<WatchedObject> expectedResault = new List<WatchedObject>();
            //WatchedObject watchedObject1 = new WatchedObject { UserEmail = "Email1" };
            //WatchedObject watchedObject2 = new WatchedObject { UserEmail = "Email2" };
            //expectedResault.Add(watchedObject1);
            //expectedResault.Add(watchedObject2);
            viewModelsRepositoryMock.Expect(dao => dao.GetWatchedMoviesData()).Return(watchedMovies);

            var watcheMoviesStatisticService = new WatcheMoviesStatisticService(viewModelsRepositoryMock);

            //Act
            var resault = watcheMoviesStatisticService.GetData();

            //Assert
            Assert.AreEqual("Email1", resault[0].UserEmail);
            Assert.AreEqual("Email2", resault[1].UserEmail);
        }

        [TestMethod]
        public void GetData_ShouldReturnTheCorrectListOfWatchedObjectsWithCorrectListOfTitles_WhenTheRepositoryPassWatchedMoviesData()
        {
            var viewModelsRepositoryMock = MockRepository.GenerateMock<IViewModelsRepository>();

            //Arrange
            List<WatchedMovies> watchedMovies = new List<WatchedMovies>();
            WatchedMovies entity1 = new WatchedMovies { Id = 1, Email = "Email1", Title = "Title1" };
            WatchedMovies entity2 = new WatchedMovies { Id = 2, Email = "Email1", Title = "Title2" };
            WatchedMovies entity3 = new WatchedMovies { Id = 3, Email = "Email2", Title = "Title3" };
            watchedMovies.Add(entity1);
            watchedMovies.Add(entity2);
            watchedMovies.Add(entity3);
            //List<WatchedObject> expectedResault = new List<WatchedObject>();
            //WatchedObject watchedObject1 = new WatchedObject { UserEmail = "Email1" };
            //WatchedObject watchedObject2 = new WatchedObject { UserEmail = "Email2" };
            //expectedResault.Add(watchedObject1);
            //expectedResault.Add(watchedObject2);
            viewModelsRepositoryMock.Expect(dao => dao.GetWatchedMoviesData()).Return(watchedMovies);

            var watcheMoviesStatisticService = new WatcheMoviesStatisticService(viewModelsRepositoryMock);

            //Act
            var resault = watcheMoviesStatisticService.GetData();

            //Assert
            Assert.AreEqual("Title1", resault[0].MovieTitles[0]);
            Assert.AreEqual("Title2", resault[0].MovieTitles[1]);
            Assert.AreEqual("Title4", resault[1].MovieTitles[0]);

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
        public void GetTitles_ShouldReturnArrayOftitles_WhenTheRepositoryPassWatchedMoviesData()
        {
            var viewModelsRepositoryMock = MockRepository.GenerateMock<IViewModelsRepository>();

            //Arrange
            List<WatchedMovies> watchedMovies = new List<WatchedMovies>();
            WatchedMovies entity1 = new WatchedMovies { Id = 1, Email = "Email1", Title = "Title1" };
            WatchedMovies entity2 = new WatchedMovies { Id = 2, Email = "Email2", Title = "Title2" };
            watchedMovies.Add(entity1);
            watchedMovies.Add(entity2);
            string[] expectedArray = new string[] { "Title1", "Title2" };
            viewModelsRepositoryMock.Expect(dao => dao.GetWatchedMoviesData()).Return(watchedMovies); 

            var watcheMoviesStatisticService = new WatcheMoviesStatisticService(viewModelsRepositoryMock);

            //Act
            var resault= watcheMoviesStatisticService.GetTitles();

            //Assert
            Assert.AreEqual(expectedArray[0], resault.ToArray()[0]);
            Assert.AreEqual(expectedArray[1], resault.ToArray()[1]);
        }

    }
}
