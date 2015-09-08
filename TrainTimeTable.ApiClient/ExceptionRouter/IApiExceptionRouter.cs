
namespace TrainTimeTable.ApiClient.ExceptionRouter
{
    public interface IApiExceptionRouter
    {
        void Route(ApiException exception);
    }
}
