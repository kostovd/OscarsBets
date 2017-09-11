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
    public class BetsStatisticServiceTests
    {
        //[TestMethod]
        //public void GetData_Should_BeCalledOnce_WhenTheCorrectRepositoryIsPassed()
        //{
        //    var viewModelsRepositoryMock = MockRepository.GenerateMock<IViewModelsRepository>();
        //    var watchedMovieRepositoryMock = MockRepository.GenerateMock<IWatchedMovieRepository>();

        //    //Arrange
        //    viewModelsRepositoryMock.Expect(dao => dao.GetBetsData(Arg<string>.Is.Anything)).Return(Arg<IEnumerable<Bet>>.Is.Anything).Repeat.Once(); ;

        //    var betService = new BetService(betRepositoryMock);

        //    //Act
        //    betService.GetAllUserBets("1");

        //    //Assert
        //    betRepositoryMock.VerifyAllExpectations();
        //}
    }
}
