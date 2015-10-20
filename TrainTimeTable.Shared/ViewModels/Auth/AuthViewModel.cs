using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainTimeTable.Shared.ViewModels.Base;

namespace TrainTimeTable.Shared.ViewModels.Auth
{
    public class AuthViewModel:LoadingScreen
    {
        public AuthViewModel()
            :base("Авторизация")
        {
            
        }
    }
}
