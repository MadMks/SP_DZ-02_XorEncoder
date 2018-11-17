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
        /// <summary>
        /// Отмена действия.
        /// </summary>
        private bool isCancel = false;
        /// <summary>
        /// Действие отменяется.
        /// </summary>
        private bool isDecoding = false;

        public MainForm()
        {
            InitializeComponent();

            this.Load += MainForm_Load;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.buttonStop.Enabled = false;

            this.textBoxKey.KeyPress += TextBoxKey_KeyPress;
        }

        /// <summary>
        /// Обработчик нажатия на textBox ключа.
        /// Доступен ввод цифр и Backspace.
        /// </summary>
        private void TextBoxKey_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            Console.WriteLine(number);
            Console.WriteLine((int)number);
            if (!Char.IsDigit(number)
                && (int)number != 8)
            {
                e.Handled = true;
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (this.IsEncodingAvailable())
            {
                this.ToggleButtons(false);
                
                ThreadPool.QueueUserWorkItem(this.Encrypt);
            }
        }

        /// <summary>
        /// Доступно кодирование.
        /// </summary>
        /// <returns>true если кодирование файла доступно.</returns>
        private bool IsEncodingAvailable()
        {
            if (this.IsFileExists()
                && this.IsKeyEntered())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Ключ введен.
        /// </summary>
        /// <returns>true если ключ введен, и содержит минимум 6 символов.</returns>
        private bool IsKeyEntered()
        {
            if (this.textBoxKey.Text.Length > 5)
            {
                return true;
            }

            MessageBox.Show("Длина ключа должна быть минимум 6 символов",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Asterisk);

            return false;
        }

        /// <summary>
        /// Файл существует.
        /// </summary>
        /// <returns>true если файл существует.</returns>
        private bool IsFileExists()
        {
            if (File.Exists(this.textBoxFileAddress.Text))
            {
                return true;
            }

            MessageBox.Show("Файл не найден", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            return false;
        }

        /// <summary>
        /// Кодирование файла.
        /// </summary>
        private void Encrypt(object state)
        {
            string filePath = this.textBoxFileAddress.Text;
            FileStream fileStream = null;

            try
            {
                fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);

                this.isDecoding = false;
                fileStream.Position = 0;
                this.progressBar.Maximum = (int)fileStream.Length;
                this.progressBar.Value = 0;
                // Кол-во байт для блока.
                int nBytesInBlock = Convert.ToInt32(this.numericUpDownQuantityByte.Value);


                byte[] dataRead = new byte[nBytesInBlock];
                int key = Convert.ToInt32(this.textBoxKey.Text);
                byte[] dataResult = new byte[nBytesInBlock];
                long endPosition = fileStream.Length;   // конечная позиция записи.
                int nBytesRead = 0; // кол-во считанных байт.

                // Кодируем файл.
                while (fileStream.Position != endPosition)
                {
                    // Считываем блок.
                    nBytesRead = fileStream.Read(dataRead, 0, dataRead.Length);
                    // Кодируем блок.
                    for (int i = 0; i < nBytesRead; i++)
                    {
                        dataResult[i] = (byte)(dataRead[i] ^ key);

                        this.ChangeValueProgressbar();
                    }
                    // Записываем блок.
                    fileStream.Position -= nBytesRead;
                    fileStream.Write(dataResult, 0, nBytesRead);

                    // Отмена кодирования (декодирование).
                    if (this.isCancel)
                    {
                        endPosition = fileStream.Position;
                        fileStream.Position = 0;
                        this.isCancel = false;

                        this.isDecoding = true;
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


            this.ToggleButtons(true);
        }

        /// <summary>
        /// Изменение значение progressBar.
        /// </summary>
        private void ChangeValueProgressbar()
        {
            if (this.isDecoding)
            {
                this.progressBar.Value--;
            }
            else
            {
                this.progressBar.Value++;
            }
        }

        /// <summary>
        /// Переключение кнопок Старт и Отмена.
        /// </summary>
        /// <param name="isEnableStart">Включить кнопку Старт.</param>
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
            this.ToggleButtons(true);

            this.isCancel = true;
        }

        private void buttonOverview_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Выбор файла";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.textBoxFileAddress.Text = openFileDialog.FileName;
            }
        }
    }
}