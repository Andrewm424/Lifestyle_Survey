namespace LifestyleSurveyApp.Models
{
    public class SurveyStatsViewModel
    {
        public int Total { get; set; }
        public double AverageAge { get; set; }
        public int OldestAge { get; set; }
        public int YoungestAge { get; set; }

        // Favorite food percentages
        public double PizzaPercentage { get; set; }
        public double PastaPercentage { get; set; }
        public double PapWorsPercentage { get; set; }

        // Rating averages
        public double EatOutAvgRating { get; set; }
        public double WatchMoviesAvgRating { get; set; }
        public double WatchTVAvgRating { get; set; }
        public double ListenRadioAvgRating { get; set; }
    }
}
