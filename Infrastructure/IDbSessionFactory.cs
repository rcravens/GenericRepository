namespace Repository.Infrastructure
{
    public interface IDbSessionFactory
    {
        IDbSession Create();
    }
    public interface IDbSessionGuidKeyedFactory
    {
        IDbSessionGuidKeyed Create();
    } 
    public interface IDbSessionIntKeyedFactory
    {
        IDbSessionIntKeyed Create();
    }
}