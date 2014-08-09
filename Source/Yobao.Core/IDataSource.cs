namespace Yobao {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    public interface IDataSource {
        IEnumerable<NavigationItem> GetMenu();
        IQueryable<object> GetQueryable(string typeName);
    }
}