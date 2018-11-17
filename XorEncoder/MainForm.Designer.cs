﻿namespace XorEncoder
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxFileAddress = new System.Windows.Forms.TextBox();
            this.buttonOverview = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.textBoxKey = new System.Windows.Forms.TextBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxFileAddress
            // 
            this.textBoxFileAddress.Location = new System.Drawing.Point(13, 14);
            this.textBoxFileAddress.Name = "textBoxFileAddress";
            this.textBoxFileAddress.Size = new System.Drawing.Size(315, 21);
            this.textBoxFileAddress.TabIndex = 0;
            // 
            // buttonOverview
            // 
            this.buttonOverview.Location = new System.Drawing.Point(334, 13);
            this.buttonOverview.Name = "buttonOverview";
            this.buttonOverview.Size = new System.Drawing.Size(75, 23);
            this.buttonOverview.TabIndex = 1;
            this.buttonOverview.Text = "Обзор";
            this.buttonOverview.UseVisualStyleBackColor = true;
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(334, 51);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 2;
            this.buttonStop.Text = "Отмена";
            this.buttonStop.UseVisualStyleBackColor = true;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(13, 94);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(396, 23);
            this.progressBar.TabIndex = 3;
            // 
            // textBoxKey
            // 
            this.textBoxKey.Location = new System.Drawing.Point(13, 52);
            this.textBoxKey.Name = "textBoxKey";
            this.textBoxKey.Size = new System.Drawing.Size(234, 21);
            this.textBoxKey.TabIndex = 4;
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(253, 51);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 5;
            this.buttonStart.Text = "Старт";
            this.buttonStart.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 134);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.textBoxKey);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonOverview);
            this.Controls.Add(this.textBoxFileAddress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "XOR Encoder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxFileAddress;
        private System.Windows.Forms.Button buttonOverview;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TextBox textBoxKey;
        private System.Windows.Forms.Button buttonStart;
    }
}
