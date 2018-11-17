using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XorEncoder
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            this.Load += MainForm_Load;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.buttonStop.Enabled = false;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Запуск метода >>> buttonStart_Click");
            // Если проверки все успешны

            // TODO: проверить наличие файла
            // TODO: проверить наличие ключа
            // TODO: ключ мин 6 символов

            // то переключит кнопки
            // и запустить метод в пул потоков.

            this.ToggleButtons(false);

            // TODO: запустить процес шифрования -> пул потоков.
            ThreadPool.QueueUserWorkItem(this.Encrypt);

            Console.WriteLine("Завершение метода >>> buttonStart_Click");
        }

        private void Encrypt(object state)
        {
            Console.WriteLine("Запуск метода >>> Encrypt");

            // TODO: метод шифрования.

            string filePath = this.textBoxFileAddress.Text;
            FileStream fileStream = null;

            try
            {
                fileStream = new FileStream(filePath, FileMode.Open);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                fileStream?.Close();
            }

            //Thread.Sleep(5000);
            this.ToggleButtons(true);

            Console.WriteLine("Завершение метода >>> Encrypt");

        }

        private void ToggleButtons(bool isEnableStart)
        {
            if (isEnableStart)
            {
                this.buttonStart.Enabled = true;
                this.buttonStop.Enabled = false;
            }
            else
            {
                this.buttonStop.Enabled = true;
                this.buttonStart.Enabled = false;

                this.buttonStop.Focus();
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            // TODO: поменять кнопки на Старт
            this.ToggleButtons(true);
        }
    }
}
