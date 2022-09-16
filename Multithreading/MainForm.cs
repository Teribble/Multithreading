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
        /// <summary>
        /// Список потоков
        /// </summary>
        private List<Thread> Threads;

        public MainForm()
        {
            InitializeComponent();

            Threads = new List<Thread>();
        }
        /// <summary>
        /// Запуск первого задания
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButton1Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(Task1);

            thread.Name = nameof(Task1);

            Threads.Add(thread);

            thread.Start(label2);
        }
        /// <summary>
        /// Задание первое
        /// </summary>
        /// <param name="obj"></param>
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
        /// <summary>
        /// Возникает при закрытии формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClosedForm(object sender, FormClosedEventArgs e)
        {
            foreach (var thread in Threads)
            {
                thread.Abort();
            }
        }
        /// <summary>
        /// Возникает при изменении текста в текст боксе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTextChangedTextBox(object sender, EventArgs e)
        {
            var bufferLubel = (TextBox)sender;

            if (Int32.TryParse(bufferLubel.Text, out int a) || bufferLubel.Text == string.Empty)
                textBox1.Invoke(new Action(() => errorLabel.Visible = false));
            else
                textBox1.Invoke(new Action(() => errorLabel.Visible = true));
        }
        /// <summary>
        /// Возникает при изменении параметра видимости
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnVisibleChange(object sender, EventArgs e)
        {
            if (errorLabel.Visible)
                button1.Enabled = false;
            else
                button1.Enabled = true;
        }
        /// <summary>
        /// Второе задание
        /// </summary>
        /// <param name="obj"></param>
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
        /// <summary>
        /// Запуск второго задания
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClickButton2(object sender, EventArgs e)
        {
            Thread thread = new Thread(Task2);

            thread.Name = nameof(Task2);

            Threads.Add(thread);

            thread.Start(label8);
        }
        /// <summary>
        /// Проверка записан ли числа
        /// </summary>
        /// <param name="str1">минимальное число</param>
        /// <param name="str2">максимальное число</param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        private void Examination(string str1, string str2, out int a, out int b)
        {
            if (str1 == "2")
                a = 2;
            else if (str1 == String.Empty)
                a = 0;
            else
                Int32.TryParse(str1, out a);

            if (str2 == "Max")
                b = int.MaxValue;
            else
                Int32.TryParse(str2, out b);

            if (a > b)
                (a, b) = (b, a);
        }
        /// <summary>
        /// Завершить первое задание
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButton3Click(object sender, EventArgs e)
        {
            Threads.Where(n => n.Name == nameof(Task1) || n.Name == nameof(ContinueTask1)).ToList().ForEach(n=>n.Abort());
        }
        /// <summary>
        /// Завершить второе задание
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButton4Click(object sender, EventArgs e)
        {
            Threads.Where(n => n.Name == nameof(Task2) || n.Name == nameof(ContinueTask2)).ToList().ForEach(n => n.Abort());
        }
        /// <summary>
        /// Остановить первое задание
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButton6Click(object sender, EventArgs e)
        {
            OnButton3Click(sender, e);
        }
        /// <summary>
        /// Возобновить работу первого задания
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButton8Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(ContinueTask1);

            thread.Name = nameof(ContinueTask1);

            Threads.Add(thread);

            thread.Start(label2);
        }
        /// <summary>
        /// Продолжить выполенение первого задания
        /// </summary>
        /// <param name="obj"></param>
        private void ContinueTask1(object obj)
        {
            int min = Int32.Parse(label2.Text);

            if (Int32.TryParse(textBox2.Text, out int max))
            {

            }
            else
            {
                max = int.MaxValue;
            }

            Label label = (Label)obj;

            for (int i = min; i < max; i++)
            {
                label.Invoke(new Action(() => label.Text = i.ToString()));

                Thread.Sleep(200);
            }
        }
        /// <summary>
        /// Продолжить выполнение второго задания
        /// </summary>
        /// <param name="obj"></param>
        private void ContinueTask2(object obj)
        {
            int min = Int32.Parse(label8.Text);

            if (Int32.TryParse(textBox2.Text, out int max)) { }
            else
            {
                max = int.MaxValue;
            }

            Label label = (Label)obj;

            int j = min;

            for (int i = min; i <= max; i += j)
            {
                label.Invoke(new Action(() => label.Text = i.ToString()));

                j = i - j;

                Thread.Sleep(200);
            }
        }
        /// <summary>
        /// Остановить второе задание
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButton5Click(object sender, EventArgs e)
        {
            OnButton4Click(sender, e);
        }
        /// <summary>
        /// Возобновить работу второго задания
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButton7Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(ContinueTask2);

            thread.Name = nameof(ContinueTask2);

            Threads.Add(thread);

            thread.Start(label8);
        }
        /// <summary>
        /// Возникает при изменении параметра видимости
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnVisibleChange2(object sender, EventArgs e)
        {
            if (label11.Visible)
                button9.Enabled = false;
            else
                button9.Enabled = true;
        }
        /// <summary>
        /// Возникает при изменении текста
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTextChangedTextBox2(object sender, EventArgs e)
        {
            var bufferLubel = (TextBox)sender;

            if (Int32.TryParse(bufferLubel.Text, out int a) || bufferLubel.Text == string.Empty)
                textBox4.Invoke(new Action(() => label11.Visible = false));
            else
                textBox4.Invoke(new Action(() => label11.Visible = true));
        }
        /// <summary>
        /// Рестарт первого и второго задания с новыми параметрами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButton9Click(object sender, EventArgs e)
        {
            OnButton3Click(sender, e);

            OnButton4Click(sender, e);

            Thread thread = new Thread(RestartTask1);

            thread.Name = nameof(Task1);

            Threads.Add(thread);

            thread.Start(label2);

            thread = new Thread(RestartTask2);

            thread.Name = nameof(Task2);

            Threads.Add(thread);

            thread.Start(label8);
        }
        /// <summary>
        /// Перезапускает первое задание с новыми параметрами
        /// </summary>
        /// <param name="obj"></param>
        private void RestartTask1(object obj)
        {
            int min = 0, max = 0;

            Examination(textBox4.Text, textBox3.Text, out min, out max);

            var label = (Label)obj;

            for (int i = min; i <= max; i++)
            {
                label.Invoke(new Action(() => label.Text = i.ToString()));

                Thread.Sleep(200);
            }
        }
        /// <summary>
        /// Перезапускает второе задание с новыми параметрами
        /// </summary>
        /// <param name="obj"></param>
        private void RestartTask2(object obj)
        {
            if (Int32.TryParse(textBox3.Text, out int max)) { }
            else
            {
                max = int.MaxValue;
            }

            Label label = (Label)obj;

            int j = Int32.Parse(textBox4.Text);

            for (int i = Int32.Parse(textBox4.Text); i <= max; i += j)
            {
                label.Invoke(new Action(() => label.Text = i.ToString()));

                j = i - j;

                Thread.Sleep(200);
            }
        }
    }

}
