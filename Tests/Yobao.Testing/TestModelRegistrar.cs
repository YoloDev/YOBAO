namespace Yobao.Testing {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	public class TestModelRegistrar : ModelRegistrar {
		public override object Persist(object item) {
			throw new NotImplementedException();
		}
	}
}