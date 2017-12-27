using HangoutsDbLibrary.Model;
using HangoutsDbLibrary.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HangoutsBusinessLibrary.Services
{
    public class AuthenticationService
    {
        public User Authenticate(User user)
        {
            using (var uow = new UnitOfWork())
            {
                var userRepository = uow.GetRepository<User>();
                List<User> users = userRepository.GetAll().Include(u => u.Address).Where(u => u.Email == user.Email).ToList();
                if (users == null || users.Count == 0)
                    users = userRepository.GetAll().Include(u => u.Address).Where(u => u.Username == user.Username).ToList();
                if (users == null || users.Count == 0)
                    throw new Exception("Incorrect email / username!");
                users = users.Where(u => u.Password == user.Password).ToList();
                if (users == null || users.Count == 0)
                    throw new Exception("Incorrect password");
                return users.FirstOrDefault();
            }
        } 
    }
}
