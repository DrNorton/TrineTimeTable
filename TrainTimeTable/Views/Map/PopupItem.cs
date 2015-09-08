using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTimeTable.ApiClient.Response;

namespace TrainTimeTable.Views.Map
{
    public class PopupItem
    {
        public string Title { get; set; }
        public PositionDto Position { get; set; }
    }
}
