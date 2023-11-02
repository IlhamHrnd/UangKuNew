using UangKu.Model.Base;

namespace UangKu.Model.Menu
{
    public class TransactionLog : BaseModel
    {
        private string title = string.Empty;
        public string Title { get => title; set => SetProperty(ref title, value); }
        private bool isbusy = false;
        public bool IsBusy { get => isbusy; set => SetProperty(ref isbusy, value); }
    }
}
