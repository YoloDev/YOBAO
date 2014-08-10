using System;
using System.Linq;

namespace Yobao
{
    public interface ISampleDatabase
    {
        IQueryable<Car> Cars { get; }
    }
}
