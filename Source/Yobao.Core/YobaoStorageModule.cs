namespace Yobao {
	using Nancy;
	using Nancy.ModelBinding;

	using System;
	using System.Linq;
	using System.IO;

	//object myModel = typeof(ModuleExtensions).GetMethods("MethodName", BindingStuff.Public and Static).Where(args is correct).MakeGeneric(type of model).Invoke(this)
	public class YobaoStorageModule : NancyModule {
		public YobaoStorageModule(IDataSource yobao, IDataPersistence dataPersistence) {
			Post["/{type}/create"] = _ => {
				var formType = yobao.ResolveType((string)_.type);
				string bodyString = string.Empty;
				using (var stream = new StreamReader(Request.Body)) {
					bodyString = stream.ReadToEnd();
				}

				var model = PullModel(formType);

				dataPersistence.Persist(model);
				return Response.AsRedirect(string.Format("/{0}/list", (string)_.type));
			};
			Post["/{type}/edit/{id}"] = _ => {
				var formObj = yobao.Load((string)_.type, (object)_.id);
				string bodyString = string.Empty;
				using (var stream = new StreamReader(Request.Body)) {
					bodyString = stream.ReadToEnd();
				}

				BindModel(formObj);
					
				dataPersistence.Persist(formObj);
				return Response.AsRedirect(string.Format("/{0}/list", (string)_.type));
			};
		}

		private object PullModel(Type type) {
			return typeof(ModuleExtensions)
					.GetMethods()
					.Where(m => m.Name == "Bind" && m.GetParameters().Length == 1 && m.GetParameters().All(x => x.ParameterType == typeof(INancyModule)))
					.First()
					.MakeGenericMethod(type)
					.Invoke(null, new object[] { this });
		}
		private void BindModel(object model){
			Type type = model.GetType();
			typeof(ModuleExtensions)
				.GetMethods()
				.Where(m => m.Name == "BindTo" && m.GetParameters().Length == 2 && m.GetParameters()[0].ParameterType == typeof(INancyModule))
				.First()
				.MakeGenericMethod(type)
				.Invoke(null, new object[] { this, model });
		}

		///// <summary>
		///// <remarks>
		///// <para>Nancy's this.BindTo({an object}) wasn't able to do the binding properly, since the variable is an `object` type, and not
		///// an actual type (like System.String, etc.).  We need to manually reflect the data coming in from the form post/put and
		///// populate the object.</para>
		///// <para>Would like to pose this to the Nancy team and get their feedback if there is a way to enhance the BindTo so that this
		///// method becomes irrelivant to the solution.</para>
		///// </remarks>
		///// </summary>
		///// <param name="toBind"></param>
		//private void Bind(object toBind) {
		//	foreach (var property in toBind.GetType().GetProperties()) {
		//		try {
		//			property.SetValue(toBind, Convert.ChangeType(Request.Form[property.Name], property.PropertyType));
		//		} catch (Exception) {
		//			// squelch for now.
		//		}
		//	}
		//}
	}
}