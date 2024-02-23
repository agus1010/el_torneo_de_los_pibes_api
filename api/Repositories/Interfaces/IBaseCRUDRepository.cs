using System.Linq.Expressions;


namespace api.Repositories.Interfaces
{
	public interface IBaseCRUDRepository<T> where T : class
	{
		Task<T> Create(T entity);
		Task<IEnumerable<T>> ReadMany(Expression<Func<T, bool>>? filter = null, bool tracked = true);
		Task<T?> ReadSingle(Expression<Func<T, bool>>? filter = null, bool tracked = true);
		Task Update(T entity);
		Task Delete(T entity);
		Task Persist();
	}
}