using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using TrainTimeTable.LocalEntities;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace TrainTimeTable.Controls
{
    public sealed partial class FavoriteStationImageControl : UserControl
    {
        private bool _a=false;
        public FavoriteTrainPath FavoriteDirection
        {
            get
            {
                return this.GetValue(FavoriteDirectionProperty) as FavoriteTrainPath;
            }
            set
            {
                this.SetValue(FavoriteDirectionProperty, value);
            }
        }
        public static readonly DependencyProperty FavoriteDirectionProperty = DependencyProperty.Register("FavoriteDirection", typeof(FavoriteTrainPath), typeof(FavoriteStationImageControl),new PropertyMetadata(null));

   


        DispatcherTimer playlistTimer = null;
        public FavoriteStationImageControl()
        {
            this.InitializeComponent();
            playlistTimer=new DispatcherTimer();
            var random=new Random().Next(5,15);
            playlistTimer.Interval = new TimeSpan(0, 0, random);
            playlistTimer.Tick += playlistTimer_Tick;
            playlistTimer.Start();
        }

        private void playlistTimer_Tick(object sender, object e)
        {
            if (FavoriteDirection != null)
            {
                if (_a)
                {
                    VisualStateManager.GoToState(this, "ContentImageFrom", true);
                }
                else
                {
                    VisualStateManager.GoToState(this, "ContentImageTo", true);
                }

                _a = !_a;
            }
        }
    }
}
