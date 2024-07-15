using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Management;
using System.Windows.Forms;

namespace Calculator
{

    public partial class Calculator : Form
    {
        public Calculator()
        {
            InitializeComponent();
        }

        public enum enOperator
        {
            Sum = '+',
            Sub = '-',
            Mul = '×',
            Div = '÷'
        }
        public struct stOpeartion
        {
            public List<string> Numbers;
            public int Index;
            public bool IsOperator;
            public string OperationType;
            public string Result;
        }

        stOpeartion Operation;
        enOperator Operator;
        string TrigonometricType;

        private void btnClear_Click(object sender, EventArgs e)
        {
            lblText.Text = "0";
            lblPrinting.Text = "";
            TrigonometricType = "";

            Operation.Numbers = null;
            Operation.Index = 0;
            Operation.IsOperator = false;
            Operation.Result = "";
            Operation.OperationType = "";

        }
        private void btnPoint_Click(object sender, EventArgs e)
        {
            lblPrinting.Text += ".";
            lblText.Text += ".";
        }
        public double Calculate(double Num1, double Num2, enOperator Operator)
        {
            switch (Operator)
            {
                case enOperator.Sum:
                    return Num1 + Num2;
                case enOperator.Sub:
                    return Num1 - Num2;
                case enOperator.Mul:
                    return Num1 * Num2;
                case enOperator.Div:
                    return Num1 / Num2;
            }
            return 0;
        }

        private double CacluateTrigonometric(double Number, string Operation)
        {
            switch (Operation)
            {
                case "sin":
                    {
                        return Convert.ToDouble(Math.Sin(Number));
                    }

                case "cos":
                    {
                        return Convert.ToDouble(Math.Cos(Number));
                    }

                case "tan":
                    {
                        return Convert.ToDouble(Math.Tan(Number));
                    }
            }
            return 0;
        }


        private void OneOperation_Click(object sender, EventArgs e)
        {
            Operation.OperationType = "OneOperator";
            Guna.UI2.WinForms.Guna2Button btn = sender as Guna.UI2.WinForms.Guna2Button;

            double Num = Convert.ToDouble(lblText.Text);
            double FinalResutl = 0;

            string operation = btn.Tag.ToString();

            switch (operation)
            {
                case "sqrt":
                    lblPrinting.Text = "sqrt(" + Num + ")";
                    FinalResutl = Math.Sqrt(Num);
                    break;

                case "square":
                    lblPrinting.Text += "²";
                    FinalResutl = Num * Num;
                    break;

                case "cube":
                    lblPrinting.Text += "³";
                    FinalResutl = Num * Num * Num;
                    break;

                case "onedivno":
                    lblPrinting.Text = "1/" + Num;
                    FinalResutl = 1 / Num;
                    break;

            }

            Operation.Result = FinalResutl.ToString();
            lblText.Text = Operation.Result;
        }
        private double CalculateOneOperator(double Number, string Operation)
        {
            switch (Operation)
            {
                case "sqrt":
                    {
                        return Convert.ToDouble(Math.Sqrt(Number));
                    }
                case "square":
                    {
                        return Convert.ToDouble(Number * Number);
                    }
                case "cube":
                    {
                        return Convert.ToDouble(Number * Number * Number);
                    }
                case "onedivno":
                    {
                        return Convert.ToDouble(1 / Number);
                    }
            }
            return 0;
        }




        public void InitilazingNumbers()
        {
            if (Operation.Numbers == null)
            {
                Operation.Index = 0;
                Operation.Numbers = new List<string>();
                Operation.Numbers.Add("");
                Operation.Numbers.Add("");
            }
            Operation.Numbers[Operation.Index] = lblText.Text;

        }

        public void RemoveZero()
        {
            if (lblText.Text == "0")
                lblText.Text = "";
        }

        public void EditTheText(string Text)
        {
            RemoveZero();

            if (Operation.IsOperator == true)
            {
                lblText.Text = "";
                Operation.IsOperator = false;
                Operation.Index++;
               
            }

            lblText.Text += Text;
            lblPrinting.Text += Text;
        }
        private void btnNumber_Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button btn = sender as Guna.UI2.WinForms.Guna2Button;

            EditTheText(btn.Tag.ToString());

            InitilazingNumbers();

            
        }
        private void btnOperators_Click(object sender, EventArgs e)
        {
            Operation.OperationType = "NormalOperator";
            Operation.IsOperator = true;
            Guna.UI2.WinForms.Guna2Button btn = sender as Guna.UI2.WinForms.Guna2Button;

            lblPrinting.Text = lblText.Text + btn.Tag.ToString();
            Operation.Numbers[Operation.Index] = lblText.Text;

            Operator = (enOperator)Convert.ToChar(btn.Tag);
        }

        private void Trigonometric_Click(object sender, EventArgs e)
        {
            Operation.OperationType = "Trigonometric";
            Guna.UI2.WinForms.Guna2Button btn = sender as Guna.UI2.WinForms.Guna2Button;

            switch (btn.Tag.ToString())
            {
                case "sin":
                    {
                        TrigonometricType = "sin";
                        break;
                    }
                case "cos":
                    {
                        TrigonometricType = "cos";
                        break;
                    }
                case "tan":
                    {
                        TrigonometricType = "tan";
                        break;
                    }
            }
            lblPrinting.Text = TrigonometricType + "(";
           // lblText.Text = TrigonometricType + "(";
        }
        private void btnResult_Click(object sender, EventArgs e)
        {
            InitilazingNumbers();
            double Number1 = Convert.ToDouble(Operation.Numbers[0]);

            switch (Operation.OperationType)
            {
                case "Trigonometric":
                    {
                        lblPrinting.Text += ")";
                        Operation.Result = CacluateTrigonometric(Number1, TrigonometricType).ToString();
                        break;
                    }

                case "NormalOperator":
                    {
                        double Number2 = Convert.ToDouble(Operation.Numbers[1]);

                        double Result = Calculate(Number1, Number2, Operator);
                        Operation.Result = Result.ToString();
                        break;
                    }
            }

            lblPrinting.Text += "=";
            lblText.Text = Operation.Result;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int FirstLength = lblText.Text.Length;
            int SecondLength = lblPrinting.Text.Length;


            if (lblText.Text != "0")
            {
                lblText.Text = lblText.Text.Substring(0, FirstLength - 1);
                lblPrinting.Text = lblPrinting.Text.Substring(0, SecondLength - 1);
            }

           if (lblText.Text == "")
            {
                lblText.Text = "0";
            }

        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            RemoveZero();

            Operation.Numbers[Operation.Index] = (Convert.ToDouble(Operation.Numbers[Operation.Index]) * -1).ToString();

            lblText.Text = Operation.Numbers[Operation.Index];
        }
    }
}

