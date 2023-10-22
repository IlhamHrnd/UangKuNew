using CommunityToolkit.Maui.Views;
using UangKu.Model.Base;
using UangKu.Model.SubMenu;
using UangKu.ViewModel.RestAPI.Profile;
using static UangKu.Model.Base.ParameterModel.PermissionManager;

namespace UangKu.ViewModel.SubMenu
{
    public class EditProfileVM : EditProfile
    {
        private NetworkModel network = NetworkModel.Instance;
        public EditProfileVM()
        {
            Title = $"Edit Profile {App.Session.personID}";
        }

        public async void LoadData(AvatarView avatar)
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
                    var person = await GetProfile.GetProfileID(App.Session.personID);
                    if (person != null)
                    {
                        ListPerson.Clear();
                        ListPerson.Add(person);
                    }

                    if (!string.IsNullOrEmpty(ListPerson[0].photo))
                    {
                        avatar.ImageSource = ImageConvert.ImgSrcAsync(ListPerson[0].photo);
                        avatar.Text = ListPerson[0].personID;
                    }
                    else
                    {
                        avatar.Text = App.Session.personID;
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

        public async void UploadPhoto_Click(AvatarView avatar)
        {
            PermissionType type = PermissionType.StorageRead;
            await PermissionRequest.RequestPermission(type);

            ImageSource source = await ImageConvert.PickImageAsync();

            if (source != null )
            {
                avatar.ImageSource = source;
            }
        }
    }
}
