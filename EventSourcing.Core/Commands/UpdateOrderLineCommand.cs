using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventSourcing.Core.Interfaces;
using EventSourcing.Core.Models;
using EventSourcing.Core.Repositories;
using MediatR;

namespace EventSourcing.Core.Commands
{
    public class UpdateOrderLineCommand:IRequest<bool>
    {
        public Guid OrderHeaderId { get; }
        public Guid OrderLineItemId { get; }
        public int Quantity { get; }

        public UpdateOrderLineCommand(Guid orderHeaderId, Guid orderLineItemId, int quantity)
        {
            OrderHeaderId = orderHeaderId;
            OrderLineItemId = orderLineItemId;
            Quantity = quantity;
        }
    }

    public class UpdateOrderLineCommandHandler:IRequestHandler<UpdateOrderLineCommand,bool>
    {
        private IRepository<OrderHeader> _repository;

        public UpdateOrderLineCommandHandler()
        {
            _repository = new OrderHeaderRepository();
        }

        public Task<bool> Handle(UpdateOrderLineCommand request, CancellationToken cancellationToken)
        {
            var orderHeader = _repository.Get(request.OrderHeaderId);
            var orderLineItem = orderHeader.LineItems.FirstOrDefault(x => x.Id == request.OrderLineItemId);
            orderLineItem.Quantity = request.Quantity;

            _repository.Update(orderHeader);

            return new Task<bool>(() => true, cancellationToken);
        }
    }
}
