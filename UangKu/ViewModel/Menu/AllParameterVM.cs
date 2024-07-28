using UangKu.Model.Base;
using UangKu.Model.Menu;
using UangKu.View.SubMenu;
using static UangKu.Model.Response.AppParameter.GetAllAppParameter;

namespace UangKu.ViewModel.Menu
{
    public class AllParameterVM : AllParameter
    {
        private NetworkModel network = NetworkModel.Instance;
        private readonly INavigation _navigation;
        public AllParameterVM(INavigation navigation)
        {
            Title = "App Parameter";
            _navigation = navigation;
        }

        public async void LoadData(int pageNumber, int pageSize)
        {
            bool isConnect = network.IsConnected;
            IsBusy = true;
            try
            {
                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }
                var param = await RestAPI.AppParameter.AllAppParameter.GetAllAppParameter(pageNumber, pageSize);
                if (param.metaData.isSucces && param.metaData.code == 200)
                {
                    ListParameter.Clear();
                    foreach (var data in param.data)
                    {
                        if (data.lastUpdateDateTime != null)
                        {
                            data.lastUpdateDateTimeString = DateFormat.FormattingDate((DateTime)data.lastUpdateDateTime, ParameterModel.DateTimeFormat.Daydatemonthyear);
                        }
                    }
                    Page = (int)param.pageNumber;
                    TotalRecords = (int)param.totalRecords;
                    TotalPages = (int)param.totalPages;
                    ListParameter.Add(param);
                }
                else
                {
                    await MsgModel.MsgNotification(param.metaData.message);
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
                    var param = await RestAPI.AppParameter.AllAppParameter.GetAllAppParameter(pages, pageSize);
                    if (param.metaData.isSucces && param.metaData.code == 200)
                    {
                        ListParameter.Clear();
                        foreach (var data in param.data)
                        {
                            if (data.lastUpdateDateTime != null)
                            {
                                data.lastUpdateDateTimeString = DateFormat.FormattingDate((DateTime)data.lastUpdateDateTime, ParameterModel.DateTimeFormat.Daydatemonthyear);
                            }
                        }
                        Page = (int)param.pageNumber;
                        TotalRecords = (int)param.totalRecords;
                        TotalPages = (int)param.totalPages;
                        ListParameter.Add(param);
                    }
                    else
                    {
                        await MsgModel.MsgNotification(param.metaData.message);
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

        public async Task SwipeItem_Invoked(object sender, string mode)
        {
            var item = sender as SwipeItem;
            if (item == null)
            {
                return;
            }

            var parameterID = item.BindingContext as Datum;
            if (parameterID == null)
            {
                return;
            }

            await _navigation.PushAsync(new AddAppParameter(mode, parameterID.parameterID));
        }

        public async Task AddAppParameter_ToolBar()
        {
            await _navigation.PushAsync(new AddAppParameter(ParameterModel.ItemDefaultValue.NewFile, string.Empty));
        }
    }
}
