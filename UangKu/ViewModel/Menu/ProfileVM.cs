using CommunityToolkit.Maui.Views;
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
                    if (profile.metaData.isSucces && profile.metaData.code == 200)
                    {
                        Profiles.Clear();
                        Profiles.Add(profile);

                        var profiles = Profiles[0];
                        if (!string.IsNullOrEmpty(profiles.photo))
                        {
                            string decodeImg = Converter.DecodeBase64ToString(profiles.photo);
                            byte[] byteImg = Converter.StringToByteImg(decodeImg);
                            ParameterModel.ImageManager.ImageByte = byteImg;
                            ParameterModel.ImageManager.ImageString = decodeImg;
                            avatar.ImageSource = ImageConvert.ImgByte(byteImg);
                        }
                        if (profiles.birthDate.HasValue)
                        {
                            profiles.birthDateFormat = profiles.birthDate.HasValue ? DateFormat.FormattingDate((DateTime)profiles.birthDate, ParameterModel.DateTimeFormat.Date) : string.Empty;
                        }
                        profiles.fullName = $"{profiles.firstName} {profiles.middleName} {profiles.lastName}";
                        avatar.Text = profiles.personID;
                    }
                    else
                    {
                        await MsgModel.MsgNotification(profile.metaData.message);
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

        public async Task EditProfile_Click()
        {
            await _navigation.PushAsync(new EditProfile(ParameterModel.ItemDefaultValue.EditFile));
        }
    }
}
