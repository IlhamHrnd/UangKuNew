﻿using UangKu.Model.Base;
using UangKu.Model.Module.UserManagement;
using static UangKu.Model.Base.PermissionManager;

namespace UangKu.ViewModel.Module.UserManagement
{
    public class ProfileEditVM : ProfileEdit
    {
        public ProfileEditVM(string mode)
        {
            Mode = mode;
        }

        public async void LoadData()
        {
            Person = new WebService.Data.Root<WebService.Data.Profile.Data>();

            if (Network.IsConnected)
            {
                AppProgram(Model.Base.AppProgram.EditProfile);

                if (Mode == ItemManager.NewFile)
                    IsEnabled = IsAdded;
                else if (Mode == ItemManager.EditFile)
                    IsEnabled = IsEdited;
                else
                    IsEnabled = false;

                IsBusy = true;
                try
                {
                    var profile = await WebService.Service.Profile.GetPersonID(new WebService.Filter.Root<WebService.Filter.Profile>
                    {
                        Data = new WebService.Filter.Profile
                        {
                            PersonID = UserID
                        }
                    });
                    if (profile.Succeeded ?? false)
                    {
                        Person = profile;

                        if (!string.IsNullOrEmpty(Person.Data.photo))
                        {
                            string decode = Converter.DecodeBase64ToString(Person.Data.photo);
                            byte[] img = Converter.StringToByteImg(decode);
                            Person.Data.source = ImageConvert.ImgByte(img);
                        }

                        if (Person.Data.birthDate != null)
                        {
                            Person.Data.dateFormat = DateFormat.FormattingDate((DateTime)Person.Data.birthDate, DateTimeFormat.Daydatemonthyear);
                            Person.Data.ageFormat = SessionModel.GetUserAge((DateTime)Person.Data.birthDate);
                        }
                    }
                    else
                        await MsgModel.MsgNotification(profile.Message);
                }
                catch (Exception e)
                {
                    await MsgModel.MsgNotification(e.Message);
                }

                IsBusy = true;
                //Load Untuk Tempat Lahir
                LoadProvince(ListBirth);

                IsBusy = true;
                //Load Untuk Province ETC
                LoadProvince(ListProvince);

                if (Mode == ItemManager.EditFile)
                {
                    var listBirth = Converter.ConvertIListToList(ListBirth);
                    int indexBirth = ControlHelper.GetIndexByName(listBirth, item => item.provName, Person.Data.placeOfBirth);

                    var listProv = Converter.ConvertIListToList(ListProvince);
                    int indexProv = ControlHelper.GetIndexByName(listProv, item => item.provName, Person.Data.province);

                    var listCity = Converter.ConvertIListToList(ListCity);
                    int indexCity = ControlHelper.GetIndexByName(listCity, item => item.cityName, Person.Data.city);

                    var listDistrict = Converter.ConvertIListToList(ListDistrict);
                    int indexDistrict = ControlHelper.GetIndexByName(listDistrict, item => item.disName, Person.Data.district);

                    var listSubdistrict = Converter.ConvertIListToList(ListSubdistrict);
                    int indexSubdistrict = ControlHelper.GetIndexByName(listSubdistrict, item => item.subdisName, Person.Data.subdistrict);
                }
                IsBusy = false;
            }
            else
                IsEnabled = false;
        }

        #region Load Function
        private async void LoadProvince(IList<WebService.Data.Location.Province> prov)
        {
            try
            {
                var province = await WebService.Service.Location.GetAllProvince();
                if (province.Succeeded ?? false)
                {
                    prov.Clear();
                    for (int i = 0; i < province.Data.Count; i++)
                    {
                        WebService.Data.Location.Province item = province.Data[i];
                        prov.Add(item);
                    }
                }
                else
                    await MsgModel.MsgNotification(province.Message);
            }
            catch (Exception e)
            {
                await MsgModel.MsgNotification(e.Message);
            }
        }

        private async void LoadCity(string provID)
        {
            var city = await WebService.Service.Location.GetAllCities(new WebService.Filter.Root<WebService.Filter.Location>
            {
                Data = new WebService.Filter.Location
                {
                    ProvID = provID
                }
            });
            if (city.Succeeded ?? false)
            {
                ListCity.Clear();
                for (int i = 0; i < city.Data.Count; i++)
                {
                    WebService.Data.Location.City item = city.Data[i];
                    ListCity.Add(item);
                }
            }
            else
                await MsgModel.MsgNotification(city.Message);
        }

        private async void LoadDistrict(string cityID)
        {
            var district = await WebService.Service.Location.GetAllDistrict(new WebService.Filter.Root<WebService.Filter.Location>
            {
                Data = new WebService.Filter.Location
                {
                    CityID = cityID
                }
            });
            if (district.Succeeded ?? false)
            {
                ListDistrict.Clear();
                for (int i = 0; i < district.Data.Count; i++)
                {
                    WebService.Data.Location.District item = district.Data[i];
                    ListDistrict.Add(item);
                }
            }
            else
                await MsgModel.MsgNotification(district.Message);
        }

        private async void LoadSubdistrict(string districtID)
        {
            var subdistrict = await WebService.Service.Location.GetAllSubDistrict(new WebService.Filter.Root<WebService.Filter.Location>
            {
                Data = new WebService.Filter.Location
                {
                    DistrictID = districtID
                }
            });
            if (subdistrict.Succeeded ?? false)
            {
                ListSubdistrict.Clear();
                for (int i = 0; i < subdistrict.Data.Count; i++)
                {
                    WebService.Data.Location.SubDistrict item = subdistrict.Data[i];
                    ListSubdistrict.Add(item);
                }
            }
            else
                await MsgModel.MsgNotification(subdistrict.Message);
        }
        #endregion
        public async void ImageUpload(ImageSource source)
        {
            await PermissionRequest.RequestPermission(PermissionType.StorageRead);
            Img = await ImageConvert.PickImageAsync();

            if (Img.ImageByte != null)
                source = ImageConvert.ImgByte(Img.ImageByte);
        }
    }
}
