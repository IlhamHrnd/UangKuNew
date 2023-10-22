using System.Collections.ObjectModel;
using UangKu.Model.Base;
using static UangKu.Model.Response.Profile.Profile;

namespace UangKu.Model.SubMenu
{
    public class EditProfile : BaseModel
    {
        private string title = string.Empty;
        public string Title { get => title; set => SetProperty(ref title, value); }
        private bool isbusy = false;
        public bool IsBusy { get => isbusy; set => SetProperty(ref isbusy, value); }
        private IList<ProfileRoot> listperson { get; set; }
        public IList<ProfileRoot> ListPerson
        {
            get
            {
                if (listperson == null)
                {
                    listperson = new ObservableCollection<ProfileRoot>();
                }
                return listperson;
            }
            set { listperson = value; }
        }
    }
}
