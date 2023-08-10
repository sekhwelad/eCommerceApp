using eShopper.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace eShopper.DTOs
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; } = new();
    }
}
