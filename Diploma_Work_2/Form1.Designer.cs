﻿namespace Diploma_Work_2
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьИнформациюИзФайлаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.графToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отрисоватьЗановоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CheckChangesAllowed = new System.Windows.Forms.CheckBox();
            this.RadioPathInfo = new System.Windows.Forms.RadioButton();
            this.RadioDeletePath = new System.Windows.Forms.RadioButton();
            this.RadioAddPath = new System.Windows.Forms.RadioButton();
            this.RadioDeletePoint = new System.Windows.Forms.RadioButton();
            this.RadioAddPoint = new System.Windows.Forms.RadioButton();
            this.Graph = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Graph)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.графToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(671, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.загрузитьИнформациюИзФайлаToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // загрузитьИнформациюИзФайлаToolStripMenuItem
            // 
            this.загрузитьИнформациюИзФайлаToolStripMenuItem.Name = "загрузитьИнформациюИзФайлаToolStripMenuItem";
            this.загрузитьИнформациюИзФайлаToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.загрузитьИнформациюИзФайлаToolStripMenuItem.Text = "Загрузить информацию из файла";
            this.загрузитьИнформациюИзФайлаToolStripMenuItem.Click += new System.EventHandler(this.загрузитьИнформациюИзФайлаToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // графToolStripMenuItem
            // 
            this.графToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.отрисоватьЗановоToolStripMenuItem});
            this.графToolStripMenuItem.Name = "графToolStripMenuItem";
            this.графToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.графToolStripMenuItem.Text = "Граф";
            // 
            // отрисоватьЗановоToolStripMenuItem
            // 
            this.отрисоватьЗановоToolStripMenuItem.Name = "отрисоватьЗановоToolStripMenuItem";
            this.отрисоватьЗановоToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.отрисоватьЗановоToolStripMenuItem.Text = "Отрисовать заново";
            this.отрисоватьЗановоToolStripMenuItem.Click += new System.EventHandler(this.отрисоватьЗановоToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CheckChangesAllowed);
            this.groupBox1.Controls.Add(this.RadioPathInfo);
            this.groupBox1.Controls.Add(this.RadioDeletePath);
            this.groupBox1.Controls.Add(this.RadioAddPath);
            this.groupBox1.Controls.Add(this.RadioDeletePoint);
            this.groupBox1.Controls.Add(this.RadioAddPoint);
            this.groupBox1.Location = new System.Drawing.Point(437, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(222, 157);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Работа с графом";
            // 
            // CheckChangesAllowed
            // 
            this.CheckChangesAllowed.AutoSize = true;
            this.CheckChangesAllowed.Location = new System.Drawing.Point(5, 133);
            this.CheckChangesAllowed.Name = "CheckChangesAllowed";
            this.CheckChangesAllowed.Size = new System.Drawing.Size(138, 17);
            this.CheckChangesAllowed.TabIndex = 1;
            this.CheckChangesAllowed.Text = "Изменения включены";
            this.CheckChangesAllowed.UseVisualStyleBackColor = true;
            // 
            // RadioPathInfo
            // 
            this.RadioPathInfo.AutoSize = true;
            this.RadioPathInfo.Location = new System.Drawing.Point(6, 110);
            this.RadioPathInfo.Name = "RadioPathInfo";
            this.RadioPathInfo.Size = new System.Drawing.Size(125, 17);
            this.RadioPathInfo.TabIndex = 0;
            this.RadioPathInfo.Text = "Информация о пути";
            this.RadioPathInfo.UseVisualStyleBackColor = true;
            this.RadioPathInfo.CheckedChanged += new System.EventHandler(this.RadioDeletePath_CheckedChanged);
            // 
            // RadioDeletePath
            // 
            this.RadioDeletePath.AutoSize = true;
            this.RadioDeletePath.Location = new System.Drawing.Point(6, 87);
            this.RadioDeletePath.Name = "RadioDeletePath";
            this.RadioDeletePath.Size = new System.Drawing.Size(100, 17);
            this.RadioDeletePath.TabIndex = 0;
            this.RadioDeletePath.Text = "Удаление пути";
            this.RadioDeletePath.UseVisualStyleBackColor = true;
            this.RadioDeletePath.CheckedChanged += new System.EventHandler(this.RadioDeletePath_CheckedChanged);
            // 
            // RadioAddPath
            // 
            this.RadioAddPath.AutoSize = true;
            this.RadioAddPath.Location = new System.Drawing.Point(6, 64);
            this.RadioAddPath.Name = "RadioAddPath";
            this.RadioAddPath.Size = new System.Drawing.Size(113, 17);
            this.RadioAddPath.TabIndex = 0;
            this.RadioAddPath.Text = "Добавление пути";
            this.RadioAddPath.UseVisualStyleBackColor = true;
            this.RadioAddPath.CheckedChanged += new System.EventHandler(this.RadioAddPath_CheckedChanged);
            // 
            // RadioDeletePoint
            // 
            this.RadioDeletePoint.AutoSize = true;
            this.RadioDeletePoint.Location = new System.Drawing.Point(6, 42);
            this.RadioDeletePoint.Name = "RadioDeletePoint";
            this.RadioDeletePoint.Size = new System.Drawing.Size(124, 17);
            this.RadioDeletePoint.TabIndex = 0;
            this.RadioDeletePoint.Text = "Удаление вершины";
            this.RadioDeletePoint.UseVisualStyleBackColor = true;
            this.RadioDeletePoint.CheckedChanged += new System.EventHandler(this.RadioDeletePoint_CheckedChanged);
            // 
            // RadioAddPoint
            // 
            this.RadioAddPoint.AutoSize = true;
            this.RadioAddPoint.Checked = true;
            this.RadioAddPoint.Location = new System.Drawing.Point(6, 19);
            this.RadioAddPoint.Name = "RadioAddPoint";
            this.RadioAddPoint.Size = new System.Drawing.Size(137, 17);
            this.RadioAddPoint.TabIndex = 0;
            this.RadioAddPoint.TabStop = true;
            this.RadioAddPoint.Text = "Добавление вершины";
            this.RadioAddPoint.UseVisualStyleBackColor = true;
            // 
            // Graph
            // 
            this.Graph.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Graph.Location = new System.Drawing.Point(15, 30);
            this.Graph.Name = "Graph";
            this.Graph.Size = new System.Drawing.Size(420, 330);
            this.Graph.TabIndex = 3;
            this.Graph.TabStop = false;
            this.Graph.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Graph_MouseClick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 365);
            this.ControlBox = false;
            this.Controls.Add(this.Graph);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Главное окно";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Graph)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox CheckChangesAllowed;
        private System.Windows.Forms.RadioButton RadioDeletePath;
        private System.Windows.Forms.RadioButton RadioAddPath;
        private System.Windows.Forms.RadioButton RadioDeletePoint;
        private System.Windows.Forms.RadioButton RadioAddPoint;
        private System.Windows.Forms.PictureBox Graph;
        private System.Windows.Forms.ToolStripMenuItem графToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отрисоватьЗановоToolStripMenuItem;
        private System.Windows.Forms.RadioButton RadioPathInfo;
        private System.Windows.Forms.ToolStripMenuItem загрузитьИнформациюИзФайлаToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

