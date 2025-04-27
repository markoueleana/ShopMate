using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.DomainAbstraction;

public interface IAggreegate<T> : IAggregate, IEntity<T>
{ 

}
public interface IAggregate : IEntity
{

}
