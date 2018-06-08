using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMDB.Models
{
    public class ProducerModel
    {
        public int ID { get; set; }
        [Display(Name = "Movie")]
        [Required(ErrorMessage = "Producer is required.")]
        public string Name { get; set; }
        [Display(Name = "Gender")]
        public string Sex { get; set; }
        [Required]
        [Display(Name = "Date Of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public System.DateTime DOB { get; set; }
        [Display(Name = "Bio")]
        [Required(ErrorMessage ="Bio is required.")]
        public string Bio { get; set; }
        public bool IsDeleted { get; set; }

    }


}