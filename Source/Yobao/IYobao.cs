using System;
using System.Collections.Generic;
using System.Linq;

namespace Yobao
{
    interface IYobao<T>
     where T : class
    {
        ICollection<DataConfiguration<T, object>> Configurations { get; }
        IQueryable<object> GetQueryable(string typeName);
        void Register<TResult>(Func<T, IQueryable<TResult>> query) where TResult : class;
    }
}
