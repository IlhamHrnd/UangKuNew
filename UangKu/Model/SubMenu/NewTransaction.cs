using System.Collections.ObjectModel;
using UangKu.Model.Base;
using static UangKu.Model.Response.AppStandardReferenceItem.AppStandardReferenceItem;

namespace UangKu.Model.SubMenu
{
    public class NewTransaction : BaseModel
    {
        private bool isallowcustomdate = false;
        public bool IsAllowCustomDate { get => isallowcustomdate; set => SetProperty(ref isallowcustomdate, value); }
        private string transtypes = string.Empty;
        public string TransTypes { get => transtypes; set => SetProperty(ref transtypes, value); }
        private string transno = string.Empty;
        public string TransNo { get => transno; set => SetProperty(ref transno, value); }
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
        private IList<AsriTwoRoot> listtransitem { get; set; }

        public IList<AsriTwoRoot> ListTransItem
        {
            get
            {
                if (listtransitem == null)
                {
                    listtransitem = new ObservableCollection<AsriTwoRoot>();
                }
                return listtransitem;
            }
            set { listtransitem = value; }
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
        private AsriTwoRoot selectedtransitem { get; set; }
        public AsriTwoRoot SelectedTransItem
        {
            get { return selectedtransitem; }
            set
            {
                if (selectedtransitem != value)
                {
                    selectedtransitem = value;
                    OnPropertyChanged(nameof(SelectedTransItem));
                }
            }
        }
    }
}
