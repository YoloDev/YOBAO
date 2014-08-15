namespace Yobao {
    using System;
    using System.Linq;
	public class DataConfiguration<T, TResult> where TResult : class {
		public string Name { get; set; }

		public bool VisibleInMenu { get; set; }

		public Type ElementType { get; set; }
		public Type DataProviderType { get; set; }

		public T DataProvider { get; set; }
		public IQueryable<TResult> Query { get; set; }
	}
}