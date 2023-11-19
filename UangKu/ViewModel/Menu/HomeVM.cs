using UangKu.Model.Base;
using UangKu.Model.Menu;
using UangKu.View.SubMenu;
using UangKu.ViewModel.RestAPI.Picture;
using UangKu.ViewModel.RestAPI.Profile;

namespace UangKu.ViewModel.Menu
{
    public class HomeVM : Home
    {
        private NetworkModel network = NetworkModel.Instance;
        private readonly INavigation _navigation;

        public HomeVM(INavigation navigation)
        {
            LoadData();
            _navigation = navigation;
        }
        private void LoadData()
        {
            string greeting;
            switch (ParameterModel.DateFormat.DateTime.Hour)
            {
                case int h when h >= 0 && h <= 10:
                    greeting = "Good Morning";
                    Image = "morning.svg";
                    break;

                case int h when h > 10 && h <= 15:
                    greeting = "Good Afternoon";
                    Image = "afternoon.svg";
                    break;

                case int h when h > 15 && h <= 19:
                    greeting = "Good Evening";
                    Image = "evening.svg";
                    break;

                default:
                    greeting = "Good Night";
                    Image = "night.svg";
                    break;
            }

            Name = $"Hello, {App.Session.username} {greeting}";
            Person = $"{App.Session.username}";
        }

        public async void LoadDataPerson()
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
                    var person = await GetProfile.GetProfileID(userID);
                    if (string.IsNullOrEmpty(person.personID))
                    {
                        await MsgModel.MsgNotification($"Please, Fill Profile For {userID} First");
                        await _navigation.PushAsync(new EditProfile(ParameterModel.ItemDefaultValue.NewFile));
                    }

                    var picture = await GetUserPicture.GetAllUserPicture(ParameterModel.ItemDefaultValue.FirstPage, ParameterModel.ItemDefaultValue.Maxresult, 
                        App.Session.personID, ParameterModel.ItemDefaultValue.IsDeleted);
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
    }
}
