using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LearningCenter.Models;
using LearningCenter.DataAccessLayer;
using System.Web.Caching;


namespace LearningCenter
{
    public interface IClassRepository
    {
        ClassModel[] Classes { get; }
        ClassModel Class(int classId);
        ClassModel[] ForUser(int userId);
    }

    public interface IUserRepository
    {
        UserModel[] Users { get; }
        UserModel User(int userId);
        UserModel[] ForClass(int classId);
        void AddClassToUser(int userId, int classId);
    }

    //public interface IUserClassRepository
    //{
    //    UserClassModel Add(int userId, int classId);
    //    bool Remove(int userId, int classId);
    //    UserClassModel[] GetAll(int UserId);
        
    //}

    //public class UserClassModel
    //{
    //    public int UserId { get; set; }
    //    public int ClassId { get; set; }

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
        public ClassModel[] ForUser(int userId)
        {
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