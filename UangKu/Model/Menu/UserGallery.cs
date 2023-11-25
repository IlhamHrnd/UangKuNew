using System.Collections.ObjectModel;
using UangKu.Model.Base;
using UangKu.Model.Response.Picture;
using static UangKu.Model.Response.Picture.UserPicture;

namespace UangKu.Model.Menu
{
    public class UserGallery : BaseModel
    {
        private string title = string.Empty;
        public string Title { get => title; set => SetProperty(ref title, value); }
        private bool isbusy = false;
        public bool IsBusy { get => isbusy; set => SetProperty(ref isbusy, value); }
        private int page = 0;
        public int Page { get => page; set => page = value; }
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
        private IList<UserPictureTwo.Datum> listuserpicturetwo { get; set; }
        public IList<UserPictureTwo.Datum> ListUserPictureTwo
        {
            get
            {
                if (listuserpicturetwo == null)
                {
                    listuserpicturetwo = new ObservableCollection<UserPictureTwo.Datum>();
                }
                return listuserpicturetwo;
            }
            set { listuserpicturetwo = value; }
        }
        private IList<DifferentUserPicture.Datum> listdifferentuserpicture { get; set; }
        public IList<DifferentUserPicture.Datum> ListDifferentUserPicture
        {
            get
            {
                if (listdifferentuserpicture == null)
                {
                    listdifferentuserpicture = new ObservableCollection<DifferentUserPicture.Datum>();
                }
                return listdifferentuserpicture;
            }
            set { listdifferentuserpicture = value; }
        }
    }
}
