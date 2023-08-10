using AutoMapper;
using eShopper.Core.Entities;
using eShopper.Core.Interfaces;
using eShopper.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace eShopper.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;   

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);

            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var customerBasket = _mapper.Map<CustomerBasketDto,CustomerBasket>(basket);
            var updatedBasket = await _basketRepository.UpdateBasketAsync(customerBasket);

            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task DeleteBasketAsync(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }
    }
}
