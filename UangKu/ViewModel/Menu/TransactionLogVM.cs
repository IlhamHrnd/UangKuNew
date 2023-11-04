using UangKu.Model.Base;
using UangKu.Model.Menu;

namespace UangKu.ViewModel.Menu
{
    public class TransactionLogVM : TransactionLog
    {
        private NetworkModel network = NetworkModel.Instance;
        public TransactionLogVM()
        {
            Title = $"Transaction Log For {App.Session.username}";
        }
        public async void LoadData(int pageNumber, int pageSize)
        {
            bool isConnect = network.IsConnected;
            IsBusy = true;
            try
            {
                if (!isConnect)
                {
                    await MsgModel.MsgNotification(ParameterModel.ItemDefaultValue.Offline);
                }
                var sumtrans = await RestAPI.Transaction.GetSumTransaction.GetSumTransactionID(App.Session.username);
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
                    }
                }
                var alltrans = await RestAPI.Transaction.AllTransaction.GetAllTransaction(pageNumber, pageSize, App.Session.username);
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
                            ParameterModel.ImageManager.ImageByte = byteImg;
                            ParameterModel.ImageManager.ImageString = decodeImg;
                            item.data[i].source = ImageConvert.ImgByte(byteImg);
                        }
                    }
                    ListAllTrans.Add(item);
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
    }
}
