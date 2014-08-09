namespace Yobao {
	using Nancy;
	using Newtonsoft.Json;
	using System.Linq;
	public class YobaoModule : Nancy.NancyModule {
		public YobaoModule(IDataSource yobao) // push this up to module creation... some how..
		{
			Get["/"] = _ => {
				return JsonConvert.SerializeObject(yobao.GetMenu());
			};

			Get["/{type}/list"] = _ => {
				//ick..

				var queryable = yobao.GetQueryable((string)_.type); //todo can we get strongly typed params?

				var result = queryable.ToList();
				//now with the config for this "type", how can we build an IQueryable to access it?
				//we know we have the type that the query is from, and we have the func to run it..

				//var result = config.Query.ToList();
				return Response.AsJson(result);
			};
		}
	}
}