using Castle.MicroKernel.Registration;
using TrainTimeTable.Api.Controllers.Services;
using TrainTimeTable.Api.Controllers.Settings;

namespace TrainTimeTable.Api.Dependencies.Installers
{
    public class ServiceInstaller:IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Component.For<ISettings, Settings>().LifestyleTransient());
            container.Register(Component.For<IYandexApiService, YandexApiService>().LifestyleTransient());
        }
    }
}
