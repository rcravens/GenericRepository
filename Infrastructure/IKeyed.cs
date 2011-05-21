using System;

namespace Repository.Infrastructure
{
    public interface IKeyed<TKey>
    {
        TKey Id { get; }
    }
}