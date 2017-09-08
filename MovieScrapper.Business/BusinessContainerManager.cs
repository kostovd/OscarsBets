using Microsoft.Practices.Unity;
using MovieScrapper.Business.Interfaces;
using MovieScrapper.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScrapper.Business
{
    public class BusinessContainerManager
    {
        public void RegisterTypes(IUnityContainer container)
        {
            // Register services 
            container.RegisterType<IBetService, BetService>();
            container.RegisterType<IBetStatisticService, BetsStatisticService>();
            container.RegisterType<ICategoryService, CategoryService>();
            container.RegisterType<IGamePropertyService, GamePropertyService>();
            container.RegisterType<IMovieService, MovieService>();
            container.RegisterType<IWatchedMovieService, WatchedMovieService>();
            container.RegisterType<IWatcheMoviesStatisticService, WatcheMoviesStatisticService>();

            // Register data types
            DataContainerManager containerManager = new DataContainerManager();
            containerManager.RegisterTypes(container);
        }
    }
}
