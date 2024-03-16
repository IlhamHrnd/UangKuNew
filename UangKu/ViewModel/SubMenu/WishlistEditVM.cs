using CommunityToolkit.Maui.Views;
using UangKu.Model.Base;
using UangKu.Model.Session;
using UangKu.Model.SubMenu;
using static UangKu.Model.Response.AppStandardReferenceItem.AppStandardReferenceItem;

namespace UangKu.ViewModel.SubMenu
{
    public class WishlistEditVM : WishlistEdit
    {
        private NetworkModel network = NetworkModel.Instance;
        public WishlistEditVM(string mode, string wishlistID)
        {
            Mode = mode;
            WishlistID = wishlistID;
        }

        public async Task LoadData(Label Lbl_ProductName, Button Btn_WishlistAction, AvatarView Avt_ProductPicture, Picker Pic_ProductCategory, Entry Ent_Quantity, 
            Entry Ent_Price, CheckBox CB_IsComplete, DatePicker Date_WishlistDate)
        {
            if (Mode == ParameterModel.ItemDefaultValue.NewFile)
            {
                Title = $"Create New Wishlist";
            }
            else if (Mode == ParameterModel.ItemDefaultValue.EditFile)
            {
                Title = $"Edit Wishlist {WishlistID}";
            }
            else if (Mode == ParameterModel.ItemDefaultValue.ViewFile)
            {
                Title = $"View Wishlist {WishlistID}";
            }

            bool isConnect = network.IsConnected;
            IsBusy = true;
            try
            {
                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }
                if (Mode == ParameterModel.ItemDefaultValue.NewFile)
                {
                    IsEnabled = true;
                    IsVisible = true;
                    IsEnabled2 = !IsEnabled;
                    IsVisible2 = !IsVisible;
                    Btn_WishlistAction.Text = $"Add New Wishlist";

                    LoadASRI("Wishlist", true, true, ListWishList);
                }
                else if (Mode == ParameterModel.ItemDefaultValue.EditFile)
                {
                    IsEnabled = true;
                    IsVisible = true;
                    IsEnabled2 = !IsEnabled;
                    IsVisible2 = !IsVisible;
                    Btn_WishlistAction.Text = $"Edit Wishlist";

                    LoadASRI("Wishlist", true, true, ListWishList);
                    LoadWishList(Lbl_ProductName, Avt_ProductPicture, Pic_ProductCategory, Ent_Quantity,
                        Ent_Price, CB_IsComplete, Date_WishlistDate, true);
                }
                else if (Mode == ParameterModel.ItemDefaultValue.ViewFile)
                {
                    IsEnabled = false;
                    IsVisible = false;
                    IsEnabled2 = !IsEnabled;
                    IsVisible2 = !IsVisible;

                    LoadASRI("Wishlist", true, true, ListWishList);
                    LoadWishList(Lbl_ProductName, Avt_ProductPicture, Pic_ProductCategory, Ent_Quantity,
                        Ent_Price, CB_IsComplete, Date_WishlistDate, false);
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

        #region Load Data Standard Reference And Wishlist
        private async void LoadASRI(string standardID, bool isUsed, bool isActive, IList<AsriRoot> list)
        {
            var wishlistID = await RestAPI.AppStandardReferenceItem.AppStandardReferenceItem.GetAsriAsync<AsriRoot>(standardID, isActive, isUsed);
            if (wishlistID.Count > 0)
            {
                list.Clear();
                for (int i = 0; i < wishlistID.Count; i++)
                {
                    list.Add(wishlistID[i]);
                }
            }
        }

        private async void LoadWishList(Label Lbl_ProductName, AvatarView Avt_ProductPicture, Picker Pic_ProductCategory, Entry Ent_Quantity,
            Entry Ent_Price, CheckBox CB_IsComplete, DatePicker Date_WishlistDate, bool isEdit)
        {
            var wishlist = await RestAPI.Wishlist.GetWishlistID.GetWishlistIDUser(WishlistID);
            if (!string.IsNullOrEmpty(wishlist.wishlistID))
            {
                //Root Model Formatting
                wishlist.priceFormat = FormatCurrency.Currency((decimal)wishlist.productPrice, Compare.StringReplace(AppParameter.CurrencyFormat, ParameterModel.AppParameterDefault.Currency));
                wishlist.wishlistDateFormat = DateFormat.FormattingDate((DateTime)wishlist.wishlistDate, ParameterModel.DateTimeFormat.Date);
                if (!string.IsNullOrEmpty(wishlist.productPicture))
                {
                    string decodeImg = ImageConvert.DecodeBase64ToString(wishlist.productPicture);
                    byte[] byteImg = ImageConvert.StringToByteImg(decodeImg);
                    wishlist.source = ImageConvert.ImgByte(byteImg);
                }

                //XAML Value From Response
                Lbl_ProductName.Text = wishlist.productName;
                Avt_ProductPicture.ImageSource = wishlist.source;
                Avt_ProductPicture.Text = wishlist.srProductCategory;
                Ent_Quantity.Text = Converter.IntToString((int)wishlist.productQuantity);
                CB_IsComplete.IsChecked = (bool)wishlist.isComplete;
                Date_WishlistDate.Date = (DateTime)wishlist.wishlistDate;

                //Formatting Price Hanya Untuk View Mode
                if (isEdit)
                {
                    Ent_Price.Text = Converter.DecimalToString((decimal)wishlist.productPrice);
                }
                else
                {
                    var price = FormatCurrency.Currency((decimal)wishlist.productPrice, Compare.StringReplace(AppParameter.CurrencyFormat, ParameterModel.AppParameterDefault.Currency));
                    Ent_Price.Text = price;
                    LinkProduct = wishlist.productLink;
                }

                //Load Picker From Index
                var newASRIList = Converter.ConvertIListToList(ListWishList);
                int selectedIndex = ControlHelper.GetIndexByName(newASRIList, item => item.itemID, wishlist.srProductCategory);
                Pic_ProductCategory.SelectedIndex = selectedIndex;
            }
        }
        #endregion

        public async Task LinkProduct_Clicked()
        {
            if (string.IsNullOrEmpty(LinkProduct))
            {
                await MsgModel.MsgNotification($"Link Product Is Empty");
            }
            else
            {
                await Launcher.OpenAsync(LinkProduct);
            }
        }
    }
}
