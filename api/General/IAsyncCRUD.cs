namespace api.General
{
	public interface IAsyncCRUD<T>
	{
		Task<T> Create(T entity);
		Task Delete(T entity);
		Task<IEnumerable<T>> Get();
		Task<T?> Get(int id);
		Task Update(int id, T entity);
	}
}
