using eShopper.Core.Entities;
using eShopper.Core.Entities.OrderAggregate;
using eShopper.Core.Interfaces;
using eShopper.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace eShopper.Controllers
{
    public class PaymentsController : BaseApiController
    {
        private const string WebHookSecret = "whsec_feffff24e307466afac226e4da6cb1fca204a23810d8fea2c0e272aa433f2b26";
        private readonly ILogger<PaymentsController>  _logger;  
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService, ILogger<PaymentsController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;   
        }

        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket =  await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            if (basket == null) return BadRequest(new ApiResponse(400, "Problem with your basket"));

            return basket;
        }
        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebHook()
        {
            var json = await new StreamReader(Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], WebHookSecret);

            PaymentIntent intent;
            Order order;

            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    intent =(PaymentIntent) stripeEvent.Data.Object;
                    _logger.LogInformation($"Payment succeeded {intent.Id}");
                    order = await _paymentService.UpdateOrderPaymentSucceeded(intent.Id);
                    _logger.LogInformation($"Order Updated to payment received {order.Id}");

                    break;
                case "payment_intent.payment_failed":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation($"Payment failed {intent.Id}");
                    order = await _paymentService.UpdateOrderPaymentFailed(intent.Id);
                    _logger.LogInformation($"Order Updated to payment failed {order.Id}");
                    break;
            }
            return new EmptyResult();
        }

    }
}
