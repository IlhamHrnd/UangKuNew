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
                if ((bool)param.succeeded)
                {
                    ListParameter.Clear();
                    for (int i = 0; i < param.data.Count; i++)
                    {
                        var data = param.data[i];
                        if (data.lastUpdateDateTime != null)
                        {
                            data.lastUpdateDateTimeString = DateFormat.FormattingDate((DateTime)data.lastUpdateDateTime, ParameterModel.DateTimeFormat.Daydatemonthyear);
                        }
                    }
                    Page = (int)param.pageNumber;
                    TotalRecords = (int)param.totalRecords;
                    ListParameter.Add(param);
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
