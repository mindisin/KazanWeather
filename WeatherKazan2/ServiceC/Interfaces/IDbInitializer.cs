namespace ServiceC.Interfaces
{
    public interface IDbInitializer
    {
        Task InitializeAsync(CancellationToken cancellationToken);
    }
}
