using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UangKu.Model.Base;
using UangKu.Model.Session;

namespace UangKu.ViewModel.Module.Transaction
{
    public class TransactionVM : Model.Module.Transaction.Transaction
    {
        public TransactionVM(INavigation navigation)
        {
            Navigation = navigation;
        }

        public void LoadData()
        {
            Summary = new WebService.Data.Root<ObservableCollection<WebService.Data.Transaction.Data>>();
            Trans = new WebService.Data.Root<ObservableCollection<WebService.Data.Transaction.Data>>();

            if (Network.IsConnected)
            {
                AppProgram(Model.Base.AppProgram.Transaction);
                LoadSumTransaction(StartDate, EndDate);
                LoadAppParameter();
                LoadAllTransaction(ItemManager.FirstPage, StartDate, EndDate);
            }
        }

        #region Load Data
        private async void LoadSumTransaction(DateTime startDate, DateTime endDate)
        {
            IsBusy = true;
            try
            {
                var summary = await WebService.Service.Transaction.GetSumTransaction(new WebService.Filter.Root<WebService.Filter.Transaction>
                {
                    Data = new WebService.Filter.Transaction
                    {
                        PersonID = UserID,
                        StartDate = startDate,
                        EndDate = endDate
                    }
                });
                if (summary.Succeeded == true)
                {
                    Summary = new WebService.Data.Root<ObservableCollection<WebService.Data.Transaction.Data>>
                    {
                        Data = new ObservableCollection<WebService.Data.Transaction.Data>(summary.Data),
                        Succeeded = summary.Succeeded,
                        Errors = summary.Errors,
                        Message = summary.Message
                    };

                    if (Summary.Data.Count > 0)
                    {
                        foreach (var item in Summary.Data)
                        {
                            item.amountFormat = FormatCurrency.Currency((decimal)item.amount, AppParameter.CurrencyFormat);
                        }
                    }
                }
                else
                    await MsgModel.MsgNotification(summary.Message);
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

        private async void LoadAppParameter()
        {
            IsBusy = true;
            try
            {
                var parameter = await WebService.Service.AppParameter.GetParameterID(new WebService.Filter.Root<WebService.Filter.AppParameter>
                {
                    Data = new WebService.Filter.AppParameter
                    {
                        ParameterID = "IsAllowCustomDate"
                    }
                });
                if (parameter.Succeeded == true)
                {
                    IsAllowCustomDate = Converter.StringToBool(parameter.Data.parameterValue);

                    if (IsAllowCustomDate)
                    {
                        var orderby = await WebService.Service.AppStandardReferenceItem.GetAllReferenceItemID(new WebService.Filter.Root<WebService.Filter.AppStandardReferenceItem>
                        {
                            Data = new WebService.Filter.AppStandardReferenceItem
                            {
                                StandardReferenceID = "OrderByTransaction",
                                IsActive = true,
                                IsUsedBySystem = true
                            }
                        });
                        if (orderby.Succeeded == true)
                            OrderBy = new WebService.Data.Root<ObservableCollection<WebService.Data.AppStandardReferenceItem.Data>>
                            {
                                Data = new ObservableCollection<WebService.Data.AppStandardReferenceItem.Data>(orderby.Data),
                                Succeeded = orderby.Succeeded,
                                Errors = orderby.Errors,
                                Message = orderby.Message
                            };
                        else
                            await MsgModel.MsgNotification(orderby.Message);
                    }
                }
                else
                    await MsgModel.MsgNotification(parameter.Message);
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

        private async void LoadAllTransaction(int pageNumber, DateTime startDate, DateTime endDate)
        {
            IsBusy = true;
            try
            {
                var transaction = await WebService.Service.Transaction.GetAllTransaction(new WebService.Filter.Root<WebService.Filter.Transaction>
                {
                    Data = new WebService.Filter.Transaction
                    {
                        PersonID = UserID,
                        StartDate = startDate,
                        EndDate = endDate,
                        OrderBy = SelectedOrderBy != null && !string.IsNullOrEmpty(SelectedOrderBy.itemId) ? SelectedOrderBy.itemId : ItemManager.OrderBy,
                        IsAscending = IsAscending
                    },
                    PageNumber = pageNumber,
                    PageSize = AppParameter.MaxResult
                });
                if (transaction.Succeeded == true)
                {
                    Trans = new WebService.Data.Root<ObservableCollection<WebService.Data.Transaction.Data>>
                    {
                        Data = new ObservableCollection<WebService.Data.Transaction.Data>(transaction.Data),
                        Succeeded = transaction.Succeeded,
                        Errors = transaction.Errors,
                        Message = transaction.Message
                    };

                    if (Trans.Data.Count > 0)
                    {
                        foreach (var item in Trans.Data)
                        {
                            if (item.amount != null)
                                item.amountFormat = FormatCurrency.Currency((decimal)item.amount, AppParameter.CurrencyFormat);

                            if (!string.IsNullOrEmpty(item.photo))
                            {
                                string decode = Converter.DecodeBase64ToString(item.photo);
                                byte[] img = Converter.StringToByteImg(decode);
                                item.source = ImageConvert.ImgByte(img);
                            }

                            if (item.transDate != null)
                                item.dateFormat = DateFormat.FormattingDate((DateOnly)item.transDate, DateTimeFormat.Date);
                        }
                    }

                    PageNumber = transaction.pageNumber ?? 1;
                    PageSize = transaction.pageSize ?? 1;
                    TotalPages = transaction.totalPages ?? 1;
                    TotalRecords = transaction.totalRecords ?? 1;
                    PrevPageLink = (string)transaction.prevPageLink;
                    NextPageLink = (string)transaction.nextPageLink;
                }
                else
                    await MsgModel.MsgNotification(transaction.Message);
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
        #endregion

        #region Control Function
        public void RadioSelection(int index)
        {
            IsCustomDateRange = index == 4;
            switch (index)
            {
                case 0:
                    StartDate = DateTime.Now;
                    EndDate = DateTime.Now;
                    break;

                case 1:
                    StartDate = DateTime.Now.AddDays(-7);
                    EndDate = DateTime.Now;
                    break;

                case 2:
                    StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    break;

                case 3:
                    StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
                    EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);
                    break;
            }

            if (Network.IsConnected && index != 4)
            {
                Summary = new WebService.Data.Root<ObservableCollection<WebService.Data.Transaction.Data>>();
                Trans = new WebService.Data.Root<ObservableCollection<WebService.Data.Transaction.Data>>();
                LoadSumTransaction(StartDate, EndDate);
                LoadAllTransaction(ItemManager.FirstPage, StartDate, EndDate);
            }
        }

        public void FilterTransaction(int index)
        {
            if (Network.IsConnected)
            {
                Summary = new WebService.Data.Root<ObservableCollection<WebService.Data.Transaction.Data>>();
                Trans = new WebService.Data.Root<ObservableCollection<WebService.Data.Transaction.Data>>();

                if (index == 4)
                {
                    LoadSumTransaction(FirstDate, LastDate);
                    LoadAllTransaction(ItemManager.FirstPage, FirstDate, LastDate);
                }
                else
                {
                    LoadSumTransaction(StartDate, EndDate);
                    LoadAllTransaction(ItemManager.FirstPage, StartDate, EndDate);
                }
            }
        }

        public async void NextPreviousPage(bool isNext, int index)
        {
            IsBusy = true;

            if (!Network.IsConnected)
                return;
            try
            {
                if (PageNumber >= TotalPages && isNext)
                    await MsgModel.MsgNotification("This Is The Latest Page");
                else if (PageNumber <= ItemManager.FirstPage && !isNext)
                    await MsgModel.MsgNotification("This Is The First Page");
                else
                {
                    int page = isNext ? PageNumber + 1 : PageNumber - 1;
                    Trans = new WebService.Data.Root<ObservableCollection<WebService.Data.Transaction.Data>>();
                    if (index == 4)
                        LoadAllTransaction(page, FirstDate, LastDate);
                    else
                        LoadAllTransaction(page, StartDate, EndDate);
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
        #endregion

        public void ScrollTopBottom(bool isScrollTop, ScrollView scroll)
        {
            double y = scroll.ContentSize.Height;

            if (isScrollTop)
                scroll.ScrollToAsync(0, 0, true);
            else
                scroll.ScrollToAsync(0, y, true);
        }

        public async Task SwipeItem(object sender, string mode)
        {
            if (sender is not SwipeItem item || item.BindingContext is not WebService.Data.Transaction.Data data)
            {
                await MsgModel.MsgNotification(ItemManager.Empty);
                return;
            }

            if (mode == ItemManager.EditFile)
                await Navigation.PushAsync(new View.Module.Transaction.TransactionEdit(mode, data.transNo));
            else if (mode == ItemManager.DeleteFile)
                await MsgModel.MsgNotification(ItemManager.DeleteFile);
        }

        public async Task ToolbarBottom()
        {
            await Navigation.PushAsync(new View.Module.Transaction.TransactionEdit(ItemManager.NewFile, string.Empty));
        }

        public async Task<bool> GenerateReport()
        {
            IsBusy = true;

            try
            {
                var report = await WebService.Service.Transaction.GenerateReportTransaction(new WebService.Filter.Root<WebService.Filter.Transaction>
                {
                    Data = new WebService.Filter.Transaction
                    {
                        PersonID = UserID,
                        StartDate = StartDate,
                        EndDate = EndDate,
                        OrderBy = SelectedOrderBy != null && !string.IsNullOrEmpty(SelectedOrderBy.itemId) ? SelectedOrderBy.itemId : ItemManager.OrderBy,
                        IsAscending = IsAscending
                    }
                });

                if (report.Succeeded == false)
                {
                    await MsgModel.MsgNotification(report.Message);
                    return false;
                }

                var token = new CancellationToken();
                var saveDir = await FileHelper.GetFilePath(AppParameter.BlankPDF, report.Data, token);
                if (!string.IsNullOrEmpty(saveDir))
                    await FileHelper.SaveFile(report.Data, saveDir);
                else
                    await MsgModel.MsgNotification(ItemManager.Cancel);

                return true;
            }
            catch (Exception e)
            {
                await MsgModel.MsgNotification(e.Message);
                return false;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
