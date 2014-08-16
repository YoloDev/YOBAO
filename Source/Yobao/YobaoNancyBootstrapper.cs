namespace Yobao {
	using Nancy;
	using Nancy.TinyIoc;
	using Yobao.Models;
	using Yobao.Repositories;
	using Yobao.Repositories.Fakes;
	public class YobaoNancyBootstrapper : DefaultNancyBootstrapper {
		//placeholder for application container setup (if needed, when needed)
		protected override void ConfigureApplicationContainer(TinyIoCContainer container) {
			base.ConfigureApplicationContainer(container);

			container.Register<IRepository<Boat>, FakeBoatRepository>().AsSingleton();
			container.Register<IRepository<Car>, FakeCarRepository>().AsSingleton();

			var modelRegistrar = container.Resolve<MyModelRegistrar>();

			container.Register<IDataSource, MyModelRegistrar>(modelRegistrar);
			container.Register<IDataPersistence, MyModelRegistrar>(modelRegistrar);
		}

		//placeholder for request specific container setup (if needed, when needed)
		protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context) {
			base.ConfigureRequestContainer(container, context);
		}
	}
}