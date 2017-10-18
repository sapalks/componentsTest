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
            this.customText1 = new ComponentsLibrary.CustomText.CustomText();
            this.SuspendLayout();
            // 
            // logComponent1
            // 
            this.logComponent1.Location = new System.Drawing.Point(264, 12);
            this.logComponent1.Name = "logComponent1";
            this.logComponent1.Size = new System.Drawing.Size(565, 405);
            this.logComponent1.TabIndex = 0;
            // 
            // customText1
            // 
            this.customText1.Location = new System.Drawing.Point(46, 70);
            this.customText1.Name = "customText1";
            this.customText1.Size = new System.Drawing.Size(75, 23);
            this.customText1.TabIndex = 1;
            this.customText1.Text = "customText1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 429);
            this.Controls.Add(this.customText1);
            this.Controls.Add(this.logComponent1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentsLibrary.LogComponent.LogComponent logComponent1;
        private CustomText customText1;
    }
}

