namespace Yobao {
	using Nancy;
	using Nancy.TinyIoc;
	public class YobaoNancyBootstrapper : DefaultNancyBootstrapper {
		IDataSource _DataSource;

		//placeholder for application container setup (if needed, when needed)
		protected override void ConfigureApplicationContainer(TinyIoCContainer container) {
			base.ConfigureApplicationContainer(container);

			container.Register<Repositories.Fakes.FakeBoatRepository>().AsSingleton();
			container.Register<Repositories.Fakes.FakeCarRepository>().AsSingleton();

			var db = container.Resolve<SampleDatabase>();
			var yobao = new Yobao<SampleDatabase>(db);
			yobao.Register(x => x.Cars);
			yobao.Register(x => x.Boats);
			_DataSource = (IDataSource)yobao;
			container.Register<IDataSource>(_DataSource);
		}

		//placeholder for request specific container setup (if needed, when needed)
		protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context) {
			base.ConfigureRequestContainer(container, context);
		}
	}
}