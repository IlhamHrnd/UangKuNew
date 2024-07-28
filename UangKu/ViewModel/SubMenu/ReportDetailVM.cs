using CommunityToolkit.Maui.Views;
using UangKu.Model.Base;
using UangKu.Model.SubMenu;
using static UangKu.Model.Base.ParameterModel.PermissionManager;
using static UangKu.Model.Response.AppStandardReferenceItem.AppStandardReferenceItem;

namespace UangKu.ViewModel.SubMenu
{
    public class ReportDetailVM : ReportDetail
    {
        private NetworkModel network = NetworkModel.Instance;
        private readonly INavigation _navigation;
        public ReportDetailVM(INavigation navigation, string mode)
        {
            _navigation = navigation;
            Mode = mode;
        }

        public void SetVisibility(Button btnAddNewReport)
        {
            IsEnabled = App.Access.IsAdmin;
            if (Mode == ParameterModel.ItemDefaultValue.NewFile)
            {
                Title = $"New Report";
                btnAddNewReport.Text = "Add New Report";
            }
            else if (Mode == ParameterModel.ItemDefaultValue.EditFile)
            {
                Title = $"Report Detail {ParameterModel.Report.ReportNo}";
                btnAddNewReport.Text = $"Update Report {ParameterModel.Report.ReportNo}";
            }
            else
            {
                Title = string.Empty;
                btnAddNewReport.Text = string.Empty;
            }
        }

        public async void LoadData(DatePicker errorDate, Editor cronologicEditor, Label reportstatusLabel, Label createdateLabel, Label lastupdateLabel, Label personidLabel, CheckBox isapproveCheckBox)
        {
            bool isConnect = network.IsConnected;
            bool isAdmin = App.Access.IsAdmin;
            IsBusy = true;
            try
            {
                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }
                if (Mode == ParameterModel.ItemDefaultValue.NewFile)
                {
                    IsEditAble = true;
                }
                else if (Mode == ParameterModel.ItemDefaultValue.EditFile)
                {
                    var reportNo = ParameterModel.Report.ReportNo;
                    var userID = SessionModel.GetUserID(App.Session);

                    if (!string.IsNullOrEmpty(reportNo))
                    {
                        var report = await RestAPI.Report.GetReportNo.GetUserReportNo(reportNo, isAdmin);
                        if (report.metaData.isSucces && report.metaData.code == 200)
                        {
                            if (report.dateErrorOccured.HasValue)
                            {
                                errorDate.Date = (DateTime)report.dateErrorOccured;
                            }
                            if (!string.IsNullOrEmpty(report.errorCronologic))
                            {
                                cronologicEditor.Text = report.errorCronologic;
                            }
                            if (!string.IsNullOrEmpty(report.personID))
                            {
                                personidLabel.Text = report.personID;
                                IsEditAble = Compare.StringCompare(report.personID, userID);
                            }
                            if (isAdmin)
                            {
                                if ((bool)report.isApprove)
                                {
                                    isapproveCheckBox.IsChecked = (bool)report.isApprove;
                                }
                                if (isapproveCheckBox.IsChecked)
                                {
                                    lastupdateLabel.Text = report.approvedDateTime.HasValue
                                        ? DateFormat.FormattingDate((DateTime)report.approvedDateTime, ParameterModel.DateTimeFormat.Datetime)
                                        : DateFormat.FormattingDate((DateTime)report.lastUpdateDateTime, ParameterModel.DateTimeFormat.Datetime);
                                }
                                else
                                {
                                    lastupdateLabel.Text = report.voidDateTime.HasValue
                                        ? DateFormat.FormattingDate((DateTime)report.voidDateTime, ParameterModel.DateTimeFormat.Datetime)
                                        : DateFormat.FormattingDate((DateTime)report.lastUpdateDateTime, ParameterModel.DateTimeFormat.Datetime);
                                }
                                if (!string.IsNullOrEmpty(report.srReportStatus))
                                {
                                    reportstatusLabel.Text = report.srReportStatus;
                                }
                                if (report.createdDateTime.HasValue)
                                {
                                    createdateLabel.Text = DateFormat.FormattingDate((DateTime)report.createdDateTime, ParameterModel.DateTimeFormat.Datetime);
                                }
                            }
                        }
                        else
                        {
                            await MsgModel.MsgNotification(report.metaData.message);
                        }
                    }
                }
                if (isAdmin)
                {
                    var status = await RestAPI.AppStandardReferenceItem.AppStandardReferenceItem.GetAsriAsync<AsriThreeRoot>("ReportStatus", true, true);
                    if (status.Count > 0)
                    {
                        ListReportStatus.Clear();
                        for (int i = 0; i < status.Count; i++)
                        {
                            ListReportStatus.Add(status[i]);
                        }
                    }
                    IsVisible = true;
                }
                else
                {
                    IsVisible = false;
                }
                var location = await RestAPI.AppStandardReferenceItem.AppStandardReferenceItem.GetAsriAsync<AsriRoot>("ErrorLocation", true, true);
                if (location.Count > 0)
                {
                    ListErrorLocation.Clear();
                    for (int i = 0; i < location.Count; i++)
                    {
                        ListErrorLocation.Add(location[i]);
                    }
                }
                var posibility = await RestAPI.AppStandardReferenceItem.AppStandardReferenceItem.GetAsriAsync<AsriTwoRoot>("ErrorPossibility", true, true);
                if (posibility.Count > 0)
                {
                    ListErrorPosibility.Clear();
                    for (int i = 0; i < posibility.Count; i++)
                    {
                        ListErrorPosibility.Add(posibility[i]);
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

        public async Task UploadPhoto_Click(AvatarView avatar)
        {
            PermissionType type = PermissionType.StorageRead;
            await PermissionRequest.RequestPermission(type);

            ImageSource source = await ImageConvert.PickImageAsync();

            if (source != null)
            {
                avatar.ImageSource = source;
                avatar.Text = ParameterModel.ImageManager.ImageName;
            }
        }

        public async Task SaveReport_Click(Editor editor, Picker PicLocation, Picker PicPosibility, DatePicker DatePic)
        {
            bool isConnect = network.IsConnected;
            IsBusy = true;
            try
            {
                bool isValidPicker = await ValidateNullChecker.PickerValidateFields(
                    (PicLocation, "Location"),
                    (PicPosibility, "Posibility")
                );

                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }
                if (isValidPicker)
                {
                    if (Mode == ParameterModel.ItemDefaultValue.NewFile)
                    {
                        var userID = SessionModel.GetUserID(App.Session);
                        var bodyPost = new Model.Index.Body.PostReport
                        {
                            reportNo = await GetNewAutoNumber.GetReportNo(),
                            dateErrorOccured = DatePic.Date,
                            srErrorLocation = SelectedErrorLocation.itemID,
                            srErrorPossibility = SelectedErrorPosibility.itemID,
                            errorCronologic = editor.Text,
                            picture = ParameterModel.ImageManager.ImageString,
                            createdDateTime = ParameterModel.DateFormat.DateTime,
                            createdByUserID = userID,
                            lastUpdateDateTime = ParameterModel.DateFormat.DateTime,
                            lastUpdateByUserID = userID,
                            personID = userID
                        };

                        var report = await RestAPI.Report.PostReport.PostNewReport(bodyPost);
                        if (!string.IsNullOrEmpty(report))
                        {
                            await MsgModel.MsgNotification($"{report}");
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
                ParameterModel.ImageManager.ImageString = string.Empty;
            }
        }
    }
}
