namespace StringMethods
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
            this.analyze_button = new System.Windows.Forms.Button();
            this.text_label = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // analyze_button
            // 
            this.analyze_button.Location = new System.Drawing.Point(131, 112);
            this.analyze_button.Name = "analyze_button";
            this.analyze_button.Size = new System.Drawing.Size(75, 23);
            this.analyze_button.TabIndex = 0;
            this.analyze_button.Text = "Проверить на палиндром";
            this.analyze_button.UseVisualStyleBackColor = true;
            this.analyze_button.Click += new System.EventHandler(this.AnalyzeButtonClick);
            // 
            // text_label
            // 
            this.text_label.AutoSize = true;
            this.text_label.Location = new System.Drawing.Point(89, 30);
            this.text_label.Name = "text_label";
            this.text_label.Size = new System.Drawing.Size(146, 13);
            this.text_label.TabIndex = 1;
            this.text_label.Text = "Введите текст для анализа";
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(117, 63);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(100, 20);
            this.textBox.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 191);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.text_label);
            this.Controls.Add(this.analyze_button);
            this.Name = "MainForm";
            this.Text = "Анализатор палиндромов";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button analyze_button;
        private System.Windows.Forms.Label text_label;
        private System.Windows.Forms.TextBox textBox;
    }
}

