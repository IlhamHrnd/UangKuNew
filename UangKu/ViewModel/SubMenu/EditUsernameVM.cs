﻿using CommunityToolkit.Maui.Views;
using UangKu.Model.Base;
using UangKu.Model.SubMenu;
using UangKu.ViewModel.RestAPI.AppStandardReferenceItem;
using UangKu.ViewModel.RestAPI.User;
using UangKu.WebService.Service;
using static UangKu.Model.Response.AppStandardReferenceItem.AppStandardReferenceItem;
using AppStandardReferenceItem = UangKu.ViewModel.RestAPI.AppStandardReferenceItem.AppStandardReferenceItem;

namespace UangKu.ViewModel.SubMenu
{
    public class EditUsernameVM : EditUsername
    {
        private NetworkModel network = NetworkModel.Instance;
        public EditUsernameVM(string itemID)
        {
            Title = $"Edit Username {itemID}";
            Name = itemID;
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

                var filter = new WebService.Filter.Root<WebService.Filter.User>
                {
                    Data = new WebService.Filter.User
                    {
                        Username = Name
                    }
                };
                var user = await User.GetUsername(filter);
                if (user.Succeeded == true && !string.IsNullOrEmpty(user.Data.Username))
                {
                    switch (user.Data.Srsex)
                    {
                        case "Laki - Laki":
                            user.Data.imgavatar = "man.svg";
                            break;
                        case "Perempuan":
                            user.Data.imgavatar = "woman.svg";
                            break;
                        default:
                            await MsgModel.MsgNotification($"Sexname For {user.Data.Srsex} Is Invalid");
                            break;
                    }

                    var newSexList = Converter.ConvertIListToList(ListSex);
                    int selectedIndex = ControlHelper.GetIndexByName(newSexList, item => item.itemName, user.Data.Srsex);
                    selection.SelectedIndex = selectedIndex;

                    var newAccessList = Converter.ConvertIListToList(ListAccess);
                    selectedIndex = new int();
                    selectedIndex = ControlHelper.GetIndexByName(newAccessList, item => item.itemName, user.Data.Sraccess);
                    picker.SelectedIndex = selectedIndex;

                    if (!string.IsNullOrEmpty(user.Data.imgavatar))
                    {
                        avatar.ImageSource = user.Data.imgavatar;
                    }
                    if (!string.IsNullOrEmpty(user.Data.Srstatus))
                    {
                        bool isActive = user.Data.Srstatus == ParameterModel.Login.Status;
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
