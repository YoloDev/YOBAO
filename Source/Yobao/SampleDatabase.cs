namespace Yobao {
	using System;
	using System.Linq;
	using System.Reflection;
	using Yobao.Models;
	using Yobao.Repositories;
	using Yobao.Repositories.Fakes;
	public class SampleDatabase : IDataPersistence {
		IRepository<Car> _CarRepository;
		IRepository<Boat> _BoatRepository;

		public SampleDatabase(FakeCarRepository carRepository, FakeBoatRepository boatRepository) {
			_CarRepository = carRepository;
			_BoatRepository = boatRepository;
		}

		public IRepository<Car> Cars {
			get { return _CarRepository; }
		}
		public IRepository<Boat> Boats {
			get { return _BoatRepository; }
		}

		public object Persist(object item) {
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