namespace Yobao {
	using Nancy;
	using Nancy.ModelBinding;

	using System;
	
	public class YobaoStorageModule : NancyModule {
		public YobaoStorageModule(IDataSource yobao, IDataPersistence dataPersistence) {
			Post["/{type}/create"] = _ => {
				var formType = yobao.ResolveType((string)_.type);
				var formObj = Activator.CreateInstance(formType);
				this.BindTo(formObj);
				dataPersistence.Persist(formObj);
				return Response.AsRedirect(string.Format("/{0}/list", (string)_.type));
			};
			Put["/{type}/edit/{id}"] = _ => {
				var formObj = yobao.Load((string)_.type, (object)_.id);
				this.BindTo(formObj);
				dataPersistence.Persist(formObj);
				return Response.AsRedirect(string.Format("/{0}/list", (string)_.type));
			};
		}
	}
}