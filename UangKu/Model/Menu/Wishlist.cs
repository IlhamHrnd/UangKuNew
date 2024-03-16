using System.Collections.ObjectModel;
using UangKu.Model.Base;
using static UangKu.Model.Response.Wishlist.GetAllUserWishlist;

namespace UangKu.Model.Menu
{
    public class Wishlist : BaseModel
    {
        public Wishlist()
        {
            
        }

        private string title = string.Empty;
        public string Title { get => title; set => SetProperty(ref title, value); }
        private bool isbusy = false;
        public bool IsBusy { get => isbusy; set => SetProperty(ref isbusy, value); }
        private int page = 0;
        public int Page { get => page; set => page = value; }
        private IList<GetAllUserWishlistRoot> listwishlist { get; set; }

        public IList<GetAllUserWishlistRoot> ListWishlist
        {
            get
            {
                if (listwishlist == null)
                {
                    listwishlist = new ObservableCollection<GetAllUserWishlistRoot>();
                }
                return listwishlist;
            }
            set { listwishlist = value; }
        }
    }
}
