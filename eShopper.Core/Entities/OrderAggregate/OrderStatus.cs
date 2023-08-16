using System.Runtime.Serialization;

namespace eShopper.Core.Entities.OrderAggregate
{
    public enum OrderStatus
    {
        [EnumMember(Value ="Pending")]
        Pending,

        [EnumMember(Value = "Payment Received")]
        PaymentReceived,

        [EnumMember(Value = "Payment Failed")]
        PaymentFailed
    }
}
