using LifestyleSurveyApp.Data;
using LifestyleSurveyApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace LifestyleSurveyApp.Controllers
{
    public class SurveyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SurveyController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Survey model)
        {
            var age = DateTime.Now.Year - model.DateOfBirth.Year;
            if (model.DateOfBirth.Date > DateTime.Now.AddYears(-age)) age--;

            if (age < 5 || age > 120)
                ModelState.AddModelError("DateOfBirth", "Age must be between 5 and 120.");

            if (!model.LikesPizza && !model.LikesPasta && !model.LikesPapAndWors && !model.LikesOther)
                ModelState.AddModelError("FavoriteFood", "Please select at least one favorite food.");

            if (model.WatchMoviesRating == 0 || model.ListenRadioRating == 0 ||
                model.EatOutRating == 0 || model.WatchTVRating == 0)
            {
                ModelState.AddModelError("LifestyleRatings", "Please rate all the lifestyle questions.");
            }

            if (ModelState.IsValid)
            {
                _context.Surveys.Add(model);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Survey submitted successfully!";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        public IActionResult Results()
        {
            var surveys = _context.Surveys.ToList();
            if (surveys.Count == 0)
            {
                ViewBag.Message = "No Surveys Available.";
                return View();
            }

            ViewBag.Total = surveys.Count;
            ViewBag.AvgAge = surveys.Average(s => DateTime.Now.Year - s.DateOfBirth.Year);
            ViewBag.MaxAge = surveys.Max(s => DateTime.Now.Year - s.DateOfBirth.Year);
            ViewBag.MinAge = surveys.Min(s => DateTime.Now.Year - s.DateOfBirth.Year);

            ViewBag.PizzaPercent = 100.0 * surveys.Count(s => s.LikesPizza) / surveys.Count;
            ViewBag.PastaPercent = 100.0 * surveys.Count(s => s.LikesPasta) / surveys.Count;
            ViewBag.PapPercent = 100.0 * surveys.Count(s => s.LikesPapAndWors) / surveys.Count;

            ViewBag.AvgMovies = surveys.Average(s => s.WatchMoviesRating);
            ViewBag.AvgRadio = surveys.Average(s => s.ListenRadioRating);
            ViewBag.AvgEatOut = surveys.Average(s => s.EatOutRating);
            ViewBag.AvgTV = surveys.Average(s => s.WatchTVRating);

            return View(surveys);
        }
    }
}
