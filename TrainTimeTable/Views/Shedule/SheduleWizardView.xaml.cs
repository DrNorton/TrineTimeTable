using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Cirrious.MvvmCross.WindowsCommon.Views;
using TrainTimeTable.ApiClient.Response;
using TrainTimeTable.Shared.ViewModels.Shedule;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TrainTimeTable.Views.Shedule
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SheduleWizardView : MvxWindowsPage
    {
        public SheduleWizardView()
        {
            this.InitializeComponent();
        }

        private void ToStationBox_OnSuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            (this.DataContext as SheduleWizardViewModel).ToStation = args.SelectedItem as StationResponse;
        }

        private void FromStationBox_OnSuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            (this.DataContext as SheduleWizardViewModel).FromStation = args.SelectedItem as StationResponse;
        }
    }
}
