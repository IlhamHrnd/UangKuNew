﻿using System.Diagnostics.CodeAnalysis;
using UangKu.Model.Base;
using UangKu.Model.Index;
using UangKu.Model.Session;
using UangKu.WebService.Service;

namespace UangKu.ViewModel.Index
{
    public class MainPageVM : MainPage
    {
        public MainPageVM()
        {
            Title = "Main Page";
        }

        [RequiresAssemblyFiles()]
        public async void LoadData(Entry entUser, Entry entPass, Button btnLogin)
        {
            if (!Network.IsConnected)
            {
                entUser.IsEnabled = false;
                entPass.IsEnabled = false;
                btnLogin.IsEnabled = false;
            }
            else
            {
                entUser.IsEnabled = true;
                entPass.IsEnabled = true;
                btnLogin.IsEnabled = true;

                var filter = new WebService.Filter.Root<WebService.Filter.AppParameter>
                {
                    Data = new WebService.Filter.AppParameter
                    {
                        ParameterID = "ShowLastBuild"
                    }
                };

                var parameter = await WebService.Service.AppParameter.GetParameterID(filter);
                if (parameter.Succeeded == true)
                {
                    var isVisible = Converter.StringToBool(parameter.Data.parameterValue);
                    if (isVisible)
                    {

                    }
                }
                else
                {
                    await MsgModel.MsgNotification(parameter.Message);
                }
            }
        }

        public async void BtnLogin_User(Entry username, Entry password)
        {
            IsBusy = true;
            try
            {
                bool isValidEntry = await ValidateNullChecker.EntryValidateFields(
                    (username.Text, "Username"),
                    (password.Text, "Password")
                );
                if (!Network.IsConnected)
                {
                    await MsgModel.MsgNotification(ItemManager.Offline);
                    return;
                }
                if (isValidEntry)
                {
                    var filter = new WebService.Filter.Root<WebService.Filter.User>
                    {
                        Data = new WebService.Filter.User
                        {
                            Username = username.Text,
                            Password = password.Text
                        }
                    };

                    var user = await User.GetLoginUserName(filter);
                    if (user.Succeeded != true)
                    {
                        await MsgModel.MsgNotification(user.Message);
                        return;
                    }

                    App.Session = new AppSession
                    {
                        username = user.Data.Username,
                        sexName = user.Data.Srsex,
                        accessName = user.Data.Sraccess,
                        statusName = user.Data.Srstatus,
                        activeDate = user.Data.ActiveDate,
                        lastLogin = user.Data.LastLogin,
                        lastUpdateDateTime = user.Data.LastUpdateDateTime,
                        lastUpdateByUser = user.Data.LastUpdateByUser,
                        personID = user.Data.PersonId
                    };

                    if (!string.IsNullOrEmpty(user.Data.Username))
                    {
                        filter = new WebService.Filter.Root<WebService.Filter.User>
                        {
                            Data = new WebService.Filter.User
                            {
                                Username = username.Text
                            }
                        };
                        var update = await User.UpdateLastLogin(filter);
                        if (update.Succeeded != true)
                            await MsgModel.MsgNotification(update.Message);
                    }

                    var isFinish = await SessionModel.LoadAppParameterAsync();
                    if (!isFinish)
                    {
                        await MsgModel.MsgNotification("Failed Retrieve Parameter");
                        return;
                    }

                    switch (user.Data.Sraccess)
                    {
                        case "Admin":
                            var masterAdmin = new View.MasterPage.MasterAdmin();
                            App.Current.MainPage = masterAdmin;
                            break;

                        case "User":
                            var masterUser = new View.MasterPage.MasterUser();
                            App.Current.MainPage = masterUser;
                            break;

                        default:
                            await MsgModel.MsgNotification($"User Access For {App.Session.username} Is {App.Session.accessName} Unknown");
                            break;
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
