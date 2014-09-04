namespace Yobao {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	public interface IModelRegistrar : IDataSource, IDataPersistence {
		IEnumerable<NavigationItem> GetMenu();
		IQueryable<object> GetQueryable(string typeName);
		object Load(string typeName, object id);
		object Persist(object item);
		Type ResolveType(string typeName);
	}
}
