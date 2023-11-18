using UangKu.Model.Base;
using UangKu.Model.Menu;
using UangKu.ViewModel.RestAPI.User;

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
                var alluser = await UserAll.GetAllUser(ParameterModel.ItemDefaultValue.FirstPage, ParameterModel.ItemDefaultValue.Maxresult);
                if ((bool)alluser.succeeded)
                {
                    ListAllUser.Clear();
                    for (int i = 0; i < alluser.data.Count; i++)
                    {
                        var data = alluser.data[i];
                        if (!string.IsNullOrEmpty(data.statusName))
                        {
                            data.isActive = data.statusName == ParameterModel.Login.Status;
                        }

                        if (data.activeDate != null)
                        {
                            data.dateActive = DateFormat.FormattingDate((DateTime)data.activeDate);
                        }

                        if (data.lastLogin != null)
                        {
                            data.dateLogin = DateFormat.FormattingDate((DateTime)data.lastLogin);
                        }
                    }
                    Page = (int)alluser.pageNumber;
                    ListAllUser.Add(alluser);
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
        public async void NextPage_Clicked(int pageSize)
        {
            var maxPage = ListAllUser[0].totalPages;
            bool isConnect = network.IsConnected;
            IsBusy = true;
            try
            {
                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }
                if (Page >= maxPage)
                {
                    await MsgModel.MsgNotification("This Is The Latest Page");
                }
                else
                {
                    var alluser = await UserAll.GetAllUser(Page + 1, pageSize);
                    if ((bool)alluser.succeeded)
                    {
                        ListAllUser.Clear();
                        for (int i = 0; i < alluser.data.Count; i++)
                        {
                            var data = alluser.data[i];
                            if (!string.IsNullOrEmpty(data.statusName))
                            {
                                data.isActive = data.statusName == ParameterModel.Login.Status;
                            }

                            if (data.activeDate != null)
                            {
                                data.dateActive = DateFormat.FormattingDate((DateTime)data.activeDate);
                            }

                            if (data.lastLogin != null)
                            {
                                data.dateLogin = DateFormat.FormattingDate((DateTime)data.lastLogin);
                            }
                        }
                        Page = (int)alluser.pageNumber;
                        ListAllUser.Add(alluser);
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
        public async void PreviousPage_Clicked(int pageSize)
        {
            bool isConnect = network.IsConnected;
            IsBusy = true;
            try
            {
                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }
                if (Page <= 1)
                {
                    await MsgModel.MsgNotification("This Is The First Page");
                }
                else
                {
                    var alluser = await UserAll.GetAllUser(Page - 1, pageSize);
                    if ((bool)alluser.succeeded)
                    {
                        ListAllUser.Clear();
                        for (int i = 0; i < alluser.data.Count; i++)
                        {
                            var data = alluser.data[i];
                            if (!string.IsNullOrEmpty(data.statusName))
                            {
                                data.isActive = data.statusName == ParameterModel.Login.Status;
                            }

                            if (data.activeDate != null)
                            {
                                data.dateActive = DateFormat.FormattingDate((DateTime)data.activeDate);
                            }

                            if (data.lastLogin != null)
                            {
                                data.dateLogin = DateFormat.FormattingDate((DateTime)data.lastLogin);
                            }
                        }
                        Page = (int)alluser.pageNumber;
                        ListAllUser.Add(alluser);
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
            var userID = args.CurrentSelection[0] as Model.Response.User.AllUser.Datum;
            var itemID = userID?.username;
            ParameterModel.User.UserID = itemID;

            if (!string.IsNullOrEmpty(itemID))
            {
                await _navigation.PushAsync(new View.SubMenu.EditUsername());
            }
            else
            {
                await MsgModel.MsgNotification($"You Haven't Selected An Item Yet");
            }
        }
    }
}
