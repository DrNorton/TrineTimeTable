using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using TrainTimeTable.ApiClient.Response;
using TrainTimeTable.Shared.ViewModels.Shedule;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace TrainTimeTable.Controls
{
    public sealed partial class SubTrainPickerControl : UserControl
    {
        public SubTrainPickerControl()
        {
            this.InitializeComponent();
            this.Loaded += SheduleWizardView_Loaded;
        }

        private void SheduleWizardView_Loaded(object sender, RoutedEventArgs e)
        {
            var width = this.ActualWidth;
            if (width > 800)
            {
                var test = VisualStateManager.GoToState(this, "NarrowState", false);
            }
            else
            {
               var test= VisualStateManager.GoToState(this, "WideState", false);
            }


        }

        private static void LoadState(VisualStateChangedEventArgs e)
        {
            foreach (var sbase in e.NewState.Setters)
            {
                var setter = sbase as Setter;
                var spath = setter.Target.Path.Path;
                var element = setter.Target.Target as FrameworkElement;
                if (spath.Contains(nameof(RelativeSize)))
                {
                    string property = spath.Split('.').Last().TrimEnd(')');
                    var prop = typeof(RelativeSize).GetMethod($"Set{property}");
                    prop.Invoke(null, new object[] { element, setter.Value });
                }
            }
        }

        private void WindowSizeStates_OnCurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            LoadState(e);
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
