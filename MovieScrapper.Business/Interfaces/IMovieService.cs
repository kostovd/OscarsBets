﻿using MovieScrapper.Business.Enums;
using MovieScrapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScrapper.Business.Interfaces
{
    public interface IMovieService
    {
        void ChangeMovieStatus(string userId, int movieId);

        IEnumerable<Movie> GetAllMovies();

        IEnumerable<Movie> GetAllMoviesByCriteria(string userId, OrderType orderType, FilterType filterType);

        Movie GetMovie(int id);

        bool HasMovie(int id);
    }
}
