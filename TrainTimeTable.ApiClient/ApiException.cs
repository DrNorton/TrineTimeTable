using System;

namespace TrainTimeTable.ApiClient
{
    public class ApiException:Exception
    {
        public int ErrorCode { get; set; }

        public ApiException(int code,string message)
            :base(message)
        {
            ErrorCode = code;
        }
    }
}
