using MovieScrapper.Business.Interfaces;
using MovieScrapper.Data;
using MovieScrapper.Entities;
using System.Collections.Generic;
using MovieScrapper.Data.Interfaces;

namespace MovieScrapper.Business
{
    public class CategoryService: ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        //public CategoryService()
        //{
        //    _categoryRepository = new CategoryRepository();
        //}

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public void AddCategory(Category category)
        {
            _categoryRepository.AddCategory(category);
        }  

        public void AddMovieInCategory(int categoryId, int movieId)
        {
            _categoryRepository.AddMovieInCategory(categoryId, movieId);
        }
              
        public bool AreWinnersSet()
        {
            return _categoryRepository.AreWinnersSet();
        }       
      
        public void DeleteCategory(int id)
        {
            _categoryRepository.DeleteCategory(id);
        }    

        public void EditCategory(Category category)
        {
            _categoryRepository.EditCategory(category);
        }    

        public IEnumerable<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }                                    
        
        public Category GetCategory(int id)
        {
            return _categoryRepository.GetCategory(id);
        }
           
        public Movie GetMovieInCategory(int categoryId, int movieId)
        {
            return _categoryRepository.GetMovieInCategory(categoryId, movieId);
        }              
                         
        public void MarkAsWinner(int categoryId, int movieId)
        {
            _categoryRepository.MarkAsWinner(categoryId, movieId);
        }

        public void RemoveMovieFromCategory(int categoryId, int movieId)
        {
            _categoryRepository.RemoveMovieFromCategory(categoryId, movieId);
        }                                

    }
}
