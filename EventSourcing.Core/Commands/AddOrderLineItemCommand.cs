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
    public class AddOrderLineItemCommand : IRequest<bool>
    {
        public Guid OrderId { get; }
        public int Quantity { get; }
        public string Description { get; }
        public decimal UnitPrice { get; }

        public AddOrderLineItemCommand(Guid orderId, int quantity, string description, decimal unitPrice)
        {
            OrderId = orderId;
            Quantity = quantity;
            Description = description;
            UnitPrice = unitPrice;
        }
    }

    public class AddOrderLineItemCommandHandler : IRequestHandler<AddOrderLineItemCommand, bool>
    {
        private readonly OrderLineItemFactory _factory;
        private readonly IReadRepository<OrderHeader> _readRepository;
        private readonly IWriteRepository<OrderHeader> _writeRepository;

        public AddOrderLineItemCommandHandler()
        {
            _readRepository = new OrderHeaderRepository();
            _writeRepository = new OrderHeaderRepository();
            _factory = new OrderLineItemFactory();
        }

        public async Task<bool> Handle(AddOrderLineItemCommand request, CancellationToken cancellationToken)
        {
            var orderHeader = _readRepository.Get(request.OrderId);
            var orderLineItem = _factory.Create(request.Quantity, request.Description, request.UnitPrice);

            orderHeader.LineItems.Add(orderLineItem);

            _writeRepository.Save();

            return Task.FromResult(true).Result;
        }
    }
}
