using System.Collections.Generic;
using System.Linq;

namespace Yobao
{
    public class SampleDatabase
    {
        public IQueryable<Car> Cars
        {
            get
            {
                var cars = new List<Car>
                {
                    new Car {Id = 1, Name = "Corolla"},
                    new Car {Id = 2, Name = "Tarago"}
                };
                return new EnumerableQuery<Car>(cars);
            }
        }
    }

    public class Car
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

}