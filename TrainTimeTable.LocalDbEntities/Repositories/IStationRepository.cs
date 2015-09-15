using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrainTimeTable.LocalDbEntities.Repositories
{
    public interface IStationRepository
    {
        Task<List<Station>> GetAllStations();
    }
}