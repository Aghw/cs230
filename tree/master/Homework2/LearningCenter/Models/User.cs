using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LearningCenter.Models
{
    public class User
    {
        //[Required(ErrorMessage = "Please enter your name")]
        //public string Name { get; set; }

        //[Required(ErrorMessage = "Please enter your phone")]
        //[Phone]
        //public string Phone { get; set; }

        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please identify if you are admin")]
        public bool IsAdmin { get; set; }

        //[Required(ErrorMessage = "Please re-enter your password")]
        //public string ConfirmPassword { get; set; }

        //[Required(ErrorMessage = "Please select if you'll attend or not.")]
        //public bool? WillAttend { get; set; }
    }
}