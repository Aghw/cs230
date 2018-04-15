using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BirthdayCard.Models
{
    public class BirthdayCard
    {
        [Required(ErrorMessage = "Please enter the From name")]
        public string From { get; set; }

        [Required(ErrorMessage = "Please enter the To name")]
        public string To { get; set; }

        [Required(ErrorMessage = "Please enter a birthday message!")]
        public string Message { get; set; }
    }
}