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
            SessionModel.Initialize();
            MainPage = new AppShell();

        }

        protected override async void OnStart()
        {
            PermissionType type = PermissionType.StorageRead;
            await PermissionRequest.RequestPermission(type);
        }
    }
}