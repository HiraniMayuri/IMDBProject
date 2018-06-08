using AutoMapper;
using IMDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace IMDB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
                        Mapper.Initialize(cfg =>
            { 
                cfg.CreateMap<MovieModel, Movie>();
                cfg.CreateMap<Movie, MovieModel>();

                cfg.CreateMap<ProducerModel, Producer>();
                cfg.CreateMap<Producer, ProducerModel>();


                cfg.CreateMap<ActorModel, Actor>();
                cfg.CreateMap<Actor, ActorModel>();
            });
        }
    }
}
