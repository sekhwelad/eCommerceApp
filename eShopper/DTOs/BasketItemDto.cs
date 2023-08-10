﻿using System.ComponentModel.DataAnnotations;

namespace eShopper.DTOs
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(0.1,double.MaxValue,ErrorMessage ="Price must be greater than zero")]
        public Decimal Price { get; set; }
        [Required]
        [Range (1,double.MaxValue, ErrorMessage = "Quantity must at least be 1")]
        public int Quantity { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
