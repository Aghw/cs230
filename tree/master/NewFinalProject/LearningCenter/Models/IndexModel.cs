using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearningCenter.Models
{
    public class IndexModel
    {
        public ClassModel[] Classes { get; set; }
        public User CurrentUser { get; set; }
    }
}