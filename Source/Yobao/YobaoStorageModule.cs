using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using System;

namespace Yobao {
    public class YobaoStorageModule : NancyModule {
        public YobaoStorageModule(IDataSource yobao, IDataPersistence dataPersistence) {
            Post["/{type}/create"] = _ => {

                var formType = yobao.ResolveType((string)_.type);
                var formObj = Activator.CreateInstance(formType);

                using (var stringReader = new System.IO.StreamReader(Request.Body)) {
                    var bodyContents = stringReader.ReadToEnd();
                    JsonConvert.PopulateObject(bodyContents, formObj);
                }

                
                dataPersistence.Persist(formObj);

                return Response.AsRedirect(string.Format("/{0}/list", (string)_.type));
            };
            Put["/{type}/edit/{id}"] = _ => {
                var formObj = yobao.Load((string)_.type, (object)_.id);
                using (var stringReader = new System.IO.StreamReader(Request.Body)) {
                    var bodyContents = stringReader.ReadToEnd();
                    JsonConvert.PopulateObject(bodyContents, formObj);
                }
                dataPersistence.Persist(formObj);
                return Response.AsRedirect(string.Format("/{0}/list", (string)_.type));
            };
        }
    }
}