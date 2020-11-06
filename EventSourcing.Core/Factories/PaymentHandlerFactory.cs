using System;
using System.Collections.Generic;
using System.Linq;
using EventSourcing.Core.Interfaces;

namespace EventSourcing.Core.Factories
{
    public class PaymentHandlerFactory
    {
        private  IEnumerable<IPaymentHandler> _handlers;

        public PaymentHandlerFactory()
        {
            if (_handlers == null)
            {
                PopulateDictionary();
            }
        }

        private void PopulateDictionary()
        {
            var type = typeof(IPaymentHandler);

            var implementations = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(type.IsAssignableFrom)
                .Where(p => !p.IsInterface)
                .ToList();

            if (!implementations.Any())
            {
                throw new NullReferenceException($"No handler found for {type}.");
            }

            var instances = implementations
                .Select(implementation => (IPaymentHandler)
                    Activator.CreateInstance(implementation));

            _handlers = instances;
        }

        public IPaymentHandler Get(string providerName)
        {
            foreach (var handler in _handlers)
            {
                var isMatch = handler.IsMatch(providerName);

                if (isMatch)
                {
                    return handler;
                }
            }

            throw new Exception("Handler not found");
        }
    }
}
