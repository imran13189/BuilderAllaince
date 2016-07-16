using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildersAlliances.CustomModel
{
    
    public class UsersModel
    {
      
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive {get;set;}
        public bool IsDeleted { get; set; }

        public string Username { get; set; }

        public string Phone { get; set; }
        public int TotalRows { get; set; }
    }
}
