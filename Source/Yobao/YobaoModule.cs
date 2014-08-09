namespace Yobao {
    using Nancy;
    using Nancy.ModelBinding;

    using Newtonsoft.Json;
    
    using System;
    using System.Linq;
    
    public class YobaoModule : NancyModule {
        public YobaoModule(IDataSource yobao) // push this up to module creation... some how..
        {
            Get["/"] = _ => {
                return JsonConvert.SerializeObject(yobao.GetMenu());
            };

            Get["/{type}/list"] = _ => {
				var queryable = yobao.GetQueryable((string)_.type); //todo can we get strongly typed params?
				var result = queryable.ToList();
				//now with the config for this "type", how can we build an IQueryable to access it?
				//we know we have the type that the query is from, and we have the func to run it..

				return Response.AsJson(result);
            };

            // create an object.
            Get["/{type}/create"] = _ => {
                var formType = yobao.ResolveType((string)_.type);
                var formObj = Activator.CreateInstance(formType);
                return formObj;
            };
            Post["/{type}/create"] = _ => {

                var formType = yobao.ResolveType((string)_.type);
                var formObj = Activator.CreateInstance(formType);

                this.BindTo(formObj);
                yobao.Store((string)_.type, formObj);

                return Response.AsRedirect(string.Format("/{0}/list", (string)_.type));
            };

            // edit an object.
            Get["/{type}/edit/{id}"] = _ => {
                return yobao.Load((string)_.type, (object)_.id);
            };
            Put["/{type}/edit/{id}"] = _ => {
                var formObj = yobao.Load((string)_.type, (object)_.id);
                this.BindTo(formObj);
                yobao.Store((string)_.type, formObj);
                return Response.AsRedirect(string.Format("/{0}/list", (string)_.type));
            };
        }
    }
}