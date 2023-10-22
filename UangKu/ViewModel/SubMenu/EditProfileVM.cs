using CommunityToolkit.Maui.Views;
using UangKu.Model.Base;
using UangKu.Model.SubMenu;
using UangKu.ViewModel.RestAPI.Location;
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

        public async void LoadData(AvatarView avatar, Entry EntFirstName, Entry EntMiddleName, Entry EntLastName, Picker PicPlaceOfBirth)
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
                    if (!string.IsNullOrEmpty(person.personID))
                    {
                        EntFirstName.Text = person.firstName;
                        EntMiddleName.Text = person.middleName;
                        EntLastName.Text = person.lastName;
                        PicPlaceOfBirth.SelectedItem = person.placeOfBirth;
                    }
                    var provinces = await GetProvince.GetProvinces();
                    if (provinces.Count > 0)
                    {
                        ListProvinces.Clear();
                        for (int i = 0; i < provinces.Count; i++)
                        {
                            ListProvinces.Add(provinces[i]);
                        }
                    }
                    if (!string.IsNullOrEmpty(person.photo))
                    {
                        avatar.ImageSource = ImageConvert.ImgSrcAsync(person.photo);
                        avatar.Text = person.personID;
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
