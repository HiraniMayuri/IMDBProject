using AutoMapper;
using IMDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMDB.Controllers
{
    public class ProducerController : Controller
    {
        // GET: Producer
        public ActionResult Index()
        {
            IList<ProducerModel> producers = new List<ProducerModel>();

            using (var ctx = new IMDBEntities())
            {
                producers = ctx.Producers.Where(p => p.IsDeleted == false).Select(p => new ProducerModel()
                {
                    ID = p.ID,
                    Name = p.Name,
                    Bio = p.Bio,
                    DOB = p.DOB,
                    Sex = p.Sex,
                 

                }).ToList<ProducerModel>();

            }
            return View(producers);

        }
        [Route("Producer/Create/{id}")]
        public ActionResult Create(int? id)
        {
            if (id != null)
            {
                using (var ctx = new IMDBEntities())
                {
                    var existProducer = ctx.Producers.FirstOrDefault(p => p.ID == id);
                    if (existProducer != null)
                    {
                        var editProducer = Mapper.Map<Producer, ProducerModel>(existProducer);
                    
                        return View(editProducer);
                    }
                }
            }
            return View();
        }

        [HttpPost]

        public ActionResult Create(ProducerModel producer)
        {
            using (var ctx = new IMDBEntities())
            {
                if (producer.ID == 0)
                {
                   var newProducer= ctx.Producers.Add(Mapper.Map<ProducerModel, Producer>(producer));
                    newProducer.IsDeleted = false;
                }
                else
                {
                    var existProducer = ctx.Producers.FirstOrDefault(p => p.ID == producer.ID);
                    if (existProducer != null)
                    {
                        existProducer.IsDeleted = false;
                        Mapper.Map(producer, existProducer);
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
                var existRecord = ctx.Producers.FirstOrDefault(p => p.ID == id);
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
