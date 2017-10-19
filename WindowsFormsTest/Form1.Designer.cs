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
            this.button1 = new System.Windows.Forms.Button();
            this.customText1 = new ComponentsLibrary.CustomText.CustomText();
            this.logComponent1 = new ComponentsLibrary.LogComponent.LogComponent();
            this.SuspendLayout();
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
            // customText1
            // 
            this.customText1.Location = new System.Drawing.Point(34, 81);
            this.customText1.Name = "customText1";
            this.customText1.Size = new System.Drawing.Size(75, 23);
            this.customText1.TabIndex = 2;
            this.customText1.Text = "customText1";
            // 
            // logComponent1
            // 
            this.logComponent1.ContainerControl = this;
            this.logComponent1.Location = new System.Drawing.Point(434, 153);
            this.logComponent1.Name = "logComponent1";
            this.logComponent1.Size = new System.Drawing.Size(200, 352);
            this.logComponent1.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 517);
            this.Controls.Add(this.logComponent1);
            this.Controls.Add(this.customText1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private CustomText customText1;
        private ComponentsLibrary.LogComponent.LogComponent logComponent1;
    }
}

