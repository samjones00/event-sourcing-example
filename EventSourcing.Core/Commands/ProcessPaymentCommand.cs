using System;
using System.Threading;
using System.Threading.Tasks;
using EventSourcing.Core.Factories;
using EventSourcing.Core.Interfaces;
using EventSourcing.Core.Models;
using EventSourcing.Core.Repositories;
using MediatR;

namespace EventSourcing.Core.Commands
{
    public class ProcessPaymentCommand : IRequest<bool>
    {
        public string PaymentProvider { get; }
        public Guid OrderHeaderId { get; }

        public ProcessPaymentCommand(Guid orderHeaderId, string paymentProvider)
        {
            OrderHeaderId = orderHeaderId;
            PaymentProvider = paymentProvider;
        }
    }

    public class ProcessPaymentCommandHandler : IRequestHandler<ProcessPaymentCommand, bool>
    {
        private IWriteRepository<OrderHeader> _repository;
        private readonly PaymentHandlerFactory _paymentHandlerFactory;

        public ProcessPaymentCommandHandler()
        {
            _repository = new OrderHeaderRepository();
            _paymentHandlerFactory = new PaymentHandlerFactory();
        }

        public Task<bool> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
        {
            var handler = _paymentHandlerFactory.Get("BARCLAYS");

            handler.Process();

            return null;
        }
    }
}
