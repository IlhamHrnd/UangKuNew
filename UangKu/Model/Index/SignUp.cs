using System.Collections.ObjectModel;
using UangKu.Model.Base;

namespace UangKu.Model.Index
{
    public class SignUp : BaseModel
    {
        public SignUp()
        {
            
        }

        private WebService.Data.AppStandardReferenceItem.Data selectedsex { get; set; }
        public WebService.Data.AppStandardReferenceItem.Data SelectedSex
        {
            get { return selectedsex; }
            set
            {
                if (selectedsex != value)
                {
                    selectedsex = value;
                    OnPropertyChanged(nameof(SelectedSex));
                }
            }
        }
        private IList<WebService.Data.AppStandardReferenceItem.Data> listsex { get; set; }

        public IList<WebService.Data.AppStandardReferenceItem.Data> ListSex
        {
            get
            {
                if (listsex == null)
                {
                    listsex = new ObservableCollection<WebService.Data.AppStandardReferenceItem.Data>();
                }
                return listsex;
            }
            set { listsex = value; }
        }
    }
}
