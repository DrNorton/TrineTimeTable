using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cirrious.MvvmCross.Plugins.Messenger;
using TrainTimeTable.ApiClient.Response;

namespace TrainTimeTable.Shared.Messages
{
    public class SetMyPositionMessage:MvxMessage
    {
        private readonly PositionDto _position;

        public SetMyPositionMessage(object sender,PositionDto position)
            :base(sender)
        {
            _position = position;
        }

        public PositionDto Position
        {
            get { return _position; }
        }
    }
}
