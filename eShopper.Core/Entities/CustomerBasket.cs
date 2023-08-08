﻿
namespace eShopper.Core.Entities
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; } = new();

        public CustomerBasket()
        {           
        }
        public CustomerBasket(string id)
        {
            Id = id;
        }
    }
}
