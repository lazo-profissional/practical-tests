namespace GoodHamburger.Domain.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string message) : base(message)
    {
    }

    public EntityNotFoundException(string entityName, int id)
        : base($"{entityName} with ID {id} not found.")
    {
    }
}