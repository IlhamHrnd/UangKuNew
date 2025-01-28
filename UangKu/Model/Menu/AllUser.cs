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

        private IList<WebService.Data.User.Data> listalluser { get; set; }

        public IList<WebService.Data.User.Data> ListAllUser
        {
            get
            {
                if (listalluser == null)
                {
                    listalluser = new ObservableCollection<WebService.Data.User.Data>();
                }
                return listalluser;
            }
            set { listalluser = value; }
        }
    }
}
