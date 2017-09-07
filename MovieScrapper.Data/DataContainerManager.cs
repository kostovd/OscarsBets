using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using MovieScrapper.Data.Interfaces;

namespace MovieScrapper.Data
{
    public class DataContainerManager
    {
        public void RegisterTypes(IUnityContainer container)
        {
            // Register repositories 
            container.RegisterType<ICategoryRepository, CategoryRepository>();
         
        }
    }
}
