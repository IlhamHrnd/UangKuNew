
using System.Collections.ObjectModel;
using UangKu.Model.Base;
using UangKu.Model.Menu;
using UangKu.Model.Session;

namespace UangKu.ViewModel.Menu
{
    public class TransactionVM : Transaction
    {
        public TransactionVM()
        {
            
        }

        public async void LoadData()
        {
            IsBusy = true;

            #region Variabel
            var userID = SessionModel.GetUserID();
            var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            #endregion

            if (Network.IsConnected)
            {
                try
                {
                    var program = await WebService.Service.AppProgram.GetAppProgramID(new WebService.Filter.Root<WebService.Filter.AppProgram>
                    {
                        Data = new WebService.Filter.AppProgram
                        {
                            ProgramID = AppProgram.Transaction
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
                    var summary = await WebService.Service.Transaction.GetSumTransaction(new WebService.Filter.Root<WebService.Filter.Transaction>
                    {
                        Data = new WebService.Filter.Transaction
                        {
                            PersonID = userID,
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

                try
                {
                    var transaction = await WebService.Service.Transaction.GetAllTransaction(new WebService.Filter.Root<WebService.Filter.Transaction>
                    {
                        Data = new WebService.Filter.Transaction
                        {
                            PersonID = userID,
                            StartDate = startDate,
                            EndDate = endDate,
                            OrderBy = ItemManager.OrderBy,
                            IsAscending = false
                        },
                        PageNumber = ItemManager.FirstPage,
                        PageSize = AppParameter.HomeMaxResult
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
                                    item.dateFormat = DateFormat.FormattingDate((DateTime)item.transDate, DateTimeFormat.Date);
                            }
                        }
                    }
                    else
                        await MsgModel.MsgNotification(transaction.Message);
                }
                catch (Exception e)
                {
                    await MsgModel.MsgNotification(e.Message);
                }
            }
            IsBusy = false;
        }

        public void RadioSelection(int index)
        {
            IsCustomDateRange = index == 3;
        }
    }
}
