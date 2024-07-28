using System.Collections.ObjectModel;
using UangKu.Model.Base;
using static UangKu.Model.Response.Wishlist.GetAllUserWishlist;
using static UangKu.Model.Response.Wishlist.GetUserWishlistPerCategory;

namespace UangKu.Model.Menu
{
    public class Wishlist : BaseModel
    {
        public Wishlist()
        {
            
        }
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
        private IList<Model.Response.Wishlist.GetUserWishlistPerCategory.Datum> listwishlistcategory { get; set; }
        public IList<Model.Response.Wishlist.GetUserWishlistPerCategory.Datum> ListWishlistCategory
        {
            get
            {
                if (listwishlistcategory == null)
                {
                    listwishlistcategory = new ObservableCollection<Model.Response.Wishlist.GetUserWishlistPerCategory.Datum>();
                }
                return listwishlistcategory;
            }
            set { listwishlistcategory= value; }
        }
    }
}
