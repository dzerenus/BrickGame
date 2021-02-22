
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
            this.PBNextFigure = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lPoints = new System.Windows.Forms.Label();
            this.lLines = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PBNextFigure)).BeginInit();
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
            this.btnStart.Location = new System.Drawing.Point(314, 512);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(126, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Старт";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnPause
            // 
            this.btnPause.Enabled = false;
            this.btnPause.Location = new System.Drawing.Point(314, 541);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(126, 23);
            this.btnPause.TabIndex = 2;
            this.btnPause.Text = "Пауза";
            this.btnPause.UseVisualStyleBackColor = true;
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(314, 571);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(126, 23);
            this.btnSettings.TabIndex = 3;
            this.btnSettings.Text = "Настройки";
            this.btnSettings.UseVisualStyleBackColor = true;
            // 
            // PBNextFigure
            // 
            this.PBNextFigure.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.PBNextFigure.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PBNextFigure.Location = new System.Drawing.Point(315, 13);
            this.PBNextFigure.Name = "PBNextFigure";
            this.PBNextFigure.Size = new System.Drawing.Size(126, 126);
            this.PBNextFigure.TabIndex = 4;
            this.PBNextFigure.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.label1.Location = new System.Drawing.Point(365, 597);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "by ShpriZZ, 2021";
            // 
            // lPoints
            // 
            this.lPoints.AutoSize = true;
            this.lPoints.Location = new System.Drawing.Point(315, 146);
            this.lPoints.Name = "lPoints";
            this.lPoints.Size = new System.Drawing.Size(44, 13);
            this.lPoints.TabIndex = 6;
            this.lPoints.Text = "Очки: 0";
            // 
            // lLines
            // 
            this.lLines.AutoSize = true;
            this.lLines.Location = new System.Drawing.Point(315, 162);
            this.lLines.Name = "lLines";
            this.lLines.Size = new System.Drawing.Size(51, 13);
            this.lLines.TabIndex = 7;
            this.lLines.Text = "Линии: 0";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 618);
            this.Controls.Add(this.lLines);
            this.Controls.Add(this.lPoints);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PBNextFigure);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.pbField);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(468, 653);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(468, 653);
            this.Name = "MainForm";
            this.Text = "BrickGame by ShpriZZ";
            ((System.ComponentModel.ISupportInitialize)(this.pbField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PBNextFigure)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbField;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.PictureBox PBNextFigure;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lPoints;
        private System.Windows.Forms.Label lLines;
    }
}

