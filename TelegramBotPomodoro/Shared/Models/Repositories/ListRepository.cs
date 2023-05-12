namespace Shared.Models.Repositories
{
    public class ListRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        private List<T> items = new List<T>();

        public IQueryable<T> Items => items.AsQueryable();

        public T Add(T item)
        {
            items.Add(item);
            return item;
        }

        public Task<T> AddAsync(T item, CancellationToken cancel = default)
        {
            return Task.FromResult( Add(item) );
        }

        public void AddRange(IEnumerable<T> items)
        {
            items.Concat( items );
        }

        public Task AddRangeAsync(IEnumerable<T> items)
        {
            AddRange(items);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            items.Clear();
        }

        public T Get(int id)
        {
            return Items.FirstOrDefault(s => s.Id == id);
        }

        public Task<T> GetAsync(int id, CancellationToken cancel = default)
        {
            return Task.FromResult(Get(id) );
        }

        public void Remove(int id)
        {
            items.Remove(Get(id));
        }

        public Task RemoveAsync(int id, CancellationToken cancel = default)
        {
            Remove(id);
            return Task.CompletedTask;
        }

        public void Update(T item)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T item, CancellationToken cancel = default)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(IEnumerable<T> items)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRangeAsync(IEnumerable<T> items)
        {
            throw new NotImplementedException();
        }
    }
}
