using iText.Kernel.Pdf;
using iText.Layout;
using UangKu.Model.Base;
using UangKu.Model.Menu;
using UangKu.Model.Response.Transaction;
using UangKu.Model.Session;
using static UangKu.Model.Base.ParameterModel.PermissionManager;
using static UangKu.Model.Response.AppStandardReferenceItem.AppStandardReferenceItem;

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
            Picker orderByPicker, InputKit.Shared.Controls.CheckBox isAscendingCheckBox, InputKit.Shared.Controls.CheckBox isFilterTransaction,
            InputKit.Shared.Controls.SelectionView transFilter)
        {
            IsAllowCustomDate = AppParameter.IsAllowCustomDate;
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
                    if (transFilter.SelectedItem != null)
                    {
                        var date = ParameterModel.DateFormat.DateTime;
                        DateTime startDays;
                        DateTime endDays;
                        string start;
                        string end;

                        switch (SelectedFilter.itemID)
                        {
                            case "TransFilter-001":
                                DateRange = $"&StartDate={date}&EndDate={date}";
                                break;

                            case "TransFilter-002":
                                startDays = DateFormat.AddDays(-7, date);
                                start = DateFormat.FormattingDate(startDays, ParameterModel.DateTimeFormat.Date);
                                end = DateFormat.FormattingDate(date, ParameterModel.DateTimeFormat.Date);
                                DateRange = $"&StartDate={start}&EndDate={end}";
                                break;

                            case "TransFilter-003":
                                startDays = DateFormat.FormattingDateSplit(date.Year, date.Month, 1);
                                start = DateFormat.FormattingDate(startDays, ParameterModel.DateTimeFormat.Date);
                                end = DateFormat.FormattingDate(date, ParameterModel.DateTimeFormat.Date);
                                DateRange = $"&StartDate={start}&EndDate={end}";
                                break;

                            case "TransFilter-004":
                                startDays = DateFormat.FormattingDateSplit(date.Year, date.Month - 1, 1);
                                endDays = DateFormat.FormattingDateSplit(date.Year, date.Month - 1, date.Day);
                                start = DateFormat.FormattingDate(startDays, ParameterModel.DateTimeFormat.Date);
                                end = DateFormat.FormattingDate(endDays, ParameterModel.DateTimeFormat.Date);
                                DateRange = $"&StartDate={start}&EndDate={end}";
                                break;
                        }
                    }
                    else if (startDate != null && endDate != null)
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
                    BuilderSum = Converter.BuilderString(DateRange);
                }
                else
                {
                    Builder = string.Empty;
                    BuilderSum = string.Empty;
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
                var transfilter = await RestAPI.AppStandardReferenceItem.AppStandardReferenceItem.GetAsriAsync<AsriTwoRoot>("TransFilter", true, true);
                if (transfilter.Count > 0)
                {
                    ListTrans.Clear();
                    for (int i = 0; i < transfilter.Count; i++)
                    {
                        ListTrans.Add(transfilter[i]);
                    }
                }

                var alltrans = await RestAPI.Transaction.AllTransaction.GetAllTransaction(pageNumber, pageSize, userID, Builder);
                if (alltrans.metaData.isSucces && alltrans.metaData.code == 200)
                {
                    ListAllTrans.Clear();
                    foreach (Model.Response.Transaction.AllTransaction.Datum datas in alltrans.data)
                    {
                        if (datas.amount != null)
                        {
                            datas.amountFormat = FormatCurrency.Currency((decimal)datas.amount, AppParameter.CurrencyFormat);
                        }

                        if (!string.IsNullOrEmpty(datas.photo))
                        {
                            string decodeImg = Converter.DecodeBase64ToString(datas.photo);
                            byte[] byteImg = Converter.StringToByteImg(decodeImg);
                            datas.source = ImageConvert.ImgByte(byteImg);
                        }

                        if (datas.transDate != null)
                        {
                            datas.transDateFormat = DateFormat.FormattingDate((DateTime)datas.transDate, ParameterModel.DateTimeFormat.Date);
                        }
                    }
                    Page = (int)alltrans.pageNumber;
                    TotalPages = (int)alltrans.totalPages;
                    TotalRecords = (int)alltrans.totalRecords;
                    Number = pageNumber;
                    Size = pageSize;
                    ListAllTrans.Add(alltrans);

                    var sumtrans = await RestAPI.Transaction.GetSumTransaction.GetSumTransactionID(userID, BuilderSum);
                    if (sumtrans.metaData.isSucces && sumtrans.metaData.code == 200 && sumtrans.data.Count > 0)
                    {
                        ListSumTrans.Clear();
                        foreach (var item in sumtrans.data)
                        {
                            if (item.amount != null)
                            {
                                item.amountFormat = FormatCurrency.Currency((decimal)item.amount, AppParameter.CurrencyFormat);
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

                    if (ParameterModel.Transaction.Income != 0 && ParameterModel.Transaction.Expenditure != 0 && ListSumTrans.Count > 0)
                    {
                        decimal? amount = ParameterModel.Transaction.Income - ParameterModel.Transaction.Expenditure;
                        string srTransaction = "Summary";
                        string amountFormat = FormatCurrency.Currency((decimal)amount, AppParameter.CurrencyFormat);

                        var item = new Model.Response.Transaction.SumTransaction.Datum
                        {
                            amount = amount,
                            srTransaction = srTransaction,
                            amountFormat = amountFormat
                        };

                        ListSumTrans.Add(item);
                    }
                }
                else
                {
                    await MsgModel.MsgNotification(alltrans.metaData.message);
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
            InputKit.Shared.Controls.CheckBox isAscendingCheckBox, bool isNext, InputKit.Shared.Controls.CheckBox isFilterTransaction,
            InputKit.Shared.Controls.SelectionView transFilter)
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
                        if (transFilter.SelectedItem != null)
                        {
                            var date = ParameterModel.DateFormat.DateTime;
                            DateTime startDays;
                            DateTime endDays;
                            string start;
                            string end;

                            switch (SelectedFilter.itemID)
                            {
                                case "TransFilter-001":
                                    DateRange = $"&StartDate={date}&EndDate={date}";
                                    break;

                                case "TransFilter-002":
                                    startDays = DateFormat.AddDays(-7, date);
                                    start = DateFormat.FormattingDate(startDays, ParameterModel.DateTimeFormat.Date);
                                    end = DateFormat.FormattingDate(date, ParameterModel.DateTimeFormat.Date);
                                    DateRange = $"&StartDate={start}&EndDate={end}";
                                    break;

                                case "TransFilter-003":
                                    startDays = DateFormat.FormattingDateSplit(date.Year, date.Month, 1);
                                    start = DateFormat.FormattingDate(startDays, ParameterModel.DateTimeFormat.Date);
                                    end = DateFormat.FormattingDate(date, ParameterModel.DateTimeFormat.Date);
                                    DateRange = $"&StartDate={start}&EndDate={end}";
                                    break;

                                case "TransFilter-004":
                                    startDays = DateFormat.FormattingDateSplit(date.Year, date.Month - 1, 1);
                                    endDays = DateFormat.FormattingDateSplit(date.Year, date.Month - 1, date.Day);
                                    start = DateFormat.FormattingDate(startDays, ParameterModel.DateTimeFormat.Date);
                                    end = DateFormat.FormattingDate(endDays, ParameterModel.DateTimeFormat.Date);
                                    DateRange = $"&StartDate={start}&EndDate={end}";
                                    break;
                            }
                        }
                        else if (startDate != null && endDate != null)
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
                    if (alltrans.metaData.isSucces && alltrans.metaData.code == 200)
                    {
                        ListAllTrans.Clear();
                        foreach (Model.Response.Transaction.AllTransaction.Datum datas in alltrans.data)
                        {
                            if (datas.amount != null)
                            {
                                datas.amountFormat = FormatCurrency.Currency((decimal)datas.amount, AppParameter.CurrencyFormat);
                            }

                            if (!string.IsNullOrEmpty(datas.photo))
                            {
                                string decodeImg = Converter.DecodeBase64ToString(datas.photo);
                                byte[] byteImg = Converter.StringToByteImg(decodeImg);
                                datas.source = ImageConvert.ImgByte(byteImg);
                            }

                            if (datas.transDate != null)
                            {
                                datas.transDateFormat = DateFormat.FormattingDate((DateTime)datas.transDate, ParameterModel.DateTimeFormat.Date);
                            }
                        }
                        Page = (int)alltrans.pageNumber;
                        TotalPages = (int)alltrans.totalPages;
                        TotalRecords = (int)alltrans.totalRecords;
                        Number = pages;
                        Size = pageSize;
                        ListAllTrans.Add(alltrans);
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
        public async Task<bool> SavePDF()
        {
            IsBusy = true;

            try
            {
                //Check Permission
                PermissionType[] type =
                {
                    PermissionType.StorageRead,
                    PermissionType.StorageWrite
                };

                foreach (var item in type)
                {
                    await PermissionRequest.RequestPermission(item);
                    var isGranted = await PermissionRequest.CheckPermissionAsync(item);
                    if (!isGranted)
                    {
                        await MsgModel.MsgNotification($"{item} Not Granted");
                        return false;
                    }
                }

                //Save Directory Location File
                SaveDir = FileHelper.GetFilePath(AppParameter.BlankPDF);
                var size = GeneratePDFFile.SetPageSize(AppParameter.PDFPageSize);

                //Process Generate PDF
                PdfWriter writer = new PdfWriter(SaveDir);
                PdfDocument pdfdoc = new PdfDocument(writer);
                Document doc = new Document(pdfdoc, size, false);

                #region Process PDF Data
                var sessionID = App.Session;
                string userID = SessionModel.GetUserID(sessionID);
                var alltrans = await RestAPI.Transaction.GetAllPDFTransaction.AllPDFTransaction(userID, Builder);
                if (alltrans.metaData.isSucces && alltrans.metaData.code == 200)
                {
                    var list = new List<GetAllPDFTransaction.Datum>();
                    foreach (var transaction in alltrans.data)
                    {
                        list.Add(transaction);
                    }
                    var sumtrans = await RestAPI.Transaction.GetSumTransaction.GetSumTransactionID(userID, BuilderSum);

                    //Header
                    string title = $"{userID} Transaction Report";
                    var header = GeneratePDFFile.SetParagraph(title, 20, iText.Layout.Properties.TextAlignment.CENTER);
                    doc.Add(header);

                    //Subheader
                    var firstItem = list.FirstOrDefault();
                    var lastItem = list.LastOrDefault();

                    var firstDate = DateFormat.FormattingDate((DateTime)firstItem.transDate, ParameterModel.DateTimeFormat.Date);
                    var lastDate = DateFormat.FormattingDate((DateTime)lastItem.transDate, ParameterModel.DateTimeFormat.Date);

                    var subtitle = $"Periode {firstDate} - {lastDate}";
                    var subheader = GeneratePDFFile.SetParagraph(subtitle, 15, iText.Layout.Properties.TextAlignment.CENTER);
                    doc.Add(subheader);

                    //Line Separator
                    var ls = GeneratePDFFile.SetLine();
                    doc.Add(ls);

                    //New Line
                    var nl = GeneratePDFFile.SetNewLine();
                    doc.Add(nl);

                    // Table
                    if (alltrans.data.Count > 0)
                    {
                        var culture = AppParameter.CurrencyFormat;
                        var tbl = GeneratePDFFile.SetTable(5, true);

                        // Table Header
                        tbl.AddHeaderCell(GeneratePDFFile.SetCell(1, true, "Description", iText.Layout.Properties.TextAlignment.CENTER));
                        tbl.AddHeaderCell(GeneratePDFFile.SetCell(1, true, "TransNo", iText.Layout.Properties.TextAlignment.CENTER));
                        tbl.AddHeaderCell(GeneratePDFFile.SetCell(1, true, "Income", iText.Layout.Properties.TextAlignment.CENTER));
                        tbl.AddHeaderCell(GeneratePDFFile.SetCell(1, true, "Expenditure", iText.Layout.Properties.TextAlignment.CENTER));
                        tbl.AddHeaderCell(GeneratePDFFile.SetCell(1, true, "Trans Date", iText.Layout.Properties.TextAlignment.CENTER));

                        //Table Data
                        foreach (var item in alltrans.data)
                        {
                            var amountFormat = item.amount != null ? FormatCurrency.Currency((decimal)item.amount, culture) : string.Empty;
                            var description = string.IsNullOrEmpty(item.description) ? item.srTransItem : item.description;
                            var transDate = DateFormat.FormattingDate((DateTime)item.transDate, ParameterModel.DateTimeFormat.Date);
                            var textAlignment = item.transType == ParameterModel.ItemDefaultValue.IncomeTrans ? iText.Layout.Properties.TextAlignment.RIGHT : iText.Layout.Properties.TextAlignment.LEFT;

                            // Add Item To Cell
                            tbl.AddCell(GeneratePDFFile.SetCell(1, false, description, iText.Layout.Properties.TextAlignment.LEFT));
                            tbl.AddCell(GeneratePDFFile.SetCell(1, false, item.transNo, iText.Layout.Properties.TextAlignment.LEFT));
                            tbl.AddCell(GeneratePDFFile.SetCell(1, false, item.transType == ParameterModel.ItemDefaultValue.IncomeTrans ? amountFormat : string.Empty, iText.Layout.Properties.TextAlignment.RIGHT));
                            tbl.AddCell(GeneratePDFFile.SetCell(1, false, item.transType == ParameterModel.ItemDefaultValue.OutcomeTrans ? amountFormat : string.Empty, iText.Layout.Properties.TextAlignment.RIGHT));
                            tbl.AddCell(GeneratePDFFile.SetCell(1, false, transDate, iText.Layout.Properties.TextAlignment.RIGHT));
                        }

                        //Table Footer
                        if (sumtrans.metaData.isSucces && sumtrans.metaData.code == 200 && sumtrans.data.Count > 0)
                        {
                            decimal summary = 0;
                            foreach (var item in sumtrans.data)
                            {
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

                            if (ParameterModel.Transaction.Income != 0 && ParameterModel.Transaction.Expenditure != 0 && sumtrans.data.Count > 0)
                            {
                                summary = ParameterModel.Transaction.Income - ParameterModel.Transaction.Expenditure;
                            }

                            //Format
                            var income = FormatCurrency.Currency(ParameterModel.Transaction.Income, culture);
                            var expenditure = FormatCurrency.Currency(ParameterModel.Transaction.Expenditure, culture);
                            var sum = FormatCurrency.Currency(summary, culture);

                            tbl.AddFooterCell(GeneratePDFFile.SetCell(1, true, "Summary", iText.Layout.Properties.TextAlignment.CENTER));
                            tbl.AddFooterCell(GeneratePDFFile.SetCell(1, true, string.Empty, iText.Layout.Properties.TextAlignment.LEFT));
                            tbl.AddFooterCell(GeneratePDFFile.SetCell(1, true, income, iText.Layout.Properties.TextAlignment.RIGHT));
                            tbl.AddFooterCell(GeneratePDFFile.SetCell(1, true, expenditure, iText.Layout.Properties.TextAlignment.RIGHT));
                            tbl.AddFooterCell(GeneratePDFFile.SetCell(1, true, sum, iText.Layout.Properties.TextAlignment.RIGHT));
                        }
                        else
                        {
                            await MsgModel.MsgNotification(sumtrans.metaData.message);
                        }

                        doc.Add(tbl);
                    }

                    //Add Header Pages
                    if (AppParameter.IsAddHeader)
                    {

                    }

                    //Add Footer Pages
                    if (AppParameter.IsAddFooter)
                    {
                        var date = DateFormat.FormattingDate(ParameterModel.DateFormat.DateTime, ParameterModel.DateTimeFormat.Dateshortmonthhourminute);
                        string footer = $"Generated On {date}";
                        var parFooter = GeneratePDFFile.SetParagraph(footer, 10, iText.Layout.Properties.TextAlignment.CENTER);
                        GeneratePDFFile.SetFooterPages(parFooter, pdfdoc, doc);
                    }

                    //Add Pages Number
                    if (AppParameter.IsAddPageNumber)
                    {
                        GeneratePDFFile.SetPagesNumber(pdfdoc, doc);
                    }

                    //Close Doc
                    doc.Close();

                    if (!string.IsNullOrEmpty(SaveDir))
                    {
                        var token = new CancellationToken();
                        var pdfBytes = Converter.PDFToByte(SaveDir);
                        await FileHelper.SaveFile(AppParameter.BlankPDF, pdfBytes, token);
                    }
                    return true;
                }
                else
                {
                    await MsgModel.MsgNotification(alltrans.metaData.message);
                    return false;
                }
                #endregion
            }
            catch (Exception e)
            {
                await MsgModel.MsgNotification($"{e.Message}");
                return false;
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

            var transNo = item.BindingContext as Model.Response.Transaction.AllTransaction.Datum;
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
