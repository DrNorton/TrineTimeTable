using System.Threading.Tasks;
using TrainTimeTable.ApiClient.Response;

namespace TrainTimeTable.Shared.Services
{
    public interface IPositionReceiver
    {
        Task<PositionDto> GetCurrentCoordinates();
    }
}
