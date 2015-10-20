using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using TrainTimeTable.Shared.Models;
using TrainTimeTable.Shared.ViewModels.Base;
using TrainTimeTable.Shared.ViewModels.Shedule;

namespace TrainTimeTable.Shared.ViewModels
{
    public class ShellViewModel : MvxViewModel
    {
        private  List<NavMenuItem> _menu;
        private NavMenuItem _selectedItem;
        private ICommand _goHomeCommand;
        private ICommand _goToSheduleCommand;
        private ICommand _goToMapCommand;
        public ICommand GoHomeCommand
        {
            get
            {
                _goHomeCommand = _goHomeCommand ?? new MvxCommand(GoHome);
                return _goHomeCommand;
            }
        }
        public ICommand GoToSheduleCommand
        {
            get
            {
                _goToSheduleCommand = _goToSheduleCommand ?? new MvxCommand(GoToShedule);
                return _goToSheduleCommand;
            }
        }

        public NavMenuItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                if(value!=null)
                  Navigate(value);
                base.RaisePropertyChanged(()=>SelectedItem);
            }
        }

        public List<NavMenuItem> Menu
        {
            get { return _menu; }
            set
            {
                _menu = value;
                base.RaisePropertyChanged(()=>Menu);
            }
        }

        private void Navigate(NavMenuItem value)
        {
            ShowViewModel(value.ViewModelType);
        }


        private void GoToShedule()
        {
            ShowViewModel<SheduleWizardViewModel>();
        }

        private void GoHome()
        {
            ShowViewModel<MainViewModel>();
        }

        public ShellViewModel(List<NavMenuItem> menu)
        {
            _menu = menu;
            Debug.WriteLine("dad");
        }
    }
}
