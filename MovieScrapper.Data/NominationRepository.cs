﻿using MovieScrapper.Data.Interfaces;
using MovieScrapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MovieScrapper.Data
{
    public class NominationRepository : INominationRepository
    {
        public List<Nomination> GetAllNominationsInCategory(int categoryId)
        {
            using (var ctx = new MovieContext())
            {
                return ctx.Nominations
                    .Include(cat => cat.Movie)
                    .Include(cat => cat.Credits)
                    .Where(cat => cat.Category.Id == categoryId)
                    .ToList();
            }
        }
    }
}