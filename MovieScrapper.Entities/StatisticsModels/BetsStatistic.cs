using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScrapper.Entities.StatisticsModels
{
    public class BetsStatistic
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }

    }
}
