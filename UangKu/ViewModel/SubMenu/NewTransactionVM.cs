using CommunityToolkit.Maui.Views;
using UangKu.Model.Base;
using UangKu.Model.SubMenu;
using UangKu.ViewModel.RestAPI.AppStandardReferenceItem;
using UangKu.ViewModel.RestAPI.Transaction;
using static UangKu.Model.Base.ParameterModel.PermissionManager;
using static UangKu.Model.Response.AppStandardReferenceItem.AppStandardReferenceItem;

namespace UangKu.ViewModel.SubMenu
{
    public class NewTransactionVM : NewTransaction
    {
        private NetworkModel network = NetworkModel.Instance;
        public string Mode { get; set; }
        public string TransTypes { get; set; }
        public NewTransactionVM(string mode)
        {
            Mode = mode;
            Title = $"{Mode} Transaction For {App.Session.username}";
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
                if (Mode == ParameterModel.ItemDefaultValue.NewFile)
                {
                    
                }
                else if (Mode == ParameterModel.ItemDefaultValue.EditFile)
                {

                }
                else
                {

                }
                var trans = await AppStandardReferenceItem.GetAsriAsync<AsriRoot>("Transaction", true, true);
                if (trans.Count > 0)
                {
                    ListTransaction.Clear();
                    for (int i = 0; i < trans.Count; i++)
                    {
                        ListTransaction.Add(trans[i]);
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
        public async Task PickerTransType_Changed(Picker TransType, Entry EntryTransNo)
        {
            if (TransType.SelectedItem != null)
            {
                bool isConnect = network.IsConnected;
                IsBusy = true;
                try
                {
                    var select = SelectedTransType.itemID.ToString();
                    var itemName = SelectedTransType.itemName.ToString();
                    if (!isConnect)
                    {
                        await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                    }
                    switch (select)
                    {
                        case "Transaction-001":
                            TransTypes = ParameterModel.ItemDefaultValue.IncomeTrans;
                            break;

                        case "Transaction-002":
                            TransTypes = ParameterModel.ItemDefaultValue.OutcomeTrans;
                            break;

                        default:
                            break;
                    }
                    if (!string.IsNullOrEmpty(TransTypes) && Mode == ParameterModel.ItemDefaultValue.NewFile)
                    {
                        var transno = await NewTransNo.GetNewTransNo(TransTypes);
                        if (!string.IsNullOrEmpty(transno))
                        {
                            EntryTransNo.Text = transno;
                        }
                    }
                    if (!string.IsNullOrEmpty(itemName))
                    {
                        var transitem = await AppStandardReferenceItem.GetAsriAsync<AsriTwoRoot>(itemName, true, true);
                        if (transitem.Count > 0)
                        {
                            ListTransItem.Clear();
                            foreach (var item in transitem)
                            {
                                ListTransItem.Add(item);
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
        }
        public async Task UploadPhoto_Click(AvatarView avatar)
        {
            PermissionType type = PermissionType.StorageRead;
            await PermissionRequest.RequestPermission(type);

            ImageSource source = await ImageConvert.PickImageAsync();

            if (source != null)
            {
                avatar.ImageSource = source;
                avatar.Text = ParameterModel.ImageManager.ImageName;
            }
        }
        public async Task SaveTransaction_Click(Entry EntTransNo, Entry EntAmount, Entry EntDescription,
            Picker PicTrans, Picker PicTransItem)
        {
            bool isConnect = network.IsConnected;
            IsBusy = true;
            try
            {
                bool isValidEntry = await ValidateNullChecker.EntryValidateFields(
                    (EntTransNo.Text, "Transaction No"),
                    (EntAmount.Text, "Amount"),
                    (EntDescription.Text, "Description")
                );

                bool isValidPicker = await ValidateNullChecker.PickerValidateFields(
                    (PicTrans, "Transaction"),
                    (PicTransItem, "Transaction Item")
                );

                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }
                if (ParameterModel.ImageManager.ImageByte == null && string.IsNullOrEmpty(ParameterModel.ImageManager.ImageString))
                {
                    await MsgModel.MsgNotification($"Image Data Is Null");
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
