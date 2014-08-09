using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Yobao
{
    public class Yobao
    {

        public ICollection<DataConfiguration<SampleDatabase, Car>> Configurations { get; private set; } //todo: fix visibility

        public Yobao()
        {
            Configurations = new Collection<DataConfiguration<SampleDatabase, Car>>();
        }

        public void Register<T, TResult>(Func<SampleDatabase, IQueryable<Car>> query) where T : class where TResult : class
        {

            var database = new SampleDatabase(); //TODO: remove this from here.

            var dataConfiguration = new DataConfiguration<SampleDatabase, Car>
            {
                Name = typeof(TResult).Name, // could swap this out for something we define
                ElementType = typeof(TResult),
                DataProviderType = typeof(T),
                Query = query.Invoke(database)
                //Query = query //how do we then use this?
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

    public class DataConfiguration<T, TResult> : DataConfig
    {
        public Func<SampleDatabase, IQueryable<Car>> Test { get; set; }
        public IQueryable<Car> Query { get; set; }

    }

    public static class TConverter
    {
        public static T ChangeType<T>(object value)
        {
            return (T)ChangeType(typeof(T), value);
        }
        public static object ChangeType(Type t, object value)
        {
            TypeConverter tc = TypeDescriptor.GetConverter(t);
            return tc.ConvertFrom(value);
        }
        public static void RegisterTypeConverter<T, TC>() where TC : TypeConverter
        {

            TypeDescriptor.AddAttributes(typeof(T), new TypeConverterAttribute(typeof(TC)));
        }
    }

}