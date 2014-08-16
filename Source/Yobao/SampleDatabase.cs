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
			_Boats = new List<Boat>{
				new Boat { Id = 1, Make = "Action Craft", Model = "Flatsmaster", Price = 999 },
				new Boat { Id = 2, Make = "Action Craft", Model = "Flyfisher", Price = 999 },
				new Boat { Id = 3, Make = "Action Craft", Model = "FlatsPro", Price = 999 },
			};
		}
		List<Car> _Cars;
		List<Boat> _Boats;

		public IQueryable<Car> Cars {
			get { return new EnumerableQuery<Car>(_Cars); }
		}
		public IQueryable<Boat> Boats {
			get { return new EnumerableQuery<Boat>(_Boats); }
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

	public class Boat {
		[Key]
		public int Id { get; set; }
		public string Make { get; set; }
		public string Model { get; set; }
		public decimal Price { get; set; }
	}
}