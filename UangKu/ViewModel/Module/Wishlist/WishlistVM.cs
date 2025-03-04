using System.Collections.ObjectModel;
using UangKu.Model.Base;
using UangKu.Model.Session;

namespace UangKu.ViewModel.Module.Wishlist
{
    public class WishlistVM : Model.Module.Wishlist.Wishlist
    {
        public WishlistVM(INavigation navigation)
        {
            Navigation = navigation;
        }
        public void LoadData()
        {
            if (!Network.IsConnected)
                return;

            AppProgram(Model.Base.AppProgram.Wishlist);
            LoadMyWishlist(ItemManager.FirstPage, null);
            LoadCategoryWishlist(null);
        }

        private async void LoadMyWishlist(int pageNumber, bool? isComplete)
        {
            IsBusy = true;
            try
            {
                var wishlist = await WebService.Service.UserWishlist.GetAllUserWishlist(new WebService.Filter.Root<WebService.Filter.UserWishlist>
                {
                    Data = new WebService.Filter.UserWishlist
                    {
                        PersonID = UserID,
                        IsComplete = isComplete
                    },
                    PageNumber = pageNumber,
                    PageSize = AppParameter.MaxResult
                });

                if (wishlist.Succeeded == false)
                {
                    await MsgModel.MsgNotification(wishlist.Message);
                    return;
                }

                if (wishlist.Data.Count > 0)
                {
                    foreach (var item in wishlist.Data)
                    {
                        if (item.productPrice != null)
                            item.priceFormat = FormatCurrency.Currency((decimal)item.productPrice, AppParameter.CurrencyFormat);

                        if (!string.IsNullOrEmpty(item.productPicture))
                        {
                            string decode = Converter.DecodeBase64ToString(item.productPicture);
                            byte[] img = Converter.StringToByteImg(decode);
                            item.source = ImageConvert.ImgByte(img);
                        }
                    }
                }

                MyWishlist = new WebService.Data.Root<ObservableCollection<WebService.Data.UserWishlist.Data>>
                {
                    Data = new ObservableCollection<WebService.Data.UserWishlist.Data>(wishlist.Data),
                    Succeeded = wishlist.Succeeded,
                    Errors = wishlist.Errors,
                    Message = wishlist.Message
                };

                PageNumber = wishlist.pageNumber ?? 1;
                PageSize = wishlist.pageSize ?? 1;
                TotalPages = wishlist.totalPages ?? 1;
                TotalRecords = wishlist.totalRecords ?? 1;
                PrevPageLink = (string)wishlist.prevPageLink;
                NextPageLink = (string)wishlist.nextPageLink;
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

        private async void LoadCategoryWishlist(bool? isComplete)
        {
            IsBusy = true;
            try
            {
                var category = await WebService.Service.UserWishlist.GetUserWishlistPerCategory(new WebService.Filter.Root<WebService.Filter.UserWishlist>
                {
                    Data = new WebService.Filter.UserWishlist
                    {
                        PersonID = UserID,
                        IsComplete = isComplete
                    }
                });

                if (category.Succeeded == false)
                {
                    await MsgModel.MsgNotification(category.Message);
                    return;
                }

                if (category.Data.Count > 0)
                {
                    foreach (var item in category.Data)
                    {
                        if (!string.IsNullOrEmpty(item.productPicture))
                        {
                            string decode = Converter.DecodeBase64ToString(item.productPicture);
                            byte[] img = Converter.StringToByteImg(decode);
                            item.source = ImageConvert.ImgByte(img);
                        }
                    }
                }

                WishlistCategory = new WebService.Data.Root<ObservableCollection<WebService.Data.UserWishlist.Data>>
                {
                    Data = new ObservableCollection<WebService.Data.UserWishlist.Data>(category.Data),
                    Succeeded = category.Succeeded,
                    Errors = category.Errors,
                    Message = category.Message
                };
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

        public void ScrollTopBottom(bool isScrollTop, ScrollView scroll)
        {
            double y = scroll.ContentSize.Height;

            if (isScrollTop)
                scroll.ScrollToAsync(0, 0, true);
            else
                scroll.ScrollToAsync(0, y, true);
        }

        public async Task SwipeItem(object sender, string mode)
        {
            if (sender is not SwipeItem item || item.BindingContext is not WebService.Data.UserWishlist.Data data)
            {
                await MsgModel.MsgNotification(ItemManager.Empty);
                return;
            }

            switch (mode)
            {
                case "right":
                    if (string.IsNullOrEmpty(data.productLink))
                    {
                        await MsgModel.MsgNotification(ItemManager.Empty);
                        return;
                    }

                    await Launcher.OpenAsync(data.productLink);
                    break;

                case "left":
                    await Navigation.PushAsync(new View.Module.Wishlist.WishlistEdit(ItemManager.EditFile, data.wishlistId));
                    break;
            }
        }
    }
}
