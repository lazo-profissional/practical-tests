using MediatR;
using GoodHamburger.Application.DTOs;
using GoodHamburger.Application.Mapping;
using GoodHamburger.Domain.Interfaces;

namespace GoodHamburger.Application.Menu.Queries;

public class GetMenuQueryHandler : IRequestHandler<GetMenuQuery, List<MenuItemDto>>
{
    private readonly IMenuRepository _menuRepository;

    public GetMenuQueryHandler(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<List<MenuItemDto>> Handle(GetMenuQuery request, CancellationToken cancellationToken)
    {
        var menuItems = await _menuRepository.GetMenuAsync(cancellationToken);
        return menuItems.Select(m => m.ToDto()).ToList();
    }
}