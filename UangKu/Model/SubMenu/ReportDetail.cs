using System.Collections.ObjectModel;
using UangKu.Model.Base;
using static UangKu.Model.Response.AppStandardReferenceItem.AppStandardReferenceItem;

namespace UangKu.Model.SubMenu
{
    public class ReportDetail : BaseModel
    {
        public ReportDetail()
        {
            
        }
        private bool isvisible = false;
        public bool IsVisible { get => isvisible; set => SetProperty(ref isvisible, value); }
        private bool iseditable = false;
        public bool IsEditAble { get => iseditable; set => SetProperty(ref iseditable, value); }
        private bool isenabled = false;
        public bool IsEnabled { get => isenabled; set => SetProperty(ref isenabled, value); }
        private IList<AsriRoot> listerrorlocation { get; set; }

        public IList<AsriRoot> ListErrorLocation
        {
            get
            {
                if (listerrorlocation == null)
                {
                    listerrorlocation = new ObservableCollection<AsriRoot>();
                }
                return listerrorlocation;
            }
            set { listerrorlocation = value; }
        }
        private IList<AsriTwoRoot> listerrorposibility { get; set; }

        public IList<AsriTwoRoot> ListErrorPosibility
        {
            get
            {
                if (listerrorposibility == null)
                {
                    listerrorposibility = new ObservableCollection<AsriTwoRoot>();
                }
                return listerrorposibility;
            }
            set { listerrorposibility = value; }
        }
        private IList<AsriThreeRoot> listreportstatus { get; set; }

        public IList<AsriThreeRoot> ListReportStatus
        {
            get
            {
                if (listreportstatus == null)
                {
                    listreportstatus = new ObservableCollection<AsriThreeRoot>();
                }
                return listreportstatus;
            }
            set { listreportstatus = value; }
        }
        private AsriRoot selectederrorlocation { get; set; }
        public AsriRoot SelectedErrorLocation
        {
            get { return selectederrorlocation; }
            set
            {
                if (selectederrorlocation != value)
                {
                    selectederrorlocation = value;
                    OnPropertyChanged(nameof(SelectedErrorLocation));
                }
            }
        }
        private AsriTwoRoot selectederrorposibility { get; set; }
        public AsriTwoRoot SelectedErrorPosibility
        {
            get { return selectederrorposibility; }
            set
            {
                if (selectederrorposibility != value)
                {
                    selectederrorposibility = value;
                    OnPropertyChanged(nameof(SelectedErrorPosibility));
                }
            }
        }
        private AsriThreeRoot selectedreportstatus { get; set; }
        public AsriThreeRoot SelectedReportStatus
        {
            get { return selectedreportstatus; }
            set
            {
                if (selectedreportstatus != value)
                {
                    selectedreportstatus = value;
                    OnPropertyChanged(nameof(SelectedReportStatus));
                }
            }
        }
    }
}
