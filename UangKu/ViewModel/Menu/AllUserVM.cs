using UangKu.Model.Base;
using UangKu.Model.Menu;
using UangKu.Model.Session;
using UangKu.ViewModel.RestAPI.User;
using UangKu.WebService.Service;

namespace UangKu.ViewModel.Menu
{
    public class AllUserVM : AllUser
    {
        private NetworkModel network = NetworkModel.Instance;

        private readonly INavigation _navigation;

        public AllUserVM(INavigation navigation)
        {
            Title = $"List User";
            _navigation = navigation;
        }
        public async void LoadData()
        {
            bool isConnect = network.IsConnected;
            IsBusy = true;
            try
            {
                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }

                var filter = new WebService.Filter.Root<WebService.Filter.User>
                {
                    PageNumber = ParameterModel.ItemDefaultValue.FirstPage,
                    PageSize = 15
                };
                var alluser = await User.GetAllUser(filter);
                if (alluser.Succeeded == true)
                {
                    ListAllUser.Clear();
                    foreach (var data in alluser.Data)
                    {
                        if (!string.IsNullOrEmpty(data.Srstatus))
                        {
                            data.isActive = data.Srstatus == ParameterModel.Login.Status;
                        }

                        if (data.ActiveDate != null)
                        {
                            data.dateActive = DateFormat.FormattingDate((DateTime)data.ActiveDate, ParameterModel.DateTimeFormat.Date);
                        }

                        if (data.LastLogin != null)
                        {
                            data.dateLogin = DateFormat.FormattingDate((DateTime)data.LastLogin, ParameterModel.DateTimeFormat.Date);
                        }
                        ListAllUser.Add(data);
                    }
                    Page = (int)alluser.pageNumber;
                    TotalRecords = (int)alluser.totalRecords;
                    TotalPages = (int)alluser.totalPages;
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
        public async void NextPreviousPage_Clicked(int pageSize, bool isNext)
        {
            bool isConnect = network.IsConnected;
            IsBusy = true;
            try
            {
                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }
                if (Page >= TotalPages && isNext)
                {
                    await MsgModel.MsgNotification("This Is The Latest Page");
                }
                else if (Page <= 1 && !isNext)
                {
                    await MsgModel.MsgNotification("This Is The First Page");
                }
                else
                {
                    int pages = isNext ? Page + 1 : Page - 1;
                    var filter = new WebService.Filter.Root<WebService.Filter.User>
                    {
                        PageNumber = pages,
                        PageSize = pageSize
                    };
                    var alluser = await User.GetAllUser(filter);
                    if (alluser.Succeeded == true)
                    {
                        ListAllUser.Clear();
                        foreach (var data in alluser.Data)
                        {
                            if (!string.IsNullOrEmpty(data.Srstatus))
                            {
                                data.isActive = data.Srstatus == ParameterModel.Login.Status;
                            }

                            if (data.ActiveDate != null)
                            {
                                data.dateActive = DateFormat.FormattingDate((DateTime)data.ActiveDate, ParameterModel.DateTimeFormat.Date);
                            }

                            if (data.LastLogin != null)
                            {
                                data.dateLogin = DateFormat.FormattingDate((DateTime)data.LastLogin, ParameterModel.DateTimeFormat.Date);
                            }
                            ListAllUser.Add(data);
                        }
                        Page = (int)alluser.pageNumber;
                        TotalRecords = (int)alluser.totalRecords;
                        TotalPages = (int)alluser.totalPages;
                    }
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
        public async Task AllUser_PopUp(SelectionChangedEventArgs args)
        {
            var userID = args.CurrentSelection[0] as WebService.Data.User.Data;
            var itemID = userID?.Username;
            if (!string.IsNullOrEmpty(itemID))
            {
                await _navigation.PushAsync(new View.SubMenu.EditUsername(itemID));
            }
            else
            {
                await MsgModel.MsgNotification($"You Haven't Selected An Item Yet");
            }
        }
    }
}
