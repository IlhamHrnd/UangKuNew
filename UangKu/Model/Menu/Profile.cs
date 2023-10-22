using System.Collections.ObjectModel;
using UangKu.Model.Base;
using static UangKu.Model.Response.Profile.Profile;

namespace UangKu.Model.Menu
{
    public class Profile : BaseModel
    {
        private string title = string.Empty;
        public string Title { get => title; set => SetProperty(ref title, value); }
        private bool isbusy = false;
        public bool IsBusy { get => isbusy; set => SetProperty(ref isbusy, value); }
        private IList<ProfileRoot> profiles { get; set; }
        public IList<ProfileRoot> Profiles
        {
            get
            {
                if (profiles == null)
                {
                    profiles = new ObservableCollection<ProfileRoot>();
                }
                return profiles;
            }
            set { profiles = value; }
        }
    }
}
