using System.Collections.ObjectModel;
using UangKu.Model.Base;
using static UangKu.Model.Response.Profile.Profile;

namespace UangKu.Model.Menu
{
    public class Profile : BaseModel
    {
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
