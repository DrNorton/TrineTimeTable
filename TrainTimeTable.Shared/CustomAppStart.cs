using Cirrious.MvvmCross.ViewModels;
using TrainTimeTable.Shared.ViewModels;

namespace TrainTimeTable.Shared
{
    public class CustomAppStart : MvxNavigatingObject, IMvxAppStart
    {
        public CustomAppStart()
        {

        }

        public void Start(object hint = null)
        {
           ShowViewModel<MainViewModel>();
        }
    }
}
