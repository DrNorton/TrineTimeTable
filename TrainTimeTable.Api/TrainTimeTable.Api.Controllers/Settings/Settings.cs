using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTimeTable.Api.Controllers.Settings
{
    public class Settings:ISettings
    {
        public string YandexApiKey
        {
            get { return "2be696af-b00f-4ef7-a1c7-1df3f893a7e9"; }
        }

        public string YandexBaseUrl
        {
            get { return @"https://api.rasp.yandex.net/v1.0/"; }
        }
    }
}
