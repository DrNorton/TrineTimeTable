using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainTimeTable.ApiClient.ExceptionRouter;
using TrainTimeTable.ApiClient.Executer;
using TrainTimeTable.ApiClient.Models;
using TrainTimeTable.ApiClient.Requests;
using TrainTimeTable.ApiClient.Requests.Base;
using TrainTimeTable.ApiClient.Response;

namespace TrainTimeTable.ApiClient.Facade
{
    public class ApiFacade : IApiFacade
    {
        private readonly IApiExceptionRouter _exceptionRouter;
        private IApiExecuter _apiExecuter;
        private readonly IApiSettings _apiSettings;
        

        public ApiFacade(IApiExecuter apiExecuter,IApiSettings apiSettings)
        {
            _apiExecuter = apiExecuter;
            _apiSettings = apiSettings;
        }


        public Task<ApiResponse<List<StationResponse>>> SearchStationByName(string pattern)
        {
            var searchStationByName = new SearchStationByName(pattern);
            searchStationByName.BaseUrl = _apiSettings.BaseUrl;
            return ExecuteWithErrorHandling<List<StationResponse>>(searchStationByName);
        }

        public Task<ApiResponse<List<StationResponse>>> GetAllStations()
        {
            var getAllStationsRequest = new GetAllStationsRequest();
            getAllStationsRequest.BaseUrl = _apiSettings.BaseUrl;
            return ExecuteWithErrorHandling<List<StationResponse>>(getAllStationsRequest);
        }

        public Task<ApiResponse<TrainStopsResponse>> GetTrain(string uid,DateTime date)
        {
            var getAllStationsRequest = new GetTrainStopsRequest(uid,date);
            getAllStationsRequest.BaseUrl = _apiSettings.BaseUrl;
            return ExecuteWithErrorHandling<TrainStopsResponse>(getAllStationsRequest);
        }

        public Task<ApiResponse<TrainShedules>> GetShedule(long from,long to,DateTime date,int page)
        {
            var getSheduleRequest = new GetSheduleRequest(from,to,date,page);
            getSheduleRequest.BaseUrl = _apiSettings.BaseUrl;
            return ExecuteWithErrorHandling<TrainShedules>(getSheduleRequest);
        }

        public Task<ApiResponse<List<StationResponse>>> GetAllStationsCoordinates()
        {
            var getAllStationsCoordinatesRequest = new GetAllStationsCoordinatesRequest();
            getAllStationsCoordinatesRequest.BaseUrl = _apiSettings.BaseUrl;
            return ExecuteWithErrorHandling<List<StationResponse>>(getAllStationsCoordinatesRequest);
        }

        public async Task<ApiResponse<Token>> Auth(string email,string password)
        {
            var authWithGetProfileRequest = new AuthRequest(email,password);
            authWithGetProfileRequest.BaseUrl = _apiSettings.BaseUrl;
            ApiResponse<Token> response=null;
            try
            {
                 var tokenResponse =await _apiExecuter.ExecuteWithoutApiResponse<TokenResponse>(authWithGetProfileRequest);
                _apiSettings.Token=new Token(){ExpiredIn = tokenResponse.ExpiredIn,TokenType = tokenResponse.TokenType,Value = tokenResponse.Value};
           
            if (String.IsNullOrEmpty(tokenResponse.Error))
            {
                response = new ApiResponse<Token>()
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Result = _apiSettings.Token
                };
               
            }
            }
            catch (Exception e)
            {
                response = new ApiResponse<Token>()
                {
                    ErrorCode = 401,
                    ErrorMessage = e.Message,
                    Result = null
                };
            }
           

            return response;
        }

        

        private Task<ApiResponse<T>> ExecuteWithErrorHandling<T>(IRequest request)
        {
            try
            {
                request.Token = _apiSettings.Token;//Добавляем токен
               return _apiExecuter.Execute<T>(request);
            }
            catch (ApiException ex)
            {
             
               throw ex;
            }
        }


     
    }
}
