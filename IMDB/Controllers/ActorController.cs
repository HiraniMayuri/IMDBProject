using AutoMapper;
using IMDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMDB.Controllers
{
    public class ActorController : Controller
    {

        public ActionResult Index()
        {
            IList<ActorModel> actors = new List<ActorModel>();

            using (var ctx = new IMDBEntities())
            {
                actors = ctx.ActorDetails.Where(a => a.IsDeleted == false).Select(a => new ActorModel()
                {
                    ID = a.ID,
                    Name = a.Name,
                    Bio = a.Bio,
                    DOB = a.DOB,
                    Sex = a.Sex,
                    MovieName = a.MovieName 
                    

                }).ToList<ActorModel>();

            }
            return View(actors);
        }
        [Route("Actor/Create/{id}")]
        public ActionResult Create(int? id)
        {
            List<SelectListItem> Movie = new List<SelectListItem>();

            using (var ctx = new IMDBEntities())
            {
                Movie = ctx.Movies.Where(c => c.IsDeleted == false).Select(c => new SelectListItem()
                {
                    Value = c.ID.ToString(),
                    Text = c.Name.ToString()
                }).ToList<SelectListItem>();
            }
            ViewData["Movie"] = Movie;
            if (id != null)
            {
                using (var ctx = new IMDBEntities())
                {
                    var existActor = ctx.Actors.FirstOrDefault(a => a.ID == id);
                    if (existActor != null)
                    {
                        var editActor = Mapper.Map<Actor, ActorModel>(existActor);
                        return View(editActor);
                    }
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(ActorModel actor)
        {
            using (var ctx = new IMDBEntities())
            {
                if (actor.ID == 0)
                {
                    var newActor = ctx.Actors.Add(Mapper.Map<ActorModel, Actor>(actor));
                    newActor.IsDeleted = false;
                }
                else
                {
                    var existActor = ctx.Producers.FirstOrDefault(p => p.ID == actor.ID);
                    if (existActor != null)
                    {
                        existActor.IsDeleted = false;
                        Mapper.Map(actor, existActor);
                    }
                }
                ctx.SaveChanges();
            }
            return RedirectToAction("Index");
        }



        public ActionResult Delete(int id)
        {
            using (var ctx = new IMDBEntities())
            {
                var existRecord = ctx.Actors.FirstOrDefault(a => a.ID == id);
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
