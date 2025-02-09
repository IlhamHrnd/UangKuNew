using UangKu.Model.Base;
using UangKu.Model.Menu;
using UangKu.View.Module.UserManagement;

namespace UangKu.ViewModel.Menu
{
    public class ProfileVM : Profile
    {
        public ProfileVM(INavigation navigation)
        {
            Navigation = navigation;
        }

        public async void LoadData()
        {
            Person = new WebService.Data.Root<WebService.Data.Profile.Data>();

            if (Network.IsConnected)
            {
                AppProgram(Model.Base.AppProgram.Profile);

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
                    if (profile.Succeeded == true)
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

                    Mode = profile.Succeeded ?? false ? ItemManager.EditFile : ItemManager.NewFile;
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

        public async void NavigationPage()
        {
            await Navigation.PushAsync(new ProfileEdit(Mode));
        }
    }
}
