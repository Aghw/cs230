using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LearningCenter.Models;
using LearningCenter.DataAccessLayer;
using System.Web.Caching;


namespace LearningCenter
{
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
    //                    new User{ UserId=101, Email = "abc@aaa.com", Password="balls"},
    //                    new User{ UserId=201, Email = "addc@acc.com", Password="kkklll"},
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

    //public interface IClassRepository
    //{
    //    IEnumerable<ClassMaster> Classes { get; }
    //}

    public interface IClassRepository
    {
        //IEnumerable<ClassMaster> Classes { get; }
        ClassModel[] Classes { get; }
        ClassModel Class(int classId);
        ClassModel[] ForUser(int userId);
    }

    public interface IUserRepository
    {
        //IEnumerable<ClassMaster> Classes { get; }
        UserModel[] Users { get; }
        UserModel User(int userId);
        UserModel[] ForClass(int classId);
        void AddClassToUser(int userId, int classId);
    }

    public interface IUserClassRepository
    {
        UserClassModel Add(int userId, int classId);
        bool Remove(int userId, int classId);
        UserClassModel[] GetAll(int UserId);
        
    }

    public class UserClassModel
    {
        public int UserId { get; set; }
        public int ClassId { get; set; }
        //public int Quantity { get; set; }

    }

    //public class UserClassRepository : IUserClassRepository
    //{
    //    public UserClassModel Add(int userId, int classId)
    //    {
    //        var added_class = DataAccessLayer.Class

    //    }
    //}
    //public class UserModel
    //{
    //    public int UserId { get; set; }
    //    public string UserName { get; set; }
    //    public string UserPassword { get; set; }
    //}

    public class ClassModel
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "Please enter class name")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Please enter class description")]
        public string Description { get; set; }

        //[Required(ErrorMessage = "Please enter class price")]
        public decimal Price { get; set; }

        //public ICollection<ClassModel> UserClassList { get; set; }

        //public ClassModel(int id, string name, string description, decimal price)
        //{
        //    ClassId = id;
        //    ClassName = name;
        //    ClassDescription = description;
        //    ClassPrice = price;
        //}
    }

    public class UserRepository : IUserRepository
    {
        public UserModel[] Users {
            get
            {
                return DatabaseAccessor.Instance.Users
                                       .Select(a => new UserModel
                                       {
                                           Id = a.UserId,
                                           Name = a.UserEmail,
                                           //UserPassword = a.UserPassword,
                                       }).ToArray();
            }

        }


        public UserModel User(int userId)
        {
            return DatabaseAccessor.Instance
                    .Users
                    .Select(a => new UserModel
                    {
                        Id = a.UserId,
                        Name = a.UserEmail,
                        //UserPassword = a.UserPassword,
                    })
                    .Where(a => a.Id == userId).First();
        }

        public UserModel[] ForClass(int classId)
        {
            return DatabaseAccessor.Instance
                    .Classes.First(c => c.ClassId == classId)
                    .Users.Select(a => new UserModel
                    {
                        Id = a.UserId,
                        Name = a.UserEmail,
                        //UserPassword = a.UserPassword,
                    }).ToArray();
        }


        public void AddClassToUser(int userId, int classId)
        {
            // get user from DatabaseAccessor.Instance.Users
            var user = DatabaseAccessor.Instance.Users.First(a => a.UserId == userId);

            // get class from DatabaseAccessor.Instance.Classes
            var classToAdd = DatabaseAccessor.Instance.Classes
                            .FirstOrDefault(t => t.ClassId == classId);

            // Add class to user.Classes
            user.Classes.Add(classToAdd);

            // Save changes to users-classes joining table
            DatabaseAccessor.Instance.SaveChanges();
        }
    }

    public class ClassRepository : IClassRepository
    {
        //public IEnumerable<ClassMaster> Classes
        //{
        //    get
        //    {
        //        // Check if the MyClasses is NOT cached
        //        if (HttpContext.Current.Cache["MyClasses"] == null)
        //        {
        //            //var offered_classes = Classes.Select(t => )
        //            var items = new[]
        //            {
        //                new ClassMaster{ ClassId=10, ClassName = "C# Basics", ClassDescription="Learning C#", ClassPrice=300.50m},
        //                new ClassMaster{ ClassId=20, ClassName = "ASP.NET MVC", ClassDescription="Learning MVC C#", ClassPrice=500.50m},
        //            };

        //            HttpContext.Current.Cache.Insert("MyClasses",
        //                                         items,
        //                                         null,
        //                                         DateTime.Now.AddSeconds(30),
        //                                         Cache.NoSlidingExpiration);
        //        }

        //        return (IEnumerable<ClassMaster>)HttpContext.Current.Cache["MyClasses"];
        //        //return items;
        //    }
        //}

        public ClassModel[] ForUser(int userId)
        {
            //return DatabaseAccessor.Instance.Categories.First(t => t.CategoryId == categoryId)
            //        .Products.Select(t =>
            //            new ProductModel
            //            {
            //                Id = t.ProductId,
            //                Name = t.ProductName,
            //                Price = t.ProductPrice,
            //                Quantity = t.ProductQuantity
            //            })
            //.ToArray();

            return DatabaseAccessor.Instance
                    .Users.First(u => u.UserId == userId)
                    .Classes.Select(a => new ClassModel
                    {
                        Id = a.ClassId,
                        Name = a.ClassName,
                        Description = a.ClassDescription,
                        Price = a.ClassPrice
                    }).ToArray();
        }

        public ClassModel[] Classes
        {
            get
            {
                //// Check if the MyClasses is NOT cached
                //if (HttpContext.Current.Cache["MyClasses"] == null)
                //{
                //    //var offered_classes = Classes.Select(t => )
                //    var items = new[]
                //    {
                //        new ClassMaster{ ClassId=10, ClassName = "C# Basics", ClassDescription="Learning C#", ClassPrice=300.50m},
                //        new ClassMaster{ ClassId=20, ClassName = "ASP.NET MVC", ClassDescription="Learning MVC C#", ClassPrice=500.50m},
                //    };

                //    HttpContext.Current.Cache.Insert("MyClasses",
                //                                 items,
                //                                 null,
                //                                 DateTime.Now.AddSeconds(30),
                //                                 Cache.NoSlidingExpiration);
                //}

                //return (IEnumerable<ClassMaster>)HttpContext.Current.Cache["MyClasses"];
                ////return items;

                return DatabaseAccessor.Instance.Classes
                                       .Select(a => new ClassModel
                                       {
                                           Id = a.ClassId,
                                           Name = a.ClassName,
                                           Description = a.ClassDescription,
                                           Price = a.ClassPrice
                                       }).ToArray();
            }
        }


        public ClassModel Class(int classId)
        {
            var class_offered = DatabaseAccessor.Instance.Classes
                                                .Where(a => a.ClassId == classId)
                                                .Select(a => new ClassModel
                                                {
                                                    Id = a.ClassId,
                                                    Name = a.ClassName,
                                                    Description = a.ClassDescription,
                                                    Price = a.ClassPrice
                                                }).First();
            return class_offered;
        }
    }
}