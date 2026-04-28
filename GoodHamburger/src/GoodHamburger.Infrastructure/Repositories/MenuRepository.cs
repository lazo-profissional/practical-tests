using GoodHamburger.Domain.Interfaces;
using GoodHamburger.Domain.Models;

namespace GoodHamburger.Infrastructure.Repositories;

public class MenuRepository : IMenuRepository
{
    public Task<List<MenuItem>> GetMenuAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(MenuItem.GetFullMenu());
    }

    public Task<MenuItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var item = MenuItem.GetFullMenu().FirstOrDefault(m => m.Id == id);
        return Task.FromResult(item);
    }
}