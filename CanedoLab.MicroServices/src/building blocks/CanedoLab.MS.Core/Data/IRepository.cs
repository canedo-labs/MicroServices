using CanedoLab.MS.Core.DomainObjects;
using System;

namespace CanedoLab.MS.Core.Data
{
    public interface IRepository<TAggregate> : IDisposable
        where TAggregate : IAggregateRoot
    {

    }
}
