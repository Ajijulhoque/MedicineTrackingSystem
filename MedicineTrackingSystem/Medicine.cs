using System;
using System.ComponentModel.DataAnnotations;


namespace MedicineTrackingSystem
{
    public class Medicine
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        public string Notes { get; set; }
    }
}
