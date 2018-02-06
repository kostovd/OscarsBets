using MovieScrapper.Business.Interfaces;
using MovieScrapper.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MovieScrapper
{
    public class MovieClient : IMovieClient
    {
        private const string BaseUrl = "https://api.themoviedb.org";
        private const string DefaultApiVersion = "3";
        private const string MoviePath = "movie";
        private const string SearchPath = "search";
        private const string JsonMediaType = "application/json";

        private string _key;

        public MovieClient(string passedKey)
        {
            _key = passedKey;

            if (client == null)
            {
                client = new HttpClient();
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        private static HttpClient client;

        public async Task<Movie> GetMovieAsync(string movieID)
        {
            string path = $"{BaseUrl}/{DefaultApiVersion}/{MoviePath}/{movieID}?api_key={_key}&append_to_response=credits";
            var response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                JObject content = await GetContent(response);
                return MapToMovie(content);
            }

            return null;
        }

        public async Task<MoviesCollection> SearchMovieAsync(string searchString)
        {
            string path = String.Format("{0}/{1}/{2}/{3}?api_key={4}&query={5}", BaseUrl, DefaultApiVersion, SearchPath, MoviePath, _key, searchString);
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var movies = await response.Content.ReadAsAsync<MoviesCollection>();
                return movies;
            }
            else
            {
                throw new Exception("Something bad happened!");
            }
        }

        private async Task<JObject> GetContent(HttpResponseMessage responseMessage)
        {
            JObject content;

            if (responseMessage.Content == null)
            {
                throw new Exception("HttpResponseMessage content is null!");
            }

            if (responseMessage.Content.Headers.ContentType.MediaType != JsonMediaType)
            {
                throw new Exception($"HttpResponseMessage MediaType should be [{JsonMediaType}], but it is [{responseMessage.Content.Headers.ContentType.MediaType}] !");
            }

            return content = await responseMessage.Content.ReadAsAsync<JObject>();
        }

        private Movie MapToMovie(JObject movieContent)
        {
            Movie movie = new Movie()
            {
                Id = movieContent.GetValue("id").Value<int>(),
                Title = movieContent.GetValue("title").Value<string>(),
                ReleaseDate = movieContent.GetValue("release_date").Value<string>(),
                PosterPath = movieContent.GetValue("poster_path").Value<string>(),
                Overview = movieContent.GetValue("overview").Value<string>(),
                ImdbId = movieContent.GetValue("imdb_id").Value<string>(),
                Credits = MapToCredits(movieContent.GetValue("credits").Value<JObject>()),
            };

            return movie;
        }

        private List<MovieCredit> MapToCredits(JObject creditsContent)
        {
            List<MovieCredit> cresdits = new List<MovieCredit>();

            JArray castsContent = creditsContent.GetValue("cast").Value<JArray>();

            cresdits.AddRange(
                castsContent.Select(item => 
                    MapToCast(item.Value<JObject>())));

            JArray crewsContent = creditsContent.GetValue("crew").Value<JArray>();

            cresdits.AddRange(
                crewsContent.Select((item, index) => 
                    MapToCrew(item.Value<JObject>(), index)));

            return cresdits;
        }

        private MovieCredit MapToCast(JObject castContent)
        {
            MovieCredit movieCredit = new MovieCredit()
            {
                Id = castContent.GetValue("credit_id").Value<string>(),
                Order = castContent.GetValue("order").Value<int>(),
                PersonId = castContent.GetValue("id").Value<int>(),
                Name = castContent.GetValue("name").Value<string>(),
                IsCast = true,
                Role = castContent.GetValue("character").Value<string>(),
                PosterPath = castContent.GetValue("poster_path").Value<string>(),
            };

            return movieCredit;
        }

        private MovieCredit MapToCrew(JObject crewContent, int order)
        {
            MovieCredit movieCredit = new MovieCredit()
            {
                Id = crewContent.GetValue("credit_id").Value<string>(),
                Order = order,
                PersonId = crewContent.GetValue("id").Value<int>(),
                Name = crewContent.GetValue("name").Value<string>(),
                IsCast = false,
                Role = crewContent.GetValue("job").Value<string>(),
                PosterPath = crewContent.GetValue("poster_path").Value<string>(),
            };

            return movieCredit;
        }
    }
}