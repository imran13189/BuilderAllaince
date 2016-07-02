using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildersAlliances.Domain;

namespace BuildersAlliances.Services.Interfaces
{ 
   public  interface IUsers
    {
       bool CreateUser(Users model);
        bool Authenticate(string Email, string Password);
    }
}
