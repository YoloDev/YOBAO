using System.Linq;
using Newtonsoft.Json;

namespace Yobao
{
    public class YobaoModule : Nancy.NancyModule
    {
        public YobaoModule(Yobao yobao)
        {
            Get["/"] = _ =>
            {
                return JsonConvert.SerializeObject(yobao.Configurations.Select(x => x.Name).ToList());
            };

            Get["/{type}/list"] = _ =>
            {
                //ick..
                var config = yobao.Configurations.Single(x => x.Name == _.type);

                //now with the config for this "type", how can we build an IQueryable to access it?
                //we know we have the type that the query is from, and we have the func to run it..

                var result = config.Query.ToList();

                return JsonConvert.SerializeObject(result);
            };
        }
    }
}