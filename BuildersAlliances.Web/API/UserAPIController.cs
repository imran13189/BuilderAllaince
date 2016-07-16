using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BuildersAlliances.Domain;
using BuildersAlliances.Services.Interfaces;
using BuildersAlliances.CustomModel;
using Newtonsoft.Json.Linq;

namespace BuildersAlliances.Web.API
{
    public class UserFilter : DataObject
    {
       public UsersModel model;
    }
    public class UserDetail
    {
        public List<UsersModel> data;
        public int count;
    }




    public class UserAPIController : ApiController
    {
        IUsers _users = null;
        public UserAPIController(IUsers users)
        {
            _users = users;
        }

        [HttpPost]
        public dynamic GetUsers(JObject Obj)
      {
            UserFilter filter = Obj.ToObject<UserFilter>();
            UserDetail re = new UserDetail();
            re.data = _users.GetUser(filter.limit, filter.offset, filter.order,filter.sort, filter.model);
            return new { rows = re.data, total = re.data.Count>0?re.data.First().TotalRows:0 };
        }

       
    }
}
