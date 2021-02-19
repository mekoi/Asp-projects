using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Irisi_Bruno_lab3.Models;

namespace Irisi_Bruno_lab3.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly MoviesReviewsContext _context;
        private readonly IDynamoDBOperations _dynamodbOperations;

        public ReviewsController(MoviesReviewsContext context, IDynamoDBOperations dynamodbOperations)
        {
            _context = context;
            _dynamodbOperations = dynamodbOperations;
        }


        public IActionResult MovieRatings(int givenMovieId)
        {
            ViewBag.specificMovieId = givenMovieId;
            return View();
        }

        public ActionResult ShowReview(int id)
        {
            List<DynamoReview> movieReviews = _dynamodbOperations.GetAllDynamoReview().Where(r => r.Movie_Id == id).OrderByDescending(c => c.ReviewMadeAt).ToList();

            if (movieReviews.Count > 0)
            {
                int noOfReviews = movieReviews.Count();
                double averageMovieRating = movieReviews.Average((x => x.Stars));

                Movie movie = _context.Movie.Where(p => p.MovieId == id).FirstOrDefault();

                ViewBag.MovieReviews = movieReviews;
                ViewBag.Movie = movie;
                ViewBag.Id = movie.MovieId;
                ViewBag.noOfReviews = noOfReviews;            
                ViewBag.AverageMovieRating = Math.Round(averageMovieRating, 1).ToString();
            }                    
            return View();
        }

        // POST: Post Review 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PostReview(DynamoReview review)
        {
            //if (UsersController.userActiveSession == null)

            if (ModelState.IsValid)
            {
                DynamoReview newDynamoReview = new DynamoReview();
                newDynamoReview.Movie_Id = review.Movie_Id;
                newDynamoReview.Review_Text = review.Review_Text;

              
            
                newDynamoReview.Stars = review.Stars;
                _dynamodbOperations.AddReview(review);
            }

            List<DynamoReview> movieReviews = _dynamodbOperations.GetAllDynamoReview().Where(r => r.Movie_Id == review.Movie_Id).OrderByDescending(c => c.Review_Id).ToList();
            ViewBag.movieReviews = movieReviews;

            Movie movie = _context.Movie.Where(p => p.MovieId == review.Movie_Id).FirstOrDefault();
            ViewBag.Movie = movie;
            var title = _context.Movie.Where(p => p.MovieId == review.Movie_Id).Select(p=>p.MovieTitle).FirstOrDefault();
            ViewBag.Title = title;

            double averageMovieRating = movieReviews.Average((x => x.Stars));
            ViewBag.AverageMovieRating = Math.Round(averageMovieRating, 1).ToString();

            return View("PostedReview");
        }
    }
}
