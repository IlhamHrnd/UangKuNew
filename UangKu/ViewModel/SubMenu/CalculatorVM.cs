using UangKu.Model.SubMenu;

namespace UangKu.ViewModel.SubMenu
{
    public class CalculatorVM : Calculator
    {
        private readonly INavigation _navigation;
        public CalculatorVM(INavigation navigation)
        {
            Title = "Calculator";
            _navigation = navigation;
        }

        public void OnSelectNumber(object sender, Label result)
        {

            Button button = (Button)sender;
            string pressed = button.Text;

            CurrentEntry += pressed;

            if ((result.Text == "0" && pressed == "0") || (CurrentEntry.Length <= 1 && pressed != "0")
                || CurrentState < 0)
            {
                result.Text = "";
                if (CurrentState < 0)
                    CurrentState *= -1;
            }

            if (pressed == "." && DecimalFormat != "N2")
            {
                DecimalFormat = "N2";
            }

            result.Text += pressed;
        }

        public void OnSelectOperator(object sender, Label result)
        {
            LockNumberValue(result.Text);

            CurrentState = -2;
            Button button = (Button)sender;
            string pressed = button.Text;
            MathOperator = pressed;
        }

        public void OnClear(Label result)
        {
            FirstNumber = 0;
            SecondNumber = 0;
            CurrentState = 1;
            DecimalFormat = "N0";
            result.Text = "0";
            CurrentEntry = string.Empty;
        }

        public void OnCalculate(Label result, Label current)
        {
            if (CurrentState == 2)
            {
                if (SecondNumber == 0)
                    LockNumberValue(result.Text);

                double results = Model.Base.Calculator.Calculate(FirstNumber, SecondNumber, MathOperator);

                current.Text = $"{FirstNumber} {MathOperator} {SecondNumber}";

                result.Text = Model.Base.Calculator.TrimmedString(results, DecimalFormat);
                FirstNumber = results;
                SecondNumber = 0;
                CurrentState = -1;
                CurrentEntry = string.Empty;
            }
        }

        public void OnNegative(Label result, Label current)
        {
            if (CurrentState == 1)
            {
                SecondNumber = -1;
                MathOperator = "×";
                CurrentState = 2;
                OnCalculate(result, current);
            }
        }

        public void OnPercentage(Label result, Label current)
        {
            if (CurrentState == 1)
            {
                LockNumberValue(result.Text);
                DecimalFormat = "N2";
                SecondNumber = 0.01;
                MathOperator = "×";
                CurrentState = 2;
                OnCalculate(result, current);
            }
        }

        public void OnComplete()
        {
            Model.Base.ControlHelper.OnPopNavigationAsync(_navigation);
        }

        private void LockNumberValue(string text)
        {
            double number;
            if (double.TryParse(text, out number))
            {
                if (CurrentState == 1)
                {
                    FirstNumber = number;
                }
                else
                {
                    SecondNumber = number;
                }

                CurrentEntry = string.Empty;
            }
        }
    }
}
