using System.Collections.Generic;
using Newtonsoft.Json;
using TrainTimeTable.LocalEntities;
using TrainTimeTable.LocalEntities.Repositories;
using TrainTimeTable.Shared.ViewModels.Base;
using TrainTimeTable.Shared.ViewModels.Shedule;

namespace TrainTimeTable.Shared.ViewModels
{
    public class MainViewModel:LoadingScreen
    {
        private readonly IFavoriteTrainRepository _favoriteTrainRepository;
        private readonly IStationRepository _stationRepository;
        private List<FavoriteTrainPath> _favorites;
        private FavoriteTrainPath _selectedFavoritePath;

        public MainViewModel(IFavoriteTrainRepository favoriteTrainRepository,IStationRepository stationRepository)
            :base("Электрички")
        {
            _favoriteTrainRepository = favoriteTrainRepository;
            _stationRepository = stationRepository;
         
        }

        public List<FavoriteTrainPath> Favorites
        {
            get { return _favorites; }
            set
            {
                _favorites = value;
                base.RaisePropertyChanged(()=>Favorites);
            }
        }

        public FavoriteTrainPath SelectedFavoritePath
        {
            get { return _selectedFavoritePath; }
            set
            {
                _selectedFavoritePath = value;
                NavigateToSelectedFavorite(value);
                base.RaisePropertyChanged(()=>SelectedFavoritePath);
            }
        }

        private void NavigateToSelectedFavorite(FavoriteTrainPath value)
        {
            var favorite = JsonConvert.SerializeObject(value);
            ShowViewModel<SheduleWizardViewModel>(new { favorite = favorite });
        }

        public override async void Start()
        {
            Favorites= await _favoriteTrainRepository.GetAllFavorites();
            base.Start();
        }
    }
}
