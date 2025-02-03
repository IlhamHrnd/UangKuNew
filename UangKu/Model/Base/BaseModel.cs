using System.ComponentModel;
using System.Runtime.CompilerServices;
using UangKu.Model.Session;

namespace UangKu.Model.Base
{
    public class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string title = string.Empty;
        public string Title { get => title; set => SetProperty(ref title, value); }
        private bool isbusy = false;
        public bool IsBusy { get => isbusy; set => SetProperty(ref isbusy, value); }
        private int page = 0;
        public int Page { get => page; set => page = value; }
        private int number = 0;
        public int Number { get => number; set => number = value; }
        private int size = 0;
        public int Size { get => size; set => size = value; }
        private int totalrecords = 0;
        public int TotalRecords { get => totalrecords; set => totalrecords = value; }
        private int totalpages = 0;
        public int TotalPages { get => totalpages; set => totalpages = value; }
        private NetworkModel network = NetworkModel.Instance;
        public NetworkModel Network { get => network; set => network = value; }
        private string mode = string.Empty;
        public string Mode { get => mode; set => SetProperty(ref mode, value); }
        private bool isProgram = false;
        public bool IsProgram { get => isProgram; set => SetProperty(ref isProgram, value); }
        private bool isProgramAddAble = false;
        public bool IsProgramAddAble { get => isProgramAddAble; set => SetProperty(ref isProgramAddAble, value); }
        private bool isProgramEditAble = false;
        public bool IsProgramEditAble { get => isProgramEditAble; set => SetProperty(ref isProgramEditAble, value); }
        private bool isProgramDeleteAble = false;
        public bool IsProgramDeleteAble { get => isProgramDeleteAble; set => SetProperty(ref isProgramDeleteAble, value); }
        private bool isProgramViewAble = false;
        public bool IsProgramViewAble { get => isProgramViewAble; set => SetProperty(ref isProgramViewAble, value); }
        private bool isProgramApprovalAble = false;
        public bool IsProgramApprovalAble { get => isProgramApprovalAble; set => SetProperty(ref isProgramApprovalAble, value); }
        private bool isProgramUnApprovalAble = false;
        public bool IsProgramUnApprovalAble { get => isProgramUnApprovalAble; set => SetProperty(ref isProgramUnApprovalAble, value); }
        private bool isProgramVoidAble = false;
        public bool IsProgramVoidAble { get => isProgramVoidAble; set => SetProperty(ref isProgramVoidAble, value); }
        private bool isProgramUnVoidAble = false;
        public bool IsProgramUnVoidAble { get => isProgramUnVoidAble; set => SetProperty(ref isProgramUnVoidAble, value); }
        private bool isVisible = false;
        public bool IsVisible { get => isVisible; set => SetProperty(ref isVisible, value); }
        private bool isUsedBySystem = false;
        public bool IsUsedBySystem { get => isUsedBySystem; set => SetProperty(ref isUsedBySystem, value); }
        private static int timeout;
        public static int TimeOut
        {
            get => timeout = AppParameter.Timeout > 0 ? AppParameter.Timeout : ItemManager.Timeout;
            set => timeout = value > 0 ? value : ItemManager.Timeout;
        }
        private static string url;
        public static string URL
        {
            get => url = !string.IsNullOrEmpty(AppParameter.URL) ? AppParameter.URL : ItemManager.Url;
            set => url = !string.IsNullOrEmpty(value) ? value : ItemManager.Url;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
                return false;

            backingField = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
