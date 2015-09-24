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
        private readonly IApiFacade _api;
        private readonly IStationRepository _stationRepository;
        private readonly SqliteContext _context;

        public MainViewModel(IApiFacade api,IStationRepository stationRepository,SqliteContext context)
        {
            _api = api;
            _stationRepository = stationRepository;
            _context = context;
           
        }

       
    }
}
