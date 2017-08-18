using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieScrapper.Data
{
    public class ViewModelsRepository
    {
        public DataTable GetData()
        {
            string query = "SELECT Movies.Id, Movies.Title, WatchedMovies.Watched_UserId, AspNetUsers.Email FROM Movies LEFT JOIN WatchedMovies on Movies.Id = WatchedMovies.Movie_Id LEFT JOIN AspNetUsers on WatchedMovies.Watched_UserId = AspNetUsers.Id";
            string connString = "Data Source=PC-1099\\SQLEXPRESS;Initial Catalog=MovieScrapper.Models.MovieContext;Integrated Security=True";
            DataTable results = new DataTable();

            using (SqlConnection conn = new SqlConnection(connString))
            using (SqlCommand command = new SqlCommand(query, conn))
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
            dataAdapter.Fill(results);
            return results;        } 
    }
}
