using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.MvvmCross.ViewModels;
using TrainTimeTable.Shared.ViewModels.Base;

namespace TrainTimeTable.Shared.ViewModels.Auth
{
    public class AuthViewModel:LoadingScreen
    {
        public MvxCommand RegisterCommand { get; set; }
        public MvxCommand RecoverCommand { get; set; }

        public AuthViewModel()
            :base("Авторизация")
        {
            RegisterCommand=new MvxCommand(Register);
            RecoverCommand=new MvxCommand(Recover);
        }

        private void Recover()
        {
            ShowViewModel<RecoverViewModel>();
        }

        private void Register()
        {
            ShowViewModel<RegisterViewModel>();
            
        }
    }
}
