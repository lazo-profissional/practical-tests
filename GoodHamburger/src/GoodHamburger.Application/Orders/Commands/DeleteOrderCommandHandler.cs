using MediatR;
using GoodHamburger.Domain.Exceptions;
using GoodHamburger.Domain.Interfaces;

namespace GoodHamburger.Application.Orders.Commands;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
    private readonly IOrderRepository _orderRepository;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new EntityNotFoundException("Order", request.Id);

        await _orderRepository.DeleteAsync(request.Id, cancellationToken);
    }
}