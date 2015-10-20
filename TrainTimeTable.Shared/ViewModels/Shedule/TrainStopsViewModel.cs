using System;
using TrainTimeTable.ApiClient.Facade;
using TrainTimeTable.ApiClient.Response;
using TrainTimeTable.Shared.ViewModels.Base;

namespace TrainTimeTable.Shared.ViewModels.Shedule
{
    public class TrainStopsViewModel:LoadingScreen
    {
        private readonly IApiFacade _apiFacade;
        private TrainStopsResponse _train;


        public TrainStopsViewModel(IApiFacade apiFacade)
            :base("Расписание")
        {
            _apiFacade = apiFacade;
        }

        public TrainStopsResponse Train
        {
            get { return _train; }
            set
            {
                _train = value;
                base.RaisePropertyChanged(()=>Train);
            }
        }

        public async void Init(string uid,DateTime date)
        {
            var trainResponse=(await _apiFacade.GetTrain(uid, date));
            Train=trainResponse.Result;
        }
    }
}
