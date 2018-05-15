using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IUserRepository
    {
        IEnumerable<UserModel> Users { get; }
        UserModel User(int userId);
        IEnumerable<UserModel> ForClass(int classId);
        void AddClassToUser(int userId, int classId);
        UserModel Register(string uname, string upassword);
        UserModel LogIn(string email, string password);
    }

    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }


    public class UserRepository : IUserRepository
    {
        public IEnumerable<UserModel> Users
        {
            get
            {
                return DatabaseAccessor.Instance.Users
                                       .Select(a => new UserModel
                                       {
                                           Id = a.UserId,
                                           Name = a.UserEmail,
                                       });
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

        public IEnumerable<UserModel> ForClass(int classId)
        {
            return DatabaseAccessor.Instance
                    .Classes.First(c => c.ClassId == classId)
                    .Users.Select(a => new UserModel
                    {
                        Id = a.UserId,
                        Name = a.UserEmail,
                        //UserPassword = a.UserPassword,
                    });
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


        public UserModel Register(string email, string password)
        {
            var user = DatabaseAccessor.Instance.Users
                    .Add(new EnrollmentDb.User { UserEmail = email, UserPassword = password });

            DatabaseAccessor.Instance.SaveChanges();

            return new UserModel { Id = user.UserId, Name = user.UserEmail, Password = user.UserPassword };
        }


        public UserModel LogIn(string email, string password)
        {
            var user = DatabaseAccessor.Instance.Users
                .FirstOrDefault(t => t.UserEmail.ToLower() == email.ToLower()
                                      && t.UserPassword == password);

            if (user == null)
            {
                return null;
            }

            return new UserModel { Id = user.UserId, Name = user.UserEmail, Password = user.UserPassword };
        }
    }
}
