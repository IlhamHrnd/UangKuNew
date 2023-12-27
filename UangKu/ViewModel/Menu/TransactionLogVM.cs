﻿using UangKu.Model.Base;
using UangKu.Model.Menu;
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
        public async void LoadData(int pageNumber, int pageSize)
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
                var sumtrans = await RestAPI.Transaction.GetSumTransaction.GetSumTransactionID(userID);
                if (sumtrans.Count > 0)
                {
                    ListSumTrans.Clear();
                    for (int i = 0; i < sumtrans.Count; i++)
                    {
                        var item = sumtrans[i];
                        if (item.amount != null)
                        {
                            item.amountFormat = FormatCurrency.Currency((decimal)item.amount, ParameterModel.ItemDefaultValue.Currency);
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
                var alltrans = await RestAPI.Transaction.AllTransaction.GetAllTransaction(pageNumber, pageSize, userID);
                if (alltrans.data.Count > 0)
                {
                    ListAllTrans.Clear();
                    var item = alltrans;
                    for (int i = 0; i < item.data.Count; i++)
                    {
                        if (item.data[i].amount != null)
                        {
                            item.data[i].amountFormat = FormatCurrency.Currency((decimal)item.data[i].amount, ParameterModel.ItemDefaultValue.Currency);
                        }

                        if (!string.IsNullOrEmpty(item.data[i].photo))
                        {
                            string decodeImg = ImageConvert.DecodeBase64ToString(item.data[i].photo);
                            byte[] byteImg = ImageConvert.StringToByteImg(decodeImg);
                            item.data[i].source = ImageConvert.ImgByte(byteImg);
                        }
                    }
                    Page = (int)alltrans.pageNumber;
                    ListAllTrans.Add(item);
                }
                if (ParameterModel.Transaction.Income != 0 && ParameterModel.Transaction.Expenditure != 0 && ListSumTrans.Count > 0)
                {
                    decimal? amount = ParameterModel.Transaction.Income - ParameterModel.Transaction.Expenditure;
                    string srTransaction = "Summary";
                    string amountFormat = FormatCurrency.Currency((decimal)amount, ParameterModel.ItemDefaultValue.Currency);

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
        public async void NextPage_Clicked(int pageSize)
        {
            var maxPage = ListAllTrans[0].totalPages;
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
                if (Page >= maxPage)
                {
                    await MsgModel.MsgNotification("This Is The Latest Page");
                }
                else
                {
                    var alltrans = await RestAPI.Transaction.AllTransaction.GetAllTransaction(Page + 1, pageSize, userID);
                    if (alltrans.data.Count > 0)
                    {
                        ListAllTrans.Clear();
                        var item = alltrans;
                        for (int i = 0; i < item.data.Count; i++)
                        {
                            if (item.data[i].amount != null)
                            {
                                item.data[i].amountFormat = FormatCurrency.Currency((decimal)item.data[i].amount, ParameterModel.ItemDefaultValue.Currency);
                            }

                            if (!string.IsNullOrEmpty(item.data[i].photo))
                            {
                                string decodeImg = ImageConvert.DecodeBase64ToString(item.data[i].photo);
                                byte[] byteImg = ImageConvert.StringToByteImg(decodeImg);
                                item.data[i].source = ImageConvert.ImgByte(byteImg);
                            }
                        }
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
        public async void PreviousPage_Click(int pageSize)
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
                if (Page <= 1)
                {
                    await MsgModel.MsgNotification("This Is The First Page");
                }
                else
                {
                    var alltrans = await RestAPI.Transaction.AllTransaction.GetAllTransaction(Page - 1, pageSize, userID);
                    if (alltrans.data.Count > 0)
                    {
                        ListAllTrans.Clear();
                        var item = alltrans;
                        for (int i = 0; i < item.data.Count; i++)
                        {
                            if (item.data[i].amount != null)
                            {
                                item.data[i].amountFormat = FormatCurrency.Currency((decimal)item.data[i].amount, ParameterModel.ItemDefaultValue.Currency);
                            }

                            if (!string.IsNullOrEmpty(item.data[i].photo))
                            {
                                string decodeImg = ImageConvert.DecodeBase64ToString(item.data[i].photo);
                                byte[] byteImg = ImageConvert.StringToByteImg(decodeImg);
                                item.data[i].source = ImageConvert.ImgByte(byteImg);
                            }
                        }
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
