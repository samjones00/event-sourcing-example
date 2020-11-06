using System;
using EventSourcing.Core.Interfaces;

namespace EventSourcing.Core.PaymentHandlers
{
    public class BarclaysPaymentHandler : IPaymentHandler
    {
        public string HandlerName = "BARCLAYS";

        public bool IsMatch(string providerName) => providerName.Equals(HandlerName);

        public bool Process()
        {
            return true;
        }
    }
}
