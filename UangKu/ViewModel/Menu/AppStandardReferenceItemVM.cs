using UangKu.Model.Base;
using UangKu.ViewModel.RestAPI.AppStandardReferenceItem;
using static UangKu.Model.Base.ParameterModel.PermissionManager;
using static UangKu.Model.Response.AppStandardReferenceItem.AppStandardReferenceItem;

namespace UangKu.ViewModel.Menu
{
    public class AppStandardReferenceItemVM : Model.Menu.AppStandardReferenceItem
    {
        private NetworkModel network = NetworkModel.Instance;
        public AppStandardReferenceItemVM(string itemID)
        {
            Id = itemID;
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
                var asrid = await GetAppStandardReferenceID.GetASRAsync(Id);
                if (asrid.metaData.isSucces && asrid.metaData.code == 200)
                {
                    ListASR.Clear();
                    ListASR.Add(asrid.data);
                }
                else
                {
                    await MsgModel.MsgNotification(asrid.metaData.message);
                }
                var asri = await AppStandardReferenceItem.GetAsriAsync<AsriRoot>(Id, false, false);
                if (asri.Count > 0)
                {
                    ListASRI.Clear();
                    for (int i = 0; i < asri.Count; i++)
                    {
                        ListASRI.Add(asri[i]);
                    }

                    ListASRITwo.Clear();
                    foreach (var item in asri)
                    {
                        var data = new AsriTwoRoot
                        {
                            standardReferenceID = item.standardReferenceID,
                            itemID = item.itemID,
                            itemName = item.itemName,
                            note = item.note,
                            isUsedBySystem = item.isUsedBySystem,
                            isActive = item.isActive,
                            lastUpdateDateTime = item.lastUpdateDateTime,
                            lastUpdateByUserID = item.lastUpdateByUserID,
                            itemIcon = item.itemIcon
                        };
                        ListASRITwo.Add(data);
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
            var sessionID = App.Session;
            string userID = SessionModel.GetUserID(sessionID);
            bool isConnect = network.IsConnected;
            IsBusy = true;
            try
            {
                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }
                foreach (var item in ListASR)
                {
                    var reference = item.standardReferenceID;
                    var length = item.itemLength.HasValue ? (int)item.itemLength : 0;
                    var active = item.isActive ?? false;
                    var use = item.isUsedBySystem ?? false;
                    var note = string.IsNullOrEmpty(item.note) ? "-" : item.note;

                    var updateASR = await UpdateAppStandardReference.PatchASR(Id, length, active, use, userID, note);
                    if (updateASR != null)
                    {
                        await MsgModel.MsgNotification(updateASR);
                    }
                }
                if (ListASRI.Count != ListASRITwo.Count)
                {
                    await MsgModel.MsgNotification($"Data List Is Different");
                }
                else
                {
                    for (int i = 0; i < ListASRI.Count; i++)
                    {
                        if (ListASRI[i].note != ListASRITwo[i].note || ListASRI[i].isUsedBySystem != ListASRITwo[i].isUsedBySystem
                            || ListASRI[i].isActive != ListASRITwo[i].isActive || ListASRI[i].itemName != ListASRITwo[i].itemName)
                        {
                            var different = new AsriThreeRoot
                            {
                                standardReferenceID = ListASRI[i].standardReferenceID,
                                itemID = ListASRI[i].itemID,
                                itemName = ListASRI[i].itemName,
                                note = ListASRI[i].note,
                                isUsedBySystem = ListASRI[i].isUsedBySystem,
                                isActive = ListASRI[i].isActive,
                                lastUpdateDateTime = ListASRI[i].lastUpdateDateTime,
                                lastUpdateByUserID = ListASRI[i].lastUpdateByUserID
                            };
                            ListDifferentASRI.Add(different);
                        }
                    }
                }
                if (ListDifferentASRI.Count > 0)
                {
                    for (int i = 0; i < ListDifferentASRI.Count; i++)
                    {
                        var item = ListASRI[i];
                        var reference = item.standardReferenceID;
                        var itemid = item.itemID;
                        var itemname = item.itemName;
                        var note = string.IsNullOrEmpty(item.note) ? "-" : item.note;
                        var active = item.isActive ?? false;
                        var use = item.isUsedBySystem ?? false;

                        var updateASRI = await UpdateAppStandardReferenceItem.PatchASRI(Id, itemid, itemname, note, active, use, userID);
                        if (updateASRI != null)
                        {
                            await MsgModel.MsgNotification(updateASRI);
                        }
                    }
                }
                else
                {
                    await MsgModel.MsgNotification($"No Standard Reference Are Selected For Update");
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
                ListDifferentASRI.Clear();
            }
        }

        public async Task UploadASRIIcon_Click()
        {
            PermissionType type = PermissionType.StorageRead;
            await PermissionRequest.RequestPermission(type);
            _ = await ImageConvert.PickImageAsync();
        }
    }
}
