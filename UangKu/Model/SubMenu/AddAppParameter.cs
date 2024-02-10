using UangKu.Model.Base;

namespace UangKu.Model.SubMenu
{
    public class AddAppParameter : BaseModel
    {
        private string title = string.Empty;
        public string Title { get => title; set => SetProperty(ref title, value); }
        private bool isbusy = false;
        public bool IsBusy { get => isbusy; set => SetProperty(ref isbusy, value); }
        private bool isreadonly = false;
        public bool IsReadOnly { get => isreadonly; set => SetProperty(ref isreadonly, value); }
    }
}
