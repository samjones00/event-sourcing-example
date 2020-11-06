using System;
using EventSourcing.Core.Interfaces;

namespace EventSourcing.Core.PaymentHandlers
{
    public class PayPalPaymentHandler : IPaymentHandler
    {
        private string HandlerName = "PAYPAL";

        public bool IsMatch(string providerName) => providerName.Equals(HandlerName);

        public bool Process()
        {
            return true;
        }
    }
}
