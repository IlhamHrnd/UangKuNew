using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
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

        public async void LoadData(AvatarView avatar, Entry EntFirstName, Entry EntMiddleName, Entry EntLastName, Picker PicPlaceOfBirth,
            Entry StreetName, Picker Provinces, Picker City, Picker District, Picker SubDistrict, Entry PostalCode)
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
                        StreetName.Text = person.address;
                        Provinces.SelectedItem = person.province;
                        City.SelectedItem = person.city;
                        District.SelectedItem = person.district;
                        SubDistrict.SelectedItem = person.subdistrict;
                        PostalCode.Text = person.postalCode.ToString();
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
                    if (person.photo != null)
                    {
                        ParameterModel.ImageManager.ImageByte = person.photo;
                        avatar.ImageSource = ImageConvert.ImgByte(person.photo);
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

        public async Task SaveProfile_Clicked(Entry EntFirstName, Entry EntMiddleName, Entry EntLastName,
            Entry StreetName, DatePicker BirthDate, Entry PostalCode)
        {
            bool isConnect = network.IsConnected;
            IsBusy = true;
            try
            {
                string userID;
                bool isValid = await ValidateNullChecker.ValidateFields(
                    (EntFirstName.Text, "First Name"),
                    (EntLastName.Text, "Last Name"),
                    (StreetName.Text, "Street Name")
                );
                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }
                if (!string.IsNullOrEmpty(App.Session.personID))
                {
                    userID = App.Session.personID;
                }
                else if (string.IsNullOrEmpty(App.Session.personID) && !string.IsNullOrEmpty(App.Session.username))
                {
                    userID = App.Session.username;
                }
                else
                {
                    userID = string.Empty;
                }
                if (ParameterModel.ImageManager.ImageByte == null)
                {
                    await MsgModel.MsgNotification($"Image Data Is Null");
                }
                if (!string.IsNullOrEmpty(userID))
                {
                    var user = await GetProfile.GetProfileID(userID);
                    if (string.IsNullOrEmpty(user.personID))
                    {
                        var body = new Model.Index.Body.PostProfile
                        {
                            personID = userID,
                            firstName = EntFirstName.Text,
                            middleName = EntMiddleName.Text,
                            lastName = EntLastName.Text,
                            birthDate = BirthDate.Date,
                            placeOfBirth = SelectedPlaceBirth.provName,
                            photo = ParameterModel.ImageManager.ImageByte,
                            address = StreetName.Text,
                            province = SelectedProvinces.provName,
                            city = SelectedCity.cityName,
                            district = SelectedDistrict.disName,
                            subdistrict = SelectedSubdistrict.subdisName,
                            postalCode = int.Parse(PostalCode.Text),
                            lastUpdateDateTime = DateTime.Now,
                            lastUpdateByUser = userID
                        };

                        var profile = await PostProfile.PostProfileID(body);
                        if (!string.IsNullOrEmpty(profile))
                        {
                            await MsgModel.MsgNotification($"{profile}");
                        }
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

        #region Method Proses Picker Changed
        public async Task PickerProvinces_Changed(Picker Provinces)
        {
            if (Provinces.SelectedItem != null)
            {
                bool isConnect = network.IsConnected;
                IsBusy = true;
                try
                {
                    if (!isConnect)
                    {
                        await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                    }
                    if (SelectedProvinces.provID != 0)
                    {
                        var cities = await GetCity.GetCities(SelectedProvinces.provID.ToString());
                        if (cities.Count > 0)
                        {
                            ListCities.Clear();
                            for (int i = 0; i < cities.Count; i++)
                            {
                                ListCities.Add(cities[i]);
                            }
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

        public async Task PickerCity_Changed(Picker Cities)
        {
            if (Cities.SelectedItem != null)
            {
                bool isConnect = network.IsConnected;
                IsBusy = true;
                try
                {
                    if (!isConnect)
                    {
                        await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                    }
                    if (SelectedCity.cityID != 0)
                    {
                        var districts = await GetDistrict.GetDistricts(SelectedCity.cityID.ToString());
                        if (districts.Count > 0)
                        {
                            ListDistricts.Clear();
                            for (int i = 0; i < districts.Count; i++)
                            {
                                ListDistricts.Add(districts[i]);
                            }
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

        public async Task PickerDistrict_Changed(Picker Districts)
        {
            if (Districts.SelectedItem != null)
            {
                bool isConnect = network.IsConnected;
                IsBusy = true;
                try
                {
                    if (!isConnect)
                    {
                        await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                    }
                    if (SelectedDistrict.disID != 0)
                    {
                        var subdis = await GetSubdistrict.GetSubdistricts(SelectedDistrict.disID.ToString());
                        if (subdis.Count > 0)
                        {
                            ListSubdistricts.Clear();
                            for (int i = 0; i < subdis.Count;i++)
                            {
                                ListSubdistricts.Add(subdis[i]);
                            }
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

        public async Task PickerSubdistrict_Changed(Picker Subdistrict, Entry PostalCode)
        {
            if (Subdistrict.SelectedItem != null)
            {
                bool isConnect = network.IsConnected;
                IsBusy = true;
                try
                {
                    if (!isConnect)
                    {
                        await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                    }
                    if (SelectedSubdistrict.subdisID != 0 && SelectedDistrict.disID != 0 &&
                        SelectedCity.cityID != 0 && SelectedProvinces.provID != 0)
                    {
                        var postal = await GetPostalCode.GetPostalCodes(SelectedProvinces.provID.ToString(), SelectedCity.cityID.ToString(), SelectedDistrict.disID.ToString(), SelectedSubdistrict.subdisID.ToString());
                        if (postal.postalCode != 0)
                        {
                            PostalCode.Text = postal.postalCode.ToString();
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
        #endregion

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
