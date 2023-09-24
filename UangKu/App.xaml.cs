using UangKu.Model.Session;

namespace UangKu
{
    public partial class App : Application
    {
        public static AppSession Session { get; set; }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

        }
    }
}