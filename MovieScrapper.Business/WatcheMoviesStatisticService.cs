using MovieScrapper.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScrapper.Business
{
    public class WatcheMoviesStatisticService
    {
        public Dictionary<string, List<string>> GetData()
        {
            var data = new ViewModelsRepository();
            var recievedTable = data.GetWatchedMoviesData();

            var stringArrTitles= recievedTable.AsEnumerable().Select(r => r.Field<string>("Title")).ToArray();
            var collectionWithAllDistinctTitles = stringArrTitles.Distinct().ToArray();            
            int allTitlesCount = collectionWithAllDistinctTitles.Count();
            var stringArrEmails = recievedTable.AsEnumerable().Select(r => r.Field<string>("Email")).Where(x => x != null).ToArray();
            var collectionWithDistinctEmails = stringArrEmails.Distinct().ToArray();
            int emailsCount = collectionWithDistinctEmails.Count();
            
            Dictionary<string, List<string>> myDict = new Dictionary<string, List<string>>();
          
            //for (int i = 0; i < emailsCount; i++)
            foreach(var email in collectionWithDistinctEmails)
            {               
                List<string> userTitles= new List<string>();
                for (int j =0; j<recievedTable.Rows.Count; j++)
                {
                   
                    string currentTitle;
                    foreach (DataRow row in recievedTable.Rows)
                    {
                        if (row["Email"].ToString() == email )
                        {
                            currentTitle= row["Title"].ToString();
                            userTitles.Add(currentTitle);
                        }
                    }
                   
                }
                List<string> distinctTitles = userTitles.Distinct().ToList();
                myDict.Add(email, distinctTitles);
            }
            myDict = myDict.OrderByDescending(x => x.Value.Count).ToDictionary(x => x.Key, x => x.Value);
            return myDict;
        }



        public string [] GetTitles()
        {
            var data = new ViewModelsRepository();
            var recievedTable = data.GetWatchedMoviesData();

            var stringArrTitles = recievedTable.AsEnumerable().Select(r => r.Field<string>("Title")).ToArray();
            var collectionWithDistinctTitles = stringArrTitles.Distinct().ToArray();
            int titlesCount = collectionWithDistinctTitles.Count();                                             
            return collectionWithDistinctTitles;
        }

        public string[] GetUsers()
        {
            var data = new ViewModelsRepository();
            var recievedTable = data.GetWatchedMoviesData();
            var stringArrEmails = recievedTable.AsEnumerable().Select(r => r.Field<string>("Email")).Where(x => x != null).ToArray();
            var collectionWithDistinctEmails = stringArrEmails.Distinct().ToArray();

            return collectionWithDistinctEmails;
        }

    }
}

