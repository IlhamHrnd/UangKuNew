using UangKu.Model.Base;

namespace UangKu.Model.Master
{
    public class Master : BaseModel
    {
        public Master()
        {
            
        }
        private string title = "Main Menu";
        public string Title { get => title; set => SetProperty(ref title, value); }
    }
}
