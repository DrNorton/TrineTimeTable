using System.Threading.Tasks;
using TrainTimeTable.ApiClient.Models;
using TrainTimeTable.ApiClient.Requests.Base;

namespace TrainTimeTable.ApiClient.Executer
{
    public interface IApiExecuter
    {
        Task<ApiResponse<T>> Execute<T>(IRequest request);
        Task<T> ExecuteWithoutApiResponse<T>(IRequest request);
    }
}