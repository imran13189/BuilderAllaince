using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildersAlliances.Services.Interfaces;
using BuildersAlliances.Domain;

namespace BuildersAlliances.Services
{
   public class UserService:IUsers
    {
        Repository.Interfaces.IUsers _user = null;
        public UserService()
        {
            _user = new Repository.UserRepository();
        }
        public  bool CreateUser(Users model)
        {
            return _user.CreateUser(model);
        }

        public bool Authenticate(string Email, string Password)
        {
            return _user.Authenticate(Email, Password);
        }

    }
}
