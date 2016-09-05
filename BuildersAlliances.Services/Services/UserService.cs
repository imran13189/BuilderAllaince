using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildersAlliances.Services.Interfaces;
using BuildersAlliances.Domain;
using BuildersAlliances.Repository;
using BuildersAlliances.CustomModel;
using System.Data.SqlClient;

namespace BuildersAlliances.Services
{
   public class UserService:IUsers
    {
        UnityOfWork uow = null;
        public UserService()
        {
            if (uow == null)
            {
                uow = new UnityOfWork(new BuildersAlliancesContext());
            }
        }

        public bool CreateUser(Users model)
        {
            try
            {
                if (model.UserId == 0)
                {
                    model.CreatedDate = DateTime.UtcNow;
                    uow.Repository<Users>().Add(model);
                }
                else
                {
                    Users user = uow.Repository<Users>().Get(x => x.UserId == model.UserId);
                    user.Username = model.Username;
                    user.Email = model.Email;
                    user.Phone = model.Phone;
                    user.Name = model.Name;
                    user.Email = model.Email;


                }
                uow.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                throw e;
              
            }
        }
        public Users Authenticate(string Email, string Password)
        {
            try
            {
                Email = Email.ToLower();
                Users data= uow.Repository<Users>().Get(x => x.Username.ToLower() == Email && x.Password == Password);
                return data;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public List<UsersModel> GetUser(int limit, int offset, string order, string sort, UsersModel model)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[] {
                         new SqlParameter("@offset", offset),
                         new SqlParameter("@limit", limit),
                         new SqlParameter("@order", order),
                         new SqlParameter("@sort", sort),
                         new SqlParameter("@Name", model.Name),
                         new SqlParameter("@Email", model.Email),
                         new SqlParameter("@Username", model.Username)
                         };
                var data = uow.ExecuteProcedure<UsersModel>("exec GetUsers @offset, @limit, @order,@sort, @Name, @Email, @Username", param);
                return data;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public bool DeleteUser(int UserId)
        {
           Users user= uow.Repository<Users>().Get(x => x.UserId == UserId);

            user.IsDeleted = true;
            uow.SaveChanges();
            return true;
        }

        public List<Roles> GetRoles()
        {
           return uow.Repository<Roles>().GetAll().ToList();
        }


        public bool IsEmailExist(string Email,int UserId)
        {
            try
            {
                if(UserId>0)
                {
                    Users user = uow.Repository<Users>().AsQuerable().FirstOrDefault(x => x.Email.Contains(Email) && x.UserId != UserId);
                    if (user == null)
                        return true;
                    else
                        return false;
                }
               Users model= uow.Repository<Users>().AsQuerable().FirstOrDefault(x => x.Email==Email && x.IsDeleted == false);
                if (model == null)
                    return true;
                else
                    return false;
                
            }
            catch (Exception e) { throw e; }
        }

        public bool IsUsernameExist(string username, int UserId)
        {
            try
            {
                if (UserId > 0)
                {
                    Users user = uow.Repository<Users>().AsQuerable().FirstOrDefault(x => x.Email.Contains(username) && x.UserId != UserId);
                    if (user == null)
                        return true;
                    else
                        return false;
                }
                Users model = uow.Repository<Users>().AsQuerable().FirstOrDefault(x => x.Username.Contains(username)&&x.IsDeleted==false);
                if (model == null)
                    return true;
                else
                    return false;

            }
            catch (Exception e) { throw e; }
        }


    public    UserInRoleModel AssignRole(int UserId)
        {
            try
            {
                IList<Roles> roles = uow.Repository<Roles>().GetAll().ToList();
                UserInRole userInRole;
                List<RoleModel> data = new List<RoleModel>();
                foreach (Roles item in roles)
                {
                    userInRole = uow.Repository<UserInRole>().Get(x => x.UserId == UserId && x.RoleId == item.RoleId);
                    if (userInRole == null)
                    {
                        data.Add(new RoleModel() {
                            RoleName = item.RoleName,
                            RoleId = item.RoleId,
                            IsChecked = false

                        });
                    }
                    else
                    {
                        data.Add(new RoleModel()
                        {
                            RoleName = item.RoleName,
                            RoleId = item.RoleId,
                            IsChecked = true

                        });
                    }
                }
                return new UserInRoleModel() { UserId = UserId, model = data };
            }
            catch(Exception e)
            {
                throw e;
            }
        }

       public bool AssignRole(UserInRoleModel model)
        {
            UserInRole userInRole;
            try
            {
                
                foreach (RoleModel item in model.model)
                {
                    if (item.IsChecked)
                    {
                        userInRole = uow.Repository<UserInRole>().Get(x => x.UserId == model.UserId && x.RoleId == item.RoleId);
                        if (userInRole == null)
                        {
                            uow.Repository<UserInRole>().Add(new UserInRole()
                            {
                                RoleId = item.RoleId,
                                UserId = model.UserId


                            });
                        }
                    }
                    else
                    {
                        userInRole = uow.Repository<UserInRole>().Get(x => x.UserId == model.UserId && x.RoleId == item.RoleId);
                        if (userInRole != null)
                        {
                            uow.Repository<UserInRole>().Delete(userInRole);
                        }
                    }
                  
                }

                uow.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        public Users GetUser(int UserId)
        {
           return uow.Repository<Users>().Get(x => x.UserId == UserId);
        }
    }
}
