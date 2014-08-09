namespace Yobao {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    public interface IDataSource {
        IEnumerable<NavigationItem> GetMenu();
        IQueryable<object> GetQueryable(string typeName);
        Type ResolveType(string typeName);
        void Store(string typeName, object toStore);
        object Load(string typeName, object id);
    }
}