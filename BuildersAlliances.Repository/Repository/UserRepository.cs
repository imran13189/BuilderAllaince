using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildersAlliances.Repository.Interfaces;
using BuildersAlliances.Domain;

namespace BuildersAlliances.Repository
{
  public  class UserRepository:IUsers
    {
        UnityOfWork uow = null;
        public UserRepository()
        {
            if (uow == null)
            {
                uow = new UnityOfWork(new BuildersAlliancesContext());
            }
        }
        public bool CreateUser(Users model)
        {
            uow.Repository<Users>().Add(model);
            return true;
        }
        public bool Authenticate(string Email,string Password)
        {
            try
            {
                return uow.Repository<Users>().AsQuerable().Any(x => x.Email == Email && x.Password == Password);
            }
            catch
            {
                return false;
            }
        }
    }
}
