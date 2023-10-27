using System.Collections.ObjectModel;
using UangKu.Model.Base;
using static UangKu.Model.Response.Profile.GetProfile;

namespace UangKu.Model.Menu
{
    public class Profile : BaseModel
    {
        private string title = string.Empty;
        public string Title { get => title; set => SetProperty(ref title, value); }
        private bool isbusy = false;
        public bool IsBusy { get => isbusy; set => SetProperty(ref isbusy, value); }
        private IList<GetProfileRoot> profiles { get; set; }
        public IList<GetProfileRoot> Profiles
        {
            get
            {
                if (profiles == null)
                {
                    profiles = new ObservableCollection<GetProfileRoot>();
                }
                return profiles;
            }
            set { profiles = value; }
        }
    }
}
