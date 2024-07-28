using UangKu.Model.Base;
using UangKu.Model.Menu;
using UangKu.Model.Response.Wishlist;
using UangKu.Model.Session;

namespace UangKu.ViewModel.Menu
{
    public class WishlistVM : Wishlist
    {
        private NetworkModel network = NetworkModel.Instance;
        private readonly INavigation _navigation;

        public WishlistVM(INavigation navigation)
        {
            _navigation = navigation;
            Title = "Wishlist";
        }

        public async Task LoadData(int pageNumber, int pageSize)
        {
            bool isConnect = network.IsConnected;
            IsBusy = true;
            try
            {
                var sessionID = App.Session;
                string userID = SessionModel.GetUserID(sessionID);

                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }
                var allwish = await RestAPI.Wishlist.GetAllUserWishlist.GetAllWishlist(userID, pageNumber, pageSize);
                if (allwish.metaData.isSucces && allwish.metaData.code == 200)
                {
                    ListWishlist.Clear();
                    foreach (GetAllUserWishlist.Datum item in allwish.data)
                    {
                        if (item.productPrice != null)
                        {
                            item.priceFormat = FormatCurrency.Currency((decimal)item.productPrice, AppParameter.CurrencyFormat);
                        }

                        if (!string.IsNullOrEmpty(item.productPicture))
                        {
                            string decodeImg = Converter.DecodeBase64ToString(item.productPicture);
                            byte[] byteImg = Converter.StringToByteImg(decodeImg);
                            item.source = ImageConvert.ImgByte(byteImg);
                        }

                        if (item.wishlistDate != null)
                        {
                            item.wishlistDateFormat = DateFormat.FormattingDate((DateTime)item.wishlistDate, ParameterModel.DateTimeFormat.Date);
                        }
                    }
                    Page = (int)allwish.pageNumber;
                    TotalRecords = (int)allwish.totalRecords;
                    TotalPages = (int)allwish.totalPages;
                    ListWishlist.Add(allwish);

                    var wishlistcategory = await RestAPI.Wishlist.GetUserWishlistPerCategory.UserWishlistPerCategory(userID, false);
                    if (wishlistcategory.metaData.isSucces && wishlistcategory.metaData.code == 200)
                    {
                        ListWishlistCategory.Clear();
                        foreach (var item in wishlistcategory.data)
                        {
                            if (!string.IsNullOrEmpty(item.itemIcon))
                            {
                                string decodeImg = Converter.DecodeBase64ToString(item.itemIcon);
                                byte[] byteImg = Converter.StringToByteImg(decodeImg);
                                item.source = ImageConvert.ImgByte(byteImg);
                            }
                            ListWishlistCategory.Add(item);
                        }
                    }
                    else
                    {
                        await MsgModel.MsgNotification(wishlistcategory.metaData.message);
                    }
                }
                else
                {
                    await MsgModel.MsgNotification(allwish.metaData.message);
                }
            }
            catch (Exception e)
            {
                await MsgModel.MsgNotification(e.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task SwipeItem_Invoked(object sender, string mode)
        {
            var item = sender as SwipeItem;
            if (item == null)
            {
                return;
            }

            var wishlistID = item.BindingContext as GetAllUserWishlist.Datum;
            if (wishlistID == null)
            {
                return;
            }

            await _navigation.PushAsync(new View.SubMenu.WishlistEdit(mode, wishlistID.wishlistID));
        }

        public async Task WishlistNew_ToolBar(string mode)
        {
            await _navigation.PushAsync(new View.SubMenu.WishlistEdit(mode, string.Empty));
        }
    }
}