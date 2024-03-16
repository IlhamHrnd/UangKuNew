using CommunityToolkit.Maui.Views;
using UangKu.Model.Base;
using UangKu.Model.SubMenu;
using UangKu.ViewModel.RestAPI.AppStandardReferenceItem;
using UangKu.ViewModel.RestAPI.User;
using static UangKu.Model.Response.AppStandardReferenceItem.AppStandardReferenceItem;

namespace UangKu.ViewModel.SubMenu
{
    public class EditUsernameVM : EditUsername
    {
        private NetworkModel network = NetworkModel.Instance;
        public EditUsernameVM()
        {
            Title = $"Edit Username {ParameterModel.User.UserID}";
            Name = ParameterModel.User.UserID;
        }
        public async void LoadData(AvatarView avatar, CheckBox checkbox, InputKit.Shared.Controls.SelectionView selection, Picker picker)
        {
            bool isConnect = network.IsConnected;
            IsBusy = true;
            try
            {
                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }
                var sex = await AppStandardReferenceItem.GetAsriAsync<AsriRoot>("Sex", true, true);
                if (sex.Count > 0 )
                {
                    ListSex.Clear();
                    foreach (var item in sex )
                    {
                        ListSex.Add(item);
                    }
                }
                var access = await AppStandardReferenceItem.GetAsriAsync<AsriTwoRoot>("Access", true, true);
                if (access.Count > 0 )
                {
                    ListAccess.Clear();
                    foreach (var item in access )
                    {
                        ListAccess.Add(item);
                    }
                }
                var user = await UserName.GetUserName(Name);
                if (!string.IsNullOrEmpty(user.username))
                {
                    switch (user.sexName)
                    {
                        case "Laki - Laki":
                            user.imgavatar = "man.svg";
                            break;
                        case "Perempuan":
                            user.imgavatar = "woman.svg";
                            break;
                        default:
                            await MsgModel.MsgNotification($"Sexname For {user.sexName} Is Invalid");
                            break;
                    }

                    var newSexList = Converter.ConvertIListToList(ListSex);
                    int selectedIndex = ControlHelper.GetIndexByName(newSexList, item => item.itemName, user.sexName);
                    selection.SelectedIndex = selectedIndex;

                    var newAccessList = Converter.ConvertIListToList(ListAccess);
                    selectedIndex = new int();
                    selectedIndex = ControlHelper.GetIndexByName(newAccessList, item => item.itemName, user.accessName);
                    picker.SelectedIndex = selectedIndex;

                    if (!string.IsNullOrEmpty(user.imgavatar))
                    {
                        avatar.ImageSource = user.imgavatar;
                    }
                    if (!string.IsNullOrEmpty(user.statusName))
                    {
                        bool isActive = user.statusName == ParameterModel.Login.Status;
                        checkbox.IsChecked = isActive;
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
        public async Task UpdateUsername_Click(Picker picker, CheckBox checkbox)
        {
            var sessionID = App.Session;
            string userID = SessionModel.GetUserID(sessionID);

            bool isConnect = network.IsConnected;
            IsBusy = true;
            try
            {
                bool isValidPicker = await ValidateNullChecker.PickerValidateFields(
                    (picker, "Access")
                );

                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }
                if (isValidPicker)
                {
                    var body = new Model.Index.Body.PatchUsername
                    {
                        sex = SelectedSex.itemID,
                        access = SelectedAccess.itemID,
                        isActive = checkbox.IsChecked,
                        lastUpdateUser = userID
                    };

                    var user = await UserUpdate.PatchUsername(body, Name);
                    if (!string.IsNullOrEmpty(user))
                    {
                        await MsgModel.MsgNotification($"{user}");
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
