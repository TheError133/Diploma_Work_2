namespace Diploma_Work_2
{
    partial class AddPathForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.TextLength = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ComboClasses = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TextQuality = new System.Windows.Forms.TextBox();
            this.AddPathButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Расстояние между вершинами";
            // 
            // TextLength
            // 
            this.TextLength.Location = new System.Drawing.Point(182, 6);
            this.TextLength.Name = "TextLength";
            this.TextLength.Size = new System.Drawing.Size(158, 20);
            this.TextLength.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(113, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Класс пути";
            // 
            // ComboClasses
            // 
            this.ComboClasses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboClasses.FormattingEnabled = true;
            this.ComboClasses.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.ComboClasses.Location = new System.Drawing.Point(182, 36);
            this.ComboClasses.Name = "ComboClasses";
            this.ComboClasses.Size = new System.Drawing.Size(62, 21);
            this.ComboClasses.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Качество(0 - 1)";
            // 
            // TextQuality
            // 
            this.TextQuality.Location = new System.Drawing.Point(182, 66);
            this.TextQuality.Name = "TextQuality";
            this.TextQuality.Size = new System.Drawing.Size(107, 20);
            this.TextQuality.TabIndex = 3;
            // 
            // AddPathButton
            // 
            this.AddPathButton.Location = new System.Drawing.Point(12, 102);
            this.AddPathButton.Name = "AddPathButton";
            this.AddPathButton.Size = new System.Drawing.Size(125, 23);
            this.AddPathButton.TabIndex = 4;
            this.AddPathButton.Text = "Добавить";
            this.AddPathButton.UseVisualStyleBackColor = true;
            this.AddPathButton.Click += new System.EventHandler(this.AddPathButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(215, 102);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(125, 23);
            this.CancelButton.TabIndex = 5;
            this.CancelButton.Text = "Отмена";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // AddPathForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 138);
            this.ControlBox = false;
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.AddPathButton);
            this.Controls.Add(this.TextQuality);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ComboClasses);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TextLength);
            this.Controls.Add(this.label1);
            this.Name = "AddPathForm";
            this.Text = "Добавление пути между вершинами";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextLength;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ComboClasses;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TextQuality;
        private System.Windows.Forms.Button AddPathButton;
        private System.Windows.Forms.Button CancelButton;
    }
}