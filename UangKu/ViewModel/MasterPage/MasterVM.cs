using UangKu.Model.Base;
using UangKu.Model.Master;

namespace UangKu.ViewModel.MasterPage
{
    public class MasterVM : Master
    {
        public INavigation _navigation { get; }
        public MasterVM(INavigation navigation)
        {
            _navigation = navigation;

            SessionCheck();
            LoadData();
        }

        private void SessionCheck()
        {
            if (App.Session == null)
            {
                var shell = new AppShell();

                App.Current.MainPage = shell;
            }
        }

        private void LoadData()
        {
            string greeting;
            switch (DateTime.Hour)
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
        }

        public async void BtnAppStandard_Reference()
        {
            //Belum bisa navigation
            await _navigation.PushAsync(new View.Index.SignUpPage());
        }
    }
}
