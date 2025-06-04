using System;
using System.ComponentModel.DataAnnotations;

namespace LifestyleSurveyApp.Models
{
    public class Survey
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [RegularExpression(@"^\d{10,15}$", ErrorMessage = "Enter a valid contact number")]
        public string ContactNumber { get; set; }

        public bool LikesPizza { get; set; }
        public bool LikesPasta { get; set; }
        public bool LikesPapAndWors { get; set; }
        public bool LikesOther { get; set; }

        [Range(1, 5)] public int WatchMoviesRating { get; set; }
        [Range(1, 5)] public int ListenRadioRating { get; set; }
        [Range(1, 5)] public int EatOutRating { get; set; }
        [Range(1, 5)] public int WatchTVRating { get; set; }
    }
}
