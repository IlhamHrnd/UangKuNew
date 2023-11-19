using System.Collections.ObjectModel;
using UangKu.Model.Base;
using static UangKu.Model.Response.Picture.UserPicture;
using static UangKu.Model.Response.Transaction.SumTransaction;

namespace UangKu.Model.Menu
{
    public class Home : BaseModel
    {
        public Home()
        {
            
        }
        private string title = "Home Page";
        public string Title { get => title; set => SetProperty(ref title, value); }
        private string name = string.Empty;
        public string Name { get => name; set => name = value; }
        private string person = string.Empty;
        public string Person { get => person; set => person = value; }
        private bool isbusy = false;
        public bool IsBusy { get => isbusy; set => SetProperty(ref isbusy, value); }
        private string image = string.Empty;
        public string Image { get => image; set => image = value; }
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
