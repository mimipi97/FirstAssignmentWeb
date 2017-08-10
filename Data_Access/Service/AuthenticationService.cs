using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access.Entities;
using Data_Access.Repositories;

namespace Data_Access.Service
{
    public class AuthenticationService
    {
        public User LoggedUser { get; private set; }

        public void Authentication(string username, string password)
        {
            UserRepository UserRepo = RepositoryFactory.GetUserRepository();
            LoggedUser = UserRepo.GetByUsernameAndPassword(username, password);
        } 
    }
}
