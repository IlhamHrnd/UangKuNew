using Microcharts;
using Microcharts.Maui;
using SkiaSharp;
using UangKu.Model.Base;
using UangKu.Model.Menu;
using UangKu.Model.Response.Picture;
using UangKu.Model.Session;
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
            Title = "Home";
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

                    var alltrans = await AllTransaction.GetAllTransaction(ParameterModel.ItemDefaultValue.FirstPage, AppParameter.HomeMaxResult,
                        userID, string.Empty);
                    if (alltrans.metaData.isSucces && alltrans.metaData.code == 200)
                    {
                        ListAllTrans.Clear();
                        foreach (var item in alltrans.data)
                        {
                            if (item.amount != null)
                            {
                                item.amountFormat = FormatCurrency.Currency((decimal)item.amount, AppParameter.CurrencyFormat);
                            }

                            if (!string.IsNullOrEmpty(item.photo))
                            {
                                string decode = Converter.DecodeBase64ToString(item.photo);
                                byte[] byteImg = Converter.StringToByteImg(decode);
                                item.source = ImageConvert.ImgByte(byteImg);
                            }
                        }
                        ListAllTrans.Add(alltrans);

                        var sumtrans = await GetSumTransaction.GetSumTransactionID(userID, string.Empty);
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
                            string amountFormat = FormatCurrency.Currency((decimal)amount, AppParameter.CurrencyFormat);

                            var item = new Datum
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
                    }
                    else
                    {
                        await MsgModel.MsgNotification(alltrans.metaData.message);
                    }

                    var picture = await GetUserPicture.GetAllUserPicture(ParameterModel.ItemDefaultValue.FirstPage, AppParameter.HomeMaxResult, 
                        userID, ParameterModel.ItemDefaultValue.IsDeleted);
                    if (picture.metaData.isSucces && picture.metaData.code == 200)
                    {
                        ListUserPicture.Clear();
                        foreach (UserPicture.Datum item in picture.data)
                        {
                            if (!string.IsNullOrEmpty(item.picture))
                            {
                                string decodeImg = Converter.DecodeBase64ToString(item.picture);
                                byte[] byteImg = Converter.StringToByteImg(decodeImg);
                                item.source = ImageConvert.ImgByte(byteImg);
                            }

                            if (!string.IsNullOrEmpty(item.pictureFormat))
                            {
                                string result = ImageConvert.SubstringContentType(picture.data[0].pictureFormat, '/');
                                item.contenttype = result;
                            }
                        }
                        ListUserPicture.Add(picture);
                    }
                    else
                    {
                        await MsgModel.MsgNotification(picture.metaData.message);
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
