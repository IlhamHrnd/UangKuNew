using System.Collections.ObjectModel;
using UangKu.Model.Base;
using static UangKu.Model.Response.Picture.UserPicture;

namespace UangKu.Model.Menu
{
    public class UserGallery : BaseModel
    {
        private string title = string.Empty;
        public string Title { get => title; set => SetProperty(ref title, value); }
        private bool isbusy = false;
        public bool IsBusy { get => isbusy; set => SetProperty(ref isbusy, value); }
        private IList<UserPictureRoot> listuserpicture { get; set; }
        public IList<UserPictureRoot> ListUserPicture
        {
            get
            {
                if (listuserpicture == null)
                {
                    listuserpicture = new ObservableCollection<UserPictureRoot>();
                }
                return listuserpicture;
            }
            set { listuserpicture = value; }
        }
    }
}
