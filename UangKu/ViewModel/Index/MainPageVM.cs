﻿using System.Diagnostics.CodeAnalysis;
using UangKu.Model.Base;
using UangKu.Model.Index;
using UangKu.Model.Session;
using UangKu.ViewModel.RestAPI.AppParameter;
using UangKu.ViewModel.RestAPI.User;

namespace UangKu.ViewModel.Index
{
    public class MainPageVM : MainPage
    {
        private NetworkModel network = NetworkModel.Instance;
        public MainPageVM()
        {
            Title = "Main Page";
        }

        [RequiresAssemblyFiles()]
        public async void LoadData(Entry entUser, Entry entPass, Button btnLogin)
        {
            bool isConnect = network.IsConnected;
            if (!isConnect)
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

                var parameterID = await GetParameterID.GetParameter("ShowLastBuild");
                if (parameterID.metaData.isSucces && parameterID.metaData.code == 200)
                {
                    var isVisible = Converter.StringToBool(parameterID.data.parameterValue, false);
                    if (isVisible)
                    {
                        IsVisible = isVisible;
                        var lastbuild = SessionModel.GetBuildDate();
                        LastBuild = lastbuild.ToString(ParameterModel.DateTimeFormat.Date);
                    }
                }
                else
                {
                    await MsgModel.MsgNotification(parameterID.metaData.message);
                }
            }
        }

        public async void BtnLogin_User(Entry username, Entry password)
        {
            bool isConnect = network.IsConnected;
            IsBusy = true;
            try
            {
                bool isValidEntry = await ValidateNullChecker.EntryValidateFields(
                    (username.Text, "Username"),
                    (password.Text, "Password")
                );
                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }
                if (isValidEntry)
                {
                    var user = await UserLogin.GetUsernameLogin(username.Text, password.Text);
                    if (user.metaData.isSucces && user.metaData.code == 200)
                    {
                        App.Session = new AppSession
                        {
                            username = user.username,
                            sexName = user.sexName,
                            accessName = user.accessName,
                            statusName = user.statusName,
                            activeDate = user.activeDate,
                            lastLogin = user.lastLogin,
                            lastUpdateDateTime = user.lastUpdateDateTime,
                            lastUpdateByUser = user.lastUpdateByUser,
                            personID = user.personID
                        };
                        if (!string.IsNullOrEmpty(App.Session.username) && App.Session.statusName == ParameterModel.Login.Status)
                        {
                            var updatelogin = await UserLastLogin.PatchUserLastLogin(username.Text);
                            if (!string.IsNullOrEmpty(updatelogin))
                            {
                                await MsgModel.MsgNotification(updatelogin);
                            }
                        }
                        if (string.IsNullOrEmpty(App.Session.username))
                        {
                            await MsgModel.MsgNotification($"Username {username.Text} Not Found");
                        }
                        else if (App.Session.statusName != ParameterModel.Login.Status)
                        {
                            await MsgModel.MsgNotification($"Username {App.Session.username} Not Active, Please Contact Administrator");
                        }
                        else
                        {
                            switch (App.Session.accessName)
                            {
                                case "Admin":
                                    SessionModel.LoadAppParameter();
                                    SessionModel.LoadProfile();
                                    var masterAdmin = new View.MasterPage.MasterAdmin();
                                    App.Current.MainPage = masterAdmin;
                                    break;

                                case "User":
                                    SessionModel.LoadAppParameter();
                                    SessionModel.LoadProfile();
                                    var masterUser = new View.MasterPage.MasterUser();
                                    App.Current.MainPage = masterUser;
                                    break;

                                default:
                                    await MsgModel.MsgNotification($"User Access For {App.Session.username} Is {App.Session.accessName} Unknown");
                                    break;
                            }
                            username.Text = string.Empty;
                            password.Text = string.Empty;
                        }
                    }
                    else
                    {
                        await MsgModel.MsgNotification(user.metaData.message);
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
