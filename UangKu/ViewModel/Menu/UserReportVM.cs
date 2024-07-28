using UangKu.Model.Base;
using UangKu.Model.Menu;
using UangKu.Model.Session;
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
                    var alluser = await UserAll.GetAllUser(ParameterModel.ItemDefaultValue.FirstPage, AppParameter.MaxResult);
                    if (alluser.metaData.isSucces && alluser.metaData.code == 200)
                    {
                        ListAllUser.Clear();
                        foreach (var data in alluser.data)
                        {
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
                var report = await GetUserReport.GetAllUserReport(ParameterModel.ItemDefaultValue.FirstPage, AppParameter.MaxResult, personID);
                if (report.metaData.isSucces && report.metaData.code == 200)
                {
                    ListReport.Clear();
                    foreach (var data in report.data)
                    {
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
                            string decodeImg = Converter.DecodeBase64ToString(data.picture);
                            byte[] byteImg = Converter.StringToByteImg(decodeImg);
                            data.source = ImageConvert.ImgByte(byteImg);
                        }
                    }
                    Page = (int)report.pageNumber;
                    ListReport.Add(report);
                }
                else
                {
                    await MsgModel.MsgNotification(report.metaData.message);
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

        public async Task SwipeItem_Invoked(object sender, string mode)
        {
            var item = sender as SwipeItem;
            if (item == null)
            {
                return;
            }

            var reportNo = item.BindingContext as Model.Response.Report.Report.Datum;
            if (reportNo == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(reportNo.reportNo))
            {
                await MsgModel.MsgNotification($"Report No Is Null");
            }
            else
            {
                ParameterModel.Report.ReportNo = reportNo.reportNo;
                await _navigation.PushAsync(new ReportDetail(mode));
            }
        }

        public async Task AddNewReport_ToolBar(string mode)
        {
            await _navigation.PushAsync(new ReportDetail(mode));
        }
    }
}
