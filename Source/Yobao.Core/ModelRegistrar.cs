namespace Yobao.Core {
	public abstract class ModelRegistrar {
		protected void Register<TModel>(TModel model);
		protected void Register<TModel>(TModel model, string path);
	}
}