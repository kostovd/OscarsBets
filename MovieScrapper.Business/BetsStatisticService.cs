using MovieScrapper.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScrapper.Business
{
    public class BetsStatisticService
    {
        public Dictionary<string, List<string[]>> GetData()
        {
            var data = new ViewModelsRepository();
            var dt = data.GetBetsData();
            var stringArrEmails = dt.AsEnumerable().Select(r => r.Field<string>("Email")).Where(x => x != null).ToArray();
            var collectionWithDistinctEmails = stringArrEmails.Distinct().ToArray();

            Dictionary<string, List<string[]>> myDict = new Dictionary<string, List<string[]>>();

            foreach (var email in collectionWithDistinctEmails)
            {

                string currentCategory;
                string currentTitle;

                List<string[]> userCategories = new List<string[]>();

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    string[] categoryMovie = new string[2];
                    DataRow row = dt.Rows[j];
                    if (row["Email"].ToString() == email)
                    {
                        currentCategory = row["Category"].ToString();
                        currentTitle = row["Title"].ToString();

                        categoryMovie[0] = currentCategory;
                        categoryMovie[1] = currentTitle;

                        userCategories.Add(categoryMovie);

                    }
                }

                myDict.Add(email, userCategories);
            }
            myDict = myDict.OrderByDescending(x => x.Value.Count).ToDictionary(x => x.Key, x => x.Value);
            return myDict;

        }

        public string[] GetCategories()
        {
            var data = new ViewModelsRepository();
            var dt = data.GetBetsData();

            var stringArrCategories = dt.AsEnumerable().Select(r => r.Field<string>("Category")).ToArray();
            var collectionWithDistinctCategories = stringArrCategories.Distinct().ToArray();
            return collectionWithDistinctCategories;
        }

        public List<string[]> GetWinners()
        {
            var data = new ViewModelsRepository();
            var dt = data.GetWinner();
          
            string currentCategory;
            string currentWinner;
            List<string[]> winners = new List<string[]>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                string[] categoryWinner = new string[2];
                DataRow row = dt.Rows[j];

                currentCategory = row["Category"].ToString();
                currentWinner = row["Winner"].ToString();

                categoryWinner[0] = currentCategory;
                categoryWinner[1] = currentWinner;

                winners.Add(categoryWinner);
            }
            return winners;
        }
    }
}
