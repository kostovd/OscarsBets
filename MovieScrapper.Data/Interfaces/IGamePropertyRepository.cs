using MovieScrapper.Entities;
using System;

namespace MovieScrapper.Data.Interfaces
{
    interface IGamePropertyRepository
    {
        void ChangeGameStartDate(DateTime startDate);

        void ChangeGameStopDate(DateTime stopDate);

        DateTime GetGameStartDate();

        DateTime GetGameStopDate();

        GameProperties GetDate();      
    }
}
