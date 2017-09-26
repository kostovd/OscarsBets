using Microsoft.Practices.Unity;
using MovieScrapper.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieScrapper
{
    public class WebContainerManager
    {
        public void RegisterTypes(IUnityContainer container)
        {          
            BusinessContainerManager containerManager = new BusinessContainerManager();
            containerManager.RegisterTypes(container);
        }
    }
}