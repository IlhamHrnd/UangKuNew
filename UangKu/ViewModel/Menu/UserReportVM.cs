using UangKu.Model.Base;
using UangKu.Model.Menu;
using UangKu.View.SubMenu;
using UangKu.ViewModel.RestAPI.Report;
using UangKu.ViewModel.RestAPI.User;

namespace UangKu.ViewModel.Menu
{
    public class UserReportVM : UserReport
    {
        private NetworkModel network = NetworkModel.Instance;
        private readonly INavigation _navigation;
        public UserReportVM(INavigation navigation)
        {
            _navigation = navigation;
        }

        public void SetVisibility()
        {
            if (App.Access.IsAdmin)
            {
                Title = $"All User Report";
                IsVisible = true;
            }
            else
            {
                Title = $"{App.Session.personID} Report";
                IsVisible = false;
            }
        }

        public async void LoadData()
        {
            bool isConnect = network.IsConnected;
            string personID = string.Empty;
            IsBusy = true;
            try
            {
                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }
                if (App.Access.IsAdmin)
                {
                    var alluser = await UserAll.GetAllUser(ParameterModel.ItemDefaultValue.FirstPage, ParameterModel.ItemDefaultValue.Maxresult);
                    if ((bool)alluser.succeeded)
                    {
                        ListAllUser.Clear();
                        for (int i = 0; i < alluser.data.Count; i++)
                        {
                            var data = alluser.data[i];
                            if (!string.IsNullOrEmpty(data.statusName))
                            {
                                data.isActive = data.statusName == ParameterModel.Login.Status;
                            }

                            if (data.activeDate != null)
                            {
                                data.dateActive = DateFormat.FormattingDate((DateTime)data.activeDate, ParameterModel.DateTimeFormat.Date);
                            }

                            if (data.lastLogin != null)
                            {
                                data.dateLogin = DateFormat.FormattingDate((DateTime)data.lastLogin, ParameterModel.DateTimeFormat.Date);
                            }
                        }
                        ListAllUser.Add(alluser);
                    }
                }
                else
                {
                    personID = $"&PersonID={App.Session.personID}";
                }
                var report = await GetUserReport.GetAllUserReport(ParameterModel.ItemDefaultValue.FirstPage, ParameterModel.ItemDefaultValue.Maxresult, personID);
                if ((bool)report.succeeded)
                {
                    ListReport.Clear();
                    for (int i = 0; i < report.data.Count; i++)
                    {
                        var data = report.data[i];
                        data.isvisible = App.Access.IsAdmin;
                        if (data.dateErrorOccured != null)
                        {
                            data.dateError = DateFormat.FormattingDate((DateTime)data.dateErrorOccured, ParameterModel.DateTimeFormat.Datetime);
                        }
                        if (data.createdDateTime != null)
                        {
                            data.dateCreated = DateFormat.FormattingDate((DateTime)data.createdDateTime, ParameterModel.DateTimeFormat.Datetime);
                        }
                        if (!string.IsNullOrEmpty(data.picture))
                        {
                            string decodeImg = ImageConvert.DecodeBase64ToString(data.picture);
                            byte[] byteImg = ImageConvert.StringToByteImg(decodeImg);
                            data.source = ImageConvert.ImgByte(byteImg);
                        } 
                    }
                    Page = (int)report.pageNumber;
                    ListReport.Add(report);
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

        public async Task UserReport_PopUp(SelectionChangedEventArgs args, string mode)
        {
            var standardID = args.CurrentSelection[0] as Model.Response.Report.Report.Datum;
            var reportNo = standardID?.reportNo;
            ParameterModel.Report.ReportNo = reportNo;

            if (!string.IsNullOrEmpty(reportNo))
            {
                await _navigation.PushAsync(new ReportDetail(mode));
            }
            else
            {
                await MsgModel.MsgNotification($"You Haven't Selected An Item Yet");
            }
        }

        public async Task AddNewReport_ToolBar(string mode)
        {
            await _navigation.PushAsync(new ReportDetail(mode));
        }
    }
}
