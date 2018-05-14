using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LearningCenter.Models
{
    public class ClassMaster
    {
        public int ClassId { get; set; }

        [Required(ErrorMessage = "Please enter class name")]
        public string ClassName { get; set; }

        [Required(ErrorMessage = "Please enter class description")]
        public string ClassDescription { get; set; }

        [Required(ErrorMessage = "Please enter class price")]
        public decimal ClassPrice { get; set; }
    }
}