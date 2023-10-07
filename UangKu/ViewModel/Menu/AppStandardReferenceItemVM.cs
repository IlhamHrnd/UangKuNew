using UangKu.Model.Base;
using UangKu.ViewModel.RestAPI.AppStandardReferenceItem;
using static UangKu.Model.Response.AppStandardReferenceItem.AppStandardReferenceItem;

namespace UangKu.ViewModel.Menu
{
    public class AppStandardReferenceItemVM : Model.Menu.AppStandardReferenceItem
    {
        private NetworkModel network = NetworkModel.Instance;
        private string Id { get; set; }
        public AppStandardReferenceItemVM()
        {
            Id = ParameterModel.AppStandardReference.ItemID;
            Title = $"App Standard Reference {Id}";

            LoadData();
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
                var asrid = await GetAppStandardReferenceID.GetASRId(Id);
                if (asrid != null)
                {
                    ListASR.Clear();
                    ListASR.Add(asrid);
                }
                var asri = await AppStandardReferenceItem.GetAsriAsync<AsriRoot>(Id, false, false);
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

        public async Task BtnUpdateAppStandard_Click()
        {
            var referenceID = ParameterModel.AppStandardReference.ItemID;
            var userID = App.Session.username;
            bool isConnect = network.IsConnected;
            IsBusy = true;
            try
            {
                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }
                //Belum Dapat Ambil Data Control(Entry & CheckBox) Dari CollectionView
                var updateASR = await UpdateAppStandardReference.PatchASR(referenceID, 15, true, true, userID, "TESSSSSSSSS");
                if (updateASR != null)
                {
                    await MsgModel.MsgNotification(updateASR);
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
