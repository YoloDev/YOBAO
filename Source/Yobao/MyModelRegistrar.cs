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
		IRepository<Category> _CategoryRepository;
		IRepository<Item> _ItemRepository;
		
		public MyModelRegistrar(IRepository<Car> carRepository, IRepository<Boat> boatRepository, IRepository<Category> categoryRepository, IRepository<Item> itemRepository) : base() {
			_CarRepository = carRepository;
			_BoatRepository = boatRepository;
			_CategoryRepository = categoryRepository;
			_ItemRepository = itemRepository;

			Register<Car>(() => _CarRepository.All(), (object id) => _CarRepository.Get(id));
			Register<Boat>(() => _BoatRepository.All(), (object id) => _BoatRepository.Get(id));

			Register<Category>(() => _CategoryRepository.All(), (object id) => _CategoryRepository.Get(id));
			
			var itemReg = Register<Item>(() => _ItemRepository.All(), (object id) => _ItemRepository.Get(id));
			itemReg.ForProperty<Item>(x => x.CategoryId)
				.AsLookup(_CategoryRepository.All(), x => x.Id, y => y.Description);
				//.DisplayAs(_CategoryRepository.All(), (x) => x.Id );
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