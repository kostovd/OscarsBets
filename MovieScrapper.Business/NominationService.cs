using MovieScrapper.Business.Interfaces;
using MovieScrapper.Data.Interfaces;
using MovieScrapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScrapper.Business
{
    public class NominationService : INominationService
    {
        private readonly INominationRepository _nominationRepository;

        public NominationService(INominationRepository nominationRepository)
        {
            _nominationRepository = nominationRepository;
        }

        public List<Nomination> GetAllNominationsInCategory(int categoryId)
        {
            return _nominationRepository.GetAllNominationsInCategory(categoryId);
        }
    }
}
