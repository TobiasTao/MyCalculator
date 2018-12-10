using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using dll;

namespace MyCalculator
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private string _str = "";              // a string to save data

        // 得到符号优先级
        private static int GetSignPriority(char sign)  
        {
            switch (sign)
            {
                case '(':
                    return 0;
                case '+':
                case '-':
                    return 1;
                case '*':
                case '/':
                    return 2;
                default:
                    return -1;
            }
        }
        
        /*从后缀表达式中得到 类型 */
        private static int GetObjectTypeFromPostfixExpression(string obj)
        {
            switch (obj)
            {
                // if obj is a sign
                case "+":
                    return '+';
                case "-":
                    return '-';
                case "*":
                    return '*';
                case "/":
                    return '/';
                default:
                    return 0;
            }
        }
        
        #region simple input Button
        private void Btn0_Click(object sender, RoutedEventArgs e)
        {
            _str += "0";
            InputTextBox.Text = _str;
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            _str += "1";
            InputTextBox.Text = _str;
        }

        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            _str += "2";
            InputTextBox.Text = _str;
        }

        private void Btn3_Click(object sender, RoutedEventArgs e)
        {
            _str += "3";
            InputTextBox.Text = _str;
        }

        private void Btn4_Click(object sender, RoutedEventArgs e)
        {
            _str += "4";
            InputTextBox.Text = _str;
        }

        private void Btn5_Click(object sender, RoutedEventArgs e)
        {
            _str += "5";
            InputTextBox.Text = _str;
        }

        private void Btn6_Click(object sender, RoutedEventArgs e)
        {
            _str += "6";
            InputTextBox.Text = _str;
        }

        private void Btn7_Click(object sender, RoutedEventArgs e)
        {
            _str += "7";
            InputTextBox.Text = _str;
        }

        private void Btn8_Click(object sender, RoutedEventArgs e)
        {
            _str += "8";
            InputTextBox.Text = _str;
        }

        private void Btn9_Click(object sender, RoutedEventArgs e)
        {
            _str += "9";
            InputTextBox.Text = _str;
        }

        private void BtnPlus_Click(object sender, RoutedEventArgs e)
        {
            InputTextBox.Text = " ";
            _str += "+";
            InputTextBox.Text = _str;
        }

        private void BtnMinus_Click(object sender, RoutedEventArgs e)
        {
            _str += "-";
            InputTextBox.Text = _str;
        }

        private void BtnMultiple_Click(object sender, RoutedEventArgs e)
        {
            _str += "*";
            InputTextBox.Text = _str;
        }

        private void BtnDivision_Click(object sender, RoutedEventArgs e)
        {
            _str += "/";
            InputTextBox.Text = _str;
        }

        private void BtnDot_Click(object sender, RoutedEventArgs e)
        {
            _str += ".";
            InputTextBox.Text = _str;
        }

        private void BtnLeft_Click(object sender, RoutedEventArgs e)
        {
            _str += "(";
            InputTextBox.Text = _str;
        }

        private void BtnRight_Click(object sender, RoutedEventArgs e)
        {
            _str += ")";
            InputTextBox.Text = _str;
        }

        private void BtnDEL_Click(object sender, RoutedEventArgs e)
        {
            if (_str.Length <= 0) return;
            _str = _str.Substring(0, _str.Length - 1);
            InputTextBox.Text = _str;
        }

        private void BtnAC_Click(object sender, RoutedEventArgs e)
        {
            _str = "";
            InputTextBox.Text = _str;
        }
#endregion


        private void BtnEqual_OnClick(object sender, RoutedEventArgs e)
        {
            var tempStack = new Stack<double>();                  // help to calculate postfix expression
            var postfixExpressionQueue = new Queue<string>();     // save postfix expression
            var signStack = new Stack<char>();                      // save signs
            var tempStr = "";                                            // save numbers

            #region translate to postfix expression
            // collect into postfixExpressionStack
            foreach (var t in _str)
            {
                if (t <= '9' && t >= '0' || t == '.')
                {
                    tempStr += t;
                }
                else
                {
                    if (tempStr.Length > 0)             // if there is a number before the sign, add to stack
                    {
                        postfixExpressionQueue.Enqueue(tempStr);
                        tempStr = "";
                    }
                    if (signStack.Count == 0)           // sign stack is empty
                    {
                        signStack.Push(t);
                    }
                    else                                // isn't empty
                    {
                        switch (t)
                        {
                            case '(':
                                signStack.Push('(');
                                break;
                            case ')':
                            {
                                // pop until ')'
                                while (true)
                                {
                                    var tempSign = signStack.Pop();
                                    if (tempSign != '(')
                                    {
                                        postfixExpressionQueue.Enqueue(Convert.ToString(tempSign));
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                break;
                            }
                            default:
                            {
                                if (GetSignPriority(t) > GetSignPriority(signStack.Peek()))
                                {
                                    signStack.Push(t);
                                }
                                else
                                {
                                    while (true)
                                    {
                                        postfixExpressionQueue.Enqueue(Convert.ToString(signStack.Pop()));
                                        if(signStack.Count == 0)
                                            break;
                                        if (GetSignPriority(t) > GetSignPriority(signStack.Peek()))
                                            break;
                                    }
                                    signStack.Push(t);
                                }

                                break;
                            }
                        }
                    }
                }
            }
            if (tempStr.Length > 0)
            {
                postfixExpressionQueue.Enqueue(tempStr);
                //tempStr = "";
            }
            while (signStack.Count > 0) 
            {
                postfixExpressionQueue.Enqueue(Convert.ToString(signStack.Pop()));
            }
            #endregion
            signStack.Clear();
            //tempStr = "";

            #region calculate the answer by pstfix expression
            while (postfixExpressionQueue.Count > 0)
            {
                var objType = GetObjectTypeFromPostfixExpression(postfixExpressionQueue.Peek());
                double tempDouble;
                switch (objType)
                {
                    case 0:                 // if is a number, save to tempStack
                        tempStack.Push(Convert.ToDouble(postfixExpressionQueue.Dequeue()));
                        break;
                    case '+':
                        postfixExpressionQueue.Dequeue();
//                        tempStack.Push(tempStack.Pop() + tempStack.Pop());
                        var result = Class1.Add(tempStack.Pop(), tempStack.Pop());
                        tempStack.Push(result);
                        break;
                    case '-':
                        postfixExpressionQueue.Dequeue();
                        tempDouble = tempStack.Pop();
                        //tempStack.Push(tempStack.Pop() - tempDouble);
                        result = Class1.Minus(tempStack.Pop(), tempDouble);
                        tempStack.Push(result);
                        break;
                    case '*':
                        postfixExpressionQueue.Dequeue();
                        result = Class1.Multiple(tempStack.Pop(), tempStack.Pop());
                        tempStack.Push(result);
                        break;
                    case '/':
                        postfixExpressionQueue.Dequeue();
                        tempDouble = tempStack.Pop();
                        if (Math.Abs(tempDouble) > 0.0)
                        {
                            result = Class1.Division(tempStack.Pop(), tempDouble);
                            tempStack.Push(result);
                        }
                        else
                        {
                            MessageBox.Show("Error: zero divisor.");
                        }
                        break;
                    default:
                        MessageBox.Show("Unknown Error.");
                        break;
                }
            }
            #endregion

            _str = Convert.ToString(tempStack.Pop(), CultureInfo.CurrentCulture);
            InputTextBox.Text = _str;
        }
    }
}
