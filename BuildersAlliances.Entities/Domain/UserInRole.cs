using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildersAlliances.Domain
{
    /// <summary>
    /// Create by Imran
    /// Date: 12 July 2016
    /// </summary>
    [Table("UserInRole")]
    public class UserInRole
    {
        [Key]
        public int UserInRoleId { get; set; }
        public int? RoleId { get; set; }
        public int? UserId { get; set; }

        public virtual Roles Roles {get;set;}
        public virtual Users Users { get; set; }
    }
}
