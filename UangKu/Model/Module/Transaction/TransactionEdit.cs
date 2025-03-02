using System.Collections.ObjectModel;
using UangKu.Model.Base;
using UangKu.WebService.Data;

namespace UangKu.Model.Module.Transaction
{
    public class TransactionEdit : BaseModel
    {
        private string transno = string.Empty;
        public string TransNo { get => transno; set => SetProperty(ref transno, value); }
        private bool isenabled = false;
        public bool IsEnabled { get => isenabled; set => SetProperty(ref isenabled, value); }
        private Root<ObservableCollection<AppStandardReferenceItem.Data>> transtype;
        public Root<ObservableCollection<AppStandardReferenceItem.Data>> TransType
        {
            get
            {
                if (transtype == null)
                {
                    transtype = new Root<ObservableCollection<AppStandardReferenceItem.Data>>
                    {
                        Data = new ObservableCollection<AppStandardReferenceItem.Data>(),
                        Succeeded = true,
                        Errors = null,
                        Message = "Initialized"
                    };
                }
                return transtype;
            }
            set
            {
                transtype = value;
                OnPropertyChanged(nameof(TransType));
            }
        }
        private AppStandardReferenceItem.Data selectedtranstype { get; set; }
        public AppStandardReferenceItem.Data SelectedTransType
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
        private Root<ObservableCollection<AppStandardReferenceItem.Data>> transitem;
        public Root<ObservableCollection<AppStandardReferenceItem.Data>> TransItem
        {
            get
            {
                if (transitem == null)
                {
                    transitem = new Root<ObservableCollection<AppStandardReferenceItem.Data>>
                    {
                        Data = new ObservableCollection<AppStandardReferenceItem.Data>(),
                        Succeeded = true,
                        Errors = null,
                        Message = "Initialized"
                    };
                }
                return transitem;
            }
            set
            {
                transitem = value;
                OnPropertyChanged(nameof(TransItem));
            }
        }
        private AppStandardReferenceItem.Data selectedtransitem { get; set; }
        public AppStandardReferenceItem.Data SelectedTransItem
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
        private ImageManager img;
        public ImageManager Img { get => img; set => img = value; }
    }
}
