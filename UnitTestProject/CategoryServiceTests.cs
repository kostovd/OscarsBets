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
    public class CategoryServiceTests
    {
        [TestMethod]
        public void AddCategory_Should_BeCalledOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var categoryRepositoryMock = MockRepository.GenerateMock<ICategoryRepository>();

            //Arrange
            var category = new Category { Id = 1 };

            categoryRepositoryMock.Expect(dao => dao.AddCategory(Arg<Category>.Is.Anything)).Repeat.Once(); ;

            var categoryService = new CategoryService(categoryRepositoryMock);

            //Act
            categoryService.AddCategory(category);

            //Assert
            categoryRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void AddMovieInCategory_Should_BeCalledOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var categoryRepositoryMock = MockRepository.GenerateMock<ICategoryRepository>();

            //Arrange
            categoryRepositoryMock.Expect(dao => dao.AddMovieInCategory(Arg<int>.Is.Anything, Arg<int>.Is.Anything)).Repeat.Once(); ;

            var categoryService = new CategoryService(categoryRepositoryMock);

            //Act
            categoryService.AddMovieInCategory(1,1);

            //Assert
            categoryRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void AreWinnersSet_Should_BeCalledOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var categoryRepositoryMock = MockRepository.GenerateMock<ICategoryRepository>();
            bool sendReslt = true;

            //Arrange
            categoryRepositoryMock.Expect(dao => dao.AreWinnersSet()).Return(sendReslt).Repeat.Once(); ;

            var categoryService = new CategoryService(categoryRepositoryMock);

            //Act
            var receivedResult = categoryService.AreWinnersSet();

            //Assert
            categoryRepositoryMock.VerifyAllExpectations();

            Assert.AreEqual(sendReslt, receivedResult);
        }

        [TestMethod]
        public void DeleteCategory_Should_BeCalledOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var categoryRepositoryMock = MockRepository.GenerateMock<ICategoryRepository>();

            //Arrange
            categoryRepositoryMock.Expect(dao => dao.DeleteCategory(Arg<int>.Is.Anything)).Repeat.Once(); ;

            var categoryService = new CategoryService(categoryRepositoryMock);

            //Act
            categoryService.DeleteCategory(1);

            //Assert
            categoryRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void EditCategory_Should_BeCalledOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var categoryRepositoryMock = MockRepository.GenerateMock<ICategoryRepository>();

            //Arrange
            categoryRepositoryMock.Expect(dao => dao.EditCategory(Arg<Category>.Is.Anything)).Repeat.Once(); ;

            var categoryService = new CategoryService(categoryRepositoryMock);

            //Act
            categoryService.EditCategory(new Category { Id=1});

            //Assert
            categoryRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetAll_Should_BeCalledOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var categoryRepositoryMock = MockRepository.GenerateMock<ICategoryRepository>();

            //Arrange
            categoryRepositoryMock.Expect(dao => dao.GetAll()).Return(Arg<IEnumerable<Category>>.Is.Anything).Repeat.Once(); ;

            var categoryService = new CategoryService(categoryRepositoryMock);

            //Act
            categoryService.GetAll();

            //Assert
            categoryRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetCategory_Should_BeCalledOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var categoryRepositoryMock = MockRepository.GenerateMock<ICategoryRepository>();

            //Arrange
            categoryRepositoryMock.Expect(dao => dao.GetCategory(Arg<int>.Is.Anything)).Return(Arg<Category>.Is.Anything).Repeat.Once(); ;

            var categoryService = new CategoryService(categoryRepositoryMock);

            //Act
            categoryService.GetCategory(1);

            //Assert
            categoryRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetMovieInCategory_Should_BeCalledOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var categoryRepositoryMock = MockRepository.GenerateMock<ICategoryRepository>();

            //Arrange
            categoryRepositoryMock.Expect(dao => dao.GetMovieInCategory(Arg<int>.Is.Anything, Arg<int>.Is.Anything)).Return(Arg<Movie>.Is.Anything).Repeat.Once(); ;

            var categoryService = new CategoryService(categoryRepositoryMock);

            //Act
            categoryService.GetMovieInCategory(1, 1);

            //Assert
            categoryRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void MarkAsWinner_Should_BeCalledOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var categoryRepositoryMock = MockRepository.GenerateMock<ICategoryRepository>();

            //Arrange
            categoryRepositoryMock.Expect(dao => dao.MarkAsWinner(Arg<int>.Is.Anything, Arg<int>.Is.Anything)).Repeat.Once(); ;

            var categoryService = new CategoryService(categoryRepositoryMock);

            //Act
            categoryService.MarkAsWinner(1, 1);

            //Assert
            categoryRepositoryMock.VerifyAllExpectations();
        }

        [TestMethod]
        public void RemoveMovieFromCategory_Should_BeCalledOnce_WhenTheCorrectRepositoryIsPassed()
        {
            var categoryRepositoryMock = MockRepository.GenerateMock<ICategoryRepository>();

            //Arrange
            categoryRepositoryMock.Expect(dao => dao.RemoveMovieFromCategory(Arg<int>.Is.Anything, Arg<int>.Is.Anything)).Repeat.Once(); ;

            var categoryService = new CategoryService(categoryRepositoryMock);

            //Act
            categoryService.RemoveMovieFromCategory(1, 1);

            //Assert
            categoryRepositoryMock.VerifyAllExpectations();
        }

    }
}
