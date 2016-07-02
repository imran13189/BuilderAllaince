using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildersAlliances.CustomModel
{
    public class RegisterModel
    {
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }

        public int State { get; set; }
        public int City { get; set; }
        [Required]
        public int StateId { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool TermCondition { get; set; }
    }
}
