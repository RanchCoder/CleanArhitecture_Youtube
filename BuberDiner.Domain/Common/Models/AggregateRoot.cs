namespace BuberDiner.Domain.Common.Models;

public abstract class AggregateRoot<Tid> : Entity<Tid> where Tid :notnull
{
    protected AggregateRoot(Tid id) : base(id)
    {
    }
}