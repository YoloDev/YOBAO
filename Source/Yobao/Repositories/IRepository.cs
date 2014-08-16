namespace Yobao.Repositories {
	using System.Linq;
	public interface IRepository<T> {
		IQueryable<T> All();
		T SaveOrUpdate(T item);
	}
}