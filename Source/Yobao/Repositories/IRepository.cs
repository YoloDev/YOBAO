namespace Yobao.Repositories {
	using System.Linq;
	public interface IRepository<T> {
		IQueryable<T> All();
		T Get(object id);
		T SaveOrUpdate(T item);
	}
}