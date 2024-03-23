using UangKu.Model.Base;
using UangKu.Model.Menu;
using UangKu.View.SubMenu;

namespace UangKu.ViewModel.Menu
{
    public class AppStandardReferenceVM : AppStandardReference
    {
        private NetworkModel network = NetworkModel.Instance;
        private readonly INavigation _navigation;
        public AppStandardReferenceVM(INavigation navigation)
        {
            _navigation = navigation;
            Title = "App Standard Reference";
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
                var asr = await RestAPI.AppStandardReferenceItem.AppStandardReference.GetAllASR(pageNumber, pageSize);
                if (asr.data.Count > 0)
                {
                    ListASR.Clear();
                    ListASR.Add(asr);
                    Page = (int)asr.pageNumber;
                    TotalPages = (int)asr.totalPages;
                    TotalRecords = (int)asr.totalRecords;
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
        public async void NextPreviousPage_Click(int pageSize, bool isNext)
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
                    var asr = await RestAPI.AppStandardReferenceItem.AppStandardReference.GetAllASR(pages, pageSize);
                    if ((bool)asr.succeeded)
                    {
                        ListASR.Clear();
                        ListASR.Add(asr);
                        Page = (int)asr.pageNumber;
                        TotalPages = (int)asr.totalPages;
                        TotalRecords = (int)asr.totalRecords;
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
        public async Task AppStandardReferenceItem_PopUp(SelectionChangedEventArgs args)
        {
            var standardID = args.CurrentSelection[0] as Model.Response.AppStandardReference.AppStandardReference.Datum;
            var itemID = standardID?.standardReferenceID;
            
            if (!string.IsNullOrEmpty(itemID))
            {
                await _navigation.PushAsync(new View.Menu.AppStandardReferenceItem(itemID));
            }
            else
            {
                await MsgModel.MsgNotification($"You Haven't Selected An Item Yet");
            }
        }
        public async Task AddASRItem_ToolBar()
        {
            await _navigation.PushAsync(new AddAppStandardReference());
        }
    }
}
