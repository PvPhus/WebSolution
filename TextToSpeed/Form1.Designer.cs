namespace TextToSpeed
{
    partial class Form1
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
            this.btnTextToSpeed = new System.Windows.Forms.Button();
            this.rtxtText = new System.Windows.Forms.RichTextBox();
            this.txtSpeed = new System.Windows.Forms.TextBox();
            this.txtLangCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnTextToSpeed
            // 
            this.btnTextToSpeed.Location = new System.Drawing.Point(524, 340);
            this.btnTextToSpeed.Name = "btnTextToSpeed";
            this.btnTextToSpeed.Size = new System.Drawing.Size(145, 48);
            this.btnTextToSpeed.TabIndex = 0;
            this.btnTextToSpeed.Text = "Text To Speed";
            this.btnTextToSpeed.UseVisualStyleBackColor = true;
            this.btnTextToSpeed.Click += new System.EventHandler(this.btnTextToSpeed_Click);
            // 
            // rtxtText
            // 
            this.rtxtText.Location = new System.Drawing.Point(50, 114);
            this.rtxtText.Name = "rtxtText";
            this.rtxtText.Size = new System.Drawing.Size(468, 274);
            this.rtxtText.TabIndex = 1;
            this.rtxtText.Text = "";
            // 
            // txtSpeed
            // 
            this.txtSpeed.Location = new System.Drawing.Point(95, 56);
            this.txtSpeed.Name = "txtSpeed";
            this.txtSpeed.Size = new System.Drawing.Size(100, 20);
            this.txtSpeed.TabIndex = 2;
            this.txtSpeed.Text = "-2";
            // 
            // txtLangCode
            // 
            this.txtLangCode.Location = new System.Drawing.Point(314, 56);
            this.txtLangCode.Name = "txtLangCode";
            this.txtLangCode.Size = new System.Drawing.Size(100, 20);
            this.txtLangCode.TabIndex = 2;
            this.txtLangCode.Text = "de-DE";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Tốc độ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(238, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Mã ngôn ngữ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtLangCode);
            this.Controls.Add(this.txtSpeed);
            this.Controls.Add(this.rtxtText);
            this.Controls.Add(this.btnTextToSpeed);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTextToSpeed;
        private System.Windows.Forms.RichTextBox rtxtText;
        private System.Windows.Forms.TextBox txtSpeed;
        private System.Windows.Forms.TextBox txtLangCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

