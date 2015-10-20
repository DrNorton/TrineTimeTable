using Cirrious.MvvmCross.ViewModels;

namespace TrainTimeTable.Shared.ViewModels.Base
{
    public class LoadingScreen : MvxViewModel
    {
        private bool _isLoading;
        private string _loadingText;
        private string _title;

        public LoadingScreen(string title)
        {
            _title = title;
        }

        protected void Wait(bool loading, string loadingText = "загрузка")
        {
            IsLoading = loading;
            if (!loading)
            {
                LoadingText = "";
            }
            else
            {
                LoadingText = loadingText;
            }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                RaisePropertyChanged(() => IsLoading);
            }
        }
        public string LoadingText
        {
            get { return _loadingText; }
            set
            {
                _loadingText = value;
                RaisePropertyChanged(() => LoadingText);
            }
        }

        public string Title
        {
            get { return _title; }
           
        }

        public void Close()
        {
            this.Close(this);
        }

    }
}
