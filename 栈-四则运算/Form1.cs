using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 栈_四则运算
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Stack<string> stack_operator = new Stack<string>();//运算符栈
        Stack<string> stack_number = new Stack<string>();//数字栈
        string str = "";
        string substr = "";


        private bool judgenumber(string text)//判断是否为数字
        {
            try
            {
                int var1 = Convert.ToInt32(text);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool judgeoperator(string text)//判断是否为运算符
        {
            if (text == "(" || text == ")" || text == "+" || text == "-" || text == "*" || text == "/")
            {
                return true;
            }
            else
                return false;
        }
        public object addition(object a, object b)//加法
        {
            Decimal d1 = Decimal.Parse(a.ToString());
            Decimal d2 = Decimal.Parse(b.ToString());
            return d2 + d1;
        }
        public object subduction(object a, object b)//减法
        {
            Decimal d1 = Decimal.Parse(a.ToString());
            Decimal d2 = Decimal.Parse(b.ToString());
            return d2 - d1;
        }
        public object multiplication(object a, object b)//乘法
        {
            Decimal d1 = Decimal.Parse(a.ToString());
            Decimal d2 = Decimal.Parse(b.ToString());
            return d2 * d1;
        }
        public object division(object a, object b)//除法
        {
            Decimal d1 = Decimal.Parse(a.ToString());
            Decimal d2 = Decimal.Parse(b.ToString());
            return d2 / d1;
        }
        int judgelevel(string text)//判断优先级
        {
            if (text.Equals("("))
            {
                return 1;
            }
            else if (text.Equals(")") || text.Equals("（") || text.Equals("）"))
            {
                return 1;
            }
            else if (text.Equals("+") || text.Equals("-"))
            {
                return 2;
            }
            else if (text.Equals("*") || text.Equals("/"))
            {
                return 3;
            }
            else
                return 10;


        }
        int operator_dected(string types, string a, string b)//根据运算符的类型返回对应的值
        {
            if (types == "+")
            {
                return Convert.ToInt32(addition(a, b));
            }
            else if (types == "-")
            {
                return Convert.ToInt32(subduction(a, b));
            }
            else if (types == "*")
            {
                return Convert.ToInt32(multiplication(a, b));
            }
            else if (types == "/")
            {
                return Convert.ToInt32(division(a, b));
            }
            else
                return 999;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            stack_number.Clear();//清空栈
            stack_operator.Clear();
            str = textBox1.Text + "!";//！为结束运算符
            int temp_count = 0;
            try
            {
                for (int i = 0; i < str.Length; i++)
                {
                    substr = str.Substring(i, 1);
                    if (judgenumber(substr))//如果是数字
                    {
                        if (temp_count == 0)
                        {
                            stack_number.Push(substr);
                        }
                        else
                            temp_count--;
                        if (judgenumber(str.Substring(i + 1, 1)))
                        {
                            string link1 = stack_number.Pop();
                            link1 += str.Substring(i + 1, 1);
                            stack_number.Push(link1);
                            temp_count++;
                        }
                    }
                    else if (judgeoperator(substr))
                    {
                        if (stack_operator.Count >= 1)
                        {
                            int new1 = judgelevel(substr);
                            int old1 = judgelevel(stack_operator.Peek());
                            if (old1 < new1 || substr == "(")//判断优先级
                            {
                                stack_operator.Push(substr); //将运算符插入栈中
                            }
                            else
                            {
                                if (substr == ")")
                                {
                                    for (; stack_operator.Count > 0; stack_operator.Pop())
                                    {
                                        if (stack_operator.Contains("(") && stack_operator.Peek() == "(")
                                        {
                                            stack_operator.Pop();
                                            break;
                                        }
                                        else
                                        {
                                            int temp1 = Convert.ToInt32(stack_number.Peek()); stack_number.Pop();
                                            int temp2 = Convert.ToInt32(stack_number.Peek()); stack_number.Pop();
                                            stack_number.Push(operator_dected(stack_operator.Peek(), temp1.ToString(), temp2.ToString()).ToString());
                                        }
                                    }
                                }
                                else
                                {
                                    for (; stack_operator.Count > 0 && stack_number.Count >= 2 && stack_operator.Peek() != "("; stack_operator.Pop())
                                    {
                                        string temp_a = substr;
                                        int new2 = judgelevel(temp_a);
                                        int old2 = judgelevel(stack_operator.Peek());
                                        if (old2 < new2 || substr == "(")//判断优先级
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            int temp3 = Convert.ToInt32(stack_number.Peek()); stack_number.Pop();
                                            int temp4 = Convert.ToInt32(stack_number.Peek()); stack_number.Pop();
                                            stack_number.Push(operator_dected(stack_operator.Peek(), temp3.ToString(), temp4.ToString()).ToString());
                                        }
                                    }
                                    stack_operator.Push(substr);
                                }
                            }
                        }
                        else
                        {
                            stack_operator.Push(substr);


                        }
                    }
                    else if (substr == "!")
                    {
                        for (; stack_operator.Count > 0 && stack_number.Count >= 2 && stack_operator.Peek() != "("; stack_operator.Pop())
                        {
                            int temp3 = Convert.ToInt32(stack_number.Peek()); stack_number.Pop();
                            int temp4 = Convert.ToInt32(stack_number.Peek()); stack_number.Pop();
                            stack_number.Push(operator_dected(stack_operator.Peek(), temp3.ToString(), temp4.ToString()).ToString());
                        }
                        label1.Text = "结果：" + stack_number.Peek();
                    }
                    else
                    {
                        MessageBox.Show("输入有非法内容,注意不能有中文括号！");
                        break;
                    }
                }
            }
            catch
            {


            }
        }
    }
}
