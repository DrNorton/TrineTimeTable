using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.WindowsCommon.Platform;
using TrainTimeTable.Services;
using TrainTimeTable.Shared;
using TrainTimeTable.Shared.Models;
using TrainTimeTable.Shared.Services;
using TrainTimeTable.Shared.ViewModels;
using TrainTimeTable.Shared.ViewModels.Map;
using TrainTimeTable.Shared.ViewModels.Shedule;

namespace TrainTimeTable
{
    public class Setup : MvxWindowsSetup
    {
        public Setup(Frame rootFrame) : base(rootFrame)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            var mainSetup=new MainSetup();
            CreateMenu();
            RegisterServices();
            return mainSetup;
        }

        private void RegisterServices()
        {
            Mvx.RegisterType<IPositionReceiver, PositionReceiver>();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        private void CreateMenu()
        {
           var navlist = new List<NavMenuItem>(
           new[]
           {
               new NavMenuItem()
                {
                    Symbol = (int)Symbol.Home,
                    Label = "Главная",
                    ViewModelType= typeof(MainViewModel)
                },
                new NavMenuItem()
                {
                    
                    Symbol = (int)Symbol.Calendar,
                    Label = "Расписание",
                    ViewModelType= typeof(SheduleWizardViewModel)
                },
                new NavMenuItem()
                {
                    Symbol = (int)Symbol.Map,
                    Label = "Карта",
                    ViewModelType= typeof(MapViewModel)
                }
                
           });
            Mvx.RegisterSingleton(typeof(List<NavMenuItem>),navlist);
    }
    }

    public class DebugTrace : IMvxTrace
    {
        public void Trace(MvxTraceLevel level, string tag, Func<string> message)
        {
            Debug.WriteLine(tag + ":" + level + ":" + message());
        }

        public void Trace(MvxTraceLevel level, string tag, string message)
        {
            Debug.WriteLine(tag + ":" + level + ":" + message);
        }

        public void Trace(MvxTraceLevel level, string tag, string message, params object[] args)
        {
            try
            {
                Debug.WriteLine(string.Format(tag + ":" + level + ":" + message, args));
            }
            catch (FormatException)
            {
                Trace(MvxTraceLevel.Error, tag, "Exception during trace of {0} {1} {2}", level, message);
            }
        }
    }
}
