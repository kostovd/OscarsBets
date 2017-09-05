using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScrapper.Business.Interfaces
{
    interface IBetStatisticService
    {

        Dictionary<string, List<string[]>> GetData();       

        string GetWinner();
              
        string[] GetCategories();

        List<string[]> GetWinners();
        
    }
}
