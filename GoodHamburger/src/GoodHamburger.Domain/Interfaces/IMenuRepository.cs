using GoodHamburger.Domain.Models;

namespace GoodHamburger.Domain.Interfaces;

public interface IMenuRepository
{
    Task<List<MenuItem>> GetMenuAsync(CancellationToken cancellationToken = default);
    Task<MenuItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}