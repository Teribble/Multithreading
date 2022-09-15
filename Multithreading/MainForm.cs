using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Multithreading
{
    public partial class MainForm : Form
    {
        private List<Thread> Threads;

        public MainForm()
        {
            InitializeComponent();

            Threads = new List<Thread>();
        }

        private void OnButton1Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(Task1);

            thread.Name = nameof(Task1);

            Threads.Add(thread);

            thread.Start(label2);
        }

        public void Task1(object obj)
        {
            int min = 0, max = 0;

            Examination(textBox1.Text, textBox2.Text, out min, out max);

            var label = (Label)obj;

            for (int i = min; i <= max; i++)
            {
                label.Invoke(new Action(() => label.Text = i.ToString()));

                Thread.Sleep(200);
            }
        }

        private void OnClosedForm(object sender, FormClosedEventArgs e)
        {
            foreach (var thread in Threads)
            {
                thread.Abort();
            }
        }

        private void OnTextChangedTextBox(object sender, EventArgs e)
        {
            var bufferLubel = (TextBox)sender;

            if (Int32.TryParse(bufferLubel.Text, out int a) || bufferLubel.Text == string.Empty)
                textBox1.Invoke(new Action(() => errorLabel.Visible = false));
            else
                textBox1.Invoke(new Action(() => errorLabel.Visible = true));
        }

        private void OnVisibleChange(object sender, EventArgs e)
        {
            if (errorLabel.Visible)
                button1.Enabled = false;
            else
                button1.Enabled = true;
        }

        public void Task2(object obj)
        {
            if(Int32.TryParse(textBox2.Text, out int max))
            {
                max = Int32.Parse(textBox2.Text);
            }
            else
            {
                max = int.MaxValue;
            }

            Label label = (Label)obj;

            int j = 1;

            for (int i = 1; i <= max; i += j)
            {
                label.Invoke(new Action(() => label.Text = i.ToString()));

                j = i - j;

                Thread.Sleep(200);
            }
        }

        private void OnClickButton2(object sender, EventArgs e)
        {
            Thread thread = new Thread(Task2);

            thread.Name = nameof(Task2);

            Threads.Add(thread);

            thread.Start(label8);
        }

        private void Examination(string str1, string str2, out int a, out int b)
        {
            if (str1 == "2")
                a = 2;
            else
                Int32.TryParse(textBox1.Text, out a);

            if (str2 == "Max")
                b = int.MaxValue;
            else
                Int32.TryParse(textBox2.Text, out b);

            if (a > b)
                (a, b) = (b, a);
        }

        private void OnButton3Click(object sender, EventArgs e)
        {
            Threads.Where(n => n.Name == nameof(Task1)).ToList().ForEach(n=>n.Abort());
        }

        private void OnButton4Click(object sender, EventArgs e)
        {
            Threads.Where(n => n.Name == nameof(Task2)).ToList().ForEach(n => n.Abort());
        }

        private void OnButton6Click(object sender, EventArgs e)
        {
            Threads.Where(n => n.Name == nameof(Task2)).ToList().ForEach(n => n.Join());

            Threads.Where(n => n.Name == nameof(Task2)).ToList().ForEach(n => n.Interrupt());
        }
    }

}
