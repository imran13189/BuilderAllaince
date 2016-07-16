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
            if (model.UserId == 0)
            {
                uow.Repository<Users>().Add(model);
            }
            else
            {
               Users user= uow.Repository<Users>().Get(x => x.UserId == model.UserId);
               user.Username = model.Username;
               user.Email = model.Email;
                user.Phone = model.Phone;
                user.Name = model.Name;
                user.Email = model.Email;


            }
            uow.SaveChanges();
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

        //public bool DeleteUser(int UserId)
        //{
        //    Users model= uow.Repository<Users>().Get(x => x.UserId == UserId);
        //    model.IsDeleted = true;
        //    uow.SaveChanges();
        //    return true;
        //}
    }
}
