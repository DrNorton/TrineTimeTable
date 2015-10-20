using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;
using Newtonsoft.Json;
using TrainTimeTable.ApiClient.Facade;
using TrainTimeTable.ApiClient.Response;
using TrainTimeTable.LocalEntities;
using TrainTimeTable.LocalEntities.Repositories;
using TrainTimeTable.Shared.Models.Enum;
using TrainTimeTable.Shared.ViewModels.Base;

namespace TrainTimeTable.Shared.ViewModels.Shedule
{
    public class SheduleWizardViewModel:LoadingScreen
    {
        private readonly IApiFacade _apiFacade;
        private readonly IFavoriteTrainRepository _favoriteTrainRepository;
        private readonly IStationRepository _stationRepository;
        private List<StationResponse> _toSuggestionStations;
        private List<StationResponse> _fromSuggestionStations;
        private string _toPattern;
        private string _fromPattern;
        private ObservableCollection<TrainTread> _trainThreads;
        private DateTimeOffset _selectDate;
        private List<TrainTread> _allTrainsThreads;
        private bool _isAll=false;
        private string _hideUnusedText;
        private CancellationTokenSource _insertStationCancelationToken;
        private TrainTread _selectedThread;

        private string _favoriteIcon= "";
        private bool _isFavorite;
        public ICommand FindCommand { get; set; }
        public ICommand HideUnusedCommand { get; set; }
        public ICommand AddToFavoritesCommand { get; set; }

        public SheduleWizardViewModel(IApiFacade apiFacade,IFavoriteTrainRepository favoriteTrainRepository,IStationRepository stationRepository)
            :base("Расписание")
        {
            _apiFacade = apiFacade;
            _favoriteTrainRepository = favoriteTrainRepository;
            _stationRepository = stationRepository;
            FindCommand=new MvxCommand(async ()=> await Find());
            AddToFavoritesCommand=new MvxCommand(async ()=> await AddToFavorites());
            _insertStationCancelationToken=new CancellationTokenSource();
            HideUnusedCommand =new MvxCommand(() =>
            {
                _isAll = !_isAll;
                ShowAndHideUnused();
                SetHideUnusedText();
            });
            _selectDate=new DateTimeOffset(DateTime.Now);
            SetHideUnusedText();
        }

        public async void Init(string favorite)
        {
            if (!String.IsNullOrEmpty(favorite)) {
            var favoriteTrainPath=JsonConvert.DeserializeObject<FavoriteTrainPath>(favorite);
            ToPattern = favoriteTrainPath.To.StationName;
            FromPattern = favoriteTrainPath.From.StationName;
            FromStation = new StationResponse()
            {
                Ecr = favoriteTrainPath.From.Ecr,
                ExpressCode = favoriteTrainPath.From.ExpressCode,
                StationName = favoriteTrainPath.From.StationName
            };

            ToStation = new StationResponse()
            {
                Ecr = favoriteTrainPath.To.Ecr,
                ExpressCode = favoriteTrainPath.To.ExpressCode,
                StationName = favoriteTrainPath.To.StationName
            };
           await Find();
          }
        }

        private async Task AddToFavorites()
        {
            if (!_isFavorite)
            {
                await _favoriteTrainRepository.AddToFavorites(FromStation.Ecr, ToStation.Ecr);
                IsFavorite = true;
            }
            else
            {
                await _favoriteTrainRepository.DeleteFromFavorites(FromStation.Ecr, ToStation.Ecr);
                IsFavorite = false;
            }
           
        }

        private void SetHideUnusedText()
        {
            if (_isAll)
            {
                HideUnusedText = "Скрыть ушедшие электрички";
            }
            else
            {
                HideUnusedText = "Показать ушедшие электрички";
            }
        }

        private void ShowAndHideUnused()
        {
            if (_isAll)
            {
                TrainTreads = new ObservableCollection<TrainTread>(_allTrainsThreads);
            }
            else
            {
                TrainTreads = new ObservableCollection<TrainTread>(_allTrainsThreads.Where(x => x.Arrival > DateTime.Now).ToList());
            }
          
        }

        public async Task<int> Find()
        {
            TrainTreads = null;
            IsLoading = true;
            await CheckFavorite();
            var apiResponse=await _apiFacade.GetShedule(FromStation.ExpressCode, ToStation.ExpressCode, _selectDate.DateTime, 1);
            _allTrainsThreads = apiResponse.Result.TrainTreads;
            ShowAndHideUnused();
            IsLoading = false;
            return 0;

        }

