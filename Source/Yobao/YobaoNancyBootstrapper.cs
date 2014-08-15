namespace Yobao {
	using Nancy;
	using Nancy.TinyIoc;
	public class YobaoNancyBootstrapper : DefaultNancyBootstrapper {
		IDataSource _DataSource;
		public YobaoNancyBootstrapper() : base() {
			_DataSource = new Yobao<SampleDatabase>(new SampleDatabase());
		}

		//placeholder for application container setup (if needed, when needed)
		protected override void ConfigureApplicationContainer(TinyIoCContainer container) {
			base.ConfigureApplicationContainer(container);
		}

		//placeholder for request specific container setup (if needed, when needed)
		protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context) {
			base.ConfigureRequestContainer(container, context);

			container.Register<IDataSource>(_DataSource);
		}
	}
}