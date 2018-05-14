using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LearningCenter.DataAccessLayer;

namespace LearningCenter
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string Password { get; set; }
        //public ICollection<ClassModel> UserClassList { get; set; }

        //public UserModel(string name, string password)
        //{
        //    //Id = id;
        //    Name = name;
        //    Password = password;
        //    UserClassList = null;
        //}
    }

    //public class ClassModel
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }

    //}

    public interface IUserManager
    {
        UserModel LogIn(string email, string password);
        UserModel Register(string email, string password);
        //void AddClass(int classId);
    }


    public class ClientRepository : IUserManager
    {
        public UserModel LogIn(string email, string password)
        {
            var user = DatabaseAccessor.Instance.Users
                .FirstOrDefault(t => t.UserEmail.ToLower() == email.ToLower()
                                      && t.UserPassword == password);

            if (user == null)
            {
                return null;
            }

            return new UserModel { Id = user.UserId, Name = user.UserEmail };
        }

        public UserModel Register(string email, string password)
        {
            var user = DatabaseAccessor.Instance.Users
                    .Add(new User { UserEmail = email, UserPassword = password });

            DatabaseAccessor.Instance.SaveChanges();

            return new UserModel { Id = user.UserId, Name = user.UserEmail };
        }

        //public void AddClass(int classId)
        //{
        //    var user = DatabaseAccessor.Instance.Users
        //            .Add(new User { UserEmail = email, UserPassword = password });

        //    DatabaseAccessor.Instance.SaveChanges();

        //    //return new UserModel { Id = user.UserId, Name = user.UserEmail };
        //}

    }
}