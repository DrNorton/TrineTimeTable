using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainTimeTable.Api.Controllers.Settings
{
    public interface ISettings
    {
        string YandexApiKey { get; }
        string YandexBaseUrl { get; }
    }
}
