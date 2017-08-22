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
            var collectionWithAllDistinctTitles = stringArrTitles.Distinct().ToArray();            
            int allTitlesCount = collectionWithAllDistinctTitles.Count();
            var stringArrEmails = recievedTable.AsEnumerable().Select(r => r.Field<string>("Email")).Where(x => x != null).ToArray();
            var collectionWithDistinctEmails = stringArrEmails.Distinct().ToArray();
            int emailsCount = collectionWithDistinctEmails.Count();
            
            Dictionary<string, IList<string>> myDict = new Dictionary<string, IList<string>>();
          
            //for (int i = 0; i < emailsCount; i++)
            foreach(var email in collectionWithDistinctEmails)
            {               
                IList<string> userTitles= new List<string>();
                for (int j =0; j<recievedTable.Rows.Count; j++)
                {
                    //var currentTitle = (from DataRow dr in recievedTable.Rows
                    //                    where (string)dr["Email"] == email
                    //                    select (string)dr["Title"]).FirstOrDefault();
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
                myDict.Add(email, userTitles);
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

