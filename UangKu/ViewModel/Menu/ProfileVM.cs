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
                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }
                if (!string.IsNullOrEmpty(App.Session.personID))
                {
                    var profile = await GetProfile.GetProfileID(App.Session.personID);
                    if (!string.IsNullOrEmpty(profile.personID))
                    {
                        Profiles.Clear();
                        Profiles.Add(profile);
                    }
                }
                for (int i = 0; i < Profiles.Count; i++)
                {
                    if (!string.IsNullOrEmpty(Profiles[i].photo))
                    {
                        avatar.ImageSource = ImageConvert.ByteSrcAsync(Profiles[i].photo);
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
            await _navigation.PushAsync(new EditProfile());
        }
    }
}
