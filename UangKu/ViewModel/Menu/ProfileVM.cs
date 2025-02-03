using UangKu.Model.Base;
using UangKu.Model.Menu;

namespace UangKu.ViewModel.Menu
{
    public class ProfileVM : Profile
    {
        public ProfileVM()
        {

        }

        public async void LoadData()
        {
            IsBusy = true;

            #region Variabel
            var userID = SessionModel.GetUserID();
            #endregion

            if (Network.IsConnected)
            {
                try
                {
                    var program = await WebService.Service.AppProgram.GetAppProgramID(new WebService.Filter.Root<WebService.Filter.AppProgram>
                    {
                        Data = new WebService.Filter.AppProgram
                        {
                            ProgramID = AppProgram.Profile
                        }
                    });
                    if (program.Succeeded == true)
                    {
                        Title = program.Data.programName;
                        IsProgram = program.Data.isProgram;
                        IsProgramAddAble = program.Data.isProgramAddAble ?? false;
                        IsProgramEditAble = program.Data.isProgramEditAble ?? false;
                        IsProgramDeleteAble = program.Data.isProgramDeleteAble ?? false;
                        IsProgramViewAble = program.Data.isProgramViewAble ?? false;
                        IsProgramApprovalAble = program.Data.isProgramApprovalAble ?? false;
                        IsProgramUnApprovalAble = program.Data.isProgramUnApprovalAble ?? false;
                        IsProgramVoidAble = program.Data.isProgramVoidAble ?? false;
                        IsProgramUnVoidAble = program.Data.isProgramUnVoidAble ?? false;
                        IsVisible = program.Data.isVisible ?? false;
                        IsUsedBySystem = program.Data.isUsedBySystem ?? false;
                    }
                    else
                        await MsgModel.MsgNotification(program.Message);
                }
                catch (Exception e)
                {
                    await MsgModel.MsgNotification(e.Message);
                }

                try
                {
                    var profile = await WebService.Service.Profile.GetPersonID(new WebService.Filter.Root<WebService.Filter.Profile>
                    {
                        Data = new WebService.Filter.Profile
                        {
                            PersonID = userID
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
                }
                catch (Exception e)
                {
                    await MsgModel.MsgNotification(e.Message);
                }
            }
            IsBusy = false;
        }
    }
}
