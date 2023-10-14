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
                var asrid = await GetAppStandardReferenceID.GetASR(Id);
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
                for (int i = 0; i < ListASR.Count; i++)
                {
                    var item = ListASR[i];
                    var reference = item.standardReferenceID;
                    var length = item.itemLength.HasValue ? (int)item.itemLength : 0; ;
                    var active = item.isActive ?? false;
                    var use = item.isUsedBySystem ?? false;
                    var note = string.IsNullOrEmpty(item.note) ? "-" : item.note;

                    var updateASR = await UpdateAppStandardReference.PatchASR(referenceID, length, active, use, userID, note);
                    if (updateASR != null)
                    {
                        await MsgModel.MsgNotification(updateASR);
                    }
                }
                for (int i = 0; i < ListASRI.Count; i++)
                {
                    var item = ListASRI[i];
                    var reference = item.standardReferenceID;
                    var itemid = item.itemID;
                    var itemname = item.itemName;
                    var note = string.IsNullOrEmpty(item.note) ? "-" : item.note;
                    var active = item.isActive ?? false;
                    var use = item.isUsedBySystem ?? false;
                    
                    var updateASRI = await UpdateAppStandardReferenceItem.PatchASRI(referenceID, itemid, itemname, note, active, use, userID);
                    if (updateASRI != null )
                    {
                        await MsgModel.MsgNotification(updateASRI);
                    }
                }
            }
            catch (Exception e)
            {
                await MsgModel.MsgNotification(e.Message);
            }
            finally
            {
                LoadData();
                IsBusy = false;
            }
        }
    }
}
