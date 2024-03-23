using System.Collections.ObjectModel;
using UangKu.Model.Base;
using static UangKu.Model.Response.AppStandardReferenceItem.AppStandardReferenceItem;

namespace UangKu.Model.SubMenu
{
    public class EditUsername : BaseModel
    {
        private string name = string.Empty;
        public string Name { get => name; set => name = value; }
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
        private IList<AsriTwoRoot> listaccess { get; set; }

        public IList<AsriTwoRoot> ListAccess
        {
            get
            {
                if (listaccess == null)
                {
                    listaccess = new ObservableCollection<AsriTwoRoot>();
                }
                return listaccess;
            }
            set { listaccess = value; }
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
                    OnPropertyChanged(nameof(selectedsex));
                }
            }
        }
        private AsriTwoRoot selectedaccess { get; set; }
        public AsriTwoRoot SelectedAccess
        {
            get { return selectedaccess; }
            set
            {
                if (selectedaccess != value)
                {
                    selectedaccess = value;
                    OnPropertyChanged(nameof(selectedaccess));
                }
            }
        }
    }
}
