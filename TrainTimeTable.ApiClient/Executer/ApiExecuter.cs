using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TrainTimeTable.ApiClient.Models;
using TrainTimeTable.ApiClient.Requests;
using TrainTimeTable.ApiClient.Requests.Base;

namespace TrainTimeTable.ApiClient.Executer
{
    public class ApiExecuter : IApiExecuter
    {


        public ApiExecuter()
        {
            Debug.WriteLine("asdad");
        }
        public async Task<ApiResponse<T>> Execute<T>(IRequest request)
        {
            var restClient = new HttpClient();
            if (request.Token != null)
            {
                //Добавляем хедер
                restClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.Token.Value);
            }

            var methodUrl = CreateAndPrepareByUrl(request);
          

            try
            {
                if (request.Type == HttpMethod.Post)
                {
                   return await ExecutePost<ApiResponse<T>>(request, restClient, methodUrl);
                }
                else
                {
                    return await ExecuteGet<ApiResponse<T>>(request, restClient, methodUrl);
                }
               
            }
            catch (Exception e)
            {
                throw new ApiException(10000, e.Message);
            }
        
         
        }

        private  async Task<T> ExecutePost<T>(IRequest request, HttpClient restClient, string methodUrl)
        {
            HttpContent restContent;
            

            if (!(request is AuthRequest))
            {
                restClient.DefaultRequestHeaders.Add("Accept", "application/json");
                restContent = new StringContent(JsonConvert.SerializeObject(request.Params));
            }
            else
            {
                restContent = new FormUrlEncodedContent(CastDictionaries(request.Params));
                
            }

            restContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseStream = restClient.PostAsync(request.BaseUrl + "/" + methodUrl, restContent).Result;
            string statusCode = responseStream.StatusCode.ToString();
            string content = await responseStream.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<T>(content);
            return data;
        }

        private  async Task<T> ExecuteGet<T>(IRequest request, HttpClient restClient, string methodUrl)
        {
           
            var urlContent = new FormUrlEncodedContent(CastDictionaries(request.Params));

            var httprequest = new HttpRequestMessage()
            {
                RequestUri = new Uri(request.BaseUrl+methodUrl, UriKind.Relative),
                Method = HttpMethod.Get,
                Content = urlContent
            };

            var response = await restClient.SendAsync(httprequest);
            string statusCode = response.StatusCode.ToString();
            string content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<T>(content);
            return data;
        }

        private Dictionary<string, string> CastDictionaries(Dictionary<string, object> oldDict)
        {
            var retdict = new Dictionary<string, string>();
            foreach (var pair in oldDict)
            {
                retdict.Add(pair.Key,pair.Value.ToString());
            }
            return retdict;
        } 

        public async Task<T> ExecuteWithoutApiResponse<T>(IRequest request)
        {
            var restClient = new HttpClient();
            if (request.Token != null)
            {
                //Добавляем хедер
                restClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", request.Token.Value);
            }

            var methodUrl = CreateAndPrepareByUrl(request);


            try
            {
                if (request.Type == HttpMethod.Post)
                {
                    return await ExecutePost<T>(request, restClient, methodUrl);
                }
                else
                {
                    return await ExecuteGet<T>(request, restClient, methodUrl);
                }

            }
            catch (Exception e)
            {
                throw new ApiException(10000, e.Message);
            }

        }

   

        private  string CreateAndPrepareByUrl(IRequest request)
        {
            string url;
            if(String.IsNullOrEmpty(request.Controller))
            {
                url = request.MethodName;
            }
            else
            {
                if (String.IsNullOrEmpty(request.MethodName))
                {
                    url = request.Controller;
                }
                else
                {
                    url = String.Format("{0}/{1}", request.Controller, request.MethodName);
                }
            }


            return url;
        }
      
    }
}
