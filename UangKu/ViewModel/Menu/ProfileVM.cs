﻿using CommunityToolkit.Maui.Views;
using UangKu.Model.Base;
using UangKu.Model.Menu;
using UangKu.View.SubMenu;
using UangKu.ViewModel.RestAPI.Profile;

namespace UangKu.ViewModel.Menu
{
    public class ProfileVM : Profile
    {
        private NetworkModel network = NetworkModel.Instance;
        private readonly INavigation _navigation;

        public ProfileVM(INavigation navigation)
        {
            Title = $"Profile {App.Session.username}";
            _navigation = navigation;
        }

        public async Task LoadData(AvatarView avatar)
        {
            bool isConnect = network.IsConnected;
            IsBusy = true;
            try
            {
                var sessionID = App.Session;
                string userID = SessionModel.GetUserID(sessionID);

                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }
                if (!string.IsNullOrEmpty(userID))
                {
                    var profile = await GetProfile.GetProfileID(userID);
                    if (!string.IsNullOrEmpty(profile.personID))
                    {
                        Profiles.Clear();
                        Profiles.Add(profile);
                    }
                }
                if (Profiles.Count > 0)
                {
                    var profile = Profiles[0];
                    if (!string.IsNullOrEmpty(profile.photo))
                    {
                        string decodeImg = ImageConvert.DecodeBase64ToString(profile.photo);
                        byte[] byteImg = ImageConvert.StringToByteImg(decodeImg);
                        ParameterModel.ImageManager.ImageByte = byteImg;
                        ParameterModel.ImageManager.ImageString = decodeImg;
                        avatar.ImageSource = ImageConvert.ImgByte(byteImg);
                    }
                    if (profile.birthDate.HasValue)
                    {
                        profile.birthDateFormat = profile.birthDate.HasValue ? DateFormat.FormattingDate((DateTime)profile.birthDate, ParameterModel.DateTimeFormat.Date) : string.Empty;
                    }
                    profile.fullName = $"{profile.firstName} {profile.middleName} {profile.lastName}";
                    avatar.Text = profile.personID;
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

        public async Task EditProfile_Click()
        {
            await _navigation.PushAsync(new EditProfile(ParameterModel.ItemDefaultValue.EditFile));
        }
    }
}
