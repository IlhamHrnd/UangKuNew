using UangKu.Model.Base;

namespace UangKu.Model.Module.Wishlist
{
    public class WishlistEdit : BaseModel
    {
        private string transno = string.Empty;
        public string TransNo { get => transno; set => SetProperty(ref transno, value); }
    }
}
