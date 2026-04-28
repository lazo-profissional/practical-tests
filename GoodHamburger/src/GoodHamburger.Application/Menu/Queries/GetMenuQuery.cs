using MediatR;
using GoodHamburger.Application.DTOs;

namespace GoodHamburger.Application.Menu.Queries;

public record GetMenuQuery : IRequest<List<MenuItemDto>>;