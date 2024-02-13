using System.Collections.ObjectModel;
using UangKu.Model.Base;
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
    }
}
