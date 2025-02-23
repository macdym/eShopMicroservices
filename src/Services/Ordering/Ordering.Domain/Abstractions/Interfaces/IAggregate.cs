namespace Ordering.Domain.Abstractions.Interfaces
{
    public interface IAggregate<T> : IAggregate, IEntity<T>
    {

    }

    public interface IAggregate : IEntity
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }
        IDomainEvent[] ClearDomainEvents();
    }
}
