using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;


namespace Yobao
{
    public class YobaoNancyBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            var database = new SampleDatabase();

            var yobao = new Yobao<SampleDatabase>(database); //todo: we probably need to provide a factory or similar (or let ioc sort it out)

            //here, we register a class with iqueryables, a type, and the func to run to get the iqueryable...
            //should in theory also allow you to do things like x.Cars.Where(x => != x.Visible)
            yobao.Register(x => x.Cars);

            //add to the nancy ioc container
            container.Register(yobao);
        }

        
    }
}