using System.Collections.ObjectModel;
using UangKu.Model.Base;
using static UangKu.Model.Response.AppStandardReferenceItem.AppStandardReferenceItem;
using static UangKu.Model.Response.Transaction.AllTransaction;
using static UangKu.Model.Response.Transaction.SumTransaction;

namespace UangKu.Model.Menu
{
    public class TransactionLog : BaseModel
    {
        private string title = string.Empty;
        public string Title { get => title; set => SetProperty(ref title, value); }
        private bool isbusy = false;
        public bool IsBusy { get => isbusy; set => SetProperty(ref isbusy, value); }
        private bool isallowcustomdate = false;
        public bool IsAllowCustomDate { get => isallowcustomdate; set => SetProperty(ref isallowcustomdate, value); }
        private int page = 0;
        public int Page { get => page; set => page = value; }
        private string daterange = string.Empty;
        public string DateRange { get => daterange; set => SetProperty(ref daterange, value); }
        private string orderby = string.Empty;
        public string OrderBy { get => orderby; set => SetProperty(ref orderby, value); }
        private string ascending = string.Empty;
        public string Ascending { get => ascending; set => SetProperty(ref ascending, value); }
        private string builder = string.Empty;
        public string Builder { get => builder; set => SetProperty(ref builder, value); }
        private AsriRoot selectedorderby { get; set; }
        public AsriRoot SelectedOrderBy
        {
            get { return selectedorderby; }
            set
            {
                if (selectedorderby != value)
                {
                    selectedorderby = value;
                    OnPropertyChanged(nameof(selectedorderby));
                }
            }
        }
        private IList<SumTransactionRoot> listsumtrans { get; set; }

        public IList<SumTransactionRoot> ListSumTrans
        {
            get
            {
                if (listsumtrans == null)
                {
                    listsumtrans = new ObservableCollection<SumTransactionRoot>();
                }
                return listsumtrans;
            }
            set { listsumtrans = value; }
        }
        private IList<AllTransactionRoot> listalltrans { get; set; }

        public IList<AllTransactionRoot> ListAllTrans
        {
            get
            {
                if (listalltrans == null)
                {
                    listalltrans = new ObservableCollection<AllTransactionRoot>();
                }
                return listalltrans;
            }
            set { listalltrans = value; }
        }
        private IList<AsriRoot> listorderby { get; set; }

        public IList<AsriRoot> ListOrderBy
        {
            get
            {
                if (listorderby == null)
                {
                    listorderby = new ObservableCollection<AsriRoot>();
                }
                return listorderby;
            }
            set { listorderby = value; }
        }
    }
}
