using System.ComponentModel;
using System.Runtime.CompilerServices;

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
        private string mode = string.Empty;
        public string Mode { get => mode; set => SetProperty(ref mode, value); }
        private string savedir = string.Empty;
        public string SaveDir { get => savedir; set => savedir = value; }

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
