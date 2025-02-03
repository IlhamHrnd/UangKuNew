using UangKu.Model.Base;
using UangKu.Model.Session;
using static UangKu.Model.Base.PermissionManager;

namespace UangKu
{
    public partial class App : Application
    {
        public static AppSession Session { get; set; }
        public static AppAccess Access { get; set; }
        public static AppParameter Param { get; set; }
        public App()
        {
            InitializeComponent();
            ExceptionHandler();
            MainPage = new AppShell();

        }

        protected override async void OnStart()
        {
            PermissionType type = PermissionType.StorageRead;
            await PermissionRequest.RequestPermission(type);
        }

        private void ExceptionHandler()
        {
            // Catch UI thread exceptions
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                HandleException(e.ExceptionObject as Exception);
            };

            // Catch Task-based (async) exceptions
            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                HandleException(e.Exception);
                e.SetObserved();
            };
        }

        private void HandleException(Exception ex)
        {
            if (ex != null)
                SessionModel.LogError(ex);
        }
    }
}