using Microsoft.Practices.Unity;
using MovieScrapper.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieScrapper
{
    public class BasePage : System.Web.UI.Page
    {
        protected T GetBuisnessService<T>()
        {
            IUnityContainer container = (IUnityContainer)Application["EntLibContainer"];
            return container.Resolve<T>();
        }

    }
}
