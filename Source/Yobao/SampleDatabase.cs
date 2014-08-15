namespace Yobao {
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;
	using System.Reflection;
	public class SampleDatabase : IDataPersistence {
		public SampleDatabase() {
			_Cars = new List<Car> {
                new Car {Id = 1, Name = "Corolla"},
                new Car {Id = 2, Name = "Tarago"}
            };
		}
		List<Car> _Cars;

		public IQueryable<Car> Cars {
			get { return new EnumerableQuery<Car>(_Cars); }
		}

		public object Persist(object item) {
			Type itemType = item.GetType();
			Type typeToFind = typeof(List<>).MakeGenericType(itemType);

			var fieldsList = this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
			var list = fieldsList.First(t => t.FieldType == typeToFind).GetValue(this);

			var methodAdd = list.GetType().GetMethod("Add");
			methodAdd.Invoke(list, new object[] { item });

			return item;
		}
	}

	public class Car {
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
	}
}