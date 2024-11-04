namespace TgBuilder
{
    partial class Main
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ObfuscatorChk = new System.Windows.Forms.CheckBox();
            this.MeltFile_Chk = new System.Windows.Forms.CheckBox();
            this.BuildBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.ChatidBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TokenBox = new System.Windows.Forms.TextBox();
            this.AboutBtn = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TgBuilder.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(12, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(63, 59);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(81, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Telegram Grabber";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(81, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "Experimental project to demonstrate\r\nthe fact of theft of telegram sessions";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ObfuscatorChk);
            this.groupBox1.Controls.Add(this.MeltFile_Chk);
            this.groupBox1.Controls.Add(this.BuildBtn);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.ChatidBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.TokenBox);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 97);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(316, 243);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Manager";
            // 
            // ObfuscatorChk
            // 
            this.ObfuscatorChk.AutoSize = true;
            this.ObfuscatorChk.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ObfuscatorChk.Location = new System.Drawing.Point(11, 130);
            this.ObfuscatorChk.Name = "ObfuscatorChk";
            this.ObfuscatorChk.Size = new System.Drawing.Size(121, 17);
            this.ObfuscatorChk.TabIndex = 10;
            this.ObfuscatorChk.Text = "Simple Obfuscator";
            this.ObfuscatorChk.UseVisualStyleBackColor = true;
            // 
            // MeltFile_Chk
            // 
            this.MeltFile_Chk.AutoSize = true;
            this.MeltFile_Chk.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MeltFile_Chk.Location = new System.Drawing.Point(234, 130);
            this.MeltFile_Chk.Name = "MeltFile_Chk";
            this.MeltFile_Chk.Size = new System.Drawing.Size(70, 17);
            this.MeltFile_Chk.TabIndex = 9;
            this.MeltFile_Chk.Text = "Melt File";
            this.MeltFile_Chk.UseVisualStyleBackColor = true;
            // 
            // BuildBtn
            // 
            this.BuildBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.BuildBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BuildBtn.Font = new System.Drawing.Font("Segoe UI Black", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BuildBtn.Location = new System.Drawing.Point(9, 175);
            this.BuildBtn.Name = "BuildBtn";
            this.BuildBtn.Size = new System.Drawing.Size(295, 52);
            this.BuildBtn.TabIndex = 8;
            this.BuildBtn.Text = "BUILD";
            this.BuildBtn.UseVisualStyleBackColor = false;
            this.BuildBtn.Click += new System.EventHandler(this.BuildBtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.ForeColor = System.Drawing.Color.Gray;
            this.label5.Location = new System.Drawing.Point(8, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "ChatId ( Telegram )";
            // 
            // ChatidBox
            // 
            this.ChatidBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ChatidBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChatidBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ChatidBox.ForeColor = System.Drawing.Color.Turquoise;
            this.ChatidBox.Location = new System.Drawing.Point(9, 102);
            this.ChatidBox.Name = "ChatidBox";
            this.ChatidBox.Size = new System.Drawing.Size(295, 22);
            this.ChatidBox.TabIndex = 6;
            this.ChatidBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(11, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "TokenBot ( Telegram )";
            // 
            // TokenBox
            // 
            this.TokenBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TokenBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TokenBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TokenBox.ForeColor = System.Drawing.Color.Turquoise;
            this.TokenBox.Location = new System.Drawing.Point(9, 43);
            this.TokenBox.Name = "TokenBox";
            this.TokenBox.Size = new System.Drawing.Size(295, 22);
            this.TokenBox.TabIndex = 0;
            this.TokenBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AboutBtn
            // 
            this.AboutBtn.AutoSize = true;
            this.AboutBtn.BackColor = System.Drawing.Color.Transparent;
            this.AboutBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AboutBtn.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AboutBtn.ForeColor = System.Drawing.Color.Gray;
            this.AboutBtn.Location = new System.Drawing.Point(291, 10);
            this.AboutBtn.Name = "AboutBtn";
            this.AboutBtn.Size = new System.Drawing.Size(39, 13);
            this.AboutBtn.TabIndex = 4;
            this.AboutBtn.Text = "About";
            this.AboutBtn.Click += new System.EventHandler(this.AboutBtn_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(340, 348);
            this.Controls.Add(this.AboutBtn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Grabber Builder v.1.0";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label AboutBtn;
        private System.Windows.Forms.TextBox TokenBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ChatidBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox ObfuscatorChk;
        private System.Windows.Forms.CheckBox MeltFile_Chk;
        private System.Windows.Forms.Button BuildBtn;
    }
}

