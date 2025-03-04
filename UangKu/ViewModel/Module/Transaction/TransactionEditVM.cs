using CommunityToolkit.Maui.Views;
using System.Collections.ObjectModel;
using UangKu.Model.Base;
using UangKu.Model.Module.Transaction;
using static UangKu.Model.Base.PermissionManager;

namespace UangKu.ViewModel.Module.Transaction
{
    public class TransactionEditVM : TransactionEdit
    {
        public TransactionEditVM(string mode, string transNo, INavigation navigation)
        {
            Mode = mode;
            TransNo = transNo;
            Navigation = navigation;
        }

        public async void LoadData()
        {
            if (!Network.IsConnected)
                return;

            if (string.IsNullOrEmpty(Mode))
                return;

            if (Mode == ItemManager.EditFile && string.IsNullOrEmpty(TransNo))
                return;

            IsBusy = true;
            AppProgram(Mode == ItemManager.EditFile ? Model.Base.AppProgram.EditTransaction : Model.Base.AppProgram.NewTransaction);

            try
            {
                var transtype = await WebService.Service.AppStandardReferenceItem.GetAllReferenceItemID(new WebService.Filter.Root<WebService.Filter.AppStandardReferenceItem>
                {
                    Data = new WebService.Filter.AppStandardReferenceItem
                    {
                        StandardReferenceID = "Transaction",
                        IsActive = true,
                        IsUsedBySystem = true
                    }
                });
                if (transtype.Succeeded == true)
                {
                    TransType = new WebService.Data.Root<ObservableCollection<WebService.Data.AppStandardReferenceItem.Data>>
                    {
                        Data = new ObservableCollection<WebService.Data.AppStandardReferenceItem.Data>(transtype.Data),
                        Succeeded = transtype.Succeeded,
                        Errors = transtype.Errors,
                        Message = transtype.Message
                    };
                }
                else
                    await MsgModel.MsgNotification(transtype.Message);
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

        public async void LoadItem()
        {
            if (SelectedTransType == null)
            {
                await MsgModel.MsgNotification(ItemManager.Empty);
                return;
            }

            if (!Network.IsConnected)
                return;

            IsBusy = true;
            try
            {
                var item = await WebService.Service.AppStandardReferenceItem.GetAllReferenceItemID(new WebService.Filter.Root<WebService.Filter.AppStandardReferenceItem>
                {
                    Data = new WebService.Filter.AppStandardReferenceItem
                    {
                        StandardReferenceID = SelectedTransType.itemName,
                        IsActive = true,
                        IsUsedBySystem = true
                    }
                });
                if (item.Succeeded ?? false)
                {
                    TransItem = new WebService.Data.Root<ObservableCollection<WebService.Data.AppStandardReferenceItem.Data>>
                    {
                        Data = new ObservableCollection<WebService.Data.AppStandardReferenceItem.Data>(item.Data),
                        Succeeded = item.Succeeded,
                        Errors = item.Errors,
                        Message = item.Message
                    };
                }
                else
                    await MsgModel.MsgNotification(item.Message);
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

        public async Task ImageUpload(AvatarView avatar)
        {
            await PermissionRequest.RequestPermission(PermissionType.StorageRead);
            Img = await ImageConvert.PickImageAsync();

            if (Img != null && Img.ImageByte != null)
                avatar.ImageSource = ImageConvert.ImgByte(Img.ImageByte);
        }

        public async Task SaveTransaction(string amount, string description, DateTime date)
        {
            if (string.IsNullOrEmpty(Mode))
            {
                await MsgModel.MsgNotification(ItemManager.Empty);
                return;
            }

            if (Mode == ItemManager.EditFile && string.IsNullOrEmpty(TransNo))
            {
                await MsgModel.MsgNotification(ItemManager.Empty);
                return;
            }

            if (SelectedTransType == null || SelectedTransItem == null)
            {
                await MsgModel.MsgNotification(ItemManager.Empty);
                return;
            }

            if (!Network.IsConnected)
                return;

            IsBusy = true;
            try
            {
                string transNo;
                if (Mode == ItemManager.NewFile)
                {
                    var autoNumber = await WebService.Service.AutoNumber.GenerateAutoNumber(new WebService.Filter.Root<WebService.Filter.AutoNumber>
                    {
                        Data = new WebService.Filter.AutoNumber
                        {
                            TransType = SelectedTransType.itemName == "Income" ? AutoNumberType.Income : AutoNumberType.Expenditure
                        }
                    });
                    if (autoNumber.Succeeded == false)
                    {
                        await MsgModel.MsgNotification(autoNumber.Message);
                        return;
                    }
                    transNo = autoNumber.Data;
                }
                else if (Mode == ItemManager.EditFile)
                    transNo = TransNo;
                else
                    transNo = string.Empty;

                if (string.IsNullOrEmpty(transNo))
                {
                    await MsgModel.MsgNotification(ItemManager.Cancel);
                    return;
                }

                var body = new WebService.Data.Transaction.Data
                {
                    transNo = transNo,
                    personId = UserID,
                    srtransaction = SelectedTransType.itemId,
                    srtransItem = SelectedTransItem.itemId,
                    amount = Converter.StringToInt(amount),
                    description = description,
                    photo = Img != null && !string.IsNullOrEmpty(Img.ImageString) ? Img.ImageString : string.Empty,
                    transType = SelectedTransType.itemName == "Income" ? AutoNumberType.Income : AutoNumberType.Expenditure,
                    transDate = DateOnly.FromDateTime(date),
                    createdDateTime = DateFormat.DateTime,
                    createdByUserId = UserID,
                    lastUpdateDateTime = DateFormat.DateTime,
                    lastUpdateByUserId = UserID
                };

                if (Mode == ItemManager.EditFile)
                {
                    var save = await WebService.Service.Transaction.PatchTransaction(body);
                    await MsgModel.MsgNotification(save.Message);
                    if (save.Succeeded ?? false)
                        ControlHelper.OnPopNavigationAsync(Navigation);
                }
                else if (Mode == ItemManager.NewFile)
                {
                    var save = await WebService.Service.Transaction.PostTransaction(body);
                    await MsgModel.MsgNotification(save.Message);
                }
                else
                {
                    await MsgModel.MsgNotification(ItemManager.Cancel);
                    return;
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
