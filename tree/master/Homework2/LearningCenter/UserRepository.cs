using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LearningCenter.Models;
using System.Web.Caching;

namespace LearningCenter
{
    //------------------------------------------------------
    //public interface IUserRepository
    //{
    //    IEnumerable<User> Users { get; }
    //}

    //public class UserRepository : IUserRepository
    //{
    //    public IEnumerable<User> Users
    //    {
    //        get
    //        {
    //            // Check if the MyUsers is NOT cached
    //            if (HttpContext.Current.Cache["MyUsers"] == null)
    //            {
    //                var items = new[]
    //                {
    //                    new User{ UserId=101, Email = "jim@abc.com", Password="jimpass"},
    //                    new User{ UserId=201, Email = "tom@abc.com", Password="tompass"},
    //                };

    //                HttpContext.Current.Cache.Insert("MyUsers",
    //                                            items,
    //                                            null,
    //                                            DateTime.Now.AddSeconds(30),
    //                                            Cache.NoSlidingExpiration);
    //            }

    //            return (IEnumerable<User>)HttpContext.Current.Cache["MyUsers"];
    //            //return items;
    //        }
    //    }
    //}
    //---------------------------------------------------

    //public interface IUserRepository
    //{
    //    User LogIn(string email, string password);
    //}

    //public class UserRepository : IUserRepository
    //{
    //    public IEnumerable<User> Users
    //    {
    //        get
    //        {
    //            var items = new[]
    //            {
    //                new User{ UserId=100, Email="admin", Password="admin", IsAdmin=true },
    //                new User{ UserId=101, Email="mike", Password="mike"},
    //                new User{ UserId=102, Email="dave", Password="dave"},
    //                new User{ UserId=103, Email="lisa", Password="lisa"},
    //            };
    //            return items;
    //        }
    //    }

    //    public User LogIn(string email, string password)
    //    {
    //        return Users.SingleOrDefault(t => t.Email.ToLower() == email.ToLower()
    //                                    && t.Password == password);
    //    }
    //}
}