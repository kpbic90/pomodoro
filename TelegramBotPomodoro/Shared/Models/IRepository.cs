namespace Shared.Models
{
    public interface IRepository<T> : IDisposable where T : class, IEntity, new()
    {
        IQueryable<T> Items { get; }
        T Get(int id);
        Task<T> GetAsync(int id, CancellationToken cancel = default);
        T Add(T item);
        void AddRange(IEnumerable<T> items);
        Task AddRangeAsync(IEnumerable<T> items);
        Task<T> AddAsync(T item, CancellationToken cancel = default);
        void Update(T item);
        Task UpdateAsync(T item, CancellationToken cancel = default);
        void UpdateRange(IEnumerable<T> items);
        Task UpdateRangeAsync(IEnumerable<T> items);
        void Remove(int id);
        Task RemoveAsync(int id, CancellationToken cancel = default);
    }
}
