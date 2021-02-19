using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Irisi_Bruno_lab3.Models;
using Microsoft.Extensions.Logging;
using Amazon.S3;
using Amazon;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using System.IO;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;

namespace Irisi_Bruno_lab3.Controllers
{
    public class MoviesController : Controller
    {
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast2;
        //private static IAmazonS3 s3Client;
        private static String bucketName = "irisi-bruno-bucket4lab3";
        private static String myS3Url = "https://irisi-bruno-bucket4lab3.s3.us-east-2.amazonaws.com/";

        private readonly MoviesReviewsContext _context;
        private IS3Processes _s3service;

        public MoviesController(MoviesReviewsContext context, IS3Processes s3service)
        {
            _context = context;
            _s3service = s3service;
        }

        public IActionResult MoviesList()
        {
            var moviesList = _context.Movie.All(p => p.MovieId > 0);
            //ViewBag.
            return View("../Home/Index",moviesList);
        }

        public IActionResult MovieDetails(int givenMovieId)
        {
            ViewBag.specificMovieId = givenMovieId;
            return View();
        }

        // GET: Upload
        public IActionResult Upload()
        {
            return View();
        }

        // POST: Upload 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Upload(IFormFile videoFile, IFormFile imageFile, Movie creatingMovie)
        {
            if (ModelState.IsValid)
            {
                await UploadVideo(videoFile);
                await UploadImage(imageFile);

                Movie newMovie = new Movie();
                newMovie.MovieTitle = creatingMovie.MovieTitle;
                newMovie.S3UrlVideo = myS3Url + videoFile.FileName;
                newMovie.S3UrlImage = myS3Url + imageFile.FileName;
                _context.Movie.Add(newMovie);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task UploadVideo(IFormFile videoFile)
        {
            using (AmazonS3Client s3Client = new AmazonS3Client(_s3service.GetBasicCredentials(), RegionEndpoint.USEast2))
            {
                using (var newMemoryStream = new MemoryStream())
                {
                    videoFile.CopyTo(newMemoryStream);

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = videoFile.FileName,
                        BucketName = bucketName,
                        CannedACL = S3CannedACL.PublicRead
                    };

                    var fileTransferUtility = new TransferUtility(s3Client);
                    await fileTransferUtility.UploadAsync(uploadRequest);
                }
            }
        }

        public async Task UploadImage(IFormFile imageFile)
        {
            using (AmazonS3Client s3Client = new AmazonS3Client(_s3service.GetBasicCredentials(), RegionEndpoint.USEast2))
            {
                using (var newMemoryStream = new MemoryStream())
                {
                    imageFile.CopyTo(newMemoryStream);

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = imageFile.FileName,
                        BucketName = bucketName,
                        CannedACL = S3CannedACL.PublicRead
                    };

                    var fileTransferUtility = new TransferUtility(s3Client);
                    await fileTransferUtility.UploadAsync(uploadRequest);
                }
            }
        }
    }

}