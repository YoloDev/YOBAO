namespace Yobao {
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	public abstract class ModelRegistrar : IDataSource, IDataPersistence {
		protected ICollection<DataConfiguration> Configurations { get; private set; }

		public ModelRegistrar() {
			Configurations = new Collection<DataConfiguration>();
		}

		public IEnumerable<NavigationItem> GetMenu() {
			return Configurations.Select(x => new NavigationItem { Name = x.Name, Url = string.Format("/{0}/List", x.Name) });
		}

		public IQueryable<object> GetQueryable(string typeName) {
			return Configurations.First(x => string.Equals(x.Name, typeName, StringComparison.InvariantCultureIgnoreCase)).Query.Invoke().Cast<object>();
		}

		public Type ResolveType(string typeName) {
			return Configurations.First(x => string.Equals(x.Name, typeName, StringComparison.InvariantCultureIgnoreCase)).ElementType;
		}

		public object Load(string typeName, object id) {
			return Configurations.First(x => string.Equals(x.Name, typeName, StringComparison.InvariantCultureIgnoreCase)).Load(id);
		}

		protected DataConfiguration Register<T>(Func<IQueryable> getAll, Func<object, object> getObject) {
			var config = new DataConfiguration {
				Name = typeof(T).Name,
				ElementType = typeof(T),
				Load = getObject,
				Query = getAll
			};
			Configurations.Add(config);
			return config;
		}

		public abstract object Persist(object item);
	}
}