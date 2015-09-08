using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TrainTimeTable.Api.Dao.Repositories;
using TrainTimeTable.Api.EfDao;
using TrainTimeTable.Api.EfDao.Repositories;

namespace TrainTimeTable.Api.Dependencies.Installers
{
    public class RepositoriesInstaller: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<traintimetable_dbEntities>().LifestyleTransient());
            container.Register(Component.For<IStationRepository, StationRepository>().LifestyleTransient());
        }
    }
}
