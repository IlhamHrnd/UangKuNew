using UangKu.Model.Base;
using UangKu.Model.Session;
using UangKu.Model.SubMenu;
using UangKu.ViewModel.RestAPI.AppStandardReferenceItem;
using static UangKu.Model.Base.ParameterModel.PermissionManager;
using static UangKu.Model.Response.AppStandardReferenceItem.AppStandardReferenceItem;

namespace UangKu.ViewModel.SubMenu
{
    public class AddAppStandardReferenceVM : AddAppStandardReferenceModel
    {
        private NetworkModel network = NetworkModel.Instance;
        public AddAppStandardReferenceVM()
        {
            Title = "Add New Standard Reference";
        }

        public async void ReferenceID_Complete(Entry referenceID, Entry referenceName, Entry itemLength, Entry note)
        {
            bool isConnect = network.IsConnected;
            IsBusy = true;
            try
            {
                ListASR.Clear();
                ListASRI.Clear();
                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }
                var asr = await GetAppStandardReferenceID.GetASRAsync(referenceID.Text);
                if (!string.IsNullOrEmpty(asr.standardReferenceID))
                {
                    ListASR.Add(asr);
                }
                switch (ListASR.Count)
                {
                    case > 0:
                        var item = ListASR[0];

                        referenceName.Text = item.standardReferenceName;
                        referenceName.IsReadOnly = true;
                        itemLength.Text = item.itemLength.ToString();
                        itemLength.IsReadOnly = true;
                        note.Text = item.note;
                        note.IsReadOnly = true;

                        var asri = await AppStandardReferenceItem.GetAsriAsync<AsriRoot>(item.standardReferenceID, false, false);
                        if (asr != null)
                        {
                            for (int i = 0; i < asri.Count; i++)
                            {
                                ListASRI.Add(asri[i]);
                            }
                        }
                        break;

                    default:
                        referenceName.Text = string.Empty;
                        referenceName.IsReadOnly = false;
                        itemLength.Text = string.Empty;
                        itemLength.IsReadOnly = false;
                        note.Text = string.Empty;
                        note.IsReadOnly = false;
                        break;
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

        public void ReferenceID_TextChanged(TextChangedEventArgs args, Entry referenceID, Entry referenceName, Entry itemLength, Entry note)
        {
            if (string.IsNullOrEmpty(args.NewTextValue))
            {
                ListASR.Clear();
                ListASRI.Clear();
                referenceID.Text = string.Empty;
                referenceName.Text = string.Empty;
                itemLength.Text = string.Empty;
                note.Text = string.Empty;
                referenceID.IsReadOnly = false;
                referenceName.IsReadOnly = false;
                itemLength.IsReadOnly = false;
                note.IsReadOnly = false;
            }
        }

        public async void AddNewASR(Entry referenceID, Entry referenceName, Entry itemLength, Entry note)
        {
            bool isConnect = network.IsConnected;
            IsBusy = true;
            try
            {
                bool isValidEntry = await ValidateNullChecker.EntryValidateFields(
                    (referenceID.Text, "ReferenceID"),
                    (referenceName.Text, "Reference Name"),
                    (itemLength.Text, "Item Length")
                );

                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }
                if (ListASRI.Count < 0 || ListASRI.Count == 0)
                {
                    await MsgModel.MsgNotification($"Please, Add Item First For Reference ID {referenceID.Text}");
                }
                if (isValidEntry)
                {
                    var addASR = await PostAppStandardReference.PostASR(referenceID.Text, referenceName.Text, Converter.StringToInt(itemLength.Text, 0), note.Text);
                    if (!string.IsNullOrEmpty(addASR))
                    {
                        await MsgModel.MsgNotification($"{addASR}");
                    }

                    if (ListASRI.Count > 0)
                    {
                        for (int i = 0; i < ListASRI.Count; i++)
                        {
                            var addASRI = await PostAppStandardReferenceItem.PostASRI(ListASRI[i].standardReferenceID, ListASRI[i].itemID, 
                                ListASRI[i].itemName, ListASRI[i].note, ListASRI[i].itemIcon);
                            if (!string.IsNullOrEmpty(addASRI))
                            {
                                await MsgModel.MsgNotification($"{addASRI}");
                            }
                        }
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

        public async Task AddASRIItem_ToolBar(Entry StandardID, Entry itemID)
        {
            if (string.IsNullOrEmpty(StandardID.Text))
            {
                await MsgModel.MsgNotification("Standard ID Cannot Null");
            }
            else
            {
                IsEdit = true;
                int number = ListASRI.Count + 1;
                var format = NumericFormat.NumberDigit(number, Compare.StringReplace(AppParameter.NumbericFormat, ParameterModel.AppParameterDefault.NumbericFormat));
                itemID.Text = $"{StandardID.Text}-{format}";
            }
        }

        public async Task AddItem_Click(Entry StandardID, Entry itemID, Entry itemName, Entry note)
        {
            var sessionID = App.Session;
            string userID = SessionModel.GetUserID(sessionID);

            string itemIcon = !string.IsNullOrEmpty(ParameterModel.ImageManager.ImageString) ? ParameterModel.ImageManager.ImageString : string.Empty;
            int oldASRI = ListASRI.Count;
            int newASRI = 0;
            if (string.IsNullOrEmpty(StandardID.Text))
            {
                await MsgModel.MsgNotification("Standard ID Cannot Null");
            }
            else
            {
                AsriRoot root = new AsriRoot
                {
                    standardReferenceID = StandardID.Text,
                    itemID = itemID.Text,
                    itemName = itemName.Text,
                    note = note.Text,
                    isUsedBySystem = ParameterModel.ItemDefaultValue.IsUsed,
                    isActive = ParameterModel.ItemDefaultValue.IsActive,
                    lastUpdateDateTime = ParameterModel.DateFormat.DateTime,
                    lastUpdateByUserID = userID,
                    itemIcon = itemIcon
                };
                ListASRI.Add(root);
                newASRI = ListASRI.Count;
                ParameterModel.ImageManager.ImageString = string.Empty;
            }

            if (newASRI > oldASRI)
            {
                await MsgModel.MsgNotification($"Successfully Add {itemID.Text} To Item");
                IsEdit = false;
                itemID.Text = string.Empty;
                itemName.Text = string.Empty;
            }
        }

        public async Task UploadItemIcon_Click()
        {
            PermissionType type = PermissionType.StorageRead;
            await PermissionRequest.RequestPermission(type);
            _ = await ImageConvert.PickImageAsync();
        }
    }
}
