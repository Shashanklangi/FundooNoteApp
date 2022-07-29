using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface IUserBL
    {
        public UserEntity Registration(UserRegistrationModel userRegistrationModel);
        public string Login(UserLoginModel userLoginModel);
        public string ForgetPassword(string Email);
        public bool ResetLink(string email, string password, string confirmPassword);
    }
}
