using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace BuildersAlliances.Domain
{
    [Table("Users")]
    public class Users:BaseEntity
    {
        [Key]
        public int UserId { get; set; }

       
        [EmailAddress]
       
        public string Email { get; set; }
        public string Name { get; set; }
        
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get;set;}
        public bool IsDeleted { get; set; }

        [Required]
        [Remote("IsUsernameExist", "User", "Admin", AdditionalFields = "UserId", ErrorMessage = "Username already exists!")]
        public string Username { get; set; }

        public string Phone { get; set; }
        public virtual ICollection<UserInRole> UserInRole {get;set;}
    }
}
