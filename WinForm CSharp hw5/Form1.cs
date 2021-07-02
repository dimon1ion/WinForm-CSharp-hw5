using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm_CSharp_hw5
{
    public partial class Form1 : Form
    {
        public delegate double Mathematic(double num1, double num2);
        Mathematic action;
        string box1 = String.Empty;
        bool write = true;
        bool clear = false;
        bool mathSymbol = false;
        string replace;
        bool defaultNull = true;
        bool clearTextbox1 = false;
        bool deleting = false;
        bool procentEnable = false;
        KeyPressEventArgs keyPress = new KeyPressEventArgs('.');
        KeyEventArgs key = new KeyEventArgs(new Keys());
        public Form1()
        {
            InitializeComponent();
        }

        private double Sum(double num1, double num2)
        {
            return num1;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (clear) { textBox1.Text = String.Empty; textBox2.Text = String.Empty; clear = false; }
            switch (e.KeyChar)
            {
                case '+':
                    Rename_TextBox2('+');
                    break;
                case '-':
                    Rename_TextBox2('-');
                    break;
                case '/':
                    Rename_TextBox2('/');
                    break;
                case '*':
                    Rename_TextBox2('*');
                    break;
                case '=':
                    procentEnable = false;
                    textBox2.Text += textBox1.Text + '=';
                    box1 = Calc().ToString();
                    write = true;
                    clear = true;
                    break;
                default:
                    if (char.IsDigit(e.KeyChar) || e.KeyChar == ',')
                    {
                        if (write)
                        {
                            textBox1.Text = String.Empty;
                            write = false;
                            mathSymbol = true;
                        }
                        if (!deleting)
                        {
                            mathSymbol = false;
                            defaultNull = false;
                        }
                    }
                    else
                    {
                        deleting = true;
                        mathSymbol = true;
                        clearTextbox1 = true;
                    }
                    break;
            }
        }

        private void Rename_TextBox2(char symbol)
        {
            if (mathSymbol == false)
            {
                procentEnable = true;
                if (textBox1.Text == "")
                {
                    textBox1.Text = "0";
                }
                textBox2.Text += textBox1.Text + symbol;
                box1 = Calc().ToString();
                write = true;
            }
            else
            {
                deleting = true;
                replace = String.Empty;
                for (int i = 0; i < textBox2.Text.Length - 1; i++)
                {
                    replace += textBox2.Text[i];
                }
                textBox2.Text = replace + symbol;
                clearTextbox1 = true;
            }
        }

        private double Calc()
        {
            double num1 = 0, num2 = 0;
            bool operat = false;
            bool dot = false;
            int dotnum1 = 1;
            int dotnum2 = 1;
            for (int i = 0; i < textBox2.TextLength; i++)
            {
                switch (textBox2.Text.ElementAt(i))
                {
                    case '+':
                        num1 /= dotnum1;
                        num2 /= dotnum2;
                        dotnum1 = 1;
                        dotnum2 = 1;
                        if (operat) { num1 = action(num1, num2); num2 = 0; }
                        action = (x, y) => (double)x + y;
                        dot = false;
                        operat = true;
                        break;
                    case '-':
                        num1 /= dotnum1;
                        num2 /= dotnum2;
                        dotnum1 = 1;
                        dotnum2 = 1;
                        if (operat) { num1 = action(num1, num2); num2 = 0; }
                        action = (x, y) => (double)x - y;
                        dot = false;
                        operat = true;
                        break;
                    case '*':
                        num1 /= dotnum1;
                        num2 /= dotnum2;
                        dotnum1 = 1;
                        dotnum2 = 1;
                        if (operat) { num1 = action(num1, num2); num2 = 0; }
                        action = (x, y) => (double)x * y;
                        dot = false;
                        operat = true;
                        break;
                    case '/':
                        num1 /= dotnum1;
                        num2 /= dotnum2;
                        dotnum1 = 1;
                        dotnum2 = 1;
                        if (operat) { num1 = action(num1, num2); num2 = 0; }
                        action = (x, y) => (double)x / y;
                        dot = false;
                        operat = true;
                        break;
                    case '=':
                        num1 /= dotnum1;
                        num2 /= dotnum2;
                        if (operat) { num1 = action(num1, num2); num2 = 0; }
                        return num1;
                    default:
                        if (!operat)
                        {
                            if (textBox2.Text.ElementAt(i) != ',')
                            {
                                num1 *= 10;
                                num1 += Double.Parse(textBox2.Text.ElementAt(i).ToString());
                                if (dot) { dotnum1 *= 10; }
                                break;
                            }
                            dot = true;
                            break;
                        }
                        if (textBox2.Text.ElementAt(i) != ',')
                        {
                            num2 *= 10;
                            num2 += Double.Parse(textBox2.Text.ElementAt(i).ToString());
                            if (dot) { dotnum2 *= 10; }
                            break;
                        }
                        dot = true;
                        break;
                }
            }
            return num1;
        }

        private void but1_Click(object sender, EventArgs e)
        {
            keyPress.KeyChar = (sender as Button).Text.ToCharArray()[0];
            textBox1_KeyPress(sender, keyPress);
            if (defaultNull)
            {
                textBox1.Text = (sender as Button).Text;
                defaultNull = false;
                return;
            }
            textBox1.Text += (sender as Button).Text;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
            textBox2.Text = String.Empty;
            clear = false;
            defaultNull = true;
            write = true;
        }


        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (write)
            {
                textBox1.Text = box1;
            }
            if (clearTextbox1)
            {
                textBox1.Text = String.Empty;
                clearTextbox1 = false;
                deleting = false;
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            replace = String.Empty;
            if (!write)
            {
                for (int i = 0; i < textBox1.Text.Length - 1; i++)
                {
                    replace += textBox1.Text[i];
                }
            }
            textBox1.Text = replace;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            keyPress.KeyChar = (sender as Button).Text.ToCharArray()[0];
            textBox1_KeyPress(sender, keyPress);
            textBox1_KeyUp(sender, key);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            replace = String.Empty;
            if (textBox1.Text[0] != '-')
            {
                replace += '-';
            }
            for (int i = 0; i < textBox1.Text.Length; i++)
            {
                if (char.IsDigit(textBox1.Text[i]))
                {
                    replace += textBox1.Text[i];
                }
            }
            textBox1.Text = replace;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
            clear = false;
            defaultNull = true;
            write = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (procentEnable)
            {
                double num;
                if (Double.TryParse(textBox1.Text, out num))
                {
                    num /= 100;
                    textBox1.Text = num.ToString();
                }
            }
            else
            {
                textBox1.Text = "0";
                write = true;
                defaultNull = true;
            }
        }
    }
}
