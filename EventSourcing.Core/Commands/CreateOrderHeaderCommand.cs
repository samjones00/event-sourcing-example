using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using EventSourcing.Core.Factories;
using EventSourcing.Core.Interfaces;
using EventSourcing.Core.Models;
using EventSourcing.Core.Repositories;
using MediatR;

namespace EventSourcing.Core.Commands
{
    public class CreateOrderHeaderCommand : IRequest<Guid>
    {
    }

    public class CreateOrderHeaderCommandHandler : IRequestHandler<CreateOrderHeaderCommand, Guid>
    {
        private readonly OrderHeaderFactory _factory;
        private readonly IWriteRepository<OrderHeader> _repository;
        private readonly IWriteRepository<IRequest> _eventRepository;

        public CreateOrderHeaderCommandHandler()
        {
            _factory = new OrderHeaderFactory();
            _repository = new OrderHeaderRepository();
        }

        public async Task<Guid> Handle(CreateOrderHeaderCommand request, CancellationToken cancellationToken)
        {
            var order = _factory.Create();

            _repository.Add(order);
            _repository.Save();

            return Task.FromResult(order.Id).Result;
        }
    }
}
