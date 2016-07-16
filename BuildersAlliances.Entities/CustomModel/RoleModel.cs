using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildersAlliances.CustomModel
{
    
    public class UserInRoleModel
    {
      public int UserId { get; set; }
        public List<RoleModel> model { get; set; }
    }

    public class RoleModel
    {
        public string RoleName { get; set; }
        public int RoleId { get; set; }



        public bool IsChecked { get; set; }
    }
}
