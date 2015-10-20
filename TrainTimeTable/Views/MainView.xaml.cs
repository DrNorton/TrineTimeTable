using Cirrious.MvvmCross.WindowsCommon.Views;



// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TrainTimeTable.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainView : MvxWindowsPage
    {
        public MainView()
        {
            this.InitializeComponent();
            this.Tag = "Электрички";
        }
    }
}
