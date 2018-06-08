using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMDB.Models
{
    public class ActorModel
    {
        public int ID { get; set; }
        [Display(Name = "Actor")]
        [Required(ErrorMessage = "Actor Name is required.")]
        public string Name { get; set; }
        public string Sex { get; set; }
        public System.DateTime DOB { get; set; }
        [Display(Name = "Movie")]
        [Required(ErrorMessage = "Bio is required.")]
        public string Bio { get; set; }
        [Display(Name = "Movie Name")]
        public int MovieId { get; set; }
        [Display(Name = "Movie Name")]
        public string MovieName { get; set; }
        public bool IsDeleted { get; set; }
    }
}