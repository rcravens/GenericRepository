using System;

namespace Repository.Infrastructure
{
    public interface IKeyed<TKey>
    {
        TKey Id { get; }
    }

    //public interface IKeyed
    //{
    //    object Id { get; }
    //}

    public interface IGuidKeyed : IKeyed<Guid>
    {
        new Guid Id { get; }
    }

    public interface IIntKeyed : IKeyed<int>
    {
        new int Id { get; }
    }
}