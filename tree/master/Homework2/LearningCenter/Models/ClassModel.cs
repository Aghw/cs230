using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearningCenter.Models
{
    public class ClassModel
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "Please enter class name")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Please enter class description")]
        public string Description { get; set; }

        //[Required(ErrorMessage = "Please enter class price")]
        public decimal Price { get; set; }

        //public ClassModel(int id, string name, string description, decimal price)
        //{
        //    ClassId = id;
        //    ClassName = name;
        //    ClassDescription = description;
        //    ClassPrice = price;
        //}
    }
}