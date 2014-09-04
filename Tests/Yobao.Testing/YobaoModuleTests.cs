namespace Yobao.Testing {
	using Nancy;
	using Nancy.Bootstrapper;
	using Nancy.Testing;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Xunit;
	public class YobaoModuleTests {
		INancyBootstrapper _Bootstrapper;
		Browser _Browser;
		public YobaoModuleTests() {
			_Bootstrapper = new DefaultNancyBootstrapper();
			_Browser = new Browser(_Bootstrapper);
		}
		[Fact(DisplayName = "An object that has been registered should allow the return of its list.")]
		public void ARegisteredObjectShouldReturnItsListOK() {
			var result = _Browser.Get("/Item/List", with => {
				with.HttpRequest();
			});

			Assert.Equal(HttpStatusCode.OK, result.StatusCode);
		}
	}
}
