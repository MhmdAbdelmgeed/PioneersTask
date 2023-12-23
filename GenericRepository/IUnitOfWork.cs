namespace GenericRepository
{
    public interface IUnitOfWork<T> : IDisposable where T : class
    {
        IGenericRepository<T> Repository { get; }

        Task SaveAsync();

        void Save();

    }
}
