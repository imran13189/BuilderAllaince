using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildersAlliances.Domain;
using BuildersAlliances.CustomModel;

namespace BuildersAlliances.Services.Interfaces
{ 
   public  interface IUsers
    {
       bool CreateUser(Users model);
        Users Authenticate(string Email, string Password);
        bool DeleteUser(int UserId);

        List<Roles> GetRoles();
        List<UsersModel> GetUser(int limit, int offset, string order, string sort, UsersModel model);
          bool IsEmailExist(string Email,int UserId);
         bool IsUsernameExist(string username, int UserId);

        UserInRoleModel AssignRole(int UserId);

        bool AssignRole(UserInRoleModel model);

        Users GetUser(int UserId);
    }
}
