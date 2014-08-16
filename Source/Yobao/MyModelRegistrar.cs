namespace Yobao {
	using System;
	using System.Linq;
	using System.Reflection;

	using Yobao.Models;
	using Yobao.Repositories;
	using Yobao.Repositories.Fakes;
	
	public class MyModelRegistrar : ModelRegistrar {
		IRepository<Car> _CarRepository;
		IRepository<Boat> _BoatRepository;
		public MyModelRegistrar(FakeCarRepository carRepository, FakeBoatRepository boatRepository)
			: base() {
			_CarRepository = carRepository;
			_BoatRepository = boatRepository;

			Register<Car>(() => _CarRepository.All(), (object id) => _CarRepository.Get(id));
			Register<Boat>(() => _BoatRepository.All(), (object id) => _BoatRepository.Get(id));
		}

		public override object Persist(object item) {
			// Not sure if this would be here.
			// if we would be using NHibernate, I would have this call ISession.SaveOrUpdate(item);
			Type itemType = item.GetType();
			Type typeToFind = typeof(IRepository<>).MakeGenericType(itemType);

			var fieldsList = this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
			var list = fieldsList.First(t => t.FieldType == typeToFind).GetValue(this);

			var methodAdd = list.GetType().GetMethod("SaveOrUpdate");
			methodAdd.Invoke(list, new object[] { item });

			return item;
		}
	}
}