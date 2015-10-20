using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Cirrious.MvvmCross.WindowsCommon.Views;
using TrainTimeTable.ApiClient.Response;
using TrainTimeTable.Controls;
using TrainTimeTable.Shared.ViewModels.Shedule;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TrainTimeTable.Views.Shedule
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SheduleWizardView : MvxWindowsPage
    {
        private List<SolidColorBrush> brushes = new List<SolidColorBrush> { new SolidColorBrush(Colors.Transparent), new SolidColorBrush(new Color() { R = 23, G = 23, B = 23, A = 255 }) };
        public List<SolidColorBrush> Brushes { get { return brushes; } }

        public SheduleWizardView()
        {
            this.InitializeComponent();
            this.Tag = "Расписание";
     
        }

        


       

        

       
    }
}
