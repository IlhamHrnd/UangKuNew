﻿using CommunityToolkit.Maui.Storage;
using System.Text;
using UangKu.Model.Base;
using UangKu.Model.Menu;
using UangKu.Model.Session;
using static UangKu.Model.Response.AppStandardReferenceItem.AppStandardReferenceItem;
using static UangKu.Model.Response.Transaction.AllTransaction;
using static UangKu.Model.Response.Transaction.SumTransaction;

namespace UangKu.ViewModel.Menu
{
    public class TransactionLogVM : TransactionLog
    {
        private NetworkModel network = NetworkModel.Instance;
        private readonly INavigation _navigation;
        public TransactionLogVM(INavigation navigation)
        {
            Title = $"Transaction Log For {App.Session.username}";
            _navigation = navigation;
        }
        public async void LoadData(int pageNumber, int pageSize, DatePicker startDate, DatePicker endDate, 
            Picker orderByPicker, InputKit.Shared.Controls.CheckBox isAscendingCheckBox, InputKit.Shared.Controls.CheckBox isFilterTransaction)
        {
            var isallow = Converter.StringToBool(AppParameter.IsAllowCustomDate, ParameterModel.AppParameterDefault.IsAllowCustomDate);
            IsAllowCustomDate = isallow;

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
                if (IsAllowCustomDate && isFilterTransaction.IsChecked)
                {
                    if (startDate != null && endDate != null)
                    {
                        DateRange = $"&StartDate={startDate.Date}&EndDate={endDate.Date}";
                    }

                    if (orderByPicker.SelectedItem != null)
                    {
                        switch (SelectedOrderBy.itemID)
                        {
                            case "OrderByTransaction-001":
                                OrderBy = "&OrderBy=TransNo";
                                break;

                            case "OrderByTransaction-002":
                                OrderBy = "&OrderBy=TransType";
                                break;

                            case "OrderByTransaction-003":
                                OrderBy = "&OrderBy=CreatedDateTime";
                                break;

                            default:
                                OrderBy = "&OrderBy=CreatedDateTime";
                                break;
                        }
                    }

                    Ascending = isAscendingCheckBox.IsChecked ? "&IsAscending=true" : "&IsAscending=false";
                    Builder = Converter.BuilderString(DateRange, OrderBy, Ascending);
                }
                else
                {
                    Builder = string.Empty;
                }
                var sumtrans = await RestAPI.Transaction.GetSumTransaction.GetSumTransactionID(userID);
                if (sumtrans.Count > 0)
                {
                    ListSumTrans.Clear();
                    for (int i = 0; i < sumtrans.Count; i++)
                    {
                        var item = sumtrans[i];
                        if (item.amount != null)
                        {
                            item.amountFormat = FormatCurrency.Currency((decimal)item.amount, Compare.StringReplace(AppParameter.CurrencyFormat, ParameterModel.AppParameterDefault.Currency));
                        }

                        ListSumTrans.Add(item);

                        switch (item.srTransaction)
                        {
                            case "Income":
                                ParameterModel.Transaction.Income = (decimal)item.amount;
                                break;

                            case "Expenditure":
                                ParameterModel.Transaction.Expenditure = (decimal)item.amount;
                                break;
                        }
                    }
                }
                var alltrans = await RestAPI.Transaction.AllTransaction.GetAllTransaction(pageNumber, pageSize, userID, Builder);
                if (alltrans.data.Count > 0)
                {
                    ListAllTrans.Clear();
                    var item = alltrans;
                    var datas = item.data;
                    for (int i = 0; i < datas.Count; i++)
                    {
                        if (datas[i].amount != null)
                        {
                            datas[i].amountFormat = FormatCurrency.Currency((decimal)datas[i].amount, Compare.StringReplace(AppParameter.CurrencyFormat, ParameterModel.AppParameterDefault.Currency));
                        }

                        if (!string.IsNullOrEmpty(datas[i].photo))
                        {
                            string decodeImg = Converter.DecodeBase64ToString(datas[i].photo);
                            byte[] byteImg = Converter.StringToByteImg(decodeImg);
                            datas[i].source = ImageConvert.ImgByte(byteImg);
                        }
                        
                        if (datas[i].transDate != null)
                        {
                            datas[i].transDateFormat = DateFormat.FormattingDate((DateTime)datas[i].transDate, ParameterModel.DateTimeFormat.Date);
                        }
                    }
                    Page = (int)alltrans.pageNumber;
                    TotalPages = (int)alltrans.totalPages;
                    TotalRecords = (int)alltrans.totalRecords;
                    ListAllTrans.Add(item);
                }
                var orderby = await RestAPI.AppStandardReferenceItem.AppStandardReferenceItem.GetAsriAsync<AsriRoot>("OrderByTransaction", true, true);
                if (orderby.Count > 0)
                {
                    ListOrderBy.Clear();
                    for (int i = 0; i < orderby.Count; i++)
                    {
                        ListOrderBy.Add(orderby[i]);
                    }
                }
                if (ParameterModel.Transaction.Income != 0 && ParameterModel.Transaction.Expenditure != 0 && ListSumTrans.Count > 0)
                {
                    decimal? amount = ParameterModel.Transaction.Income - ParameterModel.Transaction.Expenditure;
                    string srTransaction = "Summary";
                    string amountFormat = FormatCurrency.Currency((decimal)amount, Compare.StringReplace(AppParameter.CurrencyFormat, ParameterModel.AppParameterDefault.Currency));

                    var item = new SumTransactionRoot
                    {
                        amount = amount,
                        srTransaction = srTransaction,
                        amountFormat = amountFormat
                    };

                    ListSumTrans.Add(item);
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
        public async void NextPreviousPage_Clicked(int pageSize, DatePicker startDate, DatePicker endDate, Picker orderByPicker, 
            InputKit.Shared.Controls.CheckBox isAscendingCheckBox, bool isNext, InputKit.Shared.Controls.CheckBox isFilterTransaction)
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
                    if (IsAllowCustomDate && isFilterTransaction.IsChecked)
                    {
                        if (startDate != null && endDate != null)
                        {
                            DateRange = $"&StartDate={startDate.Date}&EndDate={endDate.Date}";
                        }

                        if (orderByPicker.SelectedItem != null)
                        {
                            switch (SelectedOrderBy.itemID)
                            {
                                case "OrderByTransaction-001":
                                    OrderBy = "&OrderBy=TransNo";
                                    break;

                                case "OrderByTransaction-002":
                                    OrderBy = "&OrderBy=TransType";
                                    break;

                                case "OrderByTransaction-003":
                                    OrderBy = "&OrderBy=CreatedDateTime";
                                    break;

                                default:
                                    OrderBy = "&OrderBy=CreatedDateTime";
                                    break;
                            }
                        }

                        Ascending = isAscendingCheckBox.IsChecked ? "&IsAscending=true" : "&IsAscending=false";
                        Builder = Converter.BuilderString(DateRange, OrderBy, Ascending);
                    }
                    var alltrans = await RestAPI.Transaction.AllTransaction.GetAllTransaction(pages, pageSize, userID, Builder);
                    if (alltrans.data.Count > 0)
                    {
                        ListAllTrans.Clear();
                        var item = alltrans;
                        for (int i = 0; i < item.data.Count; i++)
                        {
                            if (item.data[i].amount != null)
                            {
                                item.data[i].amountFormat = FormatCurrency.Currency((decimal)item.data[i].amount, Compare.StringReplace(AppParameter.CurrencyFormat, ParameterModel.AppParameterDefault.Currency));
                            }

                            if (!string.IsNullOrEmpty(item.data[i].photo))
                            {
                                string decodeImg = Converter.DecodeBase64ToString(item.data[i].photo);
                                byte[] byteImg = Converter.StringToByteImg(decodeImg);
                                item.data[i].source = ImageConvert.ImgByte(byteImg);
                            }
                        }
                        Page = (int)alltrans.pageNumber;
                        TotalPages = (int)alltrans.totalPages;
                        TotalRecords = (int)alltrans.totalRecords;
                        ListAllTrans.Add(item);
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
        public void ScrollTopBottom_Clicked(ScrollView scroll, double x, double y, bool isAnimated)
        {
            scroll.ScrollToAsync(x, y, isAnimated);
        }
        public async Task SavePDF(CancellationToken cancellationToken)
        {
            var sessionID = App.Session;
            string userID = SessionModel.GetUserID(sessionID);
            string title = $"{userID} Transaction Report \n";

            var month = DateFormat.FormattingDate(ParameterModel.DateFormat.DateTime, ParameterModel.DateTimeFormat.Month);
            var subtitle = $"Periode {month}";

            var str = Converter.BuilderString(title, subtitle);

            try
            {
                using var stream = new MemoryStream(Encoding.Default.GetBytes(str));
                var fileSaverResult = await FileSaver.Default.SaveAsync($"{userID} Transaction Report.txt", stream, cancellationToken);
                if (fileSaverResult.IsSuccessful)
                {
                    await MsgModel.MsgNotification($"The file was saved successfully to location: {fileSaverResult.FilePath}", cancellationToken);
                }
                else
                {
                    await MsgModel.MsgNotification($"The file was not saved successfully with error: {fileSaverResult.Exception.Message}", cancellationToken);
                }
            }
            catch (Exception e)
            {
                await MsgModel.MsgNotification($"{e.Message}");
            }
        }
        public async Task SwipeItem_Invoked(object sender, string mode)
        {
            var item = sender as SwipeItem;
            if (item == null)
            {
                return;
            }

            var transNo = item.BindingContext as Datum;
            if (transNo == null)
            {
                return;
            }

            await _navigation.PushAsync(new View.SubMenu.NewTransaction(mode, transNo.transNo));
        }
        public async Task NewTransaction_ToolBar(string mode)
        {
            await _navigation.PushAsync(new View.SubMenu.NewTransaction(mode, string.Empty));
        }
    }
}
