using System.Collections.ObjectModel;
using UangKu.Model.Base;
using static UangKu.Model.Response.AppParameter.GetAllParameterWithNoPageFilter;

namespace UangKu.Model.Index
{
    public class MainPage : BaseModel
    {
        public MainPage()
        {
            
        }

        private string title = "Main Page";
        public string Title { get => title; set => SetProperty(ref title, value); }
        private bool isbusy = false;
        public bool IsBusy { get => isbusy; set => SetProperty(ref isbusy, value); }
        private string lastbuild = string.Empty;
        public string LastBuild { get => lastbuild; set => SetProperty(ref lastbuild, value); }
        private bool isvisible = false;
        public bool IsVisible { get => isvisible; set => SetProperty(ref isvisible, value); }
    }
}
