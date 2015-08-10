using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using TrainTimeTable.Shared.ViewModels.Base;

namespace TrainTimeTable.Shared.ViewModels
{
    public class ShellViewModel : LoadingScreen
    {
        private ICommand _goHomeCommand;
        private ICommand _goToGroupsCommand;

        public ICommand GoHomeCommand
        {
            get
            {
                _goHomeCommand = _goHomeCommand ?? new MvxCommand(GoHome);
                return _goHomeCommand;
            }
        }





        public ICommand GoToGroupsCommand
        {
            get
            {
                _goToGroupsCommand = _goToGroupsCommand ?? new MvxCommand(GoToGroups);
                return _goToGroupsCommand;
            }
        }

        private void GoToGroups()
        {
          //  ShowViewModel<GroupsViewModel>();
        }

        private void GoHome()
        {
            ShowViewModel<MainViewModel>();
        }

        public ShellViewModel()
        {
            Debug.WriteLine("dad");
        }
    }
}
