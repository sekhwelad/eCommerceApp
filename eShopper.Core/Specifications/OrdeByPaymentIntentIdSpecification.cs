
using eShopper.Core.Entities.OrderAggregate;

namespace eShopper.Core.Specifications
{
    public class OrdeByPaymentIntentIdSpecification : BaseSpecification<Order>
    {
        public OrdeByPaymentIntentIdSpecification(string paymentIntentId) 
            : base(o=> o.PaymentIntentId==paymentIntentId)
        {
        }
    }
}
