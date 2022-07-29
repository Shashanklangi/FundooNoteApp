using BussinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;

        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public UserEntity Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                return userRL.Registration(userRegistrationModel);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public string Login(UserLoginModel userLoginModel)
        {
            try
            {
                return userRL.Login(userLoginModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
