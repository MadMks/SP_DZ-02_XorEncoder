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
        private bool isCancel = false;
        private bool isDecoding = false;

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
                fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
                
                fileStream.Position = 0;
                int quantityByte = Convert.ToInt32(this.numericUpDownQuantityByte.Value);
                
                byte[] dataRead = new byte[quantityByte];
                //byte[] dataKey = Encoding.Unicode.GetBytes(this.textBoxKey.Text);
                int key = Convert.ToInt32(this.textBoxKey.Text);
                byte[] dataResult = new byte[quantityByte];
                long endPosition = fileStream.Length;   // конечная позиция записи.
                int nBytes = 0;

                // Кодируем файл.
                while (fileStream.Position != /*fileStream.Length*/endPosition)
                {
                    // Считываем блок.
                    nBytes = fileStream.Read(dataRead, 0, dataRead.Length);
                    // Кодируем блок.
                    for (int i = 0; i < nBytes; i++)
                    {
                        Console.Write(dataRead[i] + " >>> ");
                        //dataResult[i] = (byte)(dataRead[i] ^ dataKey[i % dataKey.Length]);
                        dataResult[i] = (byte)(dataRead[i] ^ key);
                        Console.WriteLine(dataResult[i]);
                        
                    }
                    // Записываем блок.
                    fileStream.Position -= nBytes;
                    fileStream.Write(dataResult, 0, nBytes);

                    // Отмена кодирования (декодирование).
                    if (this.isCancel)
                    {
                        endPosition = fileStream.Position;
                        fileStream.Position = 0;
                        this.isCancel = false;
                    }
                }
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

        private void EncryptBackup(object state)
        {
            Console.WriteLine("Запуск метода >>> Encrypt");

            // TODO: метод шифрования.

            string filePath = this.textBoxFileAddress.Text;
            FileStream fileStream = null;

            try
            {
                fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);

                byte[] dataRead = new byte[(int)fileStream.Length];
                byte[] dataKey = Encoding.Unicode.GetBytes(this.textBoxKey.Text);
                byte[] dataResult = new byte[(int)fileStream.Length];

                fileStream.Read(dataRead, 0, dataRead.Length);
                this.progressBar.Maximum = dataRead.Length;

                for (int i = 0; i < dataRead.Length; i++)
                {
                    dataResult[i] = (byte)(dataRead[i] ^ dataKey[i % dataKey.Length]);
                    Console.WriteLine(dataResult[i]);

                    this.progressBar.Value = i + 1;
                    Thread.Sleep(2000);

                    if (this.isCancel)
                    {
                        Console.WriteLine(">>> Cancel!");
                    }
                }

                fileStream.Position = 0;
                fileStream.Write(dataResult, 0, dataResult.Length);
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

            this.isCancel = true;
            //Console.WriteLine(isCancel);
        }
    }
}
