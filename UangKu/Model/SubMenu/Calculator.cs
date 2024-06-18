using UangKu.Model.Base;

namespace UangKu.Model.SubMenu
{
    public class Calculator : BaseModel
    {
        private string currententry = string.Empty;
        public string CurrentEntry { get => currententry; set => SetProperty(ref currententry, value); }
        private int currentstate = 1;
        public int CurrentState { get => currentstate; set => SetProperty(ref currentstate, value); }
        private string mathoperator = string.Empty;
        public string MathOperator { get => mathoperator; set => SetProperty(ref mathoperator, value); }
        private double firstnumber = 0;
        public double FirstNumber { get => firstnumber; set => SetProperty(ref firstnumber, value); }
        private double secondnumber = 0;
        public double SecondNumber { get => secondnumber; set => SetProperty(ref secondnumber, value); }
        private string decimalformat = "N0";
        public string DecimalFormat { get => decimalformat; set => SetProperty(ref decimalformat, value); }
    }
}
