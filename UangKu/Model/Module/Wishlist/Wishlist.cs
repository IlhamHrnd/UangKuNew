using System.Collections.ObjectModel;
using UangKu.Model.Base;
using UangKu.WebService.Data;

namespace UangKu.Model.Module.Wishlist
{
    public class Wishlist : BaseModel
    {
        private Root<ObservableCollection<UserWishlist.Data>> mywishlist;
        public Root<ObservableCollection<UserWishlist.Data>> MyWishlist
        {
            get
            {
                if (mywishlist == null)
                {
                    mywishlist = new Root<ObservableCollection<UserWishlist.Data>>
                    {
                        Data = new ObservableCollection<UserWishlist.Data>(),
                        Succeeded = true,
                        Errors = null,
                        Message = "Initialized"
                    };
                }
                return mywishlist;
            }
            set
            {
                mywishlist = value;
                OnPropertyChanged(nameof(MyWishlist));
            }
        }
        private Root<ObservableCollection<UserWishlist.Data>> wishlistcategory;
        public Root<ObservableCollection<UserWishlist.Data>> WishlistCategory
        {
            get
            {
                if (wishlistcategory == null)
                {
                    wishlistcategory = new Root<ObservableCollection<UserWishlist.Data>>
                    {
                        Data = new ObservableCollection<UserWishlist.Data>(),
                        Succeeded = true,
                        Errors = null,
                        Message = "Initialized"
                    };
                }
                return wishlistcategory;
            }
            set
            {
                wishlistcategory = value;
                OnPropertyChanged(nameof(WishlistCategory));
            }
        }
    }
}
