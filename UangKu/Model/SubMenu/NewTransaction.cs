using System.Collections.ObjectModel;
using UangKu.Model.Base;
using static UangKu.Model.Response.AppStandardReferenceItem.AppStandardReferenceItem;

namespace UangKu.Model.SubMenu
{
    public class NewTransaction : BaseModel
    {
        private string title = string.Empty;
        public string Title { get => title; set => SetProperty(ref title, value); }
        private bool isbusy = false;
        public bool IsBusy { get => isbusy; set => SetProperty(ref isbusy, value); }
        private IList<AsriRoot> listtransaction { get; set; }

        public IList<AsriRoot> ListTransaction
        {
            get
            {
                if (listtransaction == null)
                {
                    listtransaction = new ObservableCollection<AsriRoot>();
                }
                return listtransaction;
            }
            set { listtransaction = value; }
        }
        private AsriRoot selectedtranstype { get; set; }
        public AsriRoot SelectedTransType
        {
            get { return selectedtranstype; }
            set
            {
                if (selectedtranstype != value)
                {
                    selectedtranstype = value;
                    OnPropertyChanged(nameof(SelectedTransType));
                }
            }
        }
    }
}
