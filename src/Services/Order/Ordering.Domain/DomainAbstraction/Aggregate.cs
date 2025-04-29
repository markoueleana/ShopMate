using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ordering.Domain.Abstractions;

namespace Ordering.Domain.DomainAbstraction;

public class Aggregate<T> : Entity<T>, IAggreegate<T>
{
    private readonly List<IDomainEvent> events = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => events.AsReadOnly();
    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        events.Add(domainEvent);
    }
}
