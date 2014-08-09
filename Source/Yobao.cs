using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Yobao
{
    public class Yobao
    {

        public ICollection<DataConfig> Configurations { get; private set; } //todo: fix visibility

        public Yobao()
        {
            Configurations = new Collection<DataConfig>();
        }

        public void Register<T, TResult>(Func<T, IQueryable<TResult>> query) where TResult : class
        {
            //how do we register the query?

            var dataConfiguration = new DataConfiguration<T, TResult>
            {
                Name = typeof(TResult).Name, // could swap this out for something we define
                ElementType = typeof(TResult),
                DataProviderType = typeof(T),
                Query = query
            };

            Configurations.Add(dataConfiguration);

        }
    }

    public abstract class DataConfig
    {
        public string Name { get; set; }

        public bool VisibleInMenu { get; set; }

        public Type ElementType { get; set; }
        public Type DataProviderType { get; set; }

        //abstract public object Query { get; set; }
    }

    public class DataConfiguration<T, TResult> : DataConfig, IQueryMe<T, TResult>
    {
        public Func<T, IQueryable<TResult>> Query { get; set; }

    }

    public interface IQueryMe<T, TResult>
    {
        Func<T, IQueryable<TResult>> Query { get; set; }
    }

}