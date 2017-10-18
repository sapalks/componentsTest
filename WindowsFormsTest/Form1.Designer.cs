using ComponentsLibrary;
using ComponentsLibrary.CustomText;
using ComponentsLibrary.TestControl;
using System;

namespace WindowsFormsTest {
    partial class Form1 {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            this.logComponent1 = new ComponentsLibrary.LogComponent.LogComponent();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // logComponent1
            // 
            this.logComponent1.Location = new System.Drawing.Point(432, 13);
            this.logComponent1.Name = "logComponent1";
            this.logComponent1.Size = new System.Drawing.Size(426, 492);
            this.logComponent1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.MouseCaptureChanged += new System.EventHandler(this.button1_MouseCaptureChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 517);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.logComponent1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentsLibrary.LogComponent.LogComponent logComponent1;
        private System.Windows.Forms.Button button1;
    }
}

