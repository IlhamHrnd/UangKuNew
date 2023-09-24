using UangKu.Model.Base;

namespace UangKu.Model.Index
{
    public class ForgetPassword : BaseModel
    {
        public ForgetPassword()
        {

        }

        private string title = "Forgot Password";
        public string Title { get => title; set => SetProperty(ref title, value); }
        private bool isbusy = false;
        public bool IsBusy { get => isbusy; set => SetProperty(ref isbusy, value); }
    }
}
