using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using TrainTimeTable.ApiClient.Facade;
using TrainTimeTable.ApiClient.Response;
using TrainTimeTable.Shared.ViewModels.Base;

namespace TrainTimeTable.Shared.ViewModels.Shedule
{
    public class SheduleWizardViewModel:LoadingScreen
    {
        private readonly IApiFacade _apiFacade;
        private List<StationResponse> _toSuggestionStations;
        private List<StationResponse> _fromSuggestionStations;
        private string _toPattern;
        private string _fromPattern;
        private TrainShedules _shedule;
        private DateTimeOffset _selectDate;
        public ICommand FindCommand { get; set; }


        public SheduleWizardViewModel(IApiFacade apiFacade)
        {
            _apiFacade = apiFacade;
           FindCommand=new MvxCommand(async ()=> await Find());
            _selectDate=new DateTimeOffset(DateTime.Now);
        }

        public SheduleWizardViewModel()
        {
             
        }

        public async Task<int> Find()
        {
           var apiResponse=await _apiFacade.GetShedule(FromStation.ExpressCode, ToStation.ExpressCode, _selectDate.DateTime, 1);
            Shedule = apiResponse.Result;
            return 0;
        }

        private async void LoadStations(int number)
        {
            if (number == 0)
            {
                ToSuggestionStations = (await _apiFacade.SearchStationByName(ToPattern)).Result;
            }
            else
            {
                FromSuggestionStations = (await _apiFacade.SearchStationByName(FromPattern)).Result;
            }
        }

        public List<StationResponse> ToSuggestionStations
        {
            get { return _toSuggestionStations; }
            set
            {
                _toSuggestionStations = value;
                base.RaisePropertyChanged("ToSuggestionStations");
            }
        }

        public List<StationResponse> FromSuggestionStations
        {
            get { return _fromSuggestionStations; }
            set
            {
                _fromSuggestionStations = value;
                base.RaisePropertyChanged("FromSuggestionStations");
            }
        }

        public string ToPattern
        {
            get { return _toPattern; }
            set
            {
                _toPattern = value;
                if (value != null && value.Length > 3)
                {
                    LoadStations(0);
                }
                base.RaisePropertyChanged(()=>ToPattern);
            }
        }

        public string FromPattern
        {
            get { return _fromPattern; }
            set
            {
                _fromPattern = value;
                if (value != null && value.Length > 3)
                {
                    LoadStations(1);
                }
                base.RaisePropertyChanged(() => FromPattern);
            }
        }

        public StationResponse FromStation { get; set; }
        public StationResponse ToStation { get; set; }

        public TrainShedules Shedule
        {
            get { return _shedule; }
            set
            {
                _shedule = value;
                base.RaisePropertyChanged(()=>Shedule);
            }
        }

        public DateTimeOffset SelectDate
        {
            get { return _selectDate; }
            set
            {
                _selectDate = value;
                base.RaisePropertyChanged(() => SelectDate);
            }
        }
    }
}
