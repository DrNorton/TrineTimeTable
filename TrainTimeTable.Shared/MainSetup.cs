using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using SQLite.Net.Interop;
using TrainTimeTable.ApiClient;
using TrainTimeTable.ApiClient.Executer;
using TrainTimeTable.ApiClient.Facade;
using TrainTimeTable.Shared.Services;
using TrainTimeTable.Shared.ViewModels;
using TrainTimeTable.Shared.ViewModels.Map;
using TrainTimeTable.Shared.ViewModels.Shedule;

namespace TrainTimeTable.Shared
{
    public class MainSetup:MvxApplication
    {
        public override void Initialize()
        {
            RegisterViewModels();
            RegisterApi();
            RegisterAppStart(new CustomAppStart());
            Cirrious.MvvmCross.Plugins.Messenger.PluginLoader.Instance.EnsureLoaded();
        }

       

        private static void RegisterViewModels()
        {
            Mvx.RegisterType<ShellViewModel, ShellViewModel>();
            Mvx.RegisterType<MainViewModel, MainViewModel>();
            Mvx.RegisterType<SheduleWizardViewModel, SheduleWizardViewModel>();
            Mvx.RegisterType<MapViewModel, MapViewModel>();
        }

        private static void RegisterApi()
        {
            Mvx.RegisterType<IApiSettings, Settings>();
            Mvx.RegisterType<IApiExecuter, ApiExecuter>();
            Mvx.RegisterType<IApiFacade, ApiFacade>();
        }
    }
}
