using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrainTimeTable.LocalEntities.Repositories
{
    public interface IStationRepository
    {
        Task<List<Station>> GetAllStations();
    }
}