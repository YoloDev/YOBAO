
using System;
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
                return JsonConvert.SerializeObject(yobao.Configurations.ToList());
            };

            Get["/{type}/list"] = _ =>
            {
                //ick..
                var config = yobao.Configurations.Single(x => x.Name == _.type);

                
                

                return JsonConvert.SerializeObject(config);
            };
        }
    }
}