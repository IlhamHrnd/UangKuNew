using UangKu.Model.Base;

namespace UangKu.Model.Index
{
    public class MainPage : BaseModel
    {
        public MainPage()
        {
            
        }

        private string lastbuild = string.Empty;
        public string LastBuild { get => lastbuild; set => SetProperty(ref lastbuild, value); }
        private bool isvisible = false;
        public bool IsVisible { get => isvisible; set => SetProperty(ref isvisible, value); }
    }
}
