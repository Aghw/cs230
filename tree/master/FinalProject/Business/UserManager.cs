using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;

namespace Business
{
    public interface IUserManager
    {
        UserModel LogIn(string email, string password);
        UserModel Register(string email, string password);
        IEnumerable<UserModel> Users { get; }
        UserModel User(int userId);
        void AddClass(int userId, int classId);
    }

    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public UserModel(int id, string name, string password)
        {
            Id = id;
            Name = name;
            Password = password;
        }
    }

    public class UserManager : IUserManager
    {
        private readonly IUserRepository userRepository;

        public UserManager(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserModel LogIn(string email, string password)
        {
            var user = userRepository.LogIn(email, password);
            return new UserModel(user.Id, user.Name, user.Password);
        }
        //public UserModel LogIn(string email, string password)
        //{
        //    var user = userRepository.Users
        //        .FirstOrDefault(t => t.Name.ToLower() == email.ToLower()
        //                              && t.Password == password);

        //    if (user == null)
        //    {
        //        return null;
        //    }

        //    //return new UserModel { Id = user.Id, Name = user.Name, Password = user.Password };
        //    return new UserModel(user.Id, user.Name, user.Password);
        //}

        public UserModel Register(string email, string password)
        {
            var user = userRepository.Register(email, password);
            //.Add(new UserModel { UserEmail = email, UserPassword = password });

            //userRepository.SaveChanges();

            return new UserModel(user.Id, user.Name, user.Password );
            //return new UserModel(userRepository.Register(email, password));
        }

        public IEnumerable<UserModel> Users
        {
            get
            {
                return userRepository.Users
                                       .Select(a => new UserModel(a.Id, a.Name, a.Password));
            }

        }


        public UserModel User(int userId)
        {
            return userRepository
                    .Users
                    .Select(a => new UserModel(a.Id, a.Name, a.Password))
                    .Where(a => a.Id == userId).First();
        }

        public void AddClass(int userId, int classId)
        {
           userRepository.AddClassToUser(userId, classId);
        }
    }
}
