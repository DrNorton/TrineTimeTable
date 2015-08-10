using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using TrainTimeTable.Shared.ViewModels;
using TrainTimeTable.Shared.ViewModels.Favorites;

namespace TrainTimeTable.Shared
{
    public class MainSetup:MvxApplication
    {
        public override void Initialize()
        {
            Mvx.RegisterType<ShellViewModel, ShellViewModel>();
            Mvx.RegisterType<AddToFavoritesViewModel, AddToFavoritesViewModel>();
            Mvx.RegisterType<MainViewModel, MainViewModel>();
            RegisterAppStart(new CustomAppStart());
        }
    }
}
