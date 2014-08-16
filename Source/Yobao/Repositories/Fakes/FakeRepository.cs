namespace Yobao.Repositories.Fakes {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	public abstract class FakeRepository<T> : IRepository<T> {
		protected List<T> _Items = new List<T>();

		public IQueryable<T> All() {
			return new EnumerableQuery<T>(_Items);
		}
		public abstract T Get(object id);
		protected abstract void SetKey(T item);
		protected abstract T FindByKey(T item);

		public T SaveOrUpdate(T item) {
			var existing = FindByKey(item);
			if (existing == null) {
				SetKey(item);
			} else {
				_Items.Remove(existing);
			}
			_Items.Add(item);
			return item;
		}
	}
}