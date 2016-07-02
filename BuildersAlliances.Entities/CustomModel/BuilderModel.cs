using System;
using System.Collections.Generic;



namespace BuildersAlliances.CustomModel
{
    
    public class BuilderModel 
    {

        public int BuilderId { get; set; }
  
        public string Address1 { get; set; }

      
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }

  
        //[StringLength(10, ErrorMessage = "Contact must be of 10 number")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsDeleted { get; set; }

      
        public string BuilderName { get; set; }

        public int TotalRows { get; set; }



    }
}
