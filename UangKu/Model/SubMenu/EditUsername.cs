using System.Collections.ObjectModel;
using UangKu.Model.Base;
using static UangKu.Model.Response.AppStandardReferenceItem.AppStandardReferenceItem;

namespace UangKu.Model.SubMenu
{
    public class EditUsername : BaseModel
    {
        private string title = string.Empty;
        public string Title { get => title; set => SetProperty(ref title, value); }
        private bool isbusy = false;
        public bool IsBusy { get => isbusy; set => SetProperty(ref isbusy, value); }
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
    }
}
