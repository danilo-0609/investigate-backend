namespace BuildingBlocks.Domain.Entities;

public abstract record EntityId<TId>
    where TId : class
{
    public abstract TId Value { get; protected set; }

    protected EntityId(TId value)
    {
        Value = value;
    }

    protected EntityId() { }
}
