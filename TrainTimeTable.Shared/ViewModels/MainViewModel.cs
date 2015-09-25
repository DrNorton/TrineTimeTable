using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTimeTable.ApiClient.Facade;
using TrainTimeTable.LocalEntities;
using TrainTimeTable.LocalEntities.Repositories;
using TrainTimeTable.Shared.ViewModels.Base;

namespace TrainTimeTable.Shared.ViewModels
{
    public class MainViewModel:LoadingScreen
    {
        private readonly IFavoriteTrainRepository _favoriteTrainRepository;
        private List<FavoriteTrainPath> _favorites;

        public MainViewModel(IFavoriteTrainRepository favoriteTrainRepository)
        {
            _favoriteTrainRepository = favoriteTrainRepository;
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

        public override async void Start()
        {
            _favorites= await _favoriteTrainRepository.GetAllFavorites();
            base.Start();
        }
    }
}
