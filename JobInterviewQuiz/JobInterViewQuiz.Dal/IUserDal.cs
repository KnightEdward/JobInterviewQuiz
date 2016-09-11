using System.Collections.Generic;
using JobInterviewQuiz.Model.Dtos;

namespace JobInterViewQuiz.Dal
{
    public interface IUserDal
    {
        //get data
        List<User> GetAllUsers();
        User GetUserById(int UserId);
        User GetUserByUsername(string Username);
        User LoginUser(string Username, string Password);


        //add data
        void AddUser(string username, string password, string email);
        void AddUser(string username, string password, bool isActive, string email, bool isAdmin);

        //update data
        void SetStateById(int Id, bool status);
        void SetStateByUsername(string Username, bool status);

        void SetAdminById(int Id, bool status);
        void SetAdminByUsername(string Username, bool status);

        void UpdateUser(int id, string username, string password, string email, bool isActive);
        void UpdateUser(int id, string username, string password, string email, bool isActive, bool isAdmin);
        void UpdateUser(int id, string username, string email, bool isActive, bool isAdmin);


        //delete user
        void DeleteUserById(int id);
        void DeleteUserByUsername(string username);
    }
}
