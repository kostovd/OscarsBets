using MovieScrapper.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScrapper.Business
{
    public class StatisticService
    {
        public Dictionary<string, IList<string>> GetData()
        {
            var data = new ViewModelsRepository();
            var recievedTable = data.GetData();

            var stringArrTitles= recievedTable.AsEnumerable().Select(r => r.Field<string>("Title")).ToArray();
            var collectionWithDistinctTitles = stringArrTitles.Distinct().ToArray();            
            int titlesCount = collectionWithDistinctTitles.Count();
            var stringArrEmails = recievedTable.AsEnumerable().Select(r => r.Field<string>("Email")).Where(x => x != null).ToArray();
            var collectionWithDistinctEmails = stringArrEmails.Distinct().ToArray();
            int emailsCount = collectionWithDistinctEmails.Count();
            
            Dictionary<string, IList<string>> myDict = new Dictionary<string, IList<string>>();
          
            for (int i = 0; i < emailsCount; i++)
            {
                
                myDict.Add(collectionWithDistinctEmails[i], collectionWithDistinctTitles);
            }

            return myDict;
        }



        public string [] GetTitles()
        {
            var data = new ViewModelsRepository();
            var recievedTable = data.GetData();

            var stringArrTitles = recievedTable.AsEnumerable().Select(r => r.Field<string>("Title")).ToArray();
            var collectionWithDistinctTitles = stringArrTitles.Distinct().ToArray();
            int titlesCount = collectionWithDistinctTitles.Count();                                             
            return collectionWithDistinctTitles;
        }

        public string[] GetUsers()
        {
            var data = new ViewModelsRepository();
            var recievedTable = data.GetData();
            var stringArrEmails = recievedTable.AsEnumerable().Select(r => r.Field<string>("Email")).Where(x => x != null).ToArray();
            var collectionWithDistinctEmails = stringArrEmails.Distinct().ToArray();

            return collectionWithDistinctEmails;
        }

    }
}

