using UangKu.Model.Base;
using UangKu.Model.Session;
using static UangKu.Model.Base.ParameterModel.PermissionManager;

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

            MainPage = new AppShell();

        }

        protected override async void OnStart()
        {
            PermissionType type = PermissionType.StorageRead;
            await PermissionRequest.RequestPermission(type);
        }
    }
}