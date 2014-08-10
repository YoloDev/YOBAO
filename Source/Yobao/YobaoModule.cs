using System.Linq;
using Nancy;

namespace Yobao
{
    public class YobaoModule : Nancy.NancyModule 
    {
        public YobaoModule(IYobao<SampleDatabase> yobao) 
        {
            Get["/"] = _ =>
            {
                return Response.AsJson(yobao.Configurations.ToList());
            };

            Get["/{type}/list"] = _ =>
            {
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