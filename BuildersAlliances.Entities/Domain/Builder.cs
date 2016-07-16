using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BuildersAlliances.Domain
{
    [Table("Builder")]
    public class Builder : BaseEntity
    {
        [Key]
        public int BuilderId { get; set; }
        [Required]
        public string Address1 { get; set; }


    
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsDeleted { get; set; }

        public string BuilderName { get; set; }

        public ICollection<Qoute> Qoute { get; set; }



    }
}
