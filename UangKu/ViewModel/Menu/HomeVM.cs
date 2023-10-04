using UangKu.Model.Base;
using UangKu.Model.Menu;

namespace UangKu.ViewModel.Menu
{
    public class HomeVM : Home
    {
        public HomeVM()
        {
            LoadData();
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
    }
}
