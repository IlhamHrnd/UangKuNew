using UangKu.Model.Base;
using UangKu.Model.Menu;
using UangKu.ViewModel.RestAPI.Picture;
using static UangKu.Model.Base.ParameterModel.PermissionManager;

namespace UangKu.ViewModel.Menu
{
    public class UserGalleryVM : UserGallery
    {
        private NetworkModel network = NetworkModel.Instance;
        public UserGalleryVM()
        {
            Title = $"{App.Session.username} Gallery";
        }
        public async Task LoadData()
        {
            bool isConnect = network.IsConnected;
            IsBusy = true;
            try
            {
                string userID;

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

                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }

                if (!string.IsNullOrEmpty(userID))
                {
                    var picture = await GetUserPicture.GetAllUserPicture(ParameterModel.ItemDefaultValue.FirstPage, ParameterModel.ItemDefaultValue.Maxresult,
                        userID, ParameterModel.ItemDefaultValue.IsDeleted);
                    if ((bool)picture.succeeded && picture.data.Count > 0)
                    {
                        ListUserPicture.Clear();
                        for (int i = 0; i < picture.data.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(picture.data[i].picture))
                            {
                                string decodeImg = ImageConvert.DecodeBase64ToString(picture.data[i].picture);
                                byte[] byteImg = ImageConvert.StringToByteImg(decodeImg);
                                picture.data[i].source = ImageConvert.ImgByte(byteImg);
                            }

                            if (!string.IsNullOrEmpty(picture.data[i].pictureFormat))
                            {
                                string result = ImageConvert.SubstringContentType(picture.data[0].pictureFormat, '/');
                                picture.data[i].contenttype = result;
                            }
                        }
                        ListUserPicture.Add(picture);
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
        public async Task UploadPicture_PopUp()
        {
            PermissionType type = PermissionType.StorageRead;
            await PermissionRequest.RequestPermission(type);

            var ImageItems = await ImageConvert.PickMultipleImageAsync();

            if (ImageItems != null && ImageItems.Count > 0)
            {
                foreach ( var item in ImageItems )
                {
                    bool isConnect = network.IsConnected;
                    IsBusy = true;
                    try
                    {
                        string userID;

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

                        if (!isConnect)
                        {
                            await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                        }

                        var body = new Model.Index.Body.PostPicture
                        {
                            pictureID = await GetNewAutoNumber.GetPictureID(),
                            picture = item.ImageString,
                            pictureName = item.ImageName,
                            pictureFormat = item.ImageFormat,
                            personID = userID,
                            isDeleted = ParameterModel.ItemDefaultValue.IsDeleted,
                            createdByUserID = userID,
                            createdDateTime = ParameterModel.DateFormat.DateTime,
                            lastUpdateDateTime = ParameterModel.DateFormat.DateTime,
                            lastUpdateByUserID = userID,
                            pictureSize = item.ImageSize
                        };

                        var picture = await PostUserPicture.PostNewUserPicture(body);
                        if (!string.IsNullOrEmpty(picture))
                        {
                            await MsgModel.MsgNotification($"{picture}");
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
                await LoadData();
            }
        }
    }
}
