
namespace BrickGame
{
    partial class MainForm
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
            this.pbField = new System.Windows.Forms.PictureBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbField)).BeginInit();
            this.SuspendLayout();
            // 
            // pbField
            // 
            this.pbField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.pbField.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbField.Location = new System.Drawing.Point(13, 13);
            this.pbField.Name = "pbField";
            this.pbField.Size = new System.Drawing.Size(295, 590);
            this.pbField.TabIndex = 0;
            this.pbField.TabStop = false;
            this.pbField.WaitOnLoad = true;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(25, 608);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(85, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Старт";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnPause
            // 
            this.btnPause.Enabled = false;
            this.btnPause.Location = new System.Drawing.Point(116, 608);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(85, 23);
            this.btnPause.TabIndex = 2;
            this.btnPause.Text = "Пауза";
            this.btnPause.UseVisualStyleBackColor = true;
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(207, 608);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(85, 23);
            this.btnSettings.TabIndex = 3;
            this.btnSettings.Text = "Настройки";
            this.btnSettings.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 638);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.pbField);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(338, 673);
            this.MinimumSize = new System.Drawing.Size(338, 673);
            this.Name = "MainForm";
            this.Text = "BrickGame by ShpriZZ";
            ((System.ComponentModel.ISupportInitialize)(this.pbField)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbField;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnSettings;
    }
}

