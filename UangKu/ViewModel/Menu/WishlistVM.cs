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
                if ((bool)allwish.succeeded && allwish.data.Count > 0)
                {
                    ListWishlist.Clear();
                    var item = allwish;
                    var datas = item.data;
                    for (int i = 0; i < datas.Count; i++)
                    {
                        if (datas[i].productPrice != null)
                        {
                            datas[i].priceFormat = FormatCurrency.Currency((decimal)datas[i].productPrice, Compare.StringReplace(AppParameter.CurrencyFormat, ParameterModel.AppParameterDefault.Currency));
                        }

                        if (!string.IsNullOrEmpty(datas[i].productPicture))
                        {
                            string decodeImg = ImageConvert.DecodeBase64ToString(datas[i].productPicture);
                            byte[] byteImg = ImageConvert.StringToByteImg(decodeImg);
                            datas[i].source = ImageConvert.ImgByte(byteImg);
                        }

                        if (datas[i].wishlistDate != null)
                        {
                            datas[i].wishlistDateFormat = DateFormat.FormattingDate((DateTime)datas[i].wishlistDate, ParameterModel.DateTimeFormat.Date);
                        }
                    }
                    Page = (int)allwish.pageNumber;
                    ListWishlist.Add(item);
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