using System.Collections.ObjectModel;
using UangKu.Model.Base;
using static UangKu.Model.Response.AppStandardReferenceItem.AppStandardReferenceItem;

namespace UangKu.Model.SubMenu
{
    public class AddAppParameter : BaseModel
    {
        private bool isreadonly = false;
        public bool IsReadOnly { get => isreadonly; set => SetProperty(ref isreadonly, value); }
        private bool ischeckboxvisible = false;
        public bool IsCheckedBoxVisible { get => ischeckboxvisible; set => SetProperty(ref ischeckboxvisible, value); }
        private bool isentryvisible = false;
        public bool IsEntryVisible { get => isentryvisible; set => SetProperty(ref isentryvisible, value); }
        private string parameterid = string.Empty;
        public string ParameterID { get => parameterid; set => SetProperty(ref parameterid, value); }
        private string parametertypes = string.Empty;
        public string ParameterTypes { get => parametertypes; set => SetProperty(ref parametertypes, value); }
        private AsriRoot selectedparametertype { get; set; }
        public AsriRoot SelectedParameterType
        {
            get { return selectedparametertype; }
            set
            {
                if (selectedparametertype != value)
                {
                    selectedparametertype = value;
                    OnPropertyChanged(nameof(selectedparametertype));
                }
            }
        }
        private IList<AsriRoot> listparametertype { get; set; }

        public IList<AsriRoot> ListParameterType
        {
            get
            {
                if (listparametertype == null)
                {
                    listparametertype = new ObservableCollection<AsriRoot>();
                }
                return listparametertype;
            }
            set { listparametertype = value; }
        }
    }
}
