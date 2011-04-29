namespace Repository.Infrastructure
{
    public interface IDbSessionFactory
    {
        IDbSession Create();
    }
}