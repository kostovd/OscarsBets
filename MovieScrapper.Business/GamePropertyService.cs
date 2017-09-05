using MovieScrapper.Business.Interfaces;
using MovieScrapper.Data;
using MovieScrapper.Entities;
using System;

namespace MovieScrapper.Business
{
    public class GamePropertyService: IGamePropertyService
    {
        public void ChangeGameStartDate(DateTime stopDate)
        {
            var repo = new GamePropertyRepository();
            repo.ChangeGameStartDate(stopDate);
        }

        public void ChangeGameStopDate(DateTime stopDate)
        {
            var repo = new GamePropertyRepository();
            repo.ChangeGameStopDate(stopDate);
        }

        public DateTime GetGameStartDate()
        {
            var repo = new GamePropertyRepository();
            return repo.GetGameStartDate();
        }

        public DateTime GetGameStopDate()
        {
            var repo = new GamePropertyRepository();
            return repo.GetGameStopDate();
        }

        public bool IsGameNotStartedYet()
        {
            var repo = new GamePropertyRepository();

            GameProperties dateObject = repo.GetDate();
            DateTime startDate = (dateObject != null ? dateObject.StartGameDate : DateTime.Now);
            return (startDate > DateTime.Now);
        }

        public bool IsGameStopped()
        {
            var repo = new GamePropertyRepository();

            GameProperties stopDateObject = repo.GetDate();
            DateTime stopDate = (stopDateObject != null ? stopDateObject.StopGameDate : DateTime.Now);
            return (stopDate < DateTime.Now);

        }
    }
}
