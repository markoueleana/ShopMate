using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.DomainAbstraction;

public class Aggregate<T> : Entity<T>, IAggreegate<T>
{
}
