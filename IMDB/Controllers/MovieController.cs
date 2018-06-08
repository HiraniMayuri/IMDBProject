using AutoMapper;
using IMDB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMDB.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        public ActionResult Index()
        {

            IList<MovieModel> movies = new List<MovieModel>();

            using (var ctx = new IMDBEntities())
            {
                movies = ctx.MovieDetails.Where(m => m.IsDeleted == false).Select(m => new MovieModel()
                {
                    ID = m.ID,
                    Name = m.Name,
                    Plot = m.Plot,
                    Poster = m.Poster,
                    producerName = m.ProducerName,
                    YearOfRelease = m.YearOfRelease,

                }).ToList<MovieModel>();

            }
            return View(movies);
        }

        [Route("Movie/Create/{id}")]
        public ActionResult Create(int? id)
        {
            List<SelectListItem> Producer = new List<SelectListItem>();

            using (var ctx = new IMDBEntities())
            {
                Producer = ctx.Producers.Where(c => c.IsDeleted == false).Select(c => new SelectListItem()
                {
                    Value = c.ID.ToString(),
                    Text = c.Name.ToString()
                }).ToList<SelectListItem>();
            }
            ViewData["Producer"] = Producer;
            if (id != null)
            {
                using (var ctx = new IMDBEntities())
                {
                    var updateMovie = ctx.Movies.FirstOrDefault(m => m.ID == id);
                    if (updateMovie != null)
                    {
                        var editMovie = Mapper.Map<Movie, MovieModel>(updateMovie);
                     
                        return View(editMovie);
                    }

                }
            }

            return View();
        }
        [HttpPost]
        public ActionResult Create(MovieModel movie)
        {
            IList<MovieModel> newMovie = new List<MovieModel>();
            var upload = new MovieModel();
            using (var ctx = new IMDBEntities())
            {
                if (movie.ID == 0)
                {

                    upload = new MovieModel
                    {
                        ID = movie.ID,
                        Name = movie.Name,
                        Plot = movie.Plot,
                        ProducerId = movie.ProducerId,
                        producerName = movie.producerName,
                        YearOfRelease = movie.YearOfRelease,
                        IsDeleted = false
                    };
                    if (movie.File != null)
                    {
                        string name = Path.GetFileName(movie.File.FileName);
                        string relativePath = "~/uploads/" + name;
                        string physicalPath = Server.MapPath(relativePath);
                        movie.File.SaveAs(physicalPath);
                        upload.Poster = name;
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Try Again";
                        return RedirectToAction("Index");
                    }
                    ctx.Movies.Add(Mapper.Map<MovieModel, Movie>(upload));
                    ctx.SaveChanges();
                }
                else
                {
                    if (movie.File != null)
                    {
                        string name = Path.GetFileName(movie.File.FileName);
                        string relativePath = "~/uploads/" + name;
                        string physicalPath = Server.MapPath(relativePath);
                        movie.File.SaveAs(physicalPath);
                        movie.Poster = name;
                    }
                    var existMovie = ctx.Movies.FirstOrDefault(m => m.ID == movie.ID);
                    existMovie.IsDeleted = false;
                    Mapper.Map(movie, existMovie);
                    ctx.SaveChanges();

                }

            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            using (var ctx = new IMDBEntities())
            {
                var existRecord = ctx.Movies.Single(m => m.ID == id);
                if (existRecord != null)
                {
                    existRecord.IsDeleted = true;
                    ctx.SaveChanges();
                }
            }

            return RedirectToAction("Index");

        }
    }
}
