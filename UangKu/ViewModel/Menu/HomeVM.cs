using Microcharts;
using Microcharts.Maui;
using SkiaSharp;
using UangKu.Model.Base;
using UangKu.Model.Menu;
using UangKu.View.SubMenu;
using UangKu.ViewModel.RestAPI.Picture;
using UangKu.ViewModel.RestAPI.Profile;
using UangKu.ViewModel.RestAPI.Transaction;
using static UangKu.Model.Response.Transaction.SumTransaction;

namespace UangKu.ViewModel.Menu
{
    public class HomeVM : Home
    {
        private NetworkModel network = NetworkModel.Instance;
        private readonly INavigation _navigation;

        public HomeVM(INavigation navigation)
        {
            LoadData();
            _navigation = navigation;
        }
        private void LoadData()
        {
            string greeting;
            switch (ParameterModel.DateFormat.DateTime.Hour)
            {
                case int h when h >= 0 && h <= 10:
                    greeting = "Good Morning";
                    Image = "morning.svg";
                    break;

                case int h when h > 10 && h <= 15:
                    greeting = "Good Afternoon";
                    Image = "afternoon.svg";
                    break;

                case int h when h > 15 && h <= 19:
                    greeting = "Good Evening";
                    Image = "evening.svg";
                    break;

                default:
                    greeting = "Good Night";
                    Image = "night.svg";
                    break;
            }

            Name = $"Hello, {App.Session.username} {greeting}";
            Person = $"{App.Session.username}";
            Month = DateFormat.FormattingDate(ParameterModel.DateFormat.DateTime, ParameterModel.DateTimeFormat.Month);
        }

        public async void LoadDataPerson(ChartView charts)
        {
            List<ChartEntry> entries = new List<ChartEntry>();
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

                if (!string.IsNullOrEmpty(userID))
                {
                    var person = await GetProfile.GetProfileID(userID);
                    if (string.IsNullOrEmpty(person.personID))
                    {
                        await MsgModel.MsgNotification($"Please, Fill Profile For {userID} First");
                        await _navigation.PushAsync(new EditProfile(ParameterModel.ItemDefaultValue.NewFile));
                    }

                    var sumtrans = await GetSumTransaction.GetSumTransactionID(userID);
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

                            entries.Add(new ChartEntry((float?)item.amount)
                            {
                                Label = item.srTransaction,
                                ValueLabel = item.amountFormat,
                                Color = RandomColorGenerator.SKGenerateRandomColor()
                            });

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
                        string amountFormat = FormatCurrency.Currency((decimal)amount, ParameterModel.ItemDefaultValue.Currency);

                        var item = new SumTransactionRoot
                        {
                            amount = amount,
                            srTransaction = srTransaction,
                            amountFormat = amountFormat
                        };

                        ListSumTrans.Add(item);
                    }

                    if (entries.Count > 0)
                    {
                        charts.Chart = new PieChart
                        {
                            Entries = entries,
                            BackgroundColor = SKColors.Transparent,
                            LabelTextSize = 25,
                            LabelMode = LabelMode.RightOnly
                        };
                    }

                    var alltrans = await AllTransaction.GetAllTransaction(ParameterModel.ItemDefaultValue.FirstPage, ParameterModel.ItemDefaultValue.HomeMaxResult,
                        userID);
                    if (alltrans != null)
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

                    var picture = await GetUserPicture.GetAllUserPicture(ParameterModel.ItemDefaultValue.FirstPage, ParameterModel.ItemDefaultValue.HomeMaxResult, 
                        userID, ParameterModel.ItemDefaultValue.IsDeleted);
                    if ((bool)picture.succeeded && picture.data.Count > 0)
                    {
                        ListUserPicture.Clear();
                        for (int i = 0; i < picture.data.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(picture.data[i].picture))
                            {
                                string decodeImg = ImageConvert.DecodeBase64ToString(picture.data[i].picture);
                                byte[] byteImg = ImageConvert.StringToByteImg(decodeImg);
                                picture.data[i].source = ImageConvert.ImgByte(byteImg);
                            }

                            if (!string.IsNullOrEmpty(picture.data[i].pictureFormat))
                            {
                                string result = ImageConvert.SubstringContentType(picture.data[0].pictureFormat, '/');
                                picture.data[i].contenttype = result;
                            }
                        }
                        ListUserPicture.Add(picture);
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
    }
}
