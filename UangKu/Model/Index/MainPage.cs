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
    }
}
