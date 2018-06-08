using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMDB.Models
{
    public class MovieModel
    {
        public int ID { get; set; }
        [Display(Name = "Movie")]
        [Required(ErrorMessage = "Movie Name is required.")]
        public string Name { get; set; }
        [Display(Name = "Year Of Release")]
        [Required(ErrorMessage = "Release Year is required.")]
        public int YearOfRelease { get; set; }
        [Display(Name = "Plot")]
        [Required(ErrorMessage = "Plot is required.")]
        public string Plot { get; set; }
        [Display(Name = "Poster")]
        [Required(ErrorMessage = "Poster is required.")]
        public string Poster { get; set; }
        [Display(Name = "Producer Name")]
        public int ProducerId { get; set; }
        [Display(Name = "Producer Name")]
        public string producerName { get; set; }
        public HttpPostedFileBase File { get; set; }
        public bool IsDeleted { get; set; }
    }
}