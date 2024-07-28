using UangKu.Model.Base;
using UangKu.Model.Menu;
using UangKu.Model.Response.Picture;
using UangKu.Model.Session;
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
                var sessionID = App.Session;
                string userID = SessionModel.GetUserID(sessionID);

                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }

                if (!string.IsNullOrEmpty(userID))
                {
                    var picture = await GetUserPicture.GetAllUserPicture(ParameterModel.ItemDefaultValue.FirstPage, AppParameter.MaxPicture,
                        userID, ParameterModel.ItemDefaultValue.IsDeleted);
                    if (picture.metaData.isSucces && picture.metaData.code == 200)
                    {
                        ListUserPicture.Clear();
                        for (int i = 0; i < picture.data.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(picture.data[i].picture))
                            {
                                string decodeImg = Converter.DecodeBase64ToString(picture.data[i].picture);
                                byte[] byteImg = Converter.StringToByteImg(decodeImg);
                                picture.data[i].source = ImageConvert.ImgByte(byteImg);
                            }

                            if (!string.IsNullOrEmpty(picture.data[i].pictureFormat))
                            {
                                string result = ImageConvert.SubstringContentType(picture.data[0].pictureFormat, '/');
                                picture.data[i].contenttype = result;
                            }
                        }
                        Page = (int)picture.pageNumber;
                        TotalPages = (int)picture.totalPages;
                        TotalRecords = (int)picture.totalRecords;
                        ListUserPicture.Add(picture);

                        ListUserPictureTwo.Clear();
                        foreach (var item in picture.data)
                        {
                            var datum = new UserPictureTwo.Datum
                            {
                                pictureID = item.pictureID,
                                isDeleted = item.isDeleted
                            };

                            ListUserPictureTwo.Add(datum);
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
        public async void NextPreviousPage_Clicked(int pageSize, bool isNext)
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
                if (Page >= TotalPages && isNext)
                {
                    await MsgModel.MsgNotification("This Is The Latest Page");
                }
                else if (Page <= 1 && !isNext)
                {
                    await MsgModel.MsgNotification("This Is The First Page");
                }
                else
                {
                    int pages = isNext ? Page + 1 : Page - 1;
                    var picture = await GetUserPicture.GetAllUserPicture(pages, pageSize,
                        userID, ParameterModel.ItemDefaultValue.IsDeleted);
                    if (picture.metaData.isSucces && picture.metaData.code == 200)
                    {
                        ListUserPicture.Clear();
                        for (int i = 0; i < picture.data.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(picture.data[i].picture))
                            {
                                string decodeImg = Converter.DecodeBase64ToString(picture.data[i].picture);
                                byte[] byteImg = Converter.StringToByteImg(decodeImg);
                                picture.data[i].source = ImageConvert.ImgByte(byteImg);
                            }

                            if (!string.IsNullOrEmpty(picture.data[i].pictureFormat))
                            {
                                string result = ImageConvert.SubstringContentType(picture.data[0].pictureFormat, '/');
                                picture.data[i].contenttype = result;
                            }
                        }
                        Page = (int)picture.pageNumber;
                        TotalPages = (int)picture.totalPages;
                        TotalRecords = (int)picture.totalRecords;
                        ListUserPicture.Add(picture);

                        ListUserPictureTwo.Clear();
                        foreach (var item in picture.data)
                        {
                            var datum = new UserPictureTwo.Datum
                            {
                                pictureID = item.pictureID,
                                isDeleted = item.isDeleted
                            };

                            ListUserPictureTwo.Add(datum);
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
        public async Task DeleteUserPicture_Clicked()
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
                if (ListUserPicture.Count == 0)
                {
                    await MsgModel.MsgNotification($"No Images Have Been Uploaded For {userID} Yet");
                }
                else
                {
                    if (ListUserPicture[0].data.Count != ListUserPictureTwo.Count)
                    {
                        await MsgModel.MsgNotification($"Data List Is Different");
                    }
                    else
                    {
                        for (int i = 0; i < ListUserPicture[0].data.Count; i++)
                        {
                            if (ListUserPicture[0].data[i].isDeleted != ListUserPictureTwo[i].isDeleted)
                            {
                                var different = new DifferentUserPicture.Datum
                                {
                                    pictureID = ListUserPicture[0].data[i].pictureID,
                                    isDeleted = ListUserPicture[0].data[i].isDeleted
                                };
                                ListDifferentUserPicture.Add(different);
                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(userID))
                {
                    if (ListDifferentUserPicture.Count == 0)
                    {
                        await MsgModel.MsgNotification($"No Picture Are Selected For Delete");
                    }
                    else
                    {
                        foreach (var item in ListDifferentUserPicture)
                        {
                            var body = new Model.Index.Body.DeleteUserPicture
                            {
                                lastUpdateUserID = userID,
                                isDeleted = item.isDeleted
                            };

                            var delete = await DeleteUserPicture.DeleteUserPictureID(item.pictureID, body);

                            if (!string.IsNullOrEmpty(delete))
                            {
                                await MsgModel.MsgNotification($"{delete}");
                            }
                        }
                        await LoadData();
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
                ListDifferentUserPicture.Clear();
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
                        var sessionID = App.Session;
                        string userID = SessionModel.GetUserID(sessionID);

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
                        item.ImageString = string.Empty;
                    }
                }
                await LoadData();
            }
        }
    }
}
