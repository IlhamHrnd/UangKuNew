using UangKu.Model.Base;
using static UangKu.Model.Response.AppStandardReferenceItem.AppStandardReferenceItem;

namespace UangKu.ViewModel.Menu
{
    public class AppStandardReferenceItemVM : Model.Menu.AppStandardReferenceItem
    {
        private NetworkModel network = NetworkModel.Instance;
        private string Id { get; }
        public AppStandardReferenceItemVM(string id)
        {
            Id = id;
        }
        public async void LoadData()
        {
            bool isConnect = network.IsConnected;
            IsBusy = true;
            try
            {
                if (!isConnect)
                {
                    await MsgModel.MsgNotification("You're Offline");
                }
                var asrid = await RestAPI.AppStandardReferenceItem.GetAppStandardReferenceID.GetASRId(Id);
                if (asrid != null)
                {
                    ListASR.Clear();
                    ListASR.Add(asrid);
                    Title = $"Item For {ListASR[0].standardReferenceID}";
                }
                var asri = await RestAPI.AppStandardReferenceItem.AppStandardReferenceItem.GetAsriAsync<AsriRoot>(Id, true, true);
                if (asri.Count > 0)
                {
                    ListASRI.Clear();
                    for (int i = 0; i < asri.Count; i++)
                    {
                        ListASRI.Add(asri[i]);
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
    }
}
