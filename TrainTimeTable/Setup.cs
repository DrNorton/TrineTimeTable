﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.WindowsCommon.Platform;
using SQLite.Net.Interop;
using SQLite.Net.Platform.WinRT;
using TrainTimeTable.LocalEntities;
using TrainTimeTable.LocalEntities.Repositories;
using TrainTimeTable.Services;
using TrainTimeTable.Shared;
using TrainTimeTable.Shared.Models;
using TrainTimeTable.Shared.Services;
using TrainTimeTable.Shared.ViewModels;
using TrainTimeTable.Shared.ViewModels.Auth;
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
      
            RegisterDatabaseServices();
            return mainSetup;
        }

        private void RegisterDatabaseServices()
        {
            var initBase = new DatabaseInitializator() { Path = Path.Combine(ApplicationData.Current.LocalFolder.Path, "local.db") };
            Mvx.RegisterSingleton<DatabaseInitializator>(initBase);
            Mvx.RegisterSingleton<ISQLitePlatform>(new SQLitePlatformWinRT());
            Mvx.ConstructAndRegisterSingleton<SqliteContext,SqliteContext>();
            Mvx.RegisterType<IStationRepository, StationRepository>();
            Mvx.RegisterType<IFavoriteTrainRepository, FavoriteTrainRepository>();
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
                    Background = "#0f5757",
                    ViewModelType= typeof(MainViewModel)
                },
                new NavMenuItem()
                {
                    
                    Symbol = (int)Symbol.Calendar,
                    Label = "Расписание",
                    Background = "#00FFFFFFF",
                    ViewModelType= typeof(SheduleWizardViewModel)
                },
                 new NavMenuItem()
                {

                    Symbol = (int)Symbol.Contact,
                    Label = "Авторизация",
                    Background = "#00FFFFFFF",
                    ViewModelType= typeof(AuthViewModel)
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
