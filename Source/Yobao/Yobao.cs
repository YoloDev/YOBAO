namespace Yobao {
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	public class Yobao<T> : IDataSource where T : class {
		private readonly T _datastore;

		public ICollection<DataConfiguration<T, object>> Configurations { get; private set; } //dynamic, the secret sauce?

		public Yobao(T datastore) {
			_datastore = datastore;
			Configurations = new Collection<DataConfiguration<T, object>>();
		}

		public object Load(string typeName, object id) {
			var thing = Configurations.First(x => string.Equals(x.Name, typeName, StringComparison.OrdinalIgnoreCase));
			throw new NotImplementedException();
		}
		public Type ResolveType(string typeName) {
			var thing = Configurations.First(x => string.Equals(x.Name, typeName, StringComparison.InvariantCultureIgnoreCase));
			if (thing != null) {
				return thing.ElementType;
			}
			return null;
		}
		public IEnumerable<NavigationItem> GetMenu() {
			return Configurations.Select(x => new NavigationItem { Name = x.Name, Url = string.Format("/{0}/List", x.Name) });
		}
		public IQueryable<object> GetQueryable(string typeName) {
			//we need to find the configs.. that have the matching name.. then we need to make a generic type... and return the queryable..
			//somehow..
			var thing = Configurations.First(x => string.Equals(x.Name, typeName, StringComparison.OrdinalIgnoreCase));
			return thing.Query;
		}

		public void Register<TResult>(Func<T, IQueryable<TResult>> query) where TResult : class {
			var dataConfiguration = new DataConfiguration<T, object> {
				Name = typeof(TResult).Name, // could swap this out for something we define
				ElementType = typeof(TResult),
				DataProviderType = typeof(T),
				DataProvider = _datastore,
				Query = query.Invoke(_datastore)
			};

			Configurations.Add(dataConfiguration);
		}
	}
}