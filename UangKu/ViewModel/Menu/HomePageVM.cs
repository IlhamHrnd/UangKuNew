﻿using UangKu.Model.Base;
using UangKu.Model.Menu;
using UangKu.Model.Session;

namespace UangKu.ViewModel.Menu
{
    public class HomePageVM : HomePage
    {
        public HomePageVM()
        {

        }

        public async void LoadData()
        {
            IsBusy = true;
            if (Network.IsConnected)
            {
                try
                {
                    var program = await WebService.Service.AppProgram.GetAppProgramID(new WebService.Filter.Root<WebService.Filter.AppProgram>
                    {
                        Data = new WebService.Filter.AppProgram
                        {
                            ProgramID = AppProgram.HomePage
                        }
                    });
                    if (program.Succeeded == true)
                        Title = program.Data.programName;
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
                            PersonID = UserID,
                            StartDate = StartDate,
                            EndDate = EndDate
                        }
                    });
                    if (summary.Succeeded == true)
                    {
                        Summary = new WebService.Data.Root<System.Collections.ObjectModel.ObservableCollection<WebService.Data.Transaction.Data>>
                        {
                            Data = new System.Collections.ObjectModel.ObservableCollection<WebService.Data.Transaction.Data>(summary.Data),
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
                    var transaction = await WebService.Service.Transaction.GetAllTransaction(new WebService.Filter.Root<WebService.Filter.Transaction>
                    {
                        Data = new WebService.Filter.Transaction
                        {
                            PersonID = UserID,
                            StartDate = StartDate,
                            EndDate = EndDate,
                            OrderBy = ItemManager.OrderBy,
                            IsAscending = false
                        },
                        PageNumber = ItemManager.FirstPage,
                        PageSize = AppParameter.HomeMaxResult
                    });
                    if (transaction.Succeeded == true)
                    {
                        Transaction = new WebService.Data.Root<System.Collections.ObjectModel.ObservableCollection<WebService.Data.Transaction.Data>>
                        {
                            Data = new System.Collections.ObjectModel.ObservableCollection<WebService.Data.Transaction.Data>(transaction.Data),
                            Succeeded = transaction.Succeeded,
                            Errors = transaction.Errors,
                            Message = transaction.Message
                        };

                        if (Transaction.Data.Count > 0)
                        {
                            foreach (var item in Transaction.Data)
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

                try
                {
                    var gallery = await WebService.Service.UserPicture.GetUserPicture(new WebService.Filter.Root<WebService.Filter.UserPicture>
                    {
                        Data = new WebService.Filter.UserPicture
                        {
                            PersonID = UserID,
                            IsDelete = ItemManager.IsDeleted
                        },
                        PageNumber = ItemManager.FirstPage,
                        PageSize = AppParameter.HomeMaxResult
                    });
                    if (gallery.Succeeded == true)
                    {
                        Gallery = new WebService.Data.Root<System.Collections.ObjectModel.ObservableCollection<WebService.Data.UserPicture.Data>>
                        {
                            Data = new System.Collections.ObjectModel.ObservableCollection<WebService.Data.UserPicture.Data>(gallery.Data),
                            Succeeded = gallery.Succeeded,
                            Errors = gallery.Errors,
                            Message = gallery.Message
                        };

                        if (Gallery.Data.Count > 0)
                        {
                            foreach (var item in Gallery.Data)
                            {
                                if (!string.IsNullOrEmpty(item.picture))
                                {
                                    string decode = Converter.DecodeBase64ToString(item.picture);
                                    byte[] img = Converter.StringToByteImg(decode);
                                    item.source = ImageConvert.ImgByte(img);
                                }

                                if (!string.IsNullOrEmpty(item.pictureFormat))
                                {
                                    string result = ImageConvert.SubstringContentType(item.pictureFormat, '/');
                                    item.contentType = result;
                                }
                            }
                        }
                    }
                    else
                        await MsgModel.MsgNotification(gallery.Message);
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
