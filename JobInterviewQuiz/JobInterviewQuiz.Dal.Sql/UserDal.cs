using System.Collections.Generic;
using System.Linq;
using JobInterviewQuiz.Model.Dtos;
using JobInterViewQuiz.Dal;

namespace JobInterviewQuiz.Dal.Sql
{
    public class UserDal : IUserDal
    {
        //get data
        /// <summary>
        /// Gets a list of all the users or an empty list if none were found.
        /// </summary>
        /// <returns> List of users or empty list </returns>
        public List<User> GetAllUsers()
        {
            using (QuizEntities context = new QuizEntities())
            {
                return context.Users.Count() == 0 ? new List<User>() : context.Users.ToList();
            }
        }

        /// <summary>
        ///  Returns a user found by a given Id or null if none were found.
        /// </summary>
        /// <param name="UserId"> Given user Id. </param>
        /// <returns> A User object or null </returns>
        public User GetUserById(int UserId)
        {
            using (QuizEntities context = new QuizEntities())
            {
                return context.Users.FirstOrDefault(r => r.Id == UserId);
            }
        }

        /// <summary>
        ///  Returns a User object if user was found with given Username or null otherwise.
        /// </summary>
        /// <param name="Username"> Given Username </param>
        /// <returns> User object or null </returns>
        public User GetUserByUsername(string Username)
        {
            using (QuizEntities context = new QuizEntities())
            {
                return context.Users.FirstOrDefault(r => r.Username == Username);
            }
        }

        /// <summary>
        ///  Returns a User object if user was found with given Username or Password, null otherwise.
        /// </summary>
        /// <param name="Username"> Given Username </param>
        /// <param name="Password"> Given Password </param>
        /// <returns> User object or null </returns>
        public User LoginUser(string Username, string Password)
        {
            using (QuizEntities context = new QuizEntities())
            {
                return context.Users.FirstOrDefault(r => r.Username == Username && r.Password == Password);
            }
        }

        //add data
        /// <summary>
        /// Adds a normal user to database have ActivStatus=False and IsAdmin=false
        /// </summary>
        /// <param name="username"> Given Username </param>
        /// <param name="password"> Given Password </param>
        /// <param name="email"> Given EMail </param>
        public void AddUser(string username, string password, string email)
        {
            using (QuizEntities context = new QuizEntities())
            {
                var user = new User
                {
                    Username = username,
                    Password = password,
                    ActivStatus = true,
                    Email = email,
                    IsAdmin = false

                };

                context.Users.Add(user);
                context.SaveChanges();

            }
        }

        /// <summary>
        /// Adds a normal user to database have ActivStatus=False and IsAdmin=false
        /// </summary>
        /// <param name="username"> Given Username </param>
        /// <param name="password"> Given Password </param>
        /// <param name="isActive"> Given Active status </param>
        /// <param name="email"> Given EMail </param>
        /// <param name="isAdmin"> Given Admin status  </param>
        public void AddUser(string username, string password, bool isActive, string email, bool isAdmin)
        {
            using (QuizEntities context = new QuizEntities())
            {
                var user = new User
                {
                    Username = username,
                    Password = password,
                    ActivStatus = isActive,
                    Email = email,
                    IsAdmin = isAdmin

                };

                context.Users.Add(user);
                context.SaveChanges();

            }
        }

        //update data
        /// <summary>
        ///  Set if user is active by a given id
        /// </summary>
        /// <param name="Id"> User id </param>
        /// <param name="status"> User status </param>
        public void SetStateById(int Id, bool status)
        {
            using (QuizEntities context = new QuizEntities())
            {
                var user = context.Users.FirstOrDefault(r => r.Id == Id);
                user.ActivStatus = status;

                context.SaveChanges();
            }

        }

        /// <summary>
        ///  Set if user is active by a given username
        /// </summary>
        /// <param name="Id"> User id </param>
        /// <param name="status"> User status </param>
        public void SetStateByUsername(string Username, bool status)
        {
            using (QuizEntities context = new QuizEntities())
            {
                var user = context.Users.FirstOrDefault(r => r.Username == Username);
                user.ActivStatus = status;

                context.SaveChanges();
            }
        }

        /// <summary>
        ///  Set if user is admin by a given id
        /// </summary>
        /// <param name="Id"> Given Id </param>
        /// <param name="status">Given status</param>
        public void SetAdminById(int Id, bool status)
        {
            using (QuizEntities context = new QuizEntities())
            {
                var user = context.Users.FirstOrDefault(r => r.Id == Id);
                user.IsAdmin = status;

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Set if user is admin by a given username
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="status"></param>
        public void SetAdminByUsername(string Username, bool status)
        {
            using (QuizEntities context = new QuizEntities())
            {
                var user = context.Users.FirstOrDefault(r => r.Username == Username);
                user.IsAdmin = status;

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates a user by a given id.
        /// </summary>
        /// <param name="id"> Given Id </param>
        /// <param name="username"> Given Username </param>
        /// <param name="password"> Given Password </param>
        /// <param name="email"> Given email </param>
        /// <param name="isActive"> Given Active status </param>
        public void UpdateUser(int id, string username, string password, string email, bool isActive)
        {
            using (QuizEntities context = new QuizEntities())
            {

                var user = context.Users.FirstOrDefault(r => r.Id == id);

                user.Username = username;
                user.Password = password;
                user.Email = email;
                user.ActivStatus = isActive;

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates a user by a given id.
        /// </summary>
        /// <param name="id"> Given Id </param>
        /// <param name="username"> Given Username </param>
        /// <param name="password"> Given Password </param>
        /// <param name="email"> Given email </param>
        /// <param name="isActive"> Given Active status </param>
        /// <param name="isAdmin"> Given Admin status </param>
        public void UpdateUser(int id, string username, string password, string email, bool isActive, bool isAdmin)
        {
            using (QuizEntities context = new QuizEntities())
            {

                var user = context.Users.FirstOrDefault(r => r.Id == id);

                user.Username = username;
                user.Password = password;
                user.Email = email;
                user.ActivStatus = isActive;
                user.IsAdmin = isAdmin;

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates a user by a given id.
        /// </summary>
        /// <param name="id"> Given Id </param>
        /// <param name="username"> Given Username </param>
        /// <param name="email"> Given email </param>
        /// <param name="isActive"> Given Active status </param>
        /// <param name="isAdmin"> Given Admin status </param>
        public void UpdateUser(int id, string username, string email, bool isActive, bool isAdmin)
        {
            using (QuizEntities context = new QuizEntities())
            {

                var user = context.Users.FirstOrDefault(r => r.Id == id);

                user.Username = username;
                user.Email = email;
                user.ActivStatus = isActive;
                user.IsAdmin = isAdmin;

                context.SaveChanges();
            }
        }

        //delete data
        /// <summary>
        ///  Deletes a user by a given Id.
        /// </summary>
        /// <param name="id"> Given user id </param>
        public void DeleteUserById(int id)
        {
            using (QuizEntities context = new QuizEntities())
            {
                var user = context.Users.FirstOrDefault(r => r.Id == id);

                context.Users.Remove(user);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes a user by a given username.
        /// </summary>
        /// <param name="username"> Given Username </param>
        public void DeleteUserByUsername(string username)
        {
            using (QuizEntities context = new QuizEntities())
            {
                var user = context.Users.FirstOrDefault(r => r.Username == username);

                context.Users.Remove(user);
                context.SaveChanges();
            }
        }

    }
}
