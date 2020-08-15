using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Repository
{
    public interface IUserRepository
    {
        UserModel LogIn(string email, string password);
        UserModel Register(string email, string password, bool isAdmin);
        bool exist(string email);
    }

    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UserRepository: IUserRepository
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

        public UserModel Register(string email, string password, bool isAdmin)
        {
            var user = DatabaseAccessor.Instance.Users
                    .Add(new Project1.ClassDatabase.User
                    {
                        UserEmail = email,
                        UserPassword = password,
                        UserIsAdmin = isAdmin
                    });

            DatabaseAccessor.Instance.SaveChanges();

            return new UserModel { Id = user.UserId, Name = user.UserEmail };
        }

        public bool exist(string email)
        {
            if (DatabaseAccessor.Instance.Users.Any(x => x.UserEmail == email))
            {
                return true;
            }

            return false;
        }
    }
}
