using Microsoft.EntityFrameworkCore;
using GoodHamburger.Domain.Exceptions;
using GoodHamburger.Domain.Interfaces;
using GoodHamburger.Domain.Models;
using GoodHamburger.Infrastructure.Data;

namespace GoodHamburger.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<List<Order>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Order> AddAsync(Order order, CancellationToken cancellationToken = default)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync(cancellationToken);
        return order;
    }

    public async Task<Order> UpdateAsync(Order order, CancellationToken cancellationToken = default)
    {
        var existingOrder = await _context.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == order.Id, cancellationToken)
            ?? throw new EntityNotFoundException("Order", order.Id);

        _context.Orders.Remove(existingOrder);
        await _context.SaveChangesAsync(cancellationToken);

        var newOrder = new Order(order.Items.ToList());
        _context.Orders.Add(newOrder);
        await _context.SaveChangesAsync(cancellationToken);

        return newOrder;
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var order = await _context.Orders.FindAsync(new object[] { id }, cancellationToken)
            ?? throw new EntityNotFoundException("Order", id);

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync(cancellationToken);
    }
}