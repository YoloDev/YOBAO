namespace Yobao {
    using System;
    using System.Linq;
	public class DataConfiguration {
		public string Name { get; set; }
		public bool VisibleInMenu { get; set; }

		public Type ElementType { get; set; }

		public Func<IQueryable> Query { get; set; }
		public Func<object, object> Load { get; set; }
	}
}