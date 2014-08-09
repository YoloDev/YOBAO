using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;


namespace Yobao
{
    public class YobaoNancyBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            var yobao = new Yobao();

            //here, we register a class with iqueryables, a type, and the func to run to get the iqueryable...
            //should in theory also allow you to do things like x.Cars.Where(x => != x.Visible)
            yobao.Register<SampleDatabase, Car>(x => x.Cars);

            //add to the nancy ioc container
            container.Register(yobao);
        }
    }
}