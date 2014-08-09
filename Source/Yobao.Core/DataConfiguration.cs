namespace Yobao {
    using System;
    using System.Linq;
    public class DataConfiguration<T> {
        public string Name { get; set; }
        public bool VisibleInMenu { get; set; }
        public Type ElementType { get; set; }
        public Type DataProviderType { get; set; }
        public T DataProvider { get; set; }
        public IQueryable<T> Query { get; set; }
    }
}