using System.Collections.ObjectModel;
using UangKu.Model.Base;
using static UangKu.Model.Response.AppStandardReferenceItem.AppStandardReferenceItem;

namespace UangKu.Model.Index
{
    public class SignUp : BaseModel
    {
        public SignUp()
        {
            
        }

        private AsriRoot selectedsex { get; set; }
        public AsriRoot SelectedSex
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
        private IList<AsriRoot> listsex { get; set; }

        public IList<AsriRoot> ListSex
        {
            get
            {
                if (listsex == null)
                {
                    listsex = new ObservableCollection<AsriRoot>();
                }
                return listsex;
            }
            set { listsex = value; }
        }
    }
}
