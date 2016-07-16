using BuildersAlliances.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildersAlliances.Repository.Interfaces
{
    public interface IUsers
    {
        bool CreateUser(Users model);
        bool Authenticate(string Email, string Password);

            
    }
}
