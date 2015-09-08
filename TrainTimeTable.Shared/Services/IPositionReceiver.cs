using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTimeTable.ApiClient.Response;

namespace TrainTimeTable.Shared.Services
{
    public interface IPositionReceiver
    {
        Task<PositionDto> GetCurrentCoordinates();
    }
}
