namespace FS.Store.WebAPI.Services.Interface
{
    public interface ICurrentCashSessionService
    {
        long? Id { get; }

        Task<long?> GetOrResolveAsync();

        void Set(long id);

        void Clear();
    }
}
