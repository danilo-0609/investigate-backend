﻿using BuildingBlocks.Domain.Events;
using BuildingBlocks.Domain.Rules;
using ErrorOr;
using MediatR;

namespace BuildingBlocks.Domain.Entities;

public abstract class Entity<TId, TIdType> : IEquatable<Entity<TId, TIdType>>, IHasDomainEvents, IEntity, ICheckRule
    where TId : EntityId<TIdType>
    where TIdType : class
{
    
    private readonly List<IDomainEvent> _domainEvents = new();

    public TId Id { get; }

    protected Entity(TId id)
    {
        Id = id;
    }

    //Domain events concerns
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();

    public IReadOnlyList<IDomainEvent> GetDomainEvents() => _domainEvents;

    protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);


    //Business rules validation
    public ErrorOr<Unit> CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
        {
            return rule.Error;
        }

        return Unit.Value;
    }


    //Equatable operations
    public bool Equals(Entity<TId, TIdType>? other)
    {
        return Equals((object?)other);
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity<TId, TIdType> entity && Id.Equals(entity.Id);
    }

    public static bool operator ==(Entity<TId, TIdType> left, Entity<TId, TIdType> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity<TId, TIdType> left, Entity<TId, TIdType> right)
    {
        return !Equals(left, right);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    protected Entity() { }
}
