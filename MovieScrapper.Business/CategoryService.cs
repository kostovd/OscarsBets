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
        private readonly IMovieRepository _movieRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public CategoryService(ICategoryRepository categoryRepository, IMovieRepository movieRepository)
        {
            _categoryRepository = categoryRepository;
            _movieRepository = movieRepository;
        }

        public void AddCategory(Category category)
        {
            _categoryRepository.AddCategory(category);
        }         

        public void AddMovieInCategory(int categoryId, Movie movie)
        {            
            var hasMovie = _movieRepository.HasMovie(movie.Id);
            var hasMovieInCategory = HasMovieInCategory(categoryId, movie.Id);

            if (!hasMovie)
            {
                _movieRepository.AddMovie(movie);
            }

            if (!hasMovieInCategory)
            {
                _categoryRepository.AddMovie(categoryId, movie.Id);
            }
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

        public bool HasMovieInCategory(int categoryId, int movieId)
        {
            var hasMovie= _categoryRepository.HasMovieInCategory(categoryId, movieId);

            return hasMovie;      
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
