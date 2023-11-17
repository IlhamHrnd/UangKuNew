using System.Collections.ObjectModel;
using UangKu.Model.Base;
using static UangKu.Model.Response.Transaction.SumTransaction;

namespace UangKu.Model.Menu
{
    public class Home : BaseModel
    {
        public Home()
        {
            
        }
        private string title = "Home Page";
        public string Title { get => title; set => SetProperty(ref title, value); }
        private string name = string.Empty;
        public string Name { get => name; set => name = value; }
        private bool isbusy = false;
        public bool IsBusy { get => isbusy; set => SetProperty(ref isbusy, value); }
        private string image = string.Empty;
        public string Image { get => image; set => image = value; }
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
    }
}
