using System;



using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BuildersAlliances.Domain
{
    [Table("ItemDiscounts")]
    public class ItemDiscounts
    {
        [Key]
        public long ItemDiscountId { get; set; }
        public int OrderItemId { get; set; }
        public int DiscountTypeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Multiplier { get; set; }



    }
}
