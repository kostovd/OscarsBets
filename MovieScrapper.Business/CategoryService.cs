﻿using MovieScrapper.Data;
using MovieScrapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScrapper.Business
{
    public class CategoryService
    {
        public IEnumerable<MovieCategory> GetAll()
        {
            var repo = new CategoryRepository();
            return repo.GetAll();
        }
        public MovieCategory GetCategory(int id)
        {
            var repo = new CategoryRepository();
            return repo.GetCategory(id);
        }
        public Movie GetMovie(string id)
        {
            var repo = new CategoryRepository();
            return repo.GetMovie(id);
        }
        public void AddCategory(MovieCategory category)
        {
            var repo = new CategoryRepository();
            repo.AddCategory(category);
        }

        public void DeleteCategory(int id)
        {
            var repo = new CategoryRepository();
            repo.DeleteCategory(id);
        }

        public void RemoveMovieFromCategory(int categoryId, string movieId)
        {
            var repo = new CategoryRepository();
            repo.RemoveMovieFromCategory(categoryId, movieId);
        }

    }
}
