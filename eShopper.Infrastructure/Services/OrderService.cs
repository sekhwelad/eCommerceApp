using eShopper.Core.Entities;
using eShopper.Core.Entities.OrderAggregate;
using eShopper.Core.Interfaces;
using eShopper.Core.Specifications;

namespace eShopper.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;   
        private readonly IBasketRepository _basketRepository;

        public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            // get basket from the basket repo
            var basket = await _basketRepository.GetBasketAsync(basketId);

            // get items from the product repo
            var items = new List<OrderItem>();
            foreach (var item in basket.Items) {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id,productItem.Name,productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }

            // get delivery method from repo
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            // calculate subtotal
            var  subTotal =items.Sum(item=>item.Price * item.Quantity);

            //check to see if order exists
            var spec = new OrdeByPaymentIntentIdSpecification(basket.PaymentIntentId);
            var order = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

            if(order != null)
            {
                order.ShipToAddress= shippingAddress;
                order.DeliveryMethod= deliveryMethod;   
                order.Subtotal= subTotal;
                _unitOfWork.Repository<Order>().Update(order);
            }
            else
            {
                // create order
                order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subTotal, basket.PaymentIntentId);
                _unitOfWork.Repository<Order>().Add(order);

            }

            // save to db
            var result = await _unitOfWork.Complete();

            if(result<=0) return null;  


            // return order
            return order;   
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(id,buyerEmail);

            return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);

            return await _unitOfWork.Repository<Order>().ListAsync(spec);   
        }
    }
}
