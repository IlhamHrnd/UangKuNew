using CommunityToolkit.Maui.Views;
using UangKu.Model.Base;
using UangKu.Model.Session;
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
        public NewTransactionVM(string mode, string transNo)
        {
            Mode = mode;
            TransNo = transNo;

            LoadTitle();
        }
        private async void LoadTitle()
        {
            if (Mode == ParameterModel.ItemDefaultValue.NewFile)
            {
                Title = $"{Mode} Transaction For {App.Session.username}";
            }
            else if (Mode == ParameterModel.ItemDefaultValue.EditFile)
            {
                Title = $"{Mode} For TransNo {TransNo}";
            }
            else
            {
                Title = string.Empty;
                await MsgModel.MsgNotification($"Mode For {Mode} Is Unknow");
            }
        }
        public async void LoadData(Entry EntTransNo, Entry EntAmount, Entry EntDescription,
            Picker PicTrans, Picker PicTransItem, AvatarView Avatar, DatePicker TransDate)
        {
            bool isConnect = network.IsConnected;
            var isallow = Converter.StringToBool(AppParameter.IsAllowCustomDate, ParameterModel.AppParameterDefault.IsAllowCustomDate);
            IsAllowCustomDate = isallow;

            IsBusy = true;
            try
            {
                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
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
                if (Mode == ParameterModel.ItemDefaultValue.EditFile)
                {
                    if (!string.IsNullOrEmpty(TransNo))
                    {
                        var transno = await GetTransNo.GetTransactionNo(TransNo);
                        if (transno.transDate.HasValue)
                        {
                            TransDate.Date = (DateTime)transno.transDate;
                        }
                        if (!string.IsNullOrEmpty(transno.transNo))
                        {
                            EntTransNo.Text = transno.transNo;

                            //Process Get Item Index To Picker
                            var newPicTransList = Converter.ConvertIListToList(ListTransaction);
                            int selectedIndex = ControlHelper.GetIndexByName(newPicTransList, item => item.itemName, transno.srTransaction);
                            PicTrans.SelectedIndex = selectedIndex;
                            await PickerTransType_Changed(PicTrans, EntTransNo);

                            var newPicTransItemList= Converter.ConvertIListToList(ListTransItem);
                            selectedIndex = new int();
                            selectedIndex = ControlHelper.GetIndexByName(newPicTransItemList, item => item.itemName, transno.srTransItem);
                            PicTransItem.SelectedIndex = selectedIndex;

                            EntDescription.Text = transno.description;
                            Avatar.Text = transno.srTransItem;
                        }
                        if (transno.amount != 0)
                        {
                            var value = Converter.DecimalToInt((decimal)transno.amount);
                            EntAmount.Text = value.ToString();
                        }
                        if (transno.photo != null)
                        {
                            string decodeImg = Converter.DecodeBase64ToString(transno.photo);
                            byte[] byteImg = Converter.StringToByteImg(decodeImg);
                            ParameterModel.ImageManager.ImageByte = byteImg;
                            ParameterModel.ImageManager.ImageString = decodeImg;
                            Avatar.ImageSource = ImageConvert.ImgByte(byteImg);
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
                            await MsgModel.MsgNotification($"TransType For {TransTypes} Is Unknow");
                            break;
                    }
                    if (!string.IsNullOrEmpty(TransTypes) && Mode == ParameterModel.ItemDefaultValue.NewFile)
                    {
                        EntryTransNo.Text = await GetNewAutoNumber.GetTransactionNo(TransTypes);
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
            Picker PicTrans, Picker PicTransItem, DatePicker DateTransDate)
        {
            var sessionID = App.Session;
            string userID = SessionModel.GetUserID(sessionID);

            bool isConnect = network.IsConnected;
            string dateOnly;
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
                dateOnly = DateTransDate != null && IsAllowCustomDate
                    ? DateFormat.FormattingDate(DateTransDate.Date, ParameterModel.DateTimeFormat.Yearmonthdate)
                    : DateFormat.FormattingDate(ParameterModel.DateFormat.DateTime, ParameterModel.DateTimeFormat.Yearmonthdate);
                if (Mode == ParameterModel.ItemDefaultValue.NewFile)
                {
                    if (!string.IsNullOrEmpty(TransTypes))
                    {
                        var bodyPost = new Model.Index.Body.PostTransaction
                        {
                            transNo = EntTransNo.Text,
                            srTransaction = SelectedTransType.itemID,
                            srTransItem = SelectedTransItem.itemID,
                            amount = Converter.StringToInt(EntAmount.Text, 0),
                            description = EntDescription.Text,
                            photo = ParameterModel.ImageManager.ImageString,
                            createdDateTime = ParameterModel.DateFormat.DateTime,
                            createdByUserID = userID,
                            lastUpdateDateTime = ParameterModel.DateFormat.DateTime,
                            lastUpdateByUserID = userID,
                            transType = TransTypes,
                            transDate = dateOnly,
                            personID = userID
                        };

                        var transaction = await PostTransaction.PostTransactionTransNo(bodyPost);
                        if (!string.IsNullOrEmpty(transaction))
                        {
                            await MsgModel.MsgNotification($"{transaction}");
                        }
                    }
                }
                else if (Mode == ParameterModel.ItemDefaultValue.EditFile)
                {
                    var bodyPatch = new Model.Index.Body.PatchTransaction
                    {
                        transNo = EntTransNo.Text,
                        srTransaction = SelectedTransType.itemID,
                        srTransItem = SelectedTransItem.itemID,
                        amount = int.Parse(EntAmount.Text),
                        description = EntDescription.Text,
                        photo = ParameterModel.ImageManager.ImageString,
                        lastUpdateDateTime = ParameterModel.DateFormat.DateTime,
                        lastUpdateByUserID = userID,
                        transType = TransTypes,
                        transDate = dateOnly
                    };

                    var transaction = await PatchTransaction.PatchTransactionTransNo(bodyPatch);
                    if (!string.IsNullOrEmpty(transaction))
                    {
                        await MsgModel.MsgNotification($"{transaction}");
                    }
                }
                else
                {
                    await MsgModel.MsgNotification($"Mode For {Mode} Is Unknow");
                }
            }
            catch (Exception e)
            {
                await MsgModel.MsgNotification(e.Message);
            }
            finally
            {
                IsBusy = false;
                ParameterModel.ImageManager.ImageString = string.Empty;
            }
        }
    }
}
