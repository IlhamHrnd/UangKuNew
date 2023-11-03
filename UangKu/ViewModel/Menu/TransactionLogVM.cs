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
        public async void LoadData()
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
                foreach (var item in sumtrans)
                {
                    if (item.amount != null)
                    {
                        item.amountFormat = FormatCurrency.Currency((decimal)item.amount, "id-ID");
                    }

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
    }
}
