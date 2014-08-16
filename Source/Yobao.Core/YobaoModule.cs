namespace Yobao {
	using Nancy;

	using System;
	using System.Linq;

	public class YobaoModule : NancyModule {
		public YobaoModule(IDataSource yobao) // push this up to module creation... some how..
		{
			Get["/"] = _ => {
				return yobao.GetMenu();
			};

			Get["/{type}/list"] = _ => {
				var queryable = yobao.GetQueryable((string)_.type); //todo can we get strongly typed params?
				var result = queryable.ToList();
				return result;
			};

			// create an object.
			Get["/{type}/create"] = _ => {
				var formType = yobao.ResolveType((string)_.type);
				var formObj = Activator.CreateInstance(formType);
				return formObj;
			};

			// edit an object.
			Get["/{type}/edit/{id}"] = _ => {
				return yobao.Load((string)_.type, (object)_.id);
			};

			// create an object.
			Get["/{type}/create"] = _ => {
				var formType = yobao.ResolveType((string)_.type);
				var formObj = Activator.CreateInstance(formType);
				return formObj;
			};

			// edit an object.
			Get["/{type}/edit/{id}"] = _ => {
				return yobao.Load((string)_.type, (object)_.id);
			};
		}
	}
}