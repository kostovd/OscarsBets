namespace MovieScrapper.Entities
{
    public class Bet
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public Movie Movie { get; set; }
        public Category Category { get; set; }
    }
}
