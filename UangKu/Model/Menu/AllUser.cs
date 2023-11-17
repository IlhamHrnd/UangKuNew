using System.Collections.ObjectModel;
using UangKu.Model.Base;
using static UangKu.Model.Response.User.AllUser;

namespace UangKu.Model.Menu
{
    public class AllUser : BaseModel
    {
        public AllUser()
        {
            
        }
        private string title = "Home Page";
        public string Title { get => title; set => SetProperty(ref title, value); }
        private bool isbusy = false;
        public bool IsBusy { get => isbusy; set => SetProperty(ref isbusy, value); }
        private IList<AllUserRoot> listalluser { get; set; }

        public IList<AllUserRoot> ListAllUser
        {
            get
            {
                if (listalluser == null)
                {
                    listalluser = new ObservableCollection<AllUserRoot>();
                }
                return listalluser;
            }
            set { listalluser = value; }
        }
    }
}
