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
            this.projectInfoComponent1 = new ComponentsLibrary.ProjectInfoComponent.ProjectInfoComponent();
            this.SuspendLayout();
            // 
            // projectInfoComponent1
            // 
            this.projectInfoComponent1.Location = new System.Drawing.Point(12, 12);
            this.projectInfoComponent1.Name = "projectInfoComponent1";
            this.projectInfoComponent1.Size = new System.Drawing.Size(407, 79);
            this.projectInfoComponent1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 98);
            this.Controls.Add(this.projectInfoComponent1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion
        private ComponentsLibrary.ProjectInfoComponent.ProjectInfoComponent projectInfoComponent1;
    }
}

