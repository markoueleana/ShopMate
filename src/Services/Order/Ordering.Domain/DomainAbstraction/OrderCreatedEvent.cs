using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ordering.Domain.Abstractions;
using Ordering.Domain.Entities;

namespace Ordering.Domain.DomainAbstraction;
public record OrderCreatedEvent(Order order) : IDomainEvent;