        private async Task CheckFavorite()
        {
            try
            {
                IsFavorite = await _favoriteTrainRepository.CheckFavorites(FromStation.Ecr, ToStation.Ecr);
            }
            catch (Exception e)
            {
                
            }
           
           
        }

        private async Task<List<StationResponse>> LoadStationsFromApi(string pattern)
        {
            List<StationResponse> loadedStations=null;
            loadedStations = (await _apiFacade.SearchStationByName(pattern)).Result;
            await InsertStations(loadedStations);
            return loadedStations;
        }

        private void SetSuggestionList(StationDirection direction, List<StationResponse> stations)
        {
            if (direction == StationDirection.To)
            {
                ToSuggestionStations = stations;
            }

            if (direction == StationDirection.From)
            {
                FromSuggestionStations = stations;
            }
        }

        private async Task<List<StationResponse>> LoadStationsFromLocalDb(string pattern)
        {
          var stationsFromLocal=await _stationRepository.FindByName(pattern);
            return
                stationsFromLocal?.Select(
                    x => new StationResponse() {ExpressCode = x.ExpressCode, Ecr = x.Ecr, StationName = x.StationName}).ToList();
        }

        private async Task InsertStations(List<StationResponse> loadedStations)
        {
            if (loadedStations != null && loadedStations.Any())
            {
               
               await _stationRepository.AddStationsIfNotExists(
                        loadedStations.Select(
                            x =>
                                new Station()
                                {
                                    Ecr = x.Ecr,
                                    ExpressCode = x.ExpressCode,
                                    ImageSourceUri = x.ImageSourceUri == null ? null : x.ImageSourceUri.ToString(),
                                    StationName = x.StationName,
                                    Position =
                                        new Position()
                                        {
                                            Latitude = x.Position.Latitude,
                                            Longitude = x.Position.Longitude
                                        },
                                    Image =
                                        new Image() {FullImageUrl = x.Image.FullImageUrl, ThumbUrl = x.Image.ThumbUrl}
                                })
                            .ToList());
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
                if (value != null)
                {
                    LoadStations(StationDirection.To,_toPattern);
                }
                base.RaisePropertyChanged(()=>ToPattern);
            }
        }

        private async void LoadStations(StationDirection direction,string pattern)
        {
            List<StationResponse> loadedStations;
            loadedStations = await LoadStationsFromLocalDb(pattern);
            if (!loadedStations.Any() && pattern.Length>3)
            {
                //грузим с апи
                loadedStations = await LoadStationsFromApi(pattern);
            }

            SetSuggestionList(direction,loadedStations);
            
        }


        public string FromPattern
        {
            get { return _fromPattern; }
            set
            {
                _fromPattern = value;
                if (value != null)
                {
                    LoadStations(StationDirection.From, value);
                }
                base.RaisePropertyChanged(() => FromPattern);
            }
        }

        public StationResponse FromStation { get; set; }
        public StationResponse ToStation { get; set; }

        public ObservableCollection<TrainTread> TrainTreads
        {
            get { return _trainThreads; }
            set
            {
                _trainThreads = value;
                base.RaisePropertyChanged(()=> TrainTreads);
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

        public string HideUnusedText
        {
            get { return _hideUnusedText; }
            set
            {
                _hideUnusedText = value;
                base.RaisePropertyChanged(()=> HideUnusedText);
            }
        }

        public string FavoriteIcon
        {
            get { return _favoriteIcon; }
            set
            {
                _favoriteIcon = value;
                base.RaisePropertyChanged(()=>FavoriteIcon);
            }
        }

        public bool IsFavorite
        {
            get { return _isFavorite; }
            set
            {
                _isFavorite = value;
                if (_isFavorite)
                {
                    FavoriteIcon = ((char)0xE195).ToString();
                }
                else
                {
                    FavoriteIcon = ((char)0xE113).ToString();
                }
            }
        }

        public TrainTread SelectedThread
        {
            get { return _selectedThread; }
            set
            {
                _selectedThread = value;
                LoadTrain(value);
                base.RaisePropertyChanged(()=>SelectedThread);
            }
        }

        private async void LoadTrain(TrainTread value)
        {
            ShowViewModel<TrainStopsViewModel>(new {uid = value.Uid,date=SelectDate.Date});
        }
    }
}
